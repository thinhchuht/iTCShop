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

        [HttpPost("add-product")]
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
