using DataAccess.Brand;
using DataAccess.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly BrandRepository _brandRepository;

        public BrandsController(BrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet("getBrands")]
        public async Task<IActionResult> GetBrands()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var brands = await _brandRepository.GetBrands();
                return Ok(brands);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

    }
}
