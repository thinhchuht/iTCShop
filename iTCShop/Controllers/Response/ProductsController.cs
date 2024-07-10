using iTCShop.Controllers.Request;
using iTCShop.Models;
using iTCShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iTCShop.Controllers.Response
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductsServices productsServices) : ControllerBase
    {
        [HttpGet("all-products")]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await productsServices.GetAllProducts();
            return Ok(products);
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody]ProductsRequest productsRequest)
        {
           var product = new Product(productsRequest.Name, productsRequest.Price, productsRequest.Description, productsRequest.Size,
               productsRequest.Battery, productsRequest.Memory, productsRequest.Color, productsRequest.RAM, productsRequest.IMEI);
            var result = await productsServices.AddProduct(product);
            return Ok(result);
        }
    }
}
