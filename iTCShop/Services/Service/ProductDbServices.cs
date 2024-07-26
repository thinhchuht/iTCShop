namespace iTCShop.Services.Service
{
    public class ProductDbServices(IBaseDbServices baseDbServices, iTCShopDbContext iTCShopDbContext) : IProductDbServices
    {
        public async Task<ResponseModel> AddProduct(ProductRequest productRequest)
        {
            try
            {
                var productType = await baseDbServices.GetById<ProductType>(productRequest.ProductId);
                var newProduct =  new Product(productRequest.Imei, productType.ID);
                await baseDbServices.AddAsync(newProduct);
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<ResponseModel> DeleteProduct(string imei)
        {
            try 
            { 
                await baseDbServices.DeleteAsync<Product>(imei);
                return ResponseModel.SuccessResponse();
            }
            catch(Exception ex) 
            { 
                return ResponseModel.FailureResponse(ex.ToString()); 
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
           return await iTCShopDbContext.Products.Include(p => p.ProductType).ToListAsync();
          
        }

        public  async Task<Product> GetProductByImei(string imei)
        {
            return await iTCShopDbContext.Products.Include(p => p.ProductType).FirstOrDefaultAsync(p => p.IMEI.Equals(imei));
        }

        public async Task<List<Product>> GetProductsByProductType(string productTypeId)
        {
            return await iTCShopDbContext.Products.Include(p=>p.ProductType).Where(p => p.ProductTypeId.Equals(productTypeId)).ToListAsync();
        }

        public async Task<ResponseModel> IsAvailableCheck(string productTypeId)
        {
            var product = await GetProductsByProductType(productTypeId);
            if (product.Count == 0) return ResponseModel.FailureResponse("Out of stocks");
            else return ResponseModel.SuccessResponse();
        }

        public async Task<ResponseModel> UpdateProduct(ProductRequest productRequest)
        {
            try
            {
              
                var productType = await baseDbServices.GetById<ProductType>(productRequest.ProductId);
                if (productType == null) return ResponseModel.FailureResponse("Can not found ProducTypeID");
                var oldProduct = await GetProductByImei(productRequest.Imei);
                var newProduct = new Product(productRequest.Imei,productType.ID);
                
                await baseDbServices.UpdateAsync(newProduct, oldProduct);
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }
    }
}
