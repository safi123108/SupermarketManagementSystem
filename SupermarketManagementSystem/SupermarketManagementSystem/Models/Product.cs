using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketManagementSystem.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Barcode { get; set; } = "";

        [Required]
        public string ProductName { get; set; } = "";

        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime RestockDate { get; set; }

        public string StockStatus { get; set; } = "Available";

        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }
    }
}