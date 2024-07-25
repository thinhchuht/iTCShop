namespace iTCShop.Models
{
    public class Cart
    {
        public string        Id            { get; set; }
        public List<CartDetails> CartDetails { get; set; }

        public Cart(string id)
        {
            Id = id;
        }
    }
}
