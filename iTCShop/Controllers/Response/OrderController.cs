using iTCShop.Extensions;
using Newtonsoft.Json;

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
        public IActionResult Order(List<CartDetailsRequest> cartDetails, decimal total)
        {
            try
            {
                TempData["cartDetails"] = JsonConvert.SerializeObject(cartDetails); 
                var customer = HttpContext.Session.GetObjectFromJson<Customer>("user");
                var order = new Order()
                {
                    TotalPay = total,
                    CustomerId = customer.ID
                };
                TempData.Keep();
                return View(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> GetAllOrders(int payMethod, string shipAddress, string customerId, decimal totalPay)
        {
            try
            {
                var order = new Order()
                {
                    TotalPay = totalPay,
                    CustomerId = customerId,
                    PayMethod = (OrderPayMethod)payMethod,
                    ShipAddress = shipAddress,
                };
                await orderService.AddOrder(order);
                var cartDetails = JsonConvert.DeserializeObject<List<CartDetailsRequest>>(TempData.Peek("cartDetails").ToString());
                foreach (var item in cartDetails)
                {
                    var allProducts = await productDbServices.GetProductsByProductType(item.ProductTypeID);
                    var products = allProducts.Take(item.Quantity).ToList();
                    foreach (var p in products)
                    {
                        var orderDetail = new OrderDetail(1, p.ProductType.Price, p.IMEI, order.ID);
                        await orderDetailServices.AddOrderDetail(orderDetail);
                        order.OrderDetails.Add(orderDetail);
                        await productDbServices.AddProductToOrder(p.IMEI);
                    }
                }
                await cartDetailsServices.DeleteAllCartDetail(customerId);
                return RedirectToAction("GetAllOrders");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
