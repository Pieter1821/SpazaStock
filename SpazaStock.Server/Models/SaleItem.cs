namespace SpazaStock.Server.Models
{
    public class SaleItem
    {
        public int SaleItemId { get; set; }
        public int SaleId { get; set; }
        public required Sale Sale { get; set; }
        public int ProductId { get; set; }
        public required Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
