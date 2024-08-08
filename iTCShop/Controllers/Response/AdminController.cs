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
            //var totalProducts = products.Count;
            //var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            //var paginatedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //list.Products.AddRange(paginatedProducts);
            var items = PaginateList.Paginate(products, page, pageSize);
            list.Products.AddRange(items.List);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = items.TotalPages;
            ViewBag.Search = TempData["Search"];
            ViewBag.Sort = TempData["Sort"];
            return View(list); ;
        }

        [Route("ATypes")]
        public async Task<IActionResult> HomeAdminProductType(int page = 1, int pageSize = 10)
        {
            var productTypes = new List<ProductType>();
            if (TempData["productTypes"] == null) productTypes = await productsTypeServices.GetAllProductTypes();
            else productTypes = TempData.Get<List<ProductType>>("productTypes");
            var items = PaginateList.Paginate(productTypes, page, pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = items.TotalPages;
            return View(items.List);
        }

        [Route("ACusts")]
        public async Task<IActionResult> HomeAdminCustomers(int page = 1, int pageSize = 10)
        {
            var customers = new List<Customer>();
            if (TempData["customers"] == null) customers = await customerServices.GetAll();
            else customers = TempData.Get<List<Customer>>("customers");
            ViewBag.Sort = TempData["sort"];
            ViewBag.Search = TempData["search"];
            //var totalProducts = customers.Count;
            //var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            //var paginatedCusts = customers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var items = PaginateList.Paginate(customers, page, pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = items.TotalPages;
            return View(items.List) ;
        }

        [Route("ARev")]
        public async Task<IActionResult> HomeAdminReport(int page = 1, int pageSize = 10)
        {
            var orders = TempData.Get<List<Order>>("orders");
            orders ??= await orderService.GetAllCompletedOrders();

           var items = PaginateList.Paginate(orders, page, pageSize);
            var mostSoldProductType = items.List
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
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = items.TotalPages;
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

        public ResponseListsView()
        {
            Products = new List<Product>();
            ProductTypes = new List<ProductType>();
        }
    }
}
