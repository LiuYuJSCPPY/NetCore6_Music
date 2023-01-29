using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core6Music.Web.Models
{
    [Table("MusicManifest",Schema ="dbo")]
    public class MusicManifest
    {
        public int Id { get; set; }
        public string MusicUserId { get; set; }
        public MusicUser MusicUser { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Context { get; set; }

        public ICollection<MusicManifestSong> musicManifestSongs { get; set; }
    }
}
