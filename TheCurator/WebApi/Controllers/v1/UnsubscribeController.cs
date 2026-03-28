using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UnsubscribeController : ControllerBase
    {
        public UnsubscribeController() { }

        [HttpGet]
        public async Task<IActionResult> Unsubscribe([FromQuery] StringTokenizer? token)
        {
            if (token is null)
            {
                return BadRequest("Token is required.");
            }

            return Ok("Unsubscribed successfully.");
        }
    }
}
