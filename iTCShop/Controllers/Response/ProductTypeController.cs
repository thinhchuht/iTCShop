namespace iTCShop.Controllers.Response
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController(IProductsTypeServices productTypesServices) : ControllerBase
    {
        [HttpGet("get-all-productTypes")]
        public async Task<IActionResult> GetAllProductType()
        {
            try
            {
                var productTypes = await productTypesServices.GetAllProductTypes();
                return Ok(productTypes);
            }
           catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-productType-id")]
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
