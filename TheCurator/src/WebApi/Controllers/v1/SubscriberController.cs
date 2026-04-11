using Application.Commands.Subscribe;
using Application.Commands.Unsubscribe;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.UseCases.Subscriber;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version}/[controller]")]
    public class SubscriberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> SubscriberAsync([FromBody] SubscriberRequest request)
        {
            var result = await _mediator.Send(new SubscribeCommand(request.Name, request.Email));

            if (!result)
            {
                return Conflict(new { message = "E-mail já cadastrado." });
            }

            return Created(string.Empty, new { message = "Inscrito com sucesso!" });
        }

        [HttpGet("unsubscribe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UnsubscriberAsync([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Token é obrigatório." });
            }

            var result = await _mediator.Send(new UnsubscribeCommand(token));

            if (!result)
            {
                return NotFound(new { message = "Token inválido" });
            }

            return Ok(new { message = "Desinscrito com sucesso!" });
        }
    }
}
