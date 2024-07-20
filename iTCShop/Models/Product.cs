using System.ComponentModel.DataAnnotations;

namespace iTCShop.Models
{
    public class Product
    {
        public Product() {}
        public Product(string iMEI , string productTypeId, ProductType productType)
        {
            IMEI = iMEI;
            ProductTypeId = productTypeId;
            ProductType = productType;
        }

        public string IMEI { get; set; }
        [Required]
        public string ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
