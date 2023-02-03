using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core6Music.Web.Models
{
    [Table("MusicManifestSong", Schema = "dbo")]
    public class MusicManifestSong
    {
        
        public int Id { get; set; }
        public int MusicManifestId { get; set; }
        public MusicManifest MusicManifest { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
