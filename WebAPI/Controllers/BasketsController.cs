using DataAccess.Basket;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly BasketRepository _basketRepository;

        public BasketsController(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("getBasketIdByCustomerId")]
        public async Task<IActionResult> GetBasketIdByCustomerId(int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var basketId = await _basketRepository.GetBasketIdByCustomerId(customerId);
                return Ok(basketId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
