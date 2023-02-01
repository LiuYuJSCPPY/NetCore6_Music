using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core6Music.Web.DateContext;
using Core6Music.Web.Models;
using Core6Music.Web.Interface;
using Core6Music.Web.Areas.Dashboard.ViewModels;
using HashidsNet;
using NToastNotify;

namespace Core6Music.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AlbumsController : Controller
    {
        private readonly MusicDateContext _context;
        private readonly IAlbum _album;
        private readonly IToastNotification _toastNotification;
        public AlbumsController(MusicDateContext context,IAlbum album, IToastNotification toastNotification)
        {
            _context = context;
            _album = album;
            _toastNotification = toastNotification;
        }

        // GET: Dashboard/Albums
        [HttpGet("Dashboard/{ArtistId}/Albums")]
        public async Task<IActionResult> Index(string ArtistId)
        {
            AllAlbumViewModels allAlbumViewModels = new AllAlbumViewModels()
            {
                AllAlbum = await _album.GetAllAlbum(),
                ArtistId = ArtistId
            };
            return View(allAlbumViewModels);
        }

        // GET: Dashboard/Albums/Details/5
        [HttpGet("Dashboard/{ArtistId}/Albums/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Albums == null)
            {   
                return NotFound();
            }
            var album = await _album.GetAlbum(id);
           
          
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        [HttpGet("Dashboard/{ArtistId}/Albums/Create")]
        // GET: Dashboard/Albums/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Dashboard/Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Dashboard/{ArtistId}/Albums/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,albumCategory,Image")] CreateAlbumViewModels createAlbumViewModels,string ArtistId)
        {
            if (ModelState.IsValid)
            {
               
                var hash = new Hashids(createAlbumViewModels.Name + ArtistId, 22);
                var hashId = hash.Encode(1);
              

                Album album = new Album()
                {
                    Id = hashId,
                    Name = createAlbumViewModels.Name,
                    ArtistId = ArtistId,
                    albumCategory = createAlbumViewModels.albumCategory,
                    Image = _album.SaveImage(createAlbumViewModels.Image)
                };
                if (_album.CreateAlbum(album))
                {
                    _toastNotification.AddSuccessToastMessage("儲存成功!");
                    return RedirectToAction(nameof(Index), new { ArtistId = ArtistId });
                }
                
            }
          
            return View(createAlbumViewModels);
        }

        [HttpGet("Dashboard/{ArtistId}/Albums/Edit/{id}")]
        // GET: Dashboard/Albums/Edit/5
        public async Task<IActionResult> Edit(string id,string ArtistId)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _album.GetAlbum(id);
            EditAlbumViewModels editAlbumViewModels = new EditAlbumViewModels
            {
                Id = album.Id,
                Name = album.Name,
                
                ImageName = album.Image,
                albumCategory = album.albumCategory
            };
            if (album == null)
            {
                return NotFound();
            }
          
            return View(editAlbumViewModels);
        }

        // POST: Dashboard/Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Dashboard/{ArtistId}/Albums/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,string ArtistId,[Bind("Id,Name,albumCategory,Image")] EditAlbumViewModels editAlbumViewModels)
        {
            if (id != editAlbumViewModels.Id)
            {
                return NotFound();
            }
           

            if (ModelState.IsValid)
            { 
               
                try
                {
                    Album album = new Album()
                    {
                        Id = editAlbumViewModels.Id,
                        Name = editAlbumViewModels.Name,
                        albumCategory = editAlbumViewModels.albumCategory,
                        ArtistId = ArtistId,
                    };

                    Album EditeImage = await _album.GetAlbum(editAlbumViewModels.Id);
                    if (editAlbumViewModels.Image != null )
                    {
                        
                        if (EditeImage.Image != null)
                        {
                           _album.DeleteImage(EditeImage.Image);
                           album.Image = _album.SaveImage(editAlbumViewModels.Image);

                        }
                        else
                        {
                            album.Image = _album.SaveImage(editAlbumViewModels.Image);
                        }
                        
                    }else
                    {
                        album.Image = EditeImage.Image;
                    }
                   

                    _album.EditAlbum(id, album);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(editAlbumViewModels.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _toastNotification.AddSuccessToastMessage("更新成功!!");
                return RedirectToAction(nameof(Index), new { ArtistId = ArtistId });
            }
            _toastNotification.AddErrorToastMessage("更新失敗!!");
            return View(editAlbumViewModels);
        }

        // GET: Dashboard/Albums/Delete/5
        [HttpGet("Dashboard/{ArtistId}/Albums/Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _album.GetAlbum(id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Dashboard/Albums/Delete/5
        [HttpPost("Dashboard/{ArtistId}/Albums/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id,string ArtistId)
        {
            if (_context.Albums == null)
            {
                return Problem("Entity set 'MusicDateContext.Albums'  is null.");
            }
            var album = await _album.GetAlbum(id);

            if(album.Image != null && album != null)
            {
                _album.DeleteImage(album.Image);
            }

            if (album != null &&_album.DeleteAlbum(id))
            {
                _toastNotification.AddSuccessToastMessage("刪除成功!!");
                return RedirectToAction(nameof(Index),new { ArtistId= ArtistId });
            }
            else
            {
                _toastNotification.AddErrorToastMessage("刪除失敗!!");
                return RedirectToAction("Edit", album.Id);
            }
            
          
          
        }

        private bool AlbumExists(string id)
        {
          return _context.Albums.Any(e => e.Id == id);
        }
    }
}
