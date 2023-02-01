using Microsoft.AspNetCore.Mvc;
using Core6Music.Web.DateContext;
using NToastNotify;
using Core6Music.Web.Models;
using Core6Music.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using Core6Music.Web.Interface;
using NuGet.Protocol.Plugins;
using Core6Music.Web.DateContext.Enum;

namespace Core6Music.Web.Controllers
{
    public class MusicController : Controller
    {
        private readonly MusicDateContext _musicDateContext;
        private readonly IToastNotification _toastNotify;

        public MusicController(MusicDateContext musicDateContext, IToastNotification toastNotification)
        {
            _musicDateContext= musicDateContext;
            _toastNotify= toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            
            var AllAlbum =await  _musicDateContext.Albums.Include(artist => artist.Artist).Take(10).ToListAsync();
            var AllArtist = await _musicDateContext.Artists.
                Include(x => x.artistContextImages).
                Include(x => x.artistBackImages).
                Include(x => x.artistHeadImages).
                Include(album => album.albums).Take(10).
                ToListAsync();
            IndexMusciViewModels indexAlbumViewModels = new IndexMusciViewModels()
            {
                albums = AllAlbum,
                artists = AllArtist,
            };
            return View(indexAlbumViewModels);
        }

        [HttpGet("Artist/{ArtistID}")]
        public async Task<IActionResult> AritstDetail (string ArtistID)
        {
            var AritstModel = await _musicDateContext.Artists.AsNoTracking().
                Include(c=>c.artistContextImages).
                Include(b => b.artistBackImages).
                Include(h => h.artistHeadImages).
                FirstOrDefaultAsync(x => x.Id == ArtistID);

            var songModel = await _musicDateContext.Songs.Include(x =>x.Album).Where(x =>x.Album.ArtistId == ArtistID).Take(10).ToListAsync();
            var AlbumModel = await _musicDateContext.Albums.Where(x => x.ArtistId == ArtistID).Take(9).ToListAsync();
            IndexArtistVIewModel indexArtistVIewModel = new IndexArtistVIewModel()
            {
                songs = songModel,
                albums = AlbumModel,
                artist = AritstModel
            };
            return View(indexArtistVIewModel);
        }


        [HttpGet("Album/{AlbumId}")]
        public async Task<IActionResult> AlbumDetail(string AlbumId)
        {
            
            var TenAlbum = await _musicDateContext.Albums.Include(artist => artist.Artist).Take(10).ToListAsync();
            var album = await _musicDateContext.Albums.Include(x=>x.songs).FirstOrDefaultAsync(x => x.Id == AlbumId);
            var AllAritst = await _musicDateContext.Artists.ToListAsync();
            AlbumDetailViewModels albumDetailViewModels = new AlbumDetailViewModels()
            {
                albums = TenAlbum,
                album = album,
                artists = AllAritst,
            };
            return View(albumDetailViewModels);
        }

        [HttpGet("Artist/{ArtistID}/AllAlbum")]
        public async Task<IActionResult> AllAlbum (string ArtistID)
        {
            var AllAlbum = await _musicDateContext.Albums.Include(song => song.songs).ToListAsync();
            return View(AllAlbum);
        }
    }
}
