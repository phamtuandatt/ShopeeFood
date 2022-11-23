using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Data.Entities;

namespace OA_Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<BusinessType> BusinessTypes { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<CityDetail> CityDetails { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<ShopMenu> ShopMenus { get; set; }

        public DbSet<CityDistrict> CityDistricts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Set Primary Key for CityDetail
            modelBuilder.Entity<CityDetail>().HasKey(key => new
            {
                key.CityId,
                key.BusinessId,
            });

            // Set Primary key for Dilevery
            modelBuilder.Entity<Delivery>().HasKey(key => new
            {
                key.EmpId,
                key.OrderId,
            });

            // Set Primary key for OrderDetail
            modelBuilder.Entity<OrderDetail>().HasKey(key => new
            {
                key.OrderId,
                key.ProductId,
            });

            // Set Primary key for ShopMenu
            modelBuilder.Entity<ShopMenu>().HasKey(key => new
            {
                key.ShopId,
                key.ProductTypeId,
                key.ProductId,
            });

        }
    }
}
