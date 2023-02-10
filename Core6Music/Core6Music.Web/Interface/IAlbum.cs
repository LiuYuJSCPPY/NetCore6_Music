using Core6Music.Web.Models;

namespace Core6Music.Web.Interface
{
    public interface IAlbum
    {
        bool CreateAlbum(Album album);
        bool EditAlbum(string Id,Album album);
        bool DeleteAlbum(string Id);
        Task<IList<Album>> GetAllAlbum(string ArtistId);
        Task<Album> GetAlbum(string Id);
        string SaveImage(IFormFile formFile);
        void DeleteImage(string DeleteImage);
    }
}
