using Application.Commands.NewslatterJob;
using Application.Commands.Subscribe;
using Application.Commands.Unsubscribe;
using Application.Common.Enums;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.UseCases.Subscriber;

namespace WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version}/[controller]")]
    [ApiVersion("1.0")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("subscriber")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> SubscriberAsync([FromBody] SubscriberRequest request)
        {
            var result = await _mediator.Send(new SubscribeCommand(
                request.Name,
                request.Email,
                [.. request.GenresMovies.Select(x => (GenresMovie)x)],
                [.. request.GenresSeries.Select(x => (GenresTV)x)],
                Enum.Parse<DayOfWeek>(request.Frequency, true)));

            if (!result)
            {
                return Conflict(new { message = "E-mail já cadastrado." });
            }

            return Created(string.Empty, new { message = "Inscrito com sucesso!" });
        }

        [HttpGet("unsubscriber")]
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

        
        [ApiKey]
        [HttpGet("newslatter-job")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> InitProcess()
        {
            await _mediator.Send(new NewslatterJobCommand());
            return NoContent();
        }
    }
}
