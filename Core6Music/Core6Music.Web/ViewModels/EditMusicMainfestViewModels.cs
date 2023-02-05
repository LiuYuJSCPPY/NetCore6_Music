using Core6Music.Web.Models;

namespace Core6Music.Web.ViewModels
{
    public class EditMusicMainfestViewModels
    {

        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? Context { get; set; }
    }
}
