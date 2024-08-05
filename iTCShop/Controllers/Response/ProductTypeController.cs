using Microsoft.IdentityModel.Tokens;

namespace iTCShop.Controllers.Response
{
    public class ProductTypeController(IProductsTypeServices productTypesServices, IProductDbServices productDbServices) : Controller
    {
        public async Task<IActionResult> ProductPartial()
        {
            var productTypes = await productTypesServices.GetAllProductTypes();
            return PartialView(productTypes);
        }

        public async Task<IActionResult> GetProductTypeById(string id)
        {
            try
            {
                var product = await productTypesServices.GetProductTypeById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Search(string search, string sort)
        {
            var productTypes = await productTypesServices.GetAllProductTypes();

            if (!string.IsNullOrEmpty(search))
            {
                switch (sort)
                {
                    case "typeID":
                        productTypes = productTypes.Where(p => p.ID.Contains(search.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                        TempData.Put("productTypes", productTypes);
                        return RedirectToAction("HomeAdminProductType", "Admin");
                    default:
                        productTypes = productTypes.Where(p => p.Name.Contains(search.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                }
            }
            switch (sort)
            {
                case "nameAZ":
                    productTypes = productTypes.OrderBy(p => p.Name).ToList();
                    break;
                case "nameZA":
                    productTypes = productTypes.OrderByDescending(p => p.Name).ToList();
                    break;
                case "priceDes":
                    productTypes = productTypes.OrderByDescending(p => p.Price).ToList();
                    break;
                case "priceAcs":
                    productTypes = productTypes.OrderBy(p => p.Price).ToList();
                    break;
            }
            TempData.Put("productTypes", productTypes);
            TempData["Search"] = search;
            TempData["Sort"] = sort;
            return RedirectToAction("HomePage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddProductType(ProductTypesRequest productTypesRequest)
        {
            try
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }
                var filePath = Path.Combine(uploadsDir, productTypesRequest.Picture.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productTypesRequest.Picture.CopyToAsync(stream);
                }
                var product = new ProductType(productTypesRequest.Name, productTypesRequest.Price, productTypesRequest.Description,
                                          productTypesRequest.Size, productTypesRequest.Battery, productTypesRequest.Memory, productTypesRequest.Color,
                                          productTypesRequest.RAM, productTypesRequest.Picture.FileName);
                var result = await productTypesServices.AddProductType(product);
                if (!result.IsSuccess()) TempData.Put("response", result);
                TempData.Keep();
                return RedirectToAction("HomeAdminProductType", "Admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductType(string id)
        {
            try
            {
                var result = await productTypesServices.DeleteProductType(id);
                if (!result.IsSuccess()) TempData.PutResponse(result);
                return RedirectToAction("HomeAdminProductType", "Admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductType(ProductTypesRequest productTypesRequest, string id)
        {
            var product = await productDbServices.GetProductsByProductType(id);
            if(!product.IsNullOrEmpty())
            {
                TempData.PutResponse(ResponseModel.FailureResponse("This product is already on the market, you can not apply change!"));
                return RedirectToAction("HomeAdminProductType", "Admin");
            }
            var rs = await productTypesServices.DeleteProductType(id);
            if (!rs.IsSuccess())
            {
                TempData.PutResponse(rs);
                return RedirectToAction("HomeAdminProductType", "Admin");
            }
            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }
            var filePath = Path.Combine(uploadsDir, productTypesRequest.Picture.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productTypesRequest.Picture.CopyToAsync(stream);
            }
            var newProduct = new ProductType(productTypesRequest.Name, productTypesRequest.Price, productTypesRequest.Description, productTypesRequest.Size,
                                     productTypesRequest.Battery, productTypesRequest.Memory, productTypesRequest.Color, productTypesRequest.RAM, productTypesRequest.Picture.FileName);
            var result = await productTypesServices.AddProductType(newProduct);
            if (!result.IsSuccess()) TempData.PutResponse(rs);
            return RedirectToAction("HomeAdminProductType", "Admin");
        }
    }
}

