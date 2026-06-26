using SupermarketManagementSystem.Data;
using SupermarketManagementSystem.Models;

namespace SupermarketManagementSystem.Services
{
    public class SupplierService
    {
        public List<Supplier> GetAll()
        {
            using var db = new SupermarketDbContext();

            return db.Suppliers.ToList();
        }

        public void Add(Supplier supplier)
        {
            using var db = new SupermarketDbContext();

            db.Suppliers.Add(supplier);
            db.SaveChanges();
        }

        public void Update(Supplier supplier)
        {
            using var db = new SupermarketDbContext();

            var s = db.Suppliers
                .FirstOrDefault(x =>
                    x.SupplierId == supplier.SupplierId);

            if (s == null)
                return;

            s.SupplierName = supplier.SupplierName;
            s.Phone = supplier.Phone;
            s.Address = supplier.Address;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            using var db = new SupermarketDbContext();

            var supplier =
                db.Suppliers.FirstOrDefault(
                    x => x.SupplierId == id);

            if (supplier == null)
                return;

            db.Suppliers.Remove(supplier);
            db.SaveChanges();
        }
    }
}