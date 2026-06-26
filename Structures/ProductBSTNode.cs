using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Structures;

public class ProductBSTNode
{
    public Product Data { get; set; }

    public ProductBSTNode? Left { get; set; }
    public ProductBSTNode? Right { get; set; }

    public ProductBSTNode(Product product)
    {
        Data = product;
    }
}