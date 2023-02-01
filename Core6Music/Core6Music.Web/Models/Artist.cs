using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core6Music.Web.Models
{
    [Table("Artist",Schema ="dbo")]
    public class Artist
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }
        public string? FaceBook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? Wikipedia { get; set; }

       


        public virtual ICollection<SongArtist> songArtists { get; set; }
        public virtual ICollection<Album> albums { get; set; }
        public virtual ICollection<Fan> fans { get; set; }
        public virtual ICollection<FavoriteArtist> favoriteArtists { get; set; }
        public virtual ArtistBackImage artistBackImages { get; set; }
        public virtual ArtistContextImage artistContextImages { get; set; }
        public virtual ArtistHeadImage artistHeadImages { get; set; }
    }
}
