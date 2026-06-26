namespace SupermarketManagementSystem.Models;

public class Supplier
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public string Address { get; set; } = "";
}