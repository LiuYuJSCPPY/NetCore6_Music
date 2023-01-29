using Microsoft.AspNetCore.Mvc;
using Core6Music.Web.Models;

using Core6Music.Web.Models;
namespace Core6Music.Web.Interface
{
    public interface IArtist
    {
        Task<IEnumerable<Artist>> GetAllArtistAsync();
        Task<Artist> GetArtistAsync(string Id);
        bool CreateArtist(Artist artist);
        Task<bool> UpateArtist(string Id, Artist artist);
        bool DeleteArtist(string Id);

        string SaveImage(IFormFile formFile, string FileCategory);

        void DeleteImage(string Id);

    }
}
