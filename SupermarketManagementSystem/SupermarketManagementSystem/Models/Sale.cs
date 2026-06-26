using System;

namespace SupermarketManagementSystem.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public int QuantitySold { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime SaleDate { get; set; }

        public Product? Product { get; set; }
    }
}