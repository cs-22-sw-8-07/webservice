
namespace quack_api.Models
{
    public class TrackDTO
    {
        public TrackDTO(Track track)
        {
            Id = track.Id;
            Name = track.Name;
            Artist = track.Artist;
            ImageUrl = track.ImageUrl;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string ImageUrl { get; set; }
    }
}