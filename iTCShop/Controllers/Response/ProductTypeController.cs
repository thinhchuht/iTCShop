using Newtonsoft.Json;

namespace iTCShop.Controllers.Response
{

    public class ProductTypeController(IProductsTypeServices productTypesServices) : Controller
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
                        productTypes = productTypes.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                        TempData["productTypes"] = JsonConvert.SerializeObject(productTypes);
                        return RedirectToAction("HomeAdminProductType", "Admin");
                    default :
                        productTypes = productTypes.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
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
            TempData["productTypes"] = JsonConvert.SerializeObject(productTypes);
            return RedirectToAction("HomePage", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> AddProductType(ProductTypesRequest productTypesRequest)
        {
            try
            {
                var product = new ProductType(productTypesRequest.ID, productTypesRequest.Name, productTypesRequest.Price, productTypesRequest.Description,
                                          productTypesRequest.Size, productTypesRequest.Battery, productTypesRequest.Memory, productTypesRequest.Color,
                                          productTypesRequest.RAM, productTypesRequest.Picture);
                var result = await productTypesServices.AddProductType(product);
                if (result.IsSuccess()) return RedirectToAction("HomeAdminProductType", "Admin");
                else return BadRequest(result);
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
                if (!result.IsSuccess()) return BadRequest(result);
               return RedirectToAction("HomeAdminProductType", "Admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductType(ProductTypesRequest productTypesRequest)
        {
            try
            {
                var newProduct = new ProductType(productTypesRequest.ID, productTypesRequest.Name, productTypesRequest.Price, productTypesRequest.Description, productTypesRequest.Size,
                                         productTypesRequest.Battery, productTypesRequest.Memory, productTypesRequest.Color, productTypesRequest.RAM, productTypesRequest.Picture);
                var result = await productTypesServices.UpdateProductType(newProduct);
                if (result.IsSuccess()) return RedirectToAction("HomeAdminProductType", "Admin");
                else return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
