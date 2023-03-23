using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Entities
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasMany(p => p.Categories)
                    .WithMany(c => c.Products);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey("OrderId");

                entity.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey("ProductId");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(o => o.User)
                    .WithMany()
                    .HasForeignKey("UserId");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey("UserId");

                entity.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey("ProductId");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey("UserId");

                entity.HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey("ProductId");
            });
        }
    }
}
