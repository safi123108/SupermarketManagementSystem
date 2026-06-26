using Microsoft.EntityFrameworkCore;
using SupermarketManagementSystem.Data;
using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Services
{
    public class StockService
    {
        public List<Product> GetAllStock()
        {
            using var db = new SupermarketDbContext();

            return db.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .ToList();
        }

        public void UpdateStock(
            int productId,
            int quantity)
        {
            using var db =
                new SupermarketDbContext();

            var product =
                db.Products.FirstOrDefault(
                    x => x.ProductId == productId);

            if (product == null)
                return;

            product.QuantityInStock = quantity;

            product.StockStatus =
                quantity > 0
                    ? "Available"
                    : "Out Of Stock";

            product.RestockDate =
                DateTime.Today;

            db.SaveChanges();
        }
    }
}