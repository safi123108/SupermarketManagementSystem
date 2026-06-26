using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Structures;

public class ProductNode
{
    public Product Data { get; set; }
    public ProductNode? Next { get; set; }

    public ProductNode(Product product)
    {
        Data = product;
        Next = null;
    }
}