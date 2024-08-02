namespace iTCShop.Services.Service
{
    public class OrderDetailService(IBaseDbServices baseDbServices) : IOrderDetailServices
    {
        public async Task<ResponseModel> AddOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                await baseDbServices.AddAsync(orderDetail);
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }

        //public Task<ResponseModel> DeleteOrderDetail(string id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
