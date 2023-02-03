using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Core6Music.Web.Models;
using Core6Music.Web.DateContext;
using NToastNotify;
using Microsoft.EntityFrameworkCore;

namespace Core6Music.Web.Controllers
{
    public class MusicUserController : Controller
    {
        private readonly UserManager<MusicUser> _userManager;
        private readonly SignInManager<MusicUser> _SignInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MusicDateContext _musicDateContext;
        private readonly IToastNotification _toastNotification;

        public MusicUserController(UserManager<MusicUser> userManager, SignInManager<MusicUser> SignInManager, IWebHostEnvironment webHostEnvironment,MusicDateContext musicDateContext,IToastNotification toastNotification)
        {
            _SignInManager = SignInManager;
            _userManager = userManager; 
            _webHostEnvironment = webHostEnvironment;
            _musicDateContext = musicDateContext;
            _toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FavoriteAlbum()
        {
            
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser sUser = await _userManager.GetUserAsync(User);
                var AllFavoriteAlbum = await _musicDateContext.FavoriteAlbums.Where(x => x.MusicUserId == sUser.Id).ToArrayAsync();
                return View(AllFavoriteAlbum);
            }

            return RedirectToAction("Index", "Music");
        }

        public async Task<IActionResult> FavoriteArtists()
        {

            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser sUser = await _userManager.GetUserAsync(User);
                var AllFavoriteArtists = await _musicDateContext.FavoriteArtists.Where(x => x.MusicUserId == sUser.Id).ToArrayAsync();
                return View(AllFavoriteArtists);
            }
            return RedirectToAction("Index", "Music");
        }
        public async Task<IActionResult> FavoriteSongs()
        {

            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser sUser = await _userManager.GetUserAsync(User);
                var AllFavoriteSongs = await _musicDateContext.FavoriteSongs.Where(x => x.MusicUserId == sUser.Id).ToArrayAsync();
                return View(AllFavoriteSongs);
            }
            return RedirectToAction("Index", "Music");
        }


        public async Task<IActionResult> CreatePlayList()
        {
            return View();
        }


        
        public async Task<IActionResult> AddorDeleteSongPlayList(int MusicMainfestId,int SongId)
        {
            var SongPlayList = await _musicDateContext.MusicManifestSongs.Include(x => x.MusicManifest).Where(x => x.MusicManifestId == MusicMainfestId).Where(x => x.SongId == SongId).FirstOrDefaultAsync();
            var PlayList = await _musicDateContext.MusicManifests.FirstOrDefaultAsync(x => x.Id == MusicMainfestId);

            string SuccessMessage = "";
            bool Result = false;
            if(SongPlayList != null)
            {
                _musicDateContext.Remove(SongPlayList);
                Result =await _musicDateContext.SaveChangesAsync() > 0;
              
            }
            else
            {
                MusicManifestSong CreatemusicManifestSong = new MusicManifestSong()
                {
                    MusicManifestId = MusicMainfestId,
                    SongId = SongId
                };
                _musicDateContext.Add(CreatemusicManifestSong);
                Result = await _musicDateContext.SaveChangesAsync() > 0;
            }
            if (Result)
            {
                _toastNotification.AddSuccessToastMessage("成功!");
                return Json(new { Success = true });
            }
            else
            {
                _toastNotification.AddSuccessToastMessage("失敗!");
                return Json(new { Success = false });
            }
          
        }
        

        [HttpGet("Favorite/Album/{Id}")]
        public async Task<IActionResult> AddFavoriteAlbum(string Id)
        {
            bool result = false;
            
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser sUser = await _userManager.GetUserAsync(User);
                var AFavoriteAlbum = await _musicDateContext.FavoriteAlbums.Where(x => x.MusicUserId == sUser.Id).Where(x => x.AlbumId == Id).FirstAsync();
                if(AFavoriteAlbum != null)
                {
                    _musicDateContext.Remove(AFavoriteAlbum);
                    result = await _musicDateContext.SaveChangesAsync() > 0;
                }
                else
                {
                    FavoriteAlbum favoriteAlbum = new FavoriteAlbum()
                    {
                        AlbumId = Id,
                        MusicUserId = sUser.Id
                    };
                    _musicDateContext.Add(favoriteAlbum);
                    result =await _musicDateContext.SaveChangesAsync() > 0;
                }
               
            }

            if (result)
            {
                _toastNotification.AddSuccessToastMessage("成功!");
                return Json(new { Success = true });
            }
            else
            {
                _toastNotification.AddErrorToastMessage("失敗");
                return Json(new { Success = false });
            }
           
        }
        [HttpGet("Favorite/Artist/{Id}")]
        public async Task<IActionResult> AddOrDeleteFavoriteArtist(string Id)
        {
            bool result = false;
            
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser sUser = await _userManager.GetUserAsync(User);
                var AFavoriteArtist = await _musicDateContext.FavoriteArtists.Where(x => x.MusicUserId == sUser.Id).Where(x => x.ArtistId == Id).FirstAsync();
                if (AFavoriteArtist != null)
                {
                    _musicDateContext.Remove(AFavoriteArtist);
                    result = await _musicDateContext.SaveChangesAsync() > 0;

                }
                else
                {
                    FavoriteArtist favoriteArtist = new FavoriteArtist()
                        {
                            ArtistId = Id,
                            MusicUserId = sUser.Id
                        };
                    _musicDateContext.Add(favoriteArtist);
                    result = await _musicDateContext.SaveChangesAsync() > 0;
                }
                
            }


            if (result)
            {
                _toastNotification.AddSuccessToastMessage("成功!");
                return Json(new { Success = true });
            }
            else
            {
                _toastNotification.AddErrorToastMessage("失敗");
                return Json(new { Success = false });
            }
           
        }
        [HttpGet("Favorite/Song/{Id}")]
        public async Task<IActionResult> AddorDeleteFavoriteSong(int Id)
        {
            bool result = false;
            
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser sUser = await _userManager.GetUserAsync(User);
                var UFavoritesong = await _musicDateContext.FavoriteSongs.Where(x => x.MusicUserId == sUser.Id).Where(x => x.SongId == Id).FirstAsync();
                if (UFavoritesong != null)
                {
                    _musicDateContext.Remove(UFavoritesong);
                    result = await _musicDateContext.SaveChangesAsync() > 0;
                }
                else
                {
                    FavoriteSong favoriteSong = new FavoriteSong()
                    {
                        SongId = Id,
                        MusicUserId = sUser.Id,
                        CreateDate= DateTime.Now,
                    
                    };
                    _musicDateContext.Add(favoriteSong);
                    result = await _musicDateContext.SaveChangesAsync() > 0;
                }
                
            }

            if (result)
            {
                _toastNotification.AddSuccessToastMessage("成功!");
                return Json(new { Success = true });
            }
            else
            {
                _toastNotification.AddErrorToastMessage("失敗");
                return Json(new { Success = false });
            }
           
        }
    }
}
