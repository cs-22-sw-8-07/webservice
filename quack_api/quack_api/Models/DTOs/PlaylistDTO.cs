
using System.Collections.Generic;
using System.Linq;

namespace quack_api.Models
{
    public class PlaylistDTO
    {
        public PlaylistDTO(Playlist playlist)
        {
            Id = playlist.Id;
            LocationType = playlist.LocationType;
            Tracks = playlist.Tracks.Select(x => new TrackDTO(x)).ToList();
        }
        public string Id { get; set; }
        public string LocationType { get; set; }
        public virtual ICollection<TrackDTO> Tracks { get; set; }
    }
}
