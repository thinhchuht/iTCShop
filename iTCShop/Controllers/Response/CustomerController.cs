namespace iTCShop.Controllers.Response
{

    public class CustomerController(ICustomerServices customerServices, ICartService cartService) : Controller
    {
        public ActionResult RegisterCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(CustomerRequest customerRequest)
        {
            try
            {
                var customer = new Customer(customerRequest.Name, customerRequest.Email, customerRequest.UserName, customerRequest.Password, customerRequest.Phone, customerRequest.Address, customerRequest.DateOfBirth);
                cartService.CreateCart(customer.ID);
                var rs = await customerServices.AddCustomer(customer);
                if (rs.IsSuccess())
                {
                    ViewBag.RegRs = "Đăng kí thành công. Đăng nhập ngay";
                    ViewBag.isReg = true;
                    return View("RegisterCustomer");
                }
                else
                {
                    ViewBag.RegRs = "Đăng kí thất bại. Hãy thử lại";
                    return View("RegisterCustomer");
                }
            }
            catch
            {
                return View();
            }

        }
    }
}
