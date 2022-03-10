
using System.Collections.Generic;

namespace quack_api.Models
{
    public class Playlist
    {
        public Playlist()
        {
            Tracks = new List<Track>();
        }
        public string Id { get; set; }
        public string LocationType { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
