using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Core6Music.Web.Models
{
    [Table("FavoriteSong",Schema ="dbo")]
    public class FavoriteSong
    {
        [Key]
        public string MusicUserId { get; set; }
        public MusicUser MusicUser { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
