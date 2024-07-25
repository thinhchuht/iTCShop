using iTCShop.Extensions;

namespace iTCShop.Controllers.Response
{
    public class CartController(ICartService cartService, ICartDetailsServices cartDetailsServices) : Controller
    {
        public async Task<IActionResult> CustomerCart()
        {
            var customer = HttpContext.Session.GetObjectFromJson<Customer>("user");
            if (customer == null)
            {
                return RedirectToAction("Login", "Login");
            } 
            else
            {
                var cartDetails = await cartDetailsServices.GetAllByCartId(customer.ID);
                return View(cartDetails);
            }
         
        }

       
        public async Task<IActionResult> AddCart([FromBody] string productType)
        {
            var customer = HttpContext.Session.GetObjectFromJson<Customer>("user");
            if (customer == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var response = await cartService.AddToCart(customer.ID, productType);
                return Json(response);
            }
          
        }
    }
}
