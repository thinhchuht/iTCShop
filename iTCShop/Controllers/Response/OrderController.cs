using iTCShop.Models;

namespace iTCShop.Controllers.Response
{
    public class OrderController(IOrderService orderService, IOrderDetailServices orderDetailServices, IProductDbServices productDbServices) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Order( List<CartDetailsRequest> cartDetails)
        {
            try
            {
                //string shipAddress, decimal totalPay, int payMethod, int status, string customerId,
                var order = new Order();
               await orderService.AddOrder(order);

                foreach(var item in cartDetails)
                {
                    var allProducts =  await productDbServices.GetProductsByProductType(item.ProductTypeID);
                    var products = allProducts.Take(item.Quantity).ToList();
                    foreach ( var p in products) {
                        var orderDetail = new OrderDetail(item.Quantity, p.ProductType.Price, p.IMEI, order.ID);
                        await orderDetailServices.AddOrderDetail(orderDetail);
                        order.OrderDetails.Add(orderDetail);
                    }
                }

                return View(order);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
