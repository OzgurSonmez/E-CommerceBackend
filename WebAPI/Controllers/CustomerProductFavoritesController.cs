using DataAccess.Auth;
using DataAccess.BasketProduct;
using DataAccess.CustomerProductFavorite;
using Entity.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProductFavoritesController : ControllerBase
    {
        private readonly CustomerProductFavoriteRepository _customerProductFavoriteRepository;

        public CustomerProductFavoritesController(CustomerProductFavoriteRepository customerProductFavoriteRepository)
        {
            _customerProductFavoriteRepository = customerProductFavoriteRepository;
        }

        [HttpPost("addProductToFavorite")]
        public async Task<IActionResult> AddProductToFavorite(int customerId, int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _customerProductFavoriteRepository.AddProductToFavorite(customerId, productId);
                return Ok(new { Message = "Add product to favorite successful." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPost("removeProductFromFavorite")]
        public async Task<IActionResult> RemoveProductFromFavorite(int customerId, int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _customerProductFavoriteRepository.RemoveProductFromFavorite(customerId, productId);
                return Ok(new { Message = "Remove product to favorite successful." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("getCustomerProductFavorite")]
        public async Task<IActionResult> GetCustomerProductFavorite(int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customerProductsFavorite = await _customerProductFavoriteRepository.GetCustomerProductFavorite(customerId);
                return Ok(customerProductsFavorite);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
