using SupermarketManagementSystem.Data;

namespace SupermarketManagementSystem.Services;

public class StockService
{
    private readonly SupermarketDbContext _db;

    public StockService()
    {
        _db = new SupermarketDbContext();
    }

    public void UpdateStock(int productId, int quantity)
    {
        var product = _db.Products.Find(productId);

        if (product == null)
            return;

        product.QuantityInStock = quantity;

        product.StockStatus =
            quantity <= 10
            ? "Low Stock"
            : "Available";

        _db.SaveChanges();
    }
}