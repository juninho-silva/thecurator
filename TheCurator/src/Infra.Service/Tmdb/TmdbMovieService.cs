using System.Net.Http.Json;
using Infra.Service.Tmdb.Models;
using Microsoft.Extensions.Configuration;

namespace Infra.Service.Tmdb
{
    public sealed class TmdbMovieService(HttpClient http, IConfiguration config) : ITmdbMovieService
    {
        private readonly string _apiKey = config["Tmdb:ApiKey"]!;
        private readonly string _imageBase = config["Tmdb:Url"]!;

        // Mapa de gêneros (TMDB não retorna o nome junto, só o ID)
        private static readonly Dictionary<int, string> Genres = new()
    {
        { 28, "Ação" },       { 12, "Aventura" },  { 16, "Animação" },
        { 35, "Comédia" },    { 80, "Crime" },      { 99, "Documentário" },
        { 18, "Drama" },      { 10751, "Família" }, { 14, "Fantasia" },
        { 36, "História" },   { 27, "Terror" },     { 10402, "Música" },
        { 9648, "Mistério" }, { 10749, "Romance" }, { 878, "Ficção Científica" },
        { 53, "Suspense" },   { 10752, "Guerra" },  { 37, "Faroeste" },
        // Séries
        { 10759, "Ação/Aventura" }, { 10762, "Kids" }, { 10763, "Notícias" },
        { 10764, "Reality" },       { 10765, "Sci-Fi/Fantasia" },
        { 10766, "Novela" },        { 10767, "Talk Show" }, { 10768, "Guerra/Política" },
    };

        public Task<List<MovieCard>> GetTrendingMoviesAsync(int count = 5)
            => FetchTrending("movie", count);

        public Task<List<MovieCard>> GetTrendingSeriesAsync(int count = 5)
            => FetchTrending("tv", count);

        private async Task<List<MovieCard>> FetchTrending(string mediaType, int count)
        {
            var url = $"trending/{mediaType}/week?api_key={_apiKey}&language=pt-BR";
            var response = await http.GetFromJsonAsync<TmdbResponse>(url);

            if (response is null) return [];

            return response.Results
                .Take(count)
                .Select(m => new MovieCard(
                    Title: m.Title,
                    Overview: m.Overview.Length > 200
                                     ? m.Overview[..200] + "..."
                                     : m.Overview,
                    PosterUrl: m.PosterPath is not null
                                     ? _imageBase + m.PosterPath
                                     : string.Empty,
                    ReleaseYear: m.ReleaseDate.Length >= 4
                                     ? m.ReleaseDate[..4]
                                     : "—",
                    Rating: Math.Round(m.VoteAverage, 1),
                    Genres: string.Join(", ", m.GenreIds
                                     .Where(Genres.ContainsKey)
                                     .Select(id => Genres[id])
                                     .Take(3))
                ))
                .ToList();
        }
    }
}
