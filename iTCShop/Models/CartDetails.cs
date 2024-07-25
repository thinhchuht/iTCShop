namespace iTCShop.Models
{
    public class CartDetails
    {
        public string      ID            { get; set; }
        public int         Quantity      { get; set; }
        public string      CartID        { get; set; }
        public string      ProductTypeID { get; set; }
        public ProductType ProductType   { get; set; }

        public CartDetails() { }

        public CartDetails(string productTypeId, string cartId)
        {
            ID = Guid.NewGuid().ToString();
            Quantity = 1;
            CartID = cartId;
            ProductTypeID = productTypeId;
        }
    }
}
