using iTCShop.Constants;
using iTCShop.Controllers.Request;
using iTCShop.Models;

namespace iTCShop.Services.Interfaces
{
    public interface IProductsServices
    {
        Task<List<Product>> GetAllProducts();
        Task<Product>       GetProductById(string id);
        Task<ResponseModel> AddProduct(Product product);
        Task<ResponseModel> DeleteProduct(string id);
        Task<ResponseModel> UpdateProduct(ProductsRequest product, string id);
    }
}
