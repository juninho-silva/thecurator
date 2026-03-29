namespace Infra.Service.Tmdb.Models
{
    public record MovieCard(
        string Title,
        string Overview,
        string PosterUrl,
        string ReleaseYear,
        double Rating,
        string Genres
    );
}
