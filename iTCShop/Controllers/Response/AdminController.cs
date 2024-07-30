using System.Text.Json;

namespace iTCShop.Controllers.Response
{
    public class AdminController(IProductDbServices productDbServices, IProductsTypeServices productsTypeServices, ICustomerServices customerServices) : Controller
    {

        [Route("AProds")]
        public async Task<IActionResult> HomeAdmin()
        {
            var list = new ResponseListsView();
            var productTypes = await productsTypeServices.GetAllProductTypes();
            list.ProductTypes.AddRange(productTypes);
            var products = new List<Product>();
            if (TempData["products"] == null) products = await productDbServices.GetAllProducts();
            else products = JsonSerializer.Deserialize<List<Product>>(TempData["products"].ToString());
            list.Products.AddRange(products);
            return View(list);
        }

        [Route("ATypes")]
        public async Task<IActionResult> HomeAdminProductType()
        {
            var productTypes = new List<ProductType>();
            if (TempData["productTypes"] == null) productTypes = await productsTypeServices.GetAllProductTypes();
            else productTypes = JsonSerializer.Deserialize<List<ProductType>>(TempData["productTypes"].ToString());
            return View(productTypes);
        }

        [Route("ACusts")]
        public async Task<IActionResult> HomeAdminCustomers()
        {
            var customers = new List<Customer>();
            if (TempData["customers"] == null) customers = await customerServices.GetAll();
            else customers = JsonSerializer.Deserialize<List<Customer>>(TempData["customers"].ToString());
            return View(customers);
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
