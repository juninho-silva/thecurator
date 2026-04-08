using Infra.Service.Tmdb.Models;

namespace Infra.Service.Tmdb
{
    public interface ITmdbMovieService
    {
        Task<List<MovieCard>> GetTrendingMoviesAsync(int count = 5);
        Task<List<MovieCard>> GetTrendingSeriesAsync(int count = 5);
    }
}
