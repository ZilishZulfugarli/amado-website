using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Entities;

namespace MiniLayihe.Data
{
	public class AppDbContext : IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

        public DbSet<Color> Colors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}

