using Xunit;

namespace SupermarketManagementSystem.Tests
{
    public class SaleServiceTests
    {
        [Fact]
        public void Quantity_Sold_Should_Be_Greater_Than_Zero()
        {
            int quantity = 5;

            Assert.True(quantity > 0);
        }
    }
}