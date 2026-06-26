using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Structures;

public class ProductLinkedList
{
    private ProductNode? head;

    public void Add(Product product)
    {
        ProductNode newNode = new ProductNode(product);

        if (head == null)
        {
            head = newNode;
            return;
        }

        ProductNode current = head;

        while (current.Next != null)
        {
            current = current.Next;
        }

        current.Next = newNode;
    }

    public Product? FindByName(string name)
    {
        ProductNode? current = head;

        while (current != null)
        {
            if (current.Data.ProductName.Equals(
                name,
                StringComparison.OrdinalIgnoreCase))
            {
                return current.Data;
            }

            current = current.Next;
        }

        return null;
    }

    public List<Product> GetAll()
    {
        List<Product> products = new();

        ProductNode? current = head;

        while (current != null)
        {
            products.Add(current.Data);
            current = current.Next;
        }

        return products;
    }
}