using Application.Common.Enums;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v{version}/[controller]")]
[ApiVersion("1.0")]
public class ContentsController : ControllerBase
{
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
}
