using Core6Music.Web.Models;
using System.Drawing;

namespace Core6Music.Web.Areas.Dashboard.ViewModels
{
    public class AllSongViewModels
    {
        public IEnumerable<Song> songs { get; set; }
        public string AlbumId { get; set; }
        public string Name { get; set; }
        public string Album { get; set; }
        public string SongTime { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
