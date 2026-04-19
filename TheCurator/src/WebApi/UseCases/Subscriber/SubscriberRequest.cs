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
        public required List<int> GenresMovies { get; set; }
        [JsonPropertyName("genres_series")]
        public required List<int> GenresSeries { get; set; }
    }
}
