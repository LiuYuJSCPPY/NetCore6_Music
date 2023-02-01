using Core6Music.Web.Models;

namespace Core6Music.Web.ViewModels
{
    public class IndexArtistVIewModel
    {
        public IEnumerable<Song> songs { get; set; }
        public IEnumerable<Album> albums { get; set; }
        public Artist artist { get; set; }

    }
}
