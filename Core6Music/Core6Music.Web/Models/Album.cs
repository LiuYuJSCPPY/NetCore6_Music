using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Core6Music.Web.DateContext.Enum;

namespace Core6Music.Web.Models
{
    [Table("Album",Schema ="dbo")]
    public class Album
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string Image { get; set; }
        public AlbumCategory albumCategory { get; set; }

        public ICollection<Song> songs { get; set; }
        public ICollection<FavoriteAlbum> favoriteAlbums { get; set; }

        public ICollection<MusicManifestSong> musicManifestSongs { get; set; }
    }
}
