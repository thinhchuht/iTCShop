namespace iTCShop.Models
{
    public class Cart
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public List<Product> Product { get; set; }
        public Cart(string id)
        {
            Id = id;
        }
    }
}
