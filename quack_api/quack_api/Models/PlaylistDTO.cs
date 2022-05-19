
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace quack_api.Models
{
    public class PlaylistDTO
    {
        public PlaylistDTO()
        {
            Tracks = new List<TrackDTO>();
        }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("location_type")]
        public int LocationType { get; set; }
        [JsonPropertyName("tracks")]
        public virtual ICollection<TrackDTO> Tracks { get; set; }
        [JsonPropertyName("offset")]
        public int Offset { get; set; }
    }
}
