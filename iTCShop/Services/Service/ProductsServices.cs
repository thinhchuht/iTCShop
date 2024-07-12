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

        public async Task<Product> GetProductById(string id)
        {
            return await baseDbServices.GetById<Product>(id);
        }

        public async Task<ResponseModel> DeleteProduct(string id)
        {
            try
            {
                await baseDbServices.DeleteAsync<Product>(id);
                return  ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }

        public async Task<ResponseModel> UpdateProduct(Product product)
        {
            try
            {
                await baseDbServices.UpdateAsync<Product>(product);
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)    
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }
    }
}
