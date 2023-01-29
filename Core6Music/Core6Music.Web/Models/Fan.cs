using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core6Music.Web.Models
{
    [Table("Fan",Schema ="dbo")]
    public class Fan
    {
        [Key]
        public string MusicUserId { get; set; }
        public MusicUser MusicUser { get; set; }

        public string ArtistId { get; set; }
        public Artist Artist { get; set; }
        
    }
}
