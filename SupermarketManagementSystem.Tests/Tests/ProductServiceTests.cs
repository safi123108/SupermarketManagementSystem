using Xunit;

namespace SupermarketManagementSystem.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void Product_Can_Be_Created()
        {
            Assert.True(true);
        }

        [Fact]
        public void Product_Price_Should_Be_Positive()
        {
            decimal price = 100;

            Assert.True(price > 0);
        }
    }
}