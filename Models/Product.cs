namespace SupermarketManagementSystem.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Barcode { get; set; } = "";
    public string ProductName { get; set; } = "";

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }

    public DateTime ExpiryDate { get; set; }
    public DateTime RestockDate { get; set; }

    public string StockStatus { get; set; } = "";
}