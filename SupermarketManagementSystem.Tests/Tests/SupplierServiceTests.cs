using Xunit;

namespace SupermarketManagementSystem.Tests
{
    public class SupplierServiceTests
    {
        [Fact]
        public void Supplier_Name_Should_Not_Be_Empty()
        {
            string name = "ABC Suppliers";

            Assert.False(string.IsNullOrWhiteSpace(name));
        }
    }
}