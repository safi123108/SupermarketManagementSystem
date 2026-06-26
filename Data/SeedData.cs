using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Data;

public static class SeedData
{
    public static void Initialize(SupermarketDbContext db)
    {
        if (db.Categories.Any())
            return;

        var dairy = new Category
        {
            CategoryName = "Dairy",
            Description = "Milk and dairy products"
        };

        var bakery = new Category
        {
            CategoryName = "Bakery",
            Description = "Bread and bakery products"
        };

        var beverages = new Category
        {
            CategoryName = "Beverages",
            Description = "Soft drinks and juices"
        };

        var frozen = new Category
        {
            CategoryName = "Frozen Foods",
            Description = "Frozen products"
        };

        db.Categories.AddRange(
            dairy,
            bakery,
            beverages,
            frozen);

        db.SaveChanges();

        var nestle = new Supplier
        {
            SupplierName = "Nestle",
            Phone = "03001234567",
            Email = "sales@nestle.com",
            Address = "Lahore"
        };

        var unilever = new Supplier
        {
            SupplierName = "Unilever",
            Phone = "03007654321",
            Email = "sales@unilever.com",
            Address = "Karachi"
        };

        var cocaCola = new Supplier
        {
            SupplierName = "Coca Cola",
            Phone = "03001112222",
            Email = "sales@coca-cola.com",
            Address = "Islamabad"
        };

        db.Suppliers.AddRange(
            nestle,
            unilever,
            cocaCola);

        db.SaveChanges();

        db.Products.AddRange(

            new Product
            {
                Barcode = "100001",
                ProductName = "Milk",
                CategoryId = dairy.CategoryId,
                SupplierId = nestle.SupplierId,
                Price = 250,
                QuantityInStock = 50,
                ExpiryDate = DateTime.Today.AddMonths(1),
                RestockDate = DateTime.Today,
                StockStatus = "Available"
            },

            new Product
            {
                Barcode = "100002",
                ProductName = "Bread",
                CategoryId = bakery.CategoryId,
                SupplierId = nestle.SupplierId,
                Price = 120,
                QuantityInStock = 30,
                ExpiryDate = DateTime.Today.AddDays(7),
                RestockDate = DateTime.Today,
                StockStatus = "Available"
            },

            new Product
            {
                Barcode = "100003",
                ProductName = "Coke",
                CategoryId = beverages.CategoryId,
                SupplierId = cocaCola.SupplierId,
                Price = 180,
                QuantityInStock = 80,
                ExpiryDate = DateTime.Today.AddMonths(6),
                RestockDate = DateTime.Today,
                StockStatus = "Available"
            },

            new Product
            {
                Barcode = "100004",
                ProductName = "Ice Cream",
                CategoryId = frozen.CategoryId,
                SupplierId = unilever.SupplierId,
                Price = 450,
                QuantityInStock = 20,
                ExpiryDate = DateTime.Today.AddMonths(3),
                RestockDate = DateTime.Today,
                StockStatus = "Available"
            }

        );

        db.SaveChanges();
    }
}