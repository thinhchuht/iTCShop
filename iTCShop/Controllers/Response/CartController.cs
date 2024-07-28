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

        public async Task<IActionResult> AddCart([FromBody] string productTypeId)
        {
            var customer = HttpContext.Session.GetObjectFromJson<Customer>("user");
            if (customer == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var response = await cartService.AddToCart(customer.ID, productTypeId);
                return Json(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DropCartDetails(string id)
        {
            var rs = await cartDetailsServices.UpdateDropQuantity(id);
            if (rs.IsSuccess()) return RedirectToAction("CustomerCart");
            return BadRequest(rs);
        }
        [HttpPost]
        public async Task<IActionResult> AddCartDetails(string productTypeId, string cartId) 
        {
            var rs = await cartDetailsServices.AddCartDetail(productTypeId, cartId);
            if (rs.IsSuccess()) return RedirectToAction("CustomerCart");
            return BadRequest(rs);
        }
    }
}
