namespace iTCShop.Services.Service
{
    public class CartDetailsServices(IBaseDbServices baseDbServices, IProductDbServices productDbServices, IProductsTypeServices productsTypeServices,  iTCShopDbContext iTCShopDbContext) : ICartDetailsServices
    {

        public async Task<CartDetails> GetCartDetailByProductTypeId(string productTypeId, string cartId)
        {
            return await iTCShopDbContext.CartDetails.Include(c => c.ProductTypes).FirstOrDefaultAsync(c => c.ProductTypeID.Equals(productTypeId) && c.ID.Equals(cartId));
        }

        //public async Task<CartDetails> GetByID(string id)
        //{
        //    return await baseDbServices.GetById<CartDetails>(id);
        //}
        public async Task<ResponseModel> CreateCart(string id)
        {
            try
            {
                var cartDetail = new CartDetails(id);
                await baseDbServices.AddAsync(cartDetail);
                return ResponseModel.SuccessResponse();
            }
            catch
            {
                return ResponseModel.FailureResponse("Create cart failed");
            }
        }

        public async Task<ResponseModel> AddCartDetail(string cartId, string productTypeId)
        {
            try
            {
                var checkStock = await productDbServices.IsAvailableCheck(productTypeId);
                if (!checkStock.IsSuccess()) return checkStock;
                var existCartDetail = await GetCartDetailByProductTypeId(productTypeId, cartId);
                if (existCartDetail == null)
                {
                    var cartDetail =  new CartDetails(cartId ,productTypeId);
                    cartDetail.ProductTypes.Add(await productsTypeServices.GetProductTypeById(productTypeId));
                    iTCShopDbContext.Update(cartDetail);
                }
                else
                {
                    existCartDetail.Quantity += 1;
                    checkStock = await productDbServices.IsAvailableCheck(productTypeId, existCartDetail.Quantity);
                    if (!checkStock.IsSuccess()) return checkStock;
                }
                iTCShopDbContext.SaveChanges();
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }

        public async Task<ResponseModel> DeleteCartDetail(string id)
        {
            try
            {
                await baseDbServices.DeleteAsync<CartDetails>(id);
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }

        }

        public async Task<CartDetails> GetById(string id)
        {
            return await baseDbServices.GetById<CartDetails>(id);
        } 
        public Task<CartDetails> GetAllByCartId(string id)
        {
            return iTCShopDbContext.CartDetails.Include(c => c.ProductTypes).FirstOrDefaultAsync(c=>c.ID.Equals(id));
        }

        public async Task<ResponseModel> UpdateDropQuantity(string id)
        {
            try
            {
                var cartDetail = await GetById(id);
                if (cartDetail.Quantity == 1) await DeleteCartDetail(id);
                else
                {
                    cartDetail.Quantity--;
                    iTCShopDbContext.Update(cartDetail);
                    iTCShopDbContext.SaveChanges();
                }
                return ResponseModel.SuccessResponse();
            }
            catch(Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }

        public async Task<ResponseModel> DeleteAllCartDetail(string id)
        {
            try
            {
                var cartDetails = await GetAllByCartId(id);
                //foreach (var productType in cartDetails.ProductTypes)
                //{
                //    await DeleteCartDetail(productType.ID);
                //}
                cartDetails.ProductTypes.Clear();
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
          
        }
    }
}
