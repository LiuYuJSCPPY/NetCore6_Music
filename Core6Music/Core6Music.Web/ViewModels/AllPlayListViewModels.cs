using Core6Music.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Core6Music.Web.ViewModels
{
    public class AllPlayListViewModels
    {
        public IEnumerable<MusicManifest> musicManifests { get; set; }
        public IEnumerable<FavoriteSong> favoriteSongs { get; set; }
    }
}
