namespace iTCShop.Controllers.Request
{
    public class ProductTypesRequest
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
        public string  Picture     { get; set; }
    }
}
