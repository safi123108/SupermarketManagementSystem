using Microsoft.EntityFrameworkCore;
using SupermarketManagementSystem.Data;
using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Services
{
    public class ProductService
    {
        public List<Product> GetAll()
        {
            using var db = new SupermarketDbContext();

            return db.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .AsNoTracking()
                .ToList();
        }

        public Product? GetByBarcode(string barcode)
        {
            using var db = new SupermarketDbContext();

            return db.Products
                .AsNoTracking()
                .FirstOrDefault(x => x.Barcode == barcode);
        }

        public void Add(Product product)
        {
            using var db = new SupermarketDbContext();

            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Update(Product product)
        {
            using var db = new SupermarketDbContext();

            var existing =
                db.Products.FirstOrDefault(
                    x => x.ProductId == product.ProductId);

            if (existing == null)
                return;

            existing.Barcode = product.Barcode;
            existing.ProductName = product.ProductName;
            existing.CategoryId = product.CategoryId;
            existing.SupplierId = product.SupplierId;
            existing.Price = product.Price;
            existing.QuantityInStock = product.QuantityInStock;
            existing.ExpiryDate = product.ExpiryDate;
            existing.RestockDate = product.RestockDate;
            existing.StockStatus = product.StockStatus;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            using var db = new SupermarketDbContext();

            var product =
                db.Products.FirstOrDefault(
                    x => x.ProductId == id);

            if (product == null)
                return;

            db.Products.Remove(product);
            db.SaveChanges();
        }
    }
}