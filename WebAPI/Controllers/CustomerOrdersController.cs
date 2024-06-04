using DataAccess.BasketProduct;
using DataAccess.CustomerOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly CustomerOrderRepository _customerOrderRepository;

        public CustomerOrdersController(CustomerOrderRepository customerOrderRepository)
        {
            _customerOrderRepository = customerOrderRepository;
        }

        [HttpGet("getCustomerOrderByCustomerId")]
        public async Task<IActionResult> GetCustomerOrderByCustomerId(int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customerOrders = await _customerOrderRepository.GetCustomerOrderByCustomerId(customerId);
                return Ok(customerOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
