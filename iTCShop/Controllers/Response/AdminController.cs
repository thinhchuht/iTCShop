namespace iTCShop.Controllers.Response
{
    public class AdminController(IProductDbServices productDbServices, IProductsTypeServices productsTypeServices, ICustomerServices customerServices, IOrderService orderService) : Controller
    {

        [Route("AProds")]
        public async Task<IActionResult> HomeAdmin(int page = 1, int pageSize = 10)
        {
          
            var list = new ResponseListsView();
            var productTypes = await productsTypeServices.GetAllProductTypes();
            list.ProductTypes.AddRange(productTypes);
            var products = new List<Product>();
            if (TempData["products"] == null) products = await productDbServices.GetAllProducts();
            else products = TempData.Get<List<Product>>("products");
            var totalProducts = products.Count;
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            var paginatedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            list.Products.AddRange(paginatedProducts);
            ViewBag.Search = TempData["Search"];
            ViewBag.Sort = TempData["Sort"];
            return View(list);
        }

        [Route("ATypes")]
        public async Task<IActionResult> HomeAdminProductType()
        {
            var productTypes = new List<ProductType>();
            if (TempData["productTypes"] == null) productTypes = await productsTypeServices.GetAllProductTypes();
            else productTypes = TempData.Get<List<ProductType>>("productTypes");
            return View(productTypes);
        }

        [Route("ACusts")]
        public async Task<IActionResult> HomeAdminCustomers()
        {
            var customers = new List<Customer>();
            if (TempData["customers"] == null) customers = await customerServices.GetAll();
            else customers = TempData.Get<List<Customer>>("customers");
            ViewBag.Sort = TempData["sort"];
            ViewBag.Search = TempData["search"];
            return View(customers);
        }

        [Route("ARev")]
        public async Task<IActionResult> HomeAdminReport()
        {
            var orders = TempData.Get<List<Order>>("orders");
            orders ??= await orderService.GetAllCompletedOrders();
            var mostSoldProductType = orders
              .SelectMany(o => o.OrderDetails)
              .GroupBy(od => od.Product.ProductTypeId)
              .Select(g => new
              {
                  ProductTypeId = g.Key,
                  TotalQuantity = g.Sum(od => od.Quantity)
              })
              .OrderByDescending(x => x.TotalQuantity);

            TempData.Put<dynamic>("mostSold", mostSoldProductType);
            ViewBag.Sort = TempData["sort"];
            ViewBag.Search = TempData["search"];
            ViewBag.StartDate = TempData.Get<DateTime>("startDate");
            ViewBag.EndDate = TempData.Get<DateTime>("endDate");
             //= TempData["startDate"];
             //= TempData["endDate"];
            return View(orders);
        }

        public IActionResult HomeAdminReports()
        {
            return View();
        }
    }

    public class ResponseListsView
    {
        public List<Product> Products { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public ResponseListsView()
        {
            Products = new List<Product>();
            ProductTypes = new List<ProductType>();
        }
    }
}
