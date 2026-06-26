using System.Linq;
using SupermarketManagementSystem.Data;

namespace SupermarketManagementSystem.Services
{
    public class ReportService
    {
        public int GetTotalProducts()
        {
            using var db = new SupermarketDbContext();
            return db.Products.Count();
        }

        public int GetTotalSuppliers()
        {
            using var db = new SupermarketDbContext();
            return db.Suppliers.Count();
        }

        public int GetTotalSales()
        {
            using var db = new SupermarketDbContext();
            return db.Sales.Count();
        }

        public decimal GetInventoryValue()
        {
            using var db = new SupermarketDbContext();

            return db.Products.Sum(
                x => x.Price * x.QuantityInStock);
        }

        public List<dynamic> GetInventoryReport()
        {
            using var db =
                new SupermarketDbContext();

            return db.Products
                .Select(p => new
                {
                    Product = p.ProductName,

                    AvailableStock =
                        p.QuantityInStock,

                    SoldStock =
                        db.Sales
                          .Where(s =>
                              s.ProductId ==
                              p.ProductId)
                          .Sum(s =>
                              (int?)s.QuantitySold)
                          ?? 0,

                    Price =
                        p.Price,

                    TotalValue =
                        p.Price *
                        p.QuantityInStock
                })
                .Cast<dynamic>()
                .ToList();
        }
    }
}