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
    public class SubscriberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("genres-movies")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult GetGenresMovies()
        {
            var values = Enum.GetValues<GenresMovie>()
            .Select(g => new
            {
                Id = (int)g,
                Name = g.ToString()
            });

            return Ok(values);
        }

        [HttpGet("genres-tv")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult GetGenresSeries()
        {
            var values = Enum.GetValues<GenresTV>()
            .Select(g => new
            {
                Id = (int)g,
                Name = g.ToString()
            });

            return Ok(values);
        }

        [HttpGet("frequencies-in-days")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult GetFrequencies()
        {
            var values = Enum.GetValues<DayOfWeek>()
            .Select(g => new
            {
                Id = (int)g,
                Name = g.ToString()
            });

            return Ok(values);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> SubscriberAsync([FromBody] SubscriberRequest request)
        {
            var result = await _mediator.Send(new SubscribeCommand(
                request.Name,
                request.Email,
                request.GenresMovies.Select(x => (GenresMovie)x).ToList(),
                request.GenresSeries.Select(x => (GenresTV)x).ToList(),
                Enum.Parse<DayOfWeek>(request.Frequency, true)));

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
