using iTCShop.Constants;
using iTCShop.Models;
using iTCShop.Services.Interfaces;

namespace iTCShop.Services.Service
{
    public class ProductsServices(IBaseDbServices baseDbServices) : IProductsServices
    {
        public async Task<ResponseModel> AddProduct(Product product)
        {
            try
            {
                await baseDbServices.AddAsync(product);
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
           
        }

        public Task<List<Product>> GetAllProducts()
        {
            return baseDbServices.GetAll<Product>();
        }

        
    }
}
