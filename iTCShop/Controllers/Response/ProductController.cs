using iTCShop.Controllers.Request;
using iTCShop.Services.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Threading;

namespace iTCShop.Controllers.Response
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("get-product-id")]
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

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest productRequest)
        {
            try
            {
                var result = await productDbServices.AddProduct(productRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct([FromBody] string imei)
        {
            try
            {
                var result = await productDbServices.DeleteProduct(imei);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductRequest productRequest)
        {
            try
            {
                var result = await productDbServices.UpdateProduct(productRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
