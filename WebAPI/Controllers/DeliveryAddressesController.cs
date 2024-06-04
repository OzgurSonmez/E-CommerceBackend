using DataAccess.BasketProduct;
using DataAccess.DeliveryAddress;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryAddressesController : ControllerBase
    {
        private readonly DeliveryAddressRepository _deliveryAddressRepository;

        public DeliveryAddressesController(DeliveryAddressRepository deliveryAddressRepository)
        {
            _deliveryAddressRepository = deliveryAddressRepository;
        }

        [HttpGet("getDeliveryAddressesByCustomerId")]
        public async Task<IActionResult> GetDeliveryAddressesByCustomerId([FromQuery] int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var deliveryAddresses = await _deliveryAddressRepository.GetDeliveryAddressesByCustomerId(customerId);
                return Ok(deliveryAddresses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
