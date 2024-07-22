using Microsoft.AspNetCore.Mvc;

namespace iTCShop.Controllers.Response
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
