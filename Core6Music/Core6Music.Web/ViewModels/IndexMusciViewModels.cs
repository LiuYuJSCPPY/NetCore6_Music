using Core6Music.Web.Models;


namespace Core6Music.Web.ViewModels
{
    public class IndexMusciViewModels
    {
        public IEnumerable<Album> albums { get; set; }
        public IEnumerable<Artist> artists { get; set; }
    }
}
