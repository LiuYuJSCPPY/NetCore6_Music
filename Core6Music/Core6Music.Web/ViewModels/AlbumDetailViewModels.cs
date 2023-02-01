using Core6Music.Web.Models;

namespace Core6Music.Web.ViewModels
{
    public class AlbumDetailViewModels
    {
        public IEnumerable<Album> albums { get; set; }
        public Album album { get; set; }
        public IEnumerable<Artist> artists { get; set; }
    }
}
