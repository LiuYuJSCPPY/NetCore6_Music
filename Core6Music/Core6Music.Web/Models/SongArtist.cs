using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core6Music.Web.Models
{
    [Table("SongArtist",Schema ="dbo")]
    public class SongArtist
    {
        public int Id { get; set; }
        public int? SongId { get; set; }
        public Song Song { get; set; }

        public string ArtistId { get; set; }
        public Artist Artist { get; set; }

    }
}
