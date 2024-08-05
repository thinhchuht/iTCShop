namespace iTCShop.Models
{
    public enum OrderStatus
    {
        OnStock    = 0,
        Pending    = 1,
        InProgress = 2,
        Completed  = 3
    }

    public enum OrderPayMethod
    {
        CashOnDelivery = 1,
        BankTransfer   = 2
     
    }

    public class Order
    {
        public string              ID           { get; set; }
        public DateTime            OrderDate    { get; set; }
        public string              ShipAddress  { get; set; }
        public decimal             TotalPay     { get; set; }
        public OrderStatus         Status       { get; set; }
        public OrderPayMethod      PayMethod    { get; set; }
        public string              CustomerId   { get; set; }
        public List<OrderDetail>   OrderDetails { get; set; }

        public Order()
        {
            ID        = Guid.NewGuid().ToString();
            OrderDate = DateTime.Now;
            Status    = OrderStatus.Pending;
        }
    }
}
