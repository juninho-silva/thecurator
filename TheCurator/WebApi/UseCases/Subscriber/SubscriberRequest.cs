using System.Text.Json.Serialization;

namespace WebApi.UseCases.Subscriber
{
    public class SubscriberRequest
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("email")]
        public required string Email { get; set; }
    }
}
