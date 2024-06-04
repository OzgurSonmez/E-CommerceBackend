using DataAccess.CustomerOrder;
using DataAccess.CustomerOrderDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderDetailsController : ControllerBase
    {

        private readonly CustomerOrderDetailRepository _customerOrderDetailRepository;

        public CustomerOrderDetailsController(CustomerOrderDetailRepository customerOrderDetailRepository)
        {
            _customerOrderDetailRepository = customerOrderDetailRepository;
        }

        [HttpGet("getCustomerOrderByCustomerId")]
        public async Task<IActionResult> GetCustomerOrderByCustomerId(int customerOrderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customerOrderDetails = await _customerOrderDetailRepository.getCustomerOrderDetailByCustomerOrderId(customerOrderId);
                return Ok(customerOrderDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
