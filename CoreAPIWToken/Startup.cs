using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreAPIWToken.Domain.Data;
using CoreAPIWToken.Domain.Repositories;
using CoreAPIWToken.Domain.Services;
using CoreAPIWToken.Domain.UnitOfWork;
using CoreAPIWToken.Security;
using CoreAPIWToken.Security.Token;
using CoreAPIWToken.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.OpenApi.Models;

namespace CoreAPIWToken
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<TokenApiDBContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"]);
            });

            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            //services.AddScoped<ProductRepository>();
            //automapper service
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("CoreApi", new OpenApiInfo { Title = "APINation", Version = "v1" });
            });
            #endregion
            #region CORS
            services.AddCors(c =>
            {
                c.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:44304")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            }
            );
            #endregion
            #region JWT

            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));//dependency injection ile service containerına bu sınıf eklenerek uygulamanın heryerinden ctor injectipn yaparak TokenOptions değerlerine ulaşılabilecek.

            var issuer = Configuration["TokenOptions:Issuer"]; //ile appsettings.json içeriğine ulaşılabilir
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    //bu bilgiler token ile gelecek her requestte kontrol edilecek
                    ValidateAudience = true, //dinleyiciyi doğrula tokenı gönderen doğrumu ayarlardaki ile
                    ValidateIssuer = true, //token içinde gelen issuer bilgisini valide et ayarlardaki ile aynımı
                    ValidateLifetime = true,//token expiretionı kontrol et
                    ValidateIssuerSigningKey = true,
                    ValidAudience = tokenOptions.Audience,//geçerli dinleyici
                    ValidIssuer = tokenOptions.Issuer,//geçerli issuer
                    IssuerSigningKey = SignHandler.GetSecurityKey(tokenOptions.SecurityKey),//verify signature gerekli tipe dönüştürülerek token optionsa değeri atanır ver validasyon bu keye göre yapılır
                    ClockSkew = TimeSpan.Zero//farklı saat dilimlerinde token ömrünü uzatmak için kullanılır
                };
            });


            //c.AddPolicy("FrontEnd", builder =>
            // builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            //c.AddPolicy("FrontEnd", builder =>
            // builder.WithOrigins("https://www.abc.com").AllowAnyMethod().AllowAnyHeader()
            //);
        }

        #endregion


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/CoreApi/swagger.json", "My API V1");
            });

            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //defualt route değişecek
            //app.UseCors("FrontEnd");sadece belirtilen originden gelen isteklere izin verilecek 
        }
    }
}
