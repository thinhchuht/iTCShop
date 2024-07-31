namespace iTCShop.Controllers.Response
{
    public class HomeController(IProductsTypeServices productsTypeServices) : Controller
    { 
  
        public async Task<IActionResult> HomePage()
        {
            var productTypes = new List<ProductType>();
            if (TempData["productTypes"] == null) productTypes = await productsTypeServices.GetAllProductTypes();
            else productTypes = TempData.Get<List<ProductType>>("productTypes");
            ViewBag.Sort = TempData["sort"];
            ViewBag.Search = TempData["search"];
            return View(productTypes);
        }

        public ActionResult Products()
        {
            return View();
        }
    }
}