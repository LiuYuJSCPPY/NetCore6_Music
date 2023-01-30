using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Core6Music.Web.Models
{
    [Table("Song", Schema = "dbo")]
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlbumId { get; set; }
        public Album Album { get; set; }
        public TimeSpan SongTime { get; set; }
        public DateTime CreateDate { get; set; }
        public string ArtistName { get; set; }
        public string Mp3NameFile { get; set; }


        public ICollection<FavoriteSong> favoriteSongs { get; set; }
        public ICollection<SongArtist> songArtists { get; set; }
        public ICollection<MusicManifestSong> musicManifestSongs { get; set; }
    }
}
