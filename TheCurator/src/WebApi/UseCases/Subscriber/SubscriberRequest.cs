using System.Text.Json.Serialization;

namespace WebApi.UseCases.Subscriber
{
    public class SubscriberRequest
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("email")]
        public required string Email { get; set; }
        [JsonPropertyName("frequency")]
        public required string Frequency { get; set; }
        [JsonPropertyName("genres_movies")]
        public required List<string> GenresMovies { get; set; }
        [JsonPropertyName("genres_series")]
        public required List<string> GenresSeries { get; set; }
    }
}
