namespace iTCShop.Models
{
    public class Order
    {
        public string            ID           { get; set; }
        public DateTime          OrderDate    { get; set; }
        public string            ShipAddress  { get; set; }
        public decimal           TotalPay     { get; set; }
        public Boolean           IsPaid       { get; set; }
        public string            PayMethod    { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
