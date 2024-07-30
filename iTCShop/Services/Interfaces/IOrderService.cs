namespace iTCShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetAllCompletedOrders();
        Task<List<Order>> GetOrdersByCustomerId(string id);
        Task<ResponseModel> AddOrder(Order order);
        Task<Order> GetOrderById(string id);
        ResponseModel UpdateOrder(Order order);
    }
}
