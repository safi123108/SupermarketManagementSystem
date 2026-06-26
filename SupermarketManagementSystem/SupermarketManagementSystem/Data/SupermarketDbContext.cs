using Microsoft.EntityFrameworkCore;
using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Data
{
    public class SupermarketDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-VPTOFDQ;
                  Database=SupermarketDB;
                  Trusted_Connection=True;
                  TrustServerCertificate=True;");
        }
    }
}