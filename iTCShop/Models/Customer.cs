namespace iTCShop.Models
{
    public class Customer
    {
        public string               ID            { get; set; }
        public string               Name          { get; set; }
        public string               Email         { get; set; }
        public string               Password      { get; set; }
        public string               Phone         { get; set; }
        public string               Address       { get; set; }
        public DateTime             DateOfBirth   { get; set; }
        public AuthorizeUser        Auth          { get; set; }
        public List<Order> Orders { get; set; }
    }
}
