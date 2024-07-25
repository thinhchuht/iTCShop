namespace iTCShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseModel> AddOrder(Order order);
    }
}
