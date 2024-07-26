namespace iTCShop.Controllers.Request
{
    public class CartDetailsRequest
    {
        public string  ProductTypeID { get; set; }
        public int     Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
