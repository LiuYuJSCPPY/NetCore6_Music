using Core6Music.Web.DateContext.Enum;
using Core6Music.Web.Models;
using System.ComponentModel;

namespace Core6Music.Web.Areas.Dashboard.ViewModels
{
    public class DetailsAlbumViewModels
    {
        [DisplayName("ID")]
        public string Id { get; set; }
        [DisplayName("名字")]
        public string Name { get; set; }
        [DisplayName("藝人")]
        public string ArtistId { get; set; }
        public Artist Artist { get; set; }
        [DisplayName("圖片")]
        public string Image { get; set; }
        [DisplayName("分類")]
        public AlbumCategory albumCategory { get; set; }
        
    }
}
