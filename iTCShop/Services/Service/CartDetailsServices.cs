namespace iTCShop.Services.Service
{
    public class CartDetailsServices(IBaseDbServices baseDbServices, IProductDbServices productDbServices, iTCShopDbContext iTCShopDbContext) : ICartDetailsServices
    {

        public async Task<CartDetails> GetCartDetailByProductTypeId(string productTypeId, string cartId)
        {
            return await iTCShopDbContext.CartDetails.Include(c => c.ProductType).FirstOrDefaultAsync(c => c.ProductTypeID.Equals(productTypeId) && c.CartID.Equals(cartId));
        }

        public async Task<ResponseModel> AddCartDetail(string productTypeId, string cartId)
        {
            try
            {
                var checkStock = await productDbServices.IsAvailableCheck(productTypeId);
                if (!checkStock.IsSuccess()) return ResponseModel.FailureResponse("Out of stocks");
                var existCartDetail = await GetCartDetailByProductTypeId(productTypeId, cartId);
                if (existCartDetail == null)
                {
                    var cartDetail = new CartDetails(productTypeId, cartId);
                    await baseDbServices.AddAsync(cartDetail);
                }
                else
                {
                    existCartDetail.Quantity += 1;
                    iTCShopDbContext.SaveChanges();
                }
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
        public Task<List<CartDetails>> GetAllByCartId(string id)
        {
            return  iTCShopDbContext.CartDetails.Include(c => c.ProductType).Where(c => c.CartID.Equals(id)).ToListAsync();
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
                foreach (var cartDetail in cartDetails)
                {
                    await DeleteCartDetail(cartDetail.ID);
                }
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
          
        }
    }
}
