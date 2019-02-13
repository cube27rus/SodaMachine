using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace SodaMachine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{secret}")]
        public async Task<IActionResult> CheckSecret([FromRoute] string secret)
        {
            var adminSecret = _configuration.GetSection("SecretKey").Value;

            if (adminSecret != secret)
            {
                return BadRequest();
            }
            
            return Ok();
        }
    }
}