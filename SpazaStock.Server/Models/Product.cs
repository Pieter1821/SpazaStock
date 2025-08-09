using System;

namespace SpazaStock.Server.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int CurrentStock { get; set; } = 0;
        public int MinimumStock { get; set; } = 5;
        public bool IsFromDemo { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
