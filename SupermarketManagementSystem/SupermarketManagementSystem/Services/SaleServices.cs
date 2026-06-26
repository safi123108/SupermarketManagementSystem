using Microsoft.EntityFrameworkCore;
using SupermarketManagementSystem.Data;
using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Services
{
    public class SaleService
    {
        public List<Sale> GetAll()
        {
            using var db =
                new SupermarketDbContext();

            return db.Sales
                .Include(x => x.Product)
                .AsNoTracking()
                .ToList();
        }

        public void RecordSale(
            int productId,
            int quantity)
        {
            using var db =
                new SupermarketDbContext();

            var product =
                db.Products.FirstOrDefault(
                    x => x.ProductId == productId);

            if (product == null)
                throw new Exception(
                    "Product not found.");

            if (quantity <= 0)
                throw new Exception(
                    "Quantity must be greater than zero.");

            if (product.QuantityInStock < quantity)
                throw new Exception(
                    "Insufficient stock.");

            product.QuantityInStock -= quantity;

            product.StockStatus =
                product.QuantityInStock > 0
                    ? "Available"
                    : "Out Of Stock";

            Sale sale = new()
            {
                ProductId = productId,
                QuantitySold = quantity,
                TotalAmount =
                    product.Price * quantity,
                SaleDate =
                    DateTime.Now
            };

            db.Sales.Add(sale);

            db.SaveChanges();
        }
    }
}