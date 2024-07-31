namespace iTCShop.Controllers.Response
{
    public class ProductController(IProductDbServices productDbServices, IOrderDetailServices orderDetailServices) : Controller
    {
        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await productDbServices.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-product")]
        public async Task<IActionResult> GetProductByImei(string imei)
        {
            try
            {
                var product = await productDbServices.GetProductByImei(imei);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductRequest productRequest)
        {
            try
            {
                var result = await productDbServices.AddProduct(productRequest);
                if (result.IsSuccess()) return Redirect("~/AProds");
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string imei)
        {
            try
            {
                var result = await productDbServices.DeleteProduct(imei);
                return RedirectToAction("HomeAdmin","Admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct( ProductRequest productRequest)
        {
            try
            {
                var result = await productDbServices.UpdateProduct(productRequest);
                if (result.IsSuccess()) return Redirect("~/AProds");
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Search(string search, string sort)
        {
            var products = await productDbServices.GetAllProducts();

           if(!string.IsNullOrEmpty(search))
            {
                switch (sort)
                {
                    case "typeID":
                        products = products.Where(p => p.ProductTypeId.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "name":
                        products = products.Where(p => p.ProductType.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "imei":
                        products = products.Where(p => p.IMEI.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                }
            }
            TempData.Put("products", products);
            TempData["Search"] = search;
            TempData["Sort"] = sort;
            return RedirectToAction("HomeAdmin", "Admin");
        }
    }
}
