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

        public async Task<IActionResult> Search(string search, string sort, string status)
        {
            ViewBag.Search = search;
            ViewBag.Sort = sort;
            ViewBag.Status = status;
            var orders = await orderService.GetAllOrders();
            var ordersomerLst = new List<Order>();
            if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(status))
            {
               
                return View("GetOrdersAdmin", orders);
            }
            else if (string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => string.Equals(o.Status.ToString(), status, StringComparison.OrdinalIgnoreCase)).ToList();
                return View("GetOrdersAdmin", orders);
            }
            else
            {
                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(o => o.Status.Equals(status)).ToList();
                }
                switch (sort)
                {
                    case "customerID":
                        ordersomerLst = orders.Where(p => p.CustomerId.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "ID":
                        ordersomerLst = orders.Where(p => p.ID.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    default:
                        return View("GetOrdersAdmin", orders);
                }
            }
            return View("GetOrdersAdmin", ordersomerLst); 
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int newStatus, string orderId)
        {
            var orderList = new List<Order>();
            var order = await orderService.GetOrderById(orderId);
            order.Status = (OrderStatus)newStatus;
           var rs = orderService.UpdateOrder(order);
            if(rs.IsSuccess())
            { 
                foreach(var item in order.OrderDetails)
                {
                  await productDbServices.UpdateProductStatus(item.ProductID, newStatus);
                }
            }
            orderList.Add(order);
            return View("GetOrdersAdmin", orderList);
        }
        [HttpPost]
        public IActionResult Order(List<CartDetailsRequest> cartDetails, decimal total)
        {
            try
            {
                if (cartDetails.Count == 0) return RedirectToAction("HomePage", "Home");
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
