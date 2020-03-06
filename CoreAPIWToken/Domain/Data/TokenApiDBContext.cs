using System;
using CoreAPIWToken.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreAPIWToken.Domain.Data
{
    public partial class TokenApiDBContext : DbContext
    {
        public TokenApiDBContext()
        {
        }

        public TokenApiDBContext(DbContextOptions<TokenApiDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-UN7J857;Initial Catalog=TokenApiDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            //modelBuilder.Entity<Products>(entity =>
            //{
            //    entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            //});
        }
    }
}
