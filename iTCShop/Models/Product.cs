using System.ComponentModel.DataAnnotations;

namespace iTCShop.Models
{
    public class Product
    {
        public Product() {}
        public Product(string iMEI , string productTypeId)
        {
            IMEI = iMEI;
            ProductTypeId = productTypeId;
        }

        public string IMEI { get; set; }
        [Required]
        public string ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
