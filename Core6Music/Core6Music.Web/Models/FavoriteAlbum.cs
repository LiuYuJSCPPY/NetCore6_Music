using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Core6Music.Web.Models
{
    [Table("FavoriteAlbum",Schema ="dbo")]
    public class FavoriteAlbum
    {
        public int Id { get; set; }
        public string MusicUserId { get; set; }
        public MusicUser MusicUser { get; set; }
        public string AlbumId { get;set; }
        public Album Album { get; set; }
    }
}
