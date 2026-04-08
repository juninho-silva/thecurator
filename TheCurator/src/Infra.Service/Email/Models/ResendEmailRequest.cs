using Infra.Service.Tmdb.Models;

namespace Infra.Service.Email.Models
{
    public record ResendEmailRequest(
        string Name,
        string Email,
        string UnsubscribeToken,
        List<MovieCard> Movies,
        List<MovieCard> Series
    );
}
