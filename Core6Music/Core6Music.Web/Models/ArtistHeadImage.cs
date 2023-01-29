using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core6Music.Web.Models
{
    [Table("ArtistHeadImage",Schema ="dbo")]
    public class ArtistHeadImage
    {
        
        public int Id { get; set; }
        public string ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string ImageName { get; set; }

    }
}
