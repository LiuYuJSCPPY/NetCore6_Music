
using Core6Music.Web.DateContext.Enum;
using Microsoft.AspNetCore.Identity;

namespace Core6Music.Web.Models
{
    public class MusicUser : IdentityUser
    {
        public string FullName { get; set; }
        public string MusicName { get; set; }
        public DateTime BirthDay { get; set; }
        public Male male { get; set; }
        public City City { get; set; }


        public ICollection<FavoriteSong> favoriteSongs { get; set; }
        public ICollection<FavoriteArtist> favoriteArtists { get; set; }
        public ICollection<FavoriteAlbum> favoriteAlbums { get; set; }
        public ICollection<MusicManifest> musicManifests { get; set; }

    }
}
