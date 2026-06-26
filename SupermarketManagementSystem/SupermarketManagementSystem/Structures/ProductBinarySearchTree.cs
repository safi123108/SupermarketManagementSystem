using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Structures;

public class ProductBinarySearchTree
{
    private ProductBSTNode? root;

    public void Insert(Product product)
    {
        root = InsertRecursive(root, product);
    }

    private ProductBSTNode InsertRecursive(
        ProductBSTNode? node,
        Product product)
    {
        if (node == null)
            return new ProductBSTNode(product);

        if (string.Compare(
                product.Barcode,
                node.Data.Barcode) < 0)
        {
            node.Left =
                InsertRecursive(node.Left, product);
        }
        else
        {
            node.Right =
                InsertRecursive(node.Right, product);
        }

        return node;
    }

    public Product? Search(string barcode)
    {
        return SearchRecursive(root, barcode);
    }

    private Product? SearchRecursive(
        ProductBSTNode? node,
        string barcode)
    {
        if (node == null)
            return null;

        if (node.Data.Barcode == barcode)
            return node.Data;

        if (string.Compare(
                barcode,
                node.Data.Barcode) < 0)
        {
            return SearchRecursive(
                node.Left,
                barcode);
        }

        return SearchRecursive(
            node.Right,
            barcode);
    }
}