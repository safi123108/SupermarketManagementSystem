using SupermarketManagementSystem.Data;
using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Services;

public class SupplierService
{
    private readonly SupermarketDbContext _db;

    public SupplierService()
    {
        _db = new SupermarketDbContext();
    }

    public List<Supplier> GetAll()
    {
        return _db.Suppliers.ToList();
    }

    public void Add(Supplier supplier)
    {
        _db.Suppliers.Add(supplier);
        _db.SaveChanges();
    }

    public void Update(Supplier supplier)
    {
        _db.Suppliers.Update(supplier);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var supplier = _db.Suppliers.Find(id);

        if (supplier != null)
        {
            _db.Suppliers.Remove(supplier);
            _db.SaveChanges();
        }
    }
}