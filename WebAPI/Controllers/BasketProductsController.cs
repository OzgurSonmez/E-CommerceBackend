using DataAccess.BasketProduct;
using DataAccess.Product;
using Entity.DTOs.BasketProduct;
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

        [HttpPost("addProductToBasket")]
        public async Task<IActionResult> AddProductToBasket([FromQuery] AddProductToBasketRequest addProductToBasketRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _basketProductRepository.AddProductToBasket(addProductToBasketRequest);
                return Ok(new { Message = "Add product to basket successful." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
