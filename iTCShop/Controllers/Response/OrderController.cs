using iTCShop.Extensions;

namespace iTCShop.Controllers.Response
{
    public class OrderController(IOrderService orderService, IOrderDetailServices orderDetailServices, IProductDbServices productDbServices, ICartDetailsServices cartDetailsServices) : Controller
    {
        public async Task<IActionResult> GetAllOrders()
        {
            var customer = HttpContext.Session.GetObjectFromJson<Customer>("user");
            var admin = HttpContext.Session.GetObjectFromJson<Admin>("admin");
            if (customer == null && admin == null) return RedirectToAction("Login", "Login");
            else
            { 
                if (admin == null)
                {
                    var cusOrders = await orderService.GetOrdersByCustomerId(customer.ID);
                    return View("GetAllOrders", cusOrders);
                }
                var allOrders = await orderService.GetAllOrders();
                return View("GetOrdersAdmin", allOrders);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Order(List<CartDetailsRequest> cartDetails, decimal total)
        {
            try
            {
                var customer = HttpContext.Session.GetObjectFromJson<Customer>("user");
                var order = new Order();
                order.TotalPay = total;
                order.CustomerId = customer.ID;
                await orderService.AddOrder(order);
                foreach (var item in cartDetails)
                {
                    var allProducts = await productDbServices.GetProductsByProductType(item.ProductTypeID);
                    var products = allProducts.Take(item.Quantity).ToList();
                    foreach (var p in products)
                    {
                        var orderDetail = new OrderDetail(item.Quantity, p.ProductType.Price, p.IMEI, order.ID);
                        await orderDetailServices.AddOrderDetail(orderDetail);
                        order.OrderDetails.Add(orderDetail);
                        await productDbServices.AddProductToOrder(p.IMEI);
                    }
                }
                await cartDetailsServices.DeleteAllCartDetail(customer.ID);
                return View(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> GetAllOrders(int payMethod, string shipAddress, string id)
        {
            try
            {
                var order = await orderService.GetOrderById(id);
                order.PayMethod = (OrderPayMethod)payMethod;
                order.ShipAddress = shipAddress;
                orderService.UpdateOrder(order);
                return RedirectToAction("GetAllOrders");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
