using iTCShop.Constants;
using iTCShop.Models;

namespace iTCShop.Services.Interfaces
{
    public interface IProductsServices
    {
        Task<List<Product>> GetAllProducts();
        Task<ResponseModel> AddProduct(Product product);
    }
}
