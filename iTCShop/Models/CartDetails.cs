namespace iTCShop.Models
{
    public class CartDetails
    {
        public string            ID            { get; set; }
        public int               Quantity      { get; set; }
        public string            ProductTypeID { get; set; }
        public string            CustomerID    { get; set; }
        public List<ProductType> ProductTypes  { get; set; }

        public CartDetails() { }
        public CartDetails(string id) 
        {
            ID         = id;
            CustomerID = id;
        }
        public CartDetails(string id, string productTypeId)
        {
            ID            = id;
            Quantity      = 1;
            ProductTypeID = productTypeId;
            CustomerID    = id;
        }
    }
}
