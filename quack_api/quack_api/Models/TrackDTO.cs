using System.Text.Json.Serialization;

namespace quack_api.Models
{
    public class TrackDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("artist")]
        public string Artist { get; set; }
        [JsonPropertyName("image")]
        public string ImageUrl { get; set; }
    }
}