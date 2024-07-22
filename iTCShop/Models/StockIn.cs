namespace iTCShop.Models
{
    public class StockIn
    {
        public string   ID          { get; set; }
        public int      Quantity    { get; set; }
        public decimal  PriceIn     { get; set; }
        public DateTime TransInDate { get; set; }
        public ProductType  Product     { get; set; }    
        public Supplier Supplier    { get; set; }
    }
}
