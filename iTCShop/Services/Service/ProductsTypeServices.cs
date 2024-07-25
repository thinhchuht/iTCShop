namespace iTCShop.Services.Service
{
    public class ProductsTypeServices(IBaseDbServices baseDbServices) : IProductsTypeServices
    {
        public async Task<ResponseModel> AddProductType(ProductType product)
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

        public Task<List<ProductType>> GetAllProductTypes()
        {
            return baseDbServices.GetAll<ProductType>();
        }

        public async Task<ProductType> GetProductTypeById(string id)
        {
            return await baseDbServices.GetById<ProductType>(id);
        }

        public async Task<ResponseModel> DeleteProductType(string id)
        {
            try
            {
                await baseDbServices.DeleteAsync<ProductType>(id);
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }

        public async Task<ResponseModel> UpdateProductType(ProductType newProduct)
        {
            try
            {
                var product = await GetProductTypeById(newProduct.ID);
                await baseDbServices.UpdateAsync<ProductType>(newProduct, product);
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }
    }
}
