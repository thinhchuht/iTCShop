namespace iTCShop.Services.Service
{
    public class CartDetailsServices(IBaseDbServices baseDbServices, IProductDbServices productDbServices, iTCShopDbContext iTCShopDbContext) : ICartDetailsServices
    {

        //public async Task<CartDetails> GetCartDetailByProductTypeId(string productTypeId)
        //{
        //    return await iTCShopDbContext.CartDetails.FirstOrDefaultAsync(c => c.ProductTypeID.Equals(productTypeId));
        //}

        //public async Task<CartDetails> GetByID(string id)
        //{
        //    return await baseDbServices.GetById<CartDetails>(id);
        //}
        //public async Task<ResponseModel> CreateCart(string id)
        //{
        //    try
        //    {
        //        var cartDetail = new CartDetails(id);
        //        await baseDbServices.AddAsync(cartDetail);
        //        return ResponseModel.SuccessResponse();
        //    }
        //    catch
        //    {
        //        return ResponseModel.FailureResponse("Create cart failed");
        //    }
        //}

        public async Task<ResponseModel> AddCartDetail( string productTypeId, string customerId)
        {
            try
            {
                var checkStock = await productDbServices.IsAvailableCheck(productTypeId);
                if (!checkStock.IsSuccess()) return checkStock;
                var existCartDetail = await GetCartDetailByTypeAndCustId(productTypeId, customerId);
                if (existCartDetail == null)
                {
                    var cartDetail = new CartDetails(customerId, productTypeId);
                    await baseDbServices.AddAsync(cartDetail);
                }
                else
                {
                    existCartDetail.Quantity += 1;
                    if (existCartDetail.Quantity > 5) return ResponseModel.FailureResponse("You should only purcharse 10 of these, if you want to buy more, contact our shop");
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
        public async Task<List<CartDetails>> GetAllByCartId(string customersID)
        {
            return await iTCShopDbContext.CartDetails.Include(c => c.ProductType).Where(c => c.CustomerID.Equals(customersID)).ToListAsync();
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
            catch (Exception ex)
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
                    await baseDbServices.DeleteAsync<CartDetails>(cartDetail.ID);
                }
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }

        }

        public async Task<CartDetails> GetCartDetailByTypeAndCustId(string productTypeId, string customerId)
        {
            return await iTCShopDbContext.CartDetails.Include(c => c.ProductType).FirstOrDefaultAsync(c => c.ProductTypeID.Equals(productTypeId) && c.CustomerID.Equals(customerId));
        }
    }
}
