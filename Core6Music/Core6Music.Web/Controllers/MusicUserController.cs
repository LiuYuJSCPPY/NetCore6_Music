using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Core6Music.Web.Models;
using Core6Music.Web.DateContext;
using NToastNotify;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core6Music.Web.ViewModels;
using NuGet.ContentModel;

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
  

        public async Task<IActionResult> FavoriteAlbum()
        {
            
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser sUser = await _userManager.GetUserAsync(User);
                var AllFavoriteAlbum = await _musicDateContext.FavoriteAlbums.Include(x =>x.Album).ThenInclude(x=> x.songs).Where(x => x.MusicUserId == sUser.Id).ToListAsync();
                return View(AllFavoriteAlbum);
            }

            return RedirectToAction("Index", "Music");
        }

        public async Task<IActionResult> FavoriteArtists()
        {

            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser sUser = await _userManager.GetUserAsync(User);
                var AllFavoriteArtists = await _musicDateContext.FavoriteArtists.Where(x => x.MusicUserId == sUser.Id).Include(x=>x.Artist).ThenInclude(x => x.artistHeadImages).ToListAsync();
                return View(AllFavoriteArtists);
            }
            return RedirectToAction("Index", "Music");
        }

        public async Task<IActionResult> FavoriteSong()
        {

            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser sUser = await _userManager.GetUserAsync(User);
                var AllFavoriteArtists = await _musicDateContext.FavoriteSongs.Where(x => x.MusicUserId == sUser.Id).Include(x => x.Song).ToListAsync();
                return View(AllFavoriteArtists);
            }
            return RedirectToAction("Index", "Music");
        }

        public async Task<IActionResult> AllPlayList()
        {
            AllPlayListViewModels allPlayListViewModels = new AllPlayListViewModels();
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser AUser = await _userManager.GetUserAsync(User);

                var MusicPlayList = await _musicDateContext.MusicManifests.Include(x => x.musicManifestSongs).Where(x => x.MusicUserId == AUser.Id).ToListAsync();

                var favoriteMusic = await _musicDateContext.FavoriteSongs.Where(x => x.MusicUserId == AUser.Id).ToListAsync();
                allPlayListViewModels.musicManifests = MusicPlayList;
                allPlayListViewModels.favoriteSongs = favoriteMusic;
                return View(allPlayListViewModels);
            }

            return RedirectToAction("Index", "Music");
        }
        public async Task<IActionResult> DetailPlayList(int Id ,string? SearchSong)
        {
            DetailPlayListViewModels detailPlayListViewModels = new DetailPlayListViewModels();
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser AUser = await _userManager.GetUserAsync(User);
                var DetailPlayList = await _musicDateContext.MusicManifests.Include(x => x.musicManifestSongs)
                    .Where(x => x.MusicUserId == AUser.Id)
                    .Where(x => x.Id == Id)
                    .FirstOrDefaultAsync();

                var DetailPlayListSong = await _musicDateContext.MusicManifestSongs.
                    Include(x => x.Song)
                    .ThenInclude(x => x.favoriteSongs)
                    .Include(x => x.Album)
                    .Where(x => x.MusicManifestId == DetailPlayList.Id).ToArrayAsync();

                if (SearchSong == null)
                {
                    detailPlayListViewModels.Songs = null;
                }
                else
                {
                    detailPlayListViewModels.Songs = await _musicDateContext.Songs.Include(x=> x.favoriteSongs).Include(x => x.Album).Where(x => x.Name == SearchSong).Take(10).ToListAsync();
                }
                detailPlayListViewModels.MusicManifest = DetailPlayList;
                detailPlayListViewModels.MusicManifestSongs = DetailPlayListSong;


                return View(detailPlayListViewModels);
            }
            return RedirectToAction("Index", "Music");

        }

        public async Task<IActionResult> CreatePlayList()
        {
            bool Result = false;
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser identityUser = await _userManager.GetUserAsync(User);
                MusicManifest musicManifest = new MusicManifest
                {
                    MusicUserId = identityUser.Id,
                };
                _musicDateContext.Add(musicManifest);
                Result = await _musicDateContext.SaveChangesAsync() > 0;
            }

            if (Result)
            {
                return Json(new { Success = true });
            }
            else
            {
                return Json(new { Success = false });
            }
            
        }

        public async Task<IActionResult> EditPlayList(int Id, [Bind("Name,Context")] MusicManifest musicManifest ,IFormFile formFile)
        {
            bool Result = false;
            string FileName;
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser identityUser = await _userManager.GetUserAsync(User);
                var PlayListEdit = await _musicDateContext.MusicManifests.Where(x => x.MusicUserId == identityUser.Id).Where(x => x.Id == Id).FirstOrDefaultAsync();

                if(formFile == null)
                {
                    FileName = PlayListEdit.Image;
                }
                else
                {
                    string SPath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/PlayList");
                    if (!Directory.Exists(SPath))
                    {
                        Directory.CreateDirectory(SPath);
                    }

                    FileName = Guid.NewGuid().ToString() + "-" + formFile.FileName;
                    string PathFile = Path.Combine(SPath, FileName);
                    using (var steam = new FileStream(PathFile, FileMode.Create))
                    {
                        formFile.CopyTo(steam);
                    }
                }
               

                    MusicManifest EditmusicManifest = new MusicManifest
                    {
                        Id = Id,
                        MusicUserId = identityUser.Id,
                        Name= musicManifest.Name,
                        Context = musicManifest.Context,
                        Image = FileName
                    };
                _musicDateContext.Add(EditmusicManifest);
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
        public async Task<IActionResult> DeletePlayList(int Id)
        {
            bool Result = false;
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser user = await _userManager.GetUserAsync(User);
                var DeletePlayList = await _musicDateContext.MusicManifests.Include(x => x.musicManifestSongs).Where(x => x.MusicUserId == user.Id).Where(x => x.Id == Id).FirstOrDefaultAsync();

                if(DeletePlayList.Image != null)
                {
                    string DeleteImage = Path.Combine(_webHostEnvironment.WebRootPath, "Image/PlayList", DeletePlayList.Image);
                    if (Directory.Exists(DeleteImage)){
                        Directory.Delete(DeleteImage);
                    };
                };
                

                _musicDateContext.Remove(DeletePlayList);
                Result = await _musicDateContext.SaveChangesAsync() > 0;


            }
            if (Result)
            {
                _toastNotification.AddSuccessToastMessage("成功!");
                return RedirectToAction("AllPlayList");
            }
            else
            {
                _toastNotification.AddSuccessToastMessage("失敗!");
                return Json(new { Success = false });
            }
        }


        public async Task<IActionResult> AddorDeleteSongPlayList(int MusicMainfestId,int SongId,string AlbumId)
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
                    SongId = SongId,
                    AlbumId = AlbumId,
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
        public async Task<IActionResult> AddorDeleteFavoriteAlbum(string Id)
        {
            bool result = false;
            
            if (_SignInManager.IsSignedIn(User))
            {
                IdentityUser sUser = await _userManager.GetUserAsync(User);
                var AFavoriteAlbum = await _musicDateContext.FavoriteAlbums.Where(x => x.MusicUserId == sUser.Id).Where(x => x.AlbumId == Id).FirstOrDefaultAsync();
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
                var AFavoriteArtist = await _musicDateContext.FavoriteArtists.Where(x => x.MusicUserId == sUser.Id).Where(x => x.ArtistId == Id).FirstOrDefaultAsync();
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
                var UFavoritesong = await _musicDateContext.FavoriteSongs.Where(x => x.MusicUserId == sUser.Id).Where(x => x.SongId == Id).FirstOrDefaultAsync();
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
