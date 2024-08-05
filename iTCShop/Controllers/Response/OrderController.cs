namespace iTCShop.Controllers.Response
{
    public class OrderController(IOrderService orderService, IOrderDetailServices orderDetailServices, IProductDbServices productDbServices, ICartDetailsServices cartDetailsServices, ICustomerServices customerServices, IMailService mailService) : Controller
    {
        public async Task<IActionResult> GetAllOrders()
        {
            var customer = HttpContext.Session.GetCustomer();
            var admin = HttpContext.Session.GetAdmin();
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

        public async Task<IActionResult> Search(string search, string sort, string status, DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Search = search;
            ViewBag.Sort = sort;
            ViewBag.Status = status;
            var orders = await orderService.GetAllOrders();
            var ordersomerLst = new List<Order>();

            if (startDate != null && endDate != null)
            {
                orders = orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate && string.Equals(o.Status.ToString(), OrderStatus.Completed.ToString())).ToList();
                TempData.Put("orders", orders);
                TempData.Put("startDate", startDate);
                TempData.Put("endDate", endDate);
                return RedirectToAction("HomeAdminReport", "Admin");
            };

            //all 3 empty
            if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(status) && string.IsNullOrEmpty(sort))
            {

                return View("GetOrdersAdmin", orders);
            }

            //seach and sort empty 
            else if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => string.Equals(o.Status.ToString(), status, StringComparison.OrdinalIgnoreCase)).ToList();
                return View("GetOrdersAdmin", orders);
            }
            else
            {
                //có sort nhưng k có search => lỗi
                if (string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(sort))
                {
                    TempData.Clear();
                    TempData.PutResponse(ResponseModel.FailureResponse("You have to find something with your type"));
                    return View("GetOrdersAdmin", orders);
                }

                //status k empty -> search empty / sort empty / sort va search k empty
                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(o => string.Equals(o.Status.ToString(), status, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                switch (sort)
                {
                    case "customerID":
                        ordersomerLst = orders.Where(p => p.CustomerId.ToString().Trim().ToLower().Contains(search.ToLower().Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "orderID":
                        ordersomerLst = orders.Where(p => p.ID.ToString().Trim().ToLower().Contains(search.ToLower().Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    default:
                        TempData.Clear();
                        TempData.PutResponse(ResponseModel.FailureResponse("Pick type of seach you want to find"));
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
            if (rs.IsSuccess())
            {
                foreach (var item in order.OrderDetails)
                {
                    await productDbServices.UpdateProductStatus(item.ProductID, newStatus);
                }
            }
            orderList.Add(order);
            if(order.Status == OrderStatus.Completed)
            {
                mailService.SendOrderCompletionEmail(order, await customerServices.GetCustomerById(order.CustomerId));
            }
            return RedirectToAction("GetAllOrders");
        }

        [HttpPost]
        public IActionResult Order(List<CartDetailsRequest> cartDetails, decimal total)
        {
            try
            {
                if (cartDetails.Count == 0)
                {
                    TempData.PutResponse(ResponseModel.FailureResponse("You dont have any thing in your cart, buy something first!"));
                    return RedirectToAction("HomePage", "Home");
                }
                TempData.Put("cartDetails", cartDetails);
                var customer = HttpContext.Session.GetCustomer();
                var order = new Order()
                {
                    TotalPay = total,
                    CustomerId = customer.ID
                };
                TempData.Keep();
                return View(order);
            }
            catch
            {
                TempData.PutResponse(ResponseModel.ExceptionResponse());
                return RedirectToAction("HomePage", "Home");

            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAllOrders(int payMethod, string shipAddress, string customerId, decimal totalPay)
        {
            try
            {
                if (payMethod == 2)
                {
                    TempData.PutResponse(ResponseModel.FailureResponse("Bank tranfer has not been supported yet, try a different method"));
                    return RedirectToAction("CustomerCart", "Cart");
                }
                var cartDetails = TempData.Peek<List<CartDetailsRequest>>("cartDetails");
                var order = new Order()
                {
                    TotalPay = totalPay,
                    CustomerId = customerId,
                    PayMethod = (OrderPayMethod)payMethod,
                    ShipAddress = shipAddress,
                };
                await orderService.AddOrder(order);
                foreach (var item in cartDetails)
                {
                    var allProducts = await productDbServices.GetOnStockProductsByProductType(item.ProductTypeID);
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
            catch
            {
                TempData.Put("response", ResponseModel.ExceptionResponse());
                return RedirectToAction("HomePage", "Home");

            }
        }
    }
}
