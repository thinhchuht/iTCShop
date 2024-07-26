using System.Text.Json;

namespace iTCShop.Controllers.Response
{
    public class AdminController(IProductDbServices productDbServices,IProductsTypeServices productsTypeServices) : Controller
    {
        [Route("AProds")]
        public async Task<IActionResult> HomeAdmin()
        {
            var products = new List<Product>();
            if (TempData["products"] == null) products = await productDbServices.GetAllProducts(); 
            else products = JsonSerializer.Deserialize<List<Product>>(TempData["products"].ToString());
            return View(products);
        }

        [Route("ATypes")]
        public async Task<IActionResult> HomeAdminProductType()
        {
            var products = new List<ProductType>();
            if (TempData["productTypes"] == null) products = await productsTypeServices.GetAllProductTypes();
            else products = JsonSerializer.Deserialize<List<ProductType>>(TempData["productTypes"].ToString());
            return View(products);
        }
    }
}
