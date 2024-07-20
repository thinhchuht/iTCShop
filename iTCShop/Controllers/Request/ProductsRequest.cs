namespace iTCShop.Controllers.Request
{
    public class ProductsRequest
    {
        public string  Name        { get; set; }
        public decimal Price       { get; set; }
        public string  Description { get; set; }
        public decimal Size        { get; set; }
        public int     Battery     { get; set; }
        public int     Memory      { get; set; }
        public string  Color       { get; set; }
        public int     RAM         { get; set; }
        public string  IMEI        { get; set; }
        public string  Picture     { get; set; }
    }
}
