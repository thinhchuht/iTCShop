namespace iTCShop.Controllers.Response
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductsServices productsServices) : ControllerBase
    {
        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var products = await productsServices.GetAllProducts();
                return Ok(products);
            }
           catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-product-id")]
        public async Task<IActionResult> GetProductById(string id)
        {
            try
            {
                var product = await productsServices.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody]ProductsRequest productsRequest)
        {
            try
            {
                var product = new Product(productsRequest.Name, productsRequest.Price, productsRequest.Description, productsRequest.Size,
                                          productsRequest.Battery, productsRequest.Memory, productsRequest.Color, productsRequest.RAM, productsRequest.IMEI);
                var result = await productsServices.AddProduct(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct([FromBody]string id) 
        {
            try
            {
                var result = await productsServices.DeleteProduct(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody]ProductsRequest productsRequest, string id)
        {
            try
            {
                var product = new Product(productsRequest.Name, productsRequest.Price, productsRequest.Description, productsRequest.Size,
                                         productsRequest.Battery, productsRequest.Memory, productsRequest.Color, productsRequest.RAM, productsRequest.IMEI);
                var result = await productsServices.UpdateProduct(product, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
