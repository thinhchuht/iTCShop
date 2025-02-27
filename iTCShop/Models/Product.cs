﻿using System.ComponentModel.DataAnnotations;

namespace iTCShop.Models
{
    public class Product
    {
        public Product() {}

        public Product(string iMEI , string productTypeId, OrderStatus orderStatus)
        {
            IMEI          = iMEI;
            ProductTypeId = productTypeId;
            Status        = orderStatus;
        }

        public string      IMEI          { get; set; }
        [Required]
        public string      ProductTypeId { get; set; }
        public OrderStatus Status        { get; set; }
        public ProductType ProductType   { get; set; }
    }
}
