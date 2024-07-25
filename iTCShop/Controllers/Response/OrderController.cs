namespace iTCShop.Controllers.Response
{
    public class OrderController(IOrderService orderService, IOrderDetailServices orderDetailServices) : Controller
    {
        public IActionResult Order( int quantity, decimal price, string productID)
        {
            try
            {
                //string shipAddress, decimal totalPay, int payMethod, int status, string customerId,
                var order = new Order();
                orderService.AddOrder(order);
                var orderDetail = new OrderDetail(quantity, price, productID, order.ID);
                orderDetailServices.AddOrderDetail(orderDetail);
                return View(orderDetail);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
