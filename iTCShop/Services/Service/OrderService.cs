
namespace iTCShop.Services.Service
{
    public class OrderService(IBaseDbServices baseDbServices) : IOrderService
    {
        public async Task<ResponseModel> AddOrder(Order order)
        {
            try
            {
               await baseDbServices.AddAsync(order);
                return ResponseModel.SuccessResponse();
            }
            catch(Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }
    }
}
