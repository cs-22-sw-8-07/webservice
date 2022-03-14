

using quack_api.Models;
using System.Text.Json.Serialization;

namespace quack_api.Models
{
    public class RecommenderResponse
    {
        public RecommenderResponse()
        {

        }

        [JsonPropertyName("result")]
        public PlaylistDTO Result { get; set; }
    }
}
