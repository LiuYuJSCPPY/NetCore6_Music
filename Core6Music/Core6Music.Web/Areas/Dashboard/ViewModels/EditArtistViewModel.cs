using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Core6Music.Web.Areas.Dashboard.ViewModels
{
    public class EditArtistViewModel
    {
        public string Id { get; set; }
        [DisplayName("名字")]
        public string Name { get; set; }

        [DisplayName("文章")]
        public string Context { get; set; }

        [DisplayName("臉書")]
        public string? FaceBook { get; set; }

        [DisplayName("IG")]
        public string? Instagram { get; set; }

        [DisplayName("推特")]
        public string? Twitter { get; set; }

        [DisplayName("維基")]
        public string? Wikipedia { get; set; }
        public string? BackImageName { get; set; }
        public string? HeadImageName { get; set; }
        public string? ContextImageName { get; set; }
        public IFormFile? ArtistBackImage { get; set; }
        public IFormFile? ArtistContextImage { get; set; }
        public IFormFile? ArtistHeadImage { get; set; }
    }
}
