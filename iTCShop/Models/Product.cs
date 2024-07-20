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
        public string  Picture     { get; set; }

        public Product(string name, decimal price, string description, decimal size, int battery, int memory, string color, int rAM, string iMEI, string picture)
        {
            ID          = Guid.NewGuid().ToString();
            Name        = name;
            Price       = price;
            Description = description;
            Size        = size;
            Battery     = battery;
            Memory      = memory;
            Color       = color;
            RAM         = rAM;
            Picture     = picture;
            IMEI        = iMEI;
        }
    }
}
