namespace iTCShop.Services.Service
{
    public class ProductsTypeServices(IBaseDbServices baseDbServices, IProductDbServices productDbServices) : IProductsTypeServices
    {
        public async Task<ResponseModel> AddProductType(ProductType product)
        {
            try
            {
                var products = await GetAllProductTypes();
                if(products.Any(p => p.Name == product.Name && p.Memory == product.Memory && p.Color == product.Color)) return ResponseModel.FailureResponse("There is already have this type of phone!"); 
                await baseDbServices.AddAsync(product);
                return ResponseModel.SuccessResponse();
            }
            catch
            {
                return ResponseModel.ExceptionResponse();
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
                var rs = await productDbServices.DeleteProductByProductTypeID(id);
                if (!rs.IsSuccess()) return rs;
                await baseDbServices.DeleteAsync<ProductType>(id);
                return ResponseModel.SuccessResponse();
            }
            catch 
            {
                return ResponseModel.ExceptionResponse();
            }
        }

        public async Task<ResponseModel> UpdateProductType(ProductType newProduct)
        {
            try
            {
                var product = await GetProductTypeById(newProduct.ID);
                await baseDbServices.UpdateAsync(newProduct, product);
                return ResponseModel.SuccessResponse();
            }
            catch
            {
                return ResponseModel.ExceptionResponse();
            }
        }
    }
}
