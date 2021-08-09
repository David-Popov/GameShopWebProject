using CarShopWebProject.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShopWebProject.Data
{
    public class GameShopDbContext : IdentityDbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Platform> Platform { get; set; }
        public DbSet<Admin> Admin { get; set; }


        public GameShopDbContext(DbContextOptions<GameShopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Admin>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Admin>(x => x.UserId);

            base.OnModelCreating(builder);
        }
    }
}
