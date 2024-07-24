using Microsoft.AspNetCore.Mvc;
using iTCShop.Extensions;
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
                session.SetObjectAsJson("user", customer);
                ViewBag.UserName = customer.Name;
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                var admin = await adminServices.CheckAdmin(userName, password);
                if(admin != null)
                {
                    session.SetObjectAsJson("user", admin);
                    return RedirectToAction("RegisterCustomer", "Customer");
                }
                else
                {
                    ViewBag.Fail = "Login failed";
                    return View("Login","Login");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
