namespace iTCShop.Controllers.Response
{
    public class AdminController(IProductDbServices productDbServices, IProductsTypeServices productsTypeServices, ICustomerServices customerServices, IOrderService orderService) : Controller
    {

        [Route("AProds")]
        public async Task<IActionResult> HomeAdmin()
        {
            var list = new ResponseListsView();
            var productTypes = await productsTypeServices.GetAllProductTypes();
            list.ProductTypes.AddRange(productTypes);
            var products = new List<Product>();
            if (TempData["products"] == null) products = await productDbServices.GetAllProducts();
            else products = JsonConvert.DeserializeObject<List<Product>>(TempData["products"].ToString());
            list.Products.AddRange(products);
            ViewBag.Search = TempData["Search"];
            ViewBag.Sort = TempData["Sort"];
            return View(list);
        }

        [Route("ATypes")]
        public async Task<IActionResult> HomeAdminProductType()
        {
            var productTypes = new List<ProductType>();
            if (TempData["productTypes"] == null) productTypes = await productsTypeServices.GetAllProductTypes();
            else productTypes = JsonConvert.DeserializeObject<List<ProductType>>(TempData["productTypes"].ToString());
            return View(productTypes);
        }

        [Route("ACusts")]
        public async Task<IActionResult> HomeAdminCustomers()
        {
            var customers = new List<Customer>();
            if (TempData["customers"] == null) customers = await customerServices.GetAll();
            else customers = JsonConvert.DeserializeObject<List<Customer>>(TempData["customers"].ToString());
            ViewBag.Sort = TempData["sort"];
            ViewBag.Search = TempData["search"];
            return View(customers);
        }

        [Route("ARev")]
        public async Task<IActionResult> HomeAdminRevenue()
        {
            var orders = await orderService.GetAllCompletedOrders();
            ViewBag.Sort = TempData["sort"];
            ViewBag.Search = TempData["search"];
            return View(orders);
        }
    }

    public class ResponseListsView
    {
        public List<Product> Products { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public ResponseListsView()
        {
            Products = new List<Product>();
            ProductTypes = new List<ProductType>();
        }
    }
}
