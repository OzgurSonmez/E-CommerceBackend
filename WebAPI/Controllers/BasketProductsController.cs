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

        [HttpPost("deleteProductToBasket")]
        public async Task<IActionResult> DeleteProductToBasket([FromQuery] int basketId, int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _basketProductRepository.DeleteproductToBasket(basketId, productId);
                return Ok(new { Message = "Delete product to basket successful." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPost("decreaseProductToBasket")]
        public async Task<IActionResult> DecreaseProductToBasket([FromQuery] AddProductToBasketRequest addProductToBasketRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _basketProductRepository.DecreaseProductToBasket(addProductToBasketRequest);
                return Ok(new { Message = "Decrease product to basket successful." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPost("reverseSelectedStatusOfTheProductInBasket")]
        public async Task<IActionResult> ReverseSelectedStatusOfTheProductInBasket([FromQuery] int basketId, int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _basketProductRepository.ReverseSelectedStatusOfTheProductInBasket(basketId, productId);
                return Ok(new { Message = "Reverse selected status of product in basket successful." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
