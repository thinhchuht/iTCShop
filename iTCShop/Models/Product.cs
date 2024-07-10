namespace iTCShop.Models
{
    public class Product
    {
        public string  ID          { get; set; }
        public string  Name        { get; set; }
        public decimal Price       { get; set; }
        public string  Description { get; set; }
        public decimal Size        { get; set; }
        public int     Battery     { get; set; }
        public int     Memory      { get; set; }
        public string  Color       { get; set; }
        public int     RAM         { get; set; } 
        public string  IMEI        { get; set; } 
    }
}
