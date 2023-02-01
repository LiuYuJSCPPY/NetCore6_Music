using Core6Music.Web.DateContext;
using Core6Music.Web.Interface;
using Core6Music.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;


namespace Core6Music.Web.Repository
{
    public class AlbumRepository : IAlbum
    {
        private readonly MusicDateContext _musicDateContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AlbumRepository(MusicDateContext musicDateContext , IWebHostEnvironment webHostEnvironment)
        {
            _musicDateContext = musicDateContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public bool CreateAlbum(Album album)
        {
            _musicDateContext.Add(album);
           return _musicDateContext.SaveChanges() > 0;
        }

        public bool DeleteAlbum(string Id)
        {
            bool Result = false;
            Album DAlbum = _musicDateContext.Albums.FirstOrDefault(m => m.Id == Id);
            if(DAlbum != null)
            {
                _musicDateContext.Remove(DAlbum);
                Result = _musicDateContext.SaveChanges() > 0;

            }
            return Result;
        }

        public bool EditAlbum(string Id, Album album)
        {
            bool Result = false;
          
            if (Id == album.Id)
            {
                _musicDateContext.Update(album);
                Result = _musicDateContext.SaveChanges() > 0;
                

            }
            return Result;
        }

        public async Task<Album> GetAlbum(string Id)
        {
            return await _musicDateContext.Albums.AsNoTracking().Include(m => m.songs).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IList<Album>> GetAllAlbum()
        {
            return await _musicDateContext.Albums.Include(x => x.Artist).Include(song => song.songs).ToListAsync();
        }
        public string SaveImage(IFormFile formFile)
        {
            string SPath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/Album");
            if (!Directory.Exists(SPath))
            {
                Directory.CreateDirectory(SPath);
            }
            string ImageName = Guid.NewGuid().ToString()+"-"+formFile.FileName;
            string SaveImagePath = Path.Combine(SPath, ImageName);
            using (var Save = new FileStream(SaveImagePath, FileMode.Create))
            {
                formFile.CopyTo(Save);
            }

            return ImageName;
        }
        public void DeleteImage(string DeleteImageName)
        {
            bool Result = false;
            string DPath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/Album", DeleteImageName);
            System.IO.File.Delete(DPath);

        }
    }
}
