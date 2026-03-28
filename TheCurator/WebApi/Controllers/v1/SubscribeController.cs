using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SubscribeController : ControllerBase
    {
        public SubscribeController()
        {}

        [HttpPost]
        public async Task<IActionResult> Subscribe()
        {
            return Ok("Subscribed successfully.");
        }
    }
}
