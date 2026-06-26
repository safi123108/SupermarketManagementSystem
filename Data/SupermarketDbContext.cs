using Microsoft.EntityFrameworkCore;
using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Data;

public class SupermarketDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();

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