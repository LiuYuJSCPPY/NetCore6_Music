using Core6Music.Web.DateContext.Enum;
using Core6Music.Web.Models;
using System.ComponentModel;

namespace Core6Music.Web.Areas.Dashboard.ViewModels
{
    public class EditAlbumViewModels
    {
        public string Id { get; set; }

        [DisplayName("名字")]
        public string Name { get; set; }
        
        
        [DisplayName("圖片")]
        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }
        [DisplayName("分類")]
        public AlbumCategory albumCategory { get; set; }
    }
}
