using Core6Music.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Core6Music.Web.ViewModels
{
    public class DetailPlayListViewModels
    {
        public MusicManifest MusicManifest { get; set; }
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<MusicManifestSong> MusicManifestSongs { get; set; }
        public string SearchSong { get; set; }
    }
}
