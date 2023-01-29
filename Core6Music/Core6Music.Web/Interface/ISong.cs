using Core6Music.Web.Models;

namespace Core6Music.Web.Interface
{
    public interface ISong
    {
       
        public bool CresteSong(IFormFile Mp3File);
        public bool DeleteSong(int Id);
        public bool EditSong(int Id);
    }
}
