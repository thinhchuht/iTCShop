

namespace iTCShop.Services.Service
{
    public class OrderService(IBaseDbServices baseDbServices, iTCShopDbContext iTCShopDbContext) : IOrderService
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

        public async Task<List<Order>> GetAllOrders()
        {
            return await iTCShopDbContext.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).ToListAsync();
        }

        public async Task<Order> GetOrderById(string id)
        {
            return await iTCShopDbContext.Orders.Include(o=>o.OrderDetails).FirstOrDefaultAsync(o => o.ID.Equals(id));
        }

        public async Task<List<Order>> GetOrdersByCustomerId(string id)
        {
            return await iTCShopDbContext.Orders.Include(o => o.OrderDetails).Where(o=> o.CustomerId.Equals(id)).ToListAsync();
        }

        public ResponseModel UpdateOrder(Order order)
        {
            try
            {
                iTCShopDbContext.Update(order);
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
