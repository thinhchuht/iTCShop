namespace iTCShop.Models
{
    public class StockOut
    {
        public string   ID           { get; set; }
        public int      Quantity     { get; set; }
        public decimal  PriceOut     { get; set; }
        public DateTime TransOutDate { get; set; }
        public ProductType  Product      { get; set; }
        public Supplier Supplier     { get; set; }
    }
}
