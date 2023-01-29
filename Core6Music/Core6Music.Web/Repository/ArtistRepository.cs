using Core6Music.Web.DateContext;
using Core6Music.Web.Interface;
using Core6Music.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core6Music.Web.Repository
{
    public class ArtistRepository : IArtist
    {
        private readonly MusicDateContext _musicDateContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArtistRepository(MusicDateContext musicDateContext, IWebHostEnvironment webHostEnvironment)
        {
            _musicDateContext = musicDateContext;
            _webHostEnvironment = webHostEnvironment;
        }
        

       

        public bool CreateArtist(Artist artist)
        {
            if (artist == null) return false;
            _musicDateContext.Add(artist);
            return _musicDateContext.SaveChanges() > 0;
        }
     

        public bool DeleteArtist(string Id)
        {
            Artist DeleteArtist = _musicDateContext.Artists.Where(x => x.Id == Id).First();
            if(DeleteArtist == null) return false;
            _musicDateContext.Remove(DeleteArtist);
            return _musicDateContext.SaveChanges() > 0;
        }

        

        public async Task<IEnumerable<Artist>> GetAllArtistAsync()
        {
            return await _musicDateContext.Artists.ToListAsync();
        }

        public async Task<Artist> GetArtistAsync(string Id)
        {
            return await _musicDateContext.Artists.Where(x => x.Id == Id).FirstAsync();
        }

        public async Task<bool> UpateArtist(string Id, Artist artist)
        {
    
            _musicDateContext.Update(artist);
            return _musicDateContext.SaveChanges() > 0;       
        }




        public string SaveImage(IFormFile formFile, string FileCategory)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Image/Artist/{FileCategory}");
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            string SaveFile = Guid.NewGuid().ToString() + "-" + DateTime.Now.ToString("yymmddss") + formFile.FileName;
            string SavePath = Path.Combine(FilePath, SaveFile);
            using (var steam = new FileStream(SavePath, FileMode.Create))
            {
                formFile.CopyTo(steam);
            }

            return SaveFile;

        }

        public async Task<Artist> ASNotGetArtistAsync(string id)
        {
            return await _musicDateContext.Artists.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public void DeleteImage(string Id)
        {
          

            Artist DeleteArtist = _musicDateContext.Artists.FirstOrDefault(x => x.Id == Id);
            if(DeleteArtist.artistBackImages != null )
            {
                ArtistBackImage artistBackImage = DeleteArtist.artistBackImages.FirstOrDefault(x => x.ArtistId == Id);
                string DeletePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/Artist/BackImage", artistBackImage.ImageName);
                if (Directory.Exists(DeletePath))
                {
                    Directory.Delete(DeletePath);
                }
            }
            if (DeleteArtist.artistHeadImages != null )
            {
                ArtistHeadImage artistHeadImage = DeleteArtist.artistHeadImages.FirstOrDefault(x =>x.ArtistId == Id);
                string DeletePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/Artist/ContextImage", artistHeadImage.ImageName);
                if (Directory.Exists(DeletePath))
                {
                    Directory.Delete(DeletePath);
                }
            }
            if (DeleteArtist.artistContextImages != null)
            {
                 ArtistContextImage artistContextImage = DeleteArtist.artistContextImages.FirstOrDefault(x => x.ArtistId == Id);
                string DeletePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/Artist/HeadImage", artistContextImage.ImageName);
                if (Directory.Exists(DeletePath))
                {
                    Directory.Delete(DeletePath);
                }
            }
           
        }
    }
}
