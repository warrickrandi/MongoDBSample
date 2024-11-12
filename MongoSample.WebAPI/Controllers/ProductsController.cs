using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoSample.Application.Services;
using MongoSample.Domain.Entities;

namespace MongoSample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var addedProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductByCode), new { code = addedProduct.ProductCode }, addedProduct);
        }

        [HttpGet("GetProductByCode/{code}")]
        public async Task<IActionResult> GetProductByCode(string code)
        {
            var product = await _productService.GetProductByCodeAsync(code);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPut("UpdateProduct/{code}")]
        public async Task<IActionResult> UpdateProduct(Product updatedProduct)
        {
            await _productService.UpdateProductAsync(updatedProduct);
            return NoContent();
        }

        [HttpDelete("DeleteProduct/{code}")]
        public async Task<IActionResult> DeleteProduct(string code)
        {
            await _productService.DeleteProductAsync(code);
            return NoContent();
        }
    }
}
