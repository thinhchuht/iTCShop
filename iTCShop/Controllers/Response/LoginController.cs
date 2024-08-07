namespace iTCShop.Controllers.Response
{
    public class LoginController(ICustomerServices customerServices, IAdminServices adminServices) : Controller
    {

        public IActionResult Login()
        {
            if (HttpContext.Session.GetCustomer() != null || HttpContext.Session.GetAdmin() != null)
            {
                HttpContext.Session.Clear();
                TempData.PutResponse(ResponseModel.FailureResponse("You have been logged out of the current account."));

            }
           
            return View();
        }


        public async Task<IActionResult> ForgotPassword(string forgotPasswordUserName, string forgotPasswordEmail, string newPassword)
        {
            var rs = await customerServices.UpdateForgetPassword(forgotPasswordUserName, forgotPasswordEmail, newPassword);
            TempData.PutResponse(rs);
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var session = HttpContext.Session;
            var customer = await customerServices.CheckCustomerAccount(userName, password);
            if (customer != null)
            {
                if (customer.Status == 0) return View(ResponseModel.FailureResponse("You have been banned due to our Policy!"));
                session.SetObjectAsJson("user", customer);
                TempData.PutResponse(ResponseModel.SuccessResponse());
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                var admin = await adminServices.CheckAdmin(userName, password);
                if (admin != null)
                {
                    session.SetObjectAsJson("admin", admin);
                    return RedirectToAction("HomeAdmin", "Admin", ResponseModel.SuccessResponse());
                }
                else
                {
                    ViewBag.Fail = "Login failed";
                    TempData.PutResponse(ResponseModel.FailureResponse("Invalid Username or Password"));
                    return View("Login");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassAdmin(string currentPassword, string confirmPassword, string newPassword)
        {
            if(newPassword != confirmPassword)
            {
                TempData.PutResponse(ResponseModel.FailureResponse("You have to confirm the right password"));
                return RedirectToAction("HomeAdmin", "Admin");
            }
            var admin = HttpContext.Session.GetAdmin();
            if(admin.Password != currentPassword)
            {
                TempData.PutResponse(ResponseModel.FailureResponse("You have entered wrong password"));
                return RedirectToAction("HomeAdmin", "Admin");
            }
            if(admin.Password == newPassword) 
            {
                TempData.PutResponse(ResponseModel.FailureResponse("You can not enter the same password"));
                return RedirectToAction("HomeAdmin", "Admin");
            }
            admin.Password = newPassword;
            var rs = await adminServices.UpdateAdmin(admin);
            if(!rs.IsSuccess())
            {
                TempData.PutResponse(rs);
            }
            HttpContext.Session.Clear();
            HttpContext.Session.SetObjectAsJson("admin", admin);
            return RedirectToAction("HomeAdmin", "Admin");

        }
        [HttpPost]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("HomePage", "Home");
        }

        public IActionResult SessionInfo()
        {
            var user = HttpContext.Session.GetCustomer;
            return View(user);
        }
    }
}
