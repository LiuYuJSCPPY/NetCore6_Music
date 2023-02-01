using Core6Music.Web.Models;

namespace Core6Music.Web.Areas.Dashboard.ViewModels
{
    public class DetailSong
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlbumId { get; set; }
        public Album Album { get; set; }
        public TimeSpan SongTime { get; set; }
        public DateTime CreateDate { get; set; }
        public string ArtistName { get; set; }
        public string Mp3NameFile { get; set; }
    }
}
