using DataAccess.Auth;
using DataAccess.OrderManagement;
using Entity.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementsController : ControllerBase
    {
        private readonly OrderManagementRepository _orderManagementRepository;

        public OrderManagementsController(OrderManagementRepository orderManagementRepository)
        {
            _orderManagementRepository = orderManagementRepository;
        }

        [HttpPost("completeOrder")]
        public async Task<IActionResult> CompleteOrder(int customerId, int deliveryAddressId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderManagementRepository.CompleteOrder(customerId, deliveryAddressId);
                return Ok(new { Message = "Order successful." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
