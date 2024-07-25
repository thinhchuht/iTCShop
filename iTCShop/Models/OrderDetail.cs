namespace iTCShop.Models
{
    public class OrderDetail
    {
        public  string     ID          { get; set; }
        public int         Quantity    { get; set; }
        public decimal     Price       { get; set; }
        public decimal     TotalAmount { get; set; }
        public string      ProductID   { get; set; }
        public ProductType Product     { get; set; }
    }
}
