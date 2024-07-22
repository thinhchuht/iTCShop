using Microsoft.AspNetCore.Mvc;

namespace iTCShop.Controllers.Response
{
    public class HomeController : Controller
    { 
  
        public ActionResult HomePage()
        {
            return View();
        }

    
        public ActionResult Contact()
        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }

 
        public ActionResult Products()
        {
            return View();
        }
    }
}