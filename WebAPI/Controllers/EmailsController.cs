using DataAccess.Brand;
using DataAccess.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly EmailRepository _emailRepository;

        public EmailsController(EmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [HttpGet("getEmailIdByEmailAddress")]
        public async Task<IActionResult> GetEmailIdByEmailAddress(string emailAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var email = await _emailRepository.GetEmailIdByEmailAddress(emailAddress);
                return Ok(email);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
