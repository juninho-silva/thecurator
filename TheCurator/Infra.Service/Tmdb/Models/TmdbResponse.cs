using System.Text.Json.Serialization;

namespace Infra.Service.Tmdb.Models
{
    public record TmdbResponse(
        [property: JsonPropertyName("results")] List<TmdbMovie> Results
    );
}
