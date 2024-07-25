namespace iTCShop.Services.Interfaces
{
    public interface IOrderDetailServices
    {
        Task<ResponseModel> AddOrderDetail(OrderDetail orderDetail);
    }
}
