using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Entity;
using Store.G04.Core.Entity.Order;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Store.G04.Repository.Data.Context
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductType> Types { get; set; }
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderItems> OrderItem { get; set; } 
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; } 



    }
}
