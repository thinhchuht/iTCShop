using OfficeOpenXml;
using System.Text;

namespace iTCShop.Controllers.Response
{
    public class ProductController(IProductDbServices productDbServices) : Controller
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
        public async Task<IActionResult> ImportExcel (IFormFile importFile)
        {
            var products = new List<ProductRequest>();

            using (var stream = new MemoryStream())
            {
                await importFile.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var product = new ProductRequest
                        {
                            Imei = worksheet.Cells[row, 1].Value.ToString(),
                            ProductTypeId = worksheet.Cells[row, 2].Value.ToString(),
                        };
                        products.Add(product);
                    }
                }
            }
            var str = new StringBuilder();
            foreach (var product in products)
            {
               var rs = await productDbServices.AddProduct(product);
                if (!rs.IsSuccess()) str.AppendLine($"{product.Imei} with type {product.ProductTypeId} due to: {rs.Message}");
            }
            if(str.Length > 0) TempData.PutResponse(ResponseModel.FailureResponse($"Cannot add these products:\n{str}"));
            return RedirectToAction("HomeAdmin", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductRequest productRequest)
        {
            try
            {
                var result = await productDbServices.AddProduct(productRequest);
                if (!result.IsSuccess()) TempData.Put("response", result);
                TempData.Keep();
                return Redirect("~/AProds");
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
                return RedirectToAction("HomeAdmin", "Admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductRequest productRequest)
        {
                var result = await productDbServices.UpdateProduct(productRequest);
                if (!result.IsSuccess()) TempData.PutResponse(result);
                    return Redirect("~/AProds");
        }

        public async Task<IActionResult> Search(string search, string sort, string status)
        {
            var products = await productDbServices.GetAllProducts();
          
            if (!string.IsNullOrEmpty(status))
            {
                products = products.Where(o => string.Equals(o.Status.ToString(), status, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            
            //search trống , sort còn -> báo lỗi
            if (string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(sort))
            {
                TempData.Clear();
                TempData.PutResponse(ResponseModel.FailureResponse("Fill out your search bar with the type of search!"));
            }

            //search có , sort trống -> lỗi
            if (!string.IsNullOrEmpty(search) && string.IsNullOrEmpty(sort))
            {
                {
                    TempData.Clear();
                    TempData.PutResponse(ResponseModel.FailureResponse("Select your type of search!"));
                }
            }

            if (!string.IsNullOrEmpty(search))
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
            if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(sort))
            {
                TempData.Clear();
            }
            TempData.Put("products", products);
            TempData["Search"] = search;
            TempData["Sort"] = sort;
            return RedirectToAction("HomeAdmin", "Admin");
        }
    }
}
