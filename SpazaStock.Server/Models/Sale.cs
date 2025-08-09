using System;
using System.Collections.Generic;

namespace SpazaStock.Server.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public List<SaleItem> SaleItems { get; set; } = new();
    }
}
