namespace iTCShop.Services.Service
{
    public class ProductDbServices(IBaseDbServices baseDbServices, iTCShopDbContext iTCShopDbContext) : IProductDbServices
    {
        public async Task<ResponseModel> AddProduct(ProductRequest productRequest)
        {
            try
            {
                //var productType = await baseDbServices.GetById<ProductType>(productRequest.ProductTypeId);
                var products = await GetAllProducts();
                var productTypes = await iTCShopDbContext.ProductTypes.ToListAsync();
                if (products.Any(p => p.IMEI == productRequest.Imei)) return ResponseModel.FailureResponse("There is already a phone with this IMEI!");
                if (!productTypes.Any(p => p.ID == productRequest.ProductTypeId)) return ResponseModel.FailureResponse("This product type does not exist!");
                var newProduct = new Product(productRequest.Imei, productRequest.ProductTypeId, OrderStatus.OnStock);
                await baseDbServices.AddAsync(newProduct);
                return ResponseModel.SuccessResponse();
            }
            catch 
            {
                return ResponseModel.ExceptionResponse();
            }
        }

        public async Task<ResponseModel> AddProductToOrder(string imei)
        {
            var product = await GetProductByImei(imei);
            product.Status = OrderStatus.Pending;
            iTCShopDbContext.Update(product);
            iTCShopDbContext.SaveChanges();
            return ResponseModel.SuccessResponse();
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

        public async Task<ResponseModel> DeleteProductByProductTypeID(string productTypeId)
        {
            var productLst = await GetProductsByProductType(productTypeId);
            foreach (var product in productLst)
            {
               var rs = await DeleteProduct(product.IMEI);
                if (!rs.IsSuccess()) return rs;
            }
            return ResponseModel.SuccessResponse();
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

        public async Task<ResponseModel> IsAvailableCheck(string productTypeId, int quantity = 0)
        {
            var products = await iTCShopDbContext.Products.Include(p => p.ProductType).Where(p => p.ProductTypeId.Equals(productTypeId) && p.Status.Equals(OrderStatus.OnStock)).ToListAsync();
            if (products.Count == 0) return ResponseModel.FailureResponse("Out of stocks");
            if (products.Count < quantity) return ResponseModel.FailureResponse("Out of stocks");
            else return ResponseModel.SuccessResponse();
        }

        public async Task<ResponseModel> UpdateProductStatus(string imei, int newStatus)
        {
            try
            {
                var product = await GetProductByImei(imei);
                product.Status = (OrderStatus)newStatus;
                iTCShopDbContext.Update(product);
               await iTCShopDbContext.SaveChangesAsync();
                return ResponseModel.SuccessResponse();
            }
            catch 
            {
                return ResponseModel.FailureResponse("Failed to update status");
            }
        }
        public async Task<ResponseModel> UpdateProduct(ProductRequest productRequest)
        {
            try
            {
              
                var productType = await baseDbServices.GetById<ProductType>(productRequest.ProductTypeId);
                if (productType == null) return ResponseModel.FailureResponse("Can not found ProducTypeID");
                var oldProduct = await GetProductByImei(productRequest.Imei);
                if(oldProduct.Status != OrderStatus.OnStock && oldProduct.Status != OrderStatus.Pending) { return ResponseModel.FailureResponse("You cannot change product that is already on the market!"); }
                var newProduct = new Product(productRequest.Imei,productType.ID,oldProduct.Status);
                
                await baseDbServices.UpdateAsync(newProduct, oldProduct);
                return ResponseModel.SuccessResponse();
            }
            catch
            {
                return ResponseModel.ExceptionResponse();
            }
        }

        public async Task<List<Product>> GetOnStockProductsByProductType(string productTypeId)
        {
            return await iTCShopDbContext.Products.Include(p=>p.ProductType).Where(p=>p.Status.Equals(OrderStatus.OnStock) && p.ProductTypeId.Equals(productTypeId)).ToListAsync();
        }
    }
}
