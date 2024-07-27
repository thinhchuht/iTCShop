using iTCShop.Extensions;

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

        public ActionResult GetCustomerInfo()
        {
            var customer = HttpContext.Session.GetObjectFromJson<Customer>("user");
            if (customer == null) return RedirectToAction("Login", "Login");
            else return View(customer);
        }

        public async Task<IActionResult> Edit()
        {
            var customer = await customerServices.GetCustomerById(HttpContext.Session.GetObjectFromJson<Customer>("user").ID);
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer updatedCustomer)
        {
            var customer = await customerServices.GetCustomerById(HttpContext.Session.GetObjectFromJson<Customer>("user").ID);
            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;
            customer.Password = updatedCustomer.Password;
            customer.Phone = updatedCustomer.Phone;
            customer.Address = updatedCustomer.Address;
            customer.DateOfBirth = updatedCustomer.DateOfBirth;

           var rs = customerServices.UpdateCustomer(customer);
            HttpContext.Session.SetObjectAsJson("user", customer);
            if (rs.IsSuccess()) return RedirectToAction("GetCustomerInfo");
            else
            {
                ViewBag.Fail = "UpdateFailed";
                return View("Edit", customer);
            }
        }
    }
}
