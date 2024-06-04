using DataAccess;
using DataAccess.Auth;
using DataAccess.Product;
using Entity.DTOs.Auth;
using Entity.DTOs.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("getFilteredProducts")]
        public async Task<IActionResult> GetFilteredProducts([FromQuery] FilteredProductRequestDto filteredProductRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var products = await _productRepository.GetFilteredProducts(filteredProductRequestDto);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

    }
}