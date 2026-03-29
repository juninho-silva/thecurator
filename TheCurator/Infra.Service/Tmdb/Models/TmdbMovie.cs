using System.Text.Json.Serialization;

namespace Infra.Service.Tmdb.Models
{
    public record TmdbMovie(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("overview")] string Overview,
    [property: JsonPropertyName("poster_path")] string PosterPath,
    [property: JsonPropertyName("vote_average")] double VoteAverage,
    [property: JsonPropertyName("release_date")] string ReleaseDate,
    [property: JsonPropertyName("genre_ids")] List<int> GenreIds
    );
}
