namespace iTCShop.Models
{
    public class Inventory
    {
        public string   ID       { get; set; }
        public int      Quantity { get; set; }
        public Product  Product  { get; set; }
        public Supplier Supplier { get; set; }
    }
}
