
namespace iTCShop.Services.Service
{
    public class CartServices(IBaseDbServices baseDbServices, iTCShopDbContext iTCShopDbContext, ICartDetailsServices cartDetailsServices) : ICartService
    {
        public async Task<Cart> GetCartById(string id)
        {
            return await iTCShopDbContext.Carts.Include(c => c.CartDetails).FirstOrDefaultAsync(c => c.Id.Equals(id));
        }
        public async Task<ResponseModel> AddToCart(string id, string productTypeId)
        {
            try
            {
                await cartDetailsServices.AddCartDetail(productTypeId, id);
                var cartDetails = await cartDetailsServices.GetCartDetailByProductTypeId(productTypeId, id);
                var cart = await GetCartById(id);
                cart.CartDetails.Add(cartDetails);

                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }

        }

        public ResponseModel CreateCart(string id)
        {
            try
            {
                iTCShopDbContext.Carts.Add(new Cart(id));
                iTCShopDbContext.SaveChanges();
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }
    }
}
