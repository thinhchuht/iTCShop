using Newtonsoft.Json;

namespace iTCShop.Controllers.Response
{

    public class ProductTypeController(IProductsTypeServices productTypesServices) : Controller
    {
        public async Task<IActionResult> ProductTypePartial()
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

            // Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                productTypes = productTypes.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }


            // Sắp xếp sản phẩm
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

        [Route("add-product")]
        public async Task<IActionResult> AddProductType([FromBody]ProductTypesRequest productTypesRequest)
        {
            try
            {
                var product = new ProductType(productTypesRequest.ID, productTypesRequest.Name, productTypesRequest.Price, productTypesRequest.Description, 
                                          productTypesRequest.Size, productTypesRequest.Battery, productTypesRequest.Memory, productTypesRequest.Color, 
                                          productTypesRequest.RAM, productTypesRequest.Picture);
                var result = await productTypesServices.AddProductType(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProductType([FromBody]string id) 
        {
            try
            {
                var result = await productTypesServices.DeleteProductType(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProductType([FromBody]ProductTypesRequest productTypesRequest)
        {
            try
            {
                var newProduct = new ProductType(productTypesRequest.ID, productTypesRequest.Name, productTypesRequest.Price, productTypesRequest.Description, productTypesRequest.Size,
                                         productTypesRequest.Battery, productTypesRequest.Memory, productTypesRequest.Color, productTypesRequest.RAM, productTypesRequest.Picture);
                var result = await productTypesServices.UpdateProductType(newProduct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
