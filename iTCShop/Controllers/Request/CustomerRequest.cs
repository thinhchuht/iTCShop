﻿namespace iTCShop.Controllers.Request
{
    public class CustomerRequest
    {
        public string   Name        { get; set; }
        public string   Email       { get; set; }
        public string   UserName    { get; set; }
        public string   Password    { get; set; }
        public string   Phone       { get; set; }
        public string   Address     { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
