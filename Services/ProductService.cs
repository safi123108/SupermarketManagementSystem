using Microsoft.EntityFrameworkCore;
using SupermarketManagementSystem.Data;
using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Services;

public class ProductService
{
    private readonly SupermarketDbContext _db;

    public ProductService()
    {
        _db = new SupermarketDbContext();
    }

    public List<Product> GetAllProducts()
    {
        return _db.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .ToList();
    }

    public Product? GetById(int id)
    {
        return _db.Products.Find(id);
    }

    public void Add(Product product)
    {
        _db.Products.Add(product);
        _db.SaveChanges();
    }

    public void Update(Product product)
    {
        _db.Products.Update(product);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var product = _db.Products.Find(id);

        if (product != null)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
        }
    }

    public Product? SearchByBarcode(string barcode)
    {
        return _db.Products
            .FirstOrDefault(p => p.Barcode == barcode);
    }

    public List<Product> SearchByName(string name)
    {
        return _db.Products
            .Where(p => p.ProductName.Contains(name))
            .ToList();
    }
}