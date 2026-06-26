using Microsoft.EntityFrameworkCore;
using SupermarketManagementSystem.Data;
using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Services;

public class ReportService
{
    private readonly SupermarketDbContext _db;

    public ReportService()
    {
        _db = new SupermarketDbContext();
    }

    public List<Product> GetLowStockProducts()
    {
        return _db.Products
            .Where(p => p.QuantityInStock <= 10)
            .ToList();
    }

    public List<Product> GetProductsByCategory(int categoryId)
    {
        return _db.Products
            .Where(p => p.CategoryId == categoryId)
            .ToList();
    }

    public List<Product> GetSupplierProducts(int supplierId)
    {
        return _db.Products
            .Where(p => p.SupplierId == supplierId)
            .ToList();
    }
}