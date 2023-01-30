using Core6Music.Web.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core6Music.Web.Areas.Dashboard.ViewModels
{
    public class SongViewModel
    {
        public Album album { get; set; }
        [DisplayName("MP3")]
        public IFormFile Mp3File { get; set; }

    }
}
