namespace iTCShop.Controllers.Response
{
    public class LoginController(ICustomerServices customerServices, IAdminServices adminServices) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password) 
        {
            var session = HttpContext.Session;
            var customer = await customerServices.CheckCustomerAccount(userName, password);
            if(customer != null)
            {
                if (customer.Status == 0) return View(ResponseModel.FailureResponse("You have been banned due to our Policy!"));
                session.SetObjectAsJson("user", customer);
                return RedirectToAction("HomePage", "Home",ResponseModel.SuccessResponse());
            }
            else
            {
                var admin = await adminServices.CheckAdmin(userName, password);
                if(admin != null)
                {
                    session.SetObjectAsJson("admin", admin);
                    return RedirectToAction("HomeAdmin", "Admin", ResponseModel.SuccessResponse());
                }
                else
                {
                    ViewBag.Fail = "Login failed";
                    return View("Login",ResponseModel.FailureResponse("Invalid Username or Password"));
                }
            }
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("HomePage", "Home");
        }

        public IActionResult SessionInfo()
        {
            var user = HttpContext.Session.GetObjectFromJson<Customer>("user");
            return View(user);
        }
    }
}
