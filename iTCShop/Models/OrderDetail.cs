﻿namespace iTCShop.Models
{
    public class OrderDetail
    {
        public  string     ID          { get; set; }
        public int         Quantity    { get; set; }
        public decimal     Price       { get; set; }
        public decimal     TotalAmount { get; set; }
        public string      OrderId { get; set; }
        public string      ProductID   { get; set; }
        public Product     Product     { get; set; }
        public OrderDetail(int quantity, decimal price, string productID,string orderId)
        {
            ID = Guid.NewGuid().ToString();
            Quantity = quantity;
            Price = price;
            TotalAmount = price;
            OrderId = orderId;
            ProductID = productID;
        }
    }
}
