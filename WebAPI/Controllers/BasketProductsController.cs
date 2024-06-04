using DataAccess.BasketProduct;
using DataAccess.Product;
using Entity.DTOs.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketProductsController : ControllerBase
    {
        private readonly BasketProductRepository _basketProductRepository;

        public BasketProductsController(BasketProductRepository basketProductRepository)
        {
            _basketProductRepository = basketProductRepository;
        }

        [HttpGet("getBasketProductByCustomerId")]
        public async Task<IActionResult> GetBasketProductByCustomerId([FromQuery] int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var basketProducts = await _basketProductRepository.GetBasketProductByCustomerId(customerId);
                return Ok(basketProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
