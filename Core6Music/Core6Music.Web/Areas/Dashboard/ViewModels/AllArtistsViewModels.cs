using Core6Music.Web.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Core6Music.Web.Areas.Dashboard.ViewModels
{
    public class AllArtistsViewModels
    {
        public IEnumerable<Artist> Artists { get; set; }

        [DisplayName("名字")]
        public string Name { get; set; }

        [DisplayName("文章")]
        public string Context { get; set; }

        [DisplayName("臉書")]
        public string FaceBook { get; set; }

        [DisplayName("IG")]
        public string Instagram { get; set; }

        [DisplayName("推特")]
        public string Twitter { get; set; }

        [DisplayName("維基")]
        public string Wikipedia { get; set; }

        [DisplayName("圖片")]
        public string ArtistBackImage { get; set; }

        [DisplayName("文章圖")]
        public string ArtistContextImage { get; set; }

        [DisplayName("頭貼")]
        public string ArtistHeadImage { get; set; }
    }
}
