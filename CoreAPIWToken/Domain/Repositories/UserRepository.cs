using CoreAPIWToken.Domain.Data;
using CoreAPIWToken.Domain.Models;
using CoreAPIWToken.Security.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Domain.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly TokenOptions tokenOptions;
        public UserRepository(TokenApiDBContext dBContext, IOptions<TokenOptions> tokenOptions) : base(dBContext)
        {
            this.tokenOptions = tokenOptions.Value;
        }
        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await db.Users.FindAsync(userId);
        }

        public async Task<User> GetUserWithRefreshTokenAsync(string refreshToken)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task RegisterUserAsync(User user)
        {
            //user password hashlenip dbye kaydedilmesi lazım ör MD5 ile hashlenebilir
            await db.Users.AddAsync(user);
        }

        public async Task RevokeRefreshTokenAsync(User user)
        {
            var appUser = await db.Users.FindAsync(user.Id);
            appUser.RefreshToken = null;
            appUser.RefreshTokenEndDate = null;
            //db save service katmanında yapılacak

        }

        public async Task<bool> SaveRefreshTokenAsync(User user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = DateTime.Now.AddMinutes(tokenOptions.RefreshTokenExpiration);
            return db.Users.Update(user) != null ;
        
        }

    }
}
