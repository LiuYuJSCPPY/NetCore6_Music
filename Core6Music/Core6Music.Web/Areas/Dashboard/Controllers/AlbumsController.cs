using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core6Music.Web.DateContext;
using Core6Music.Web.Models;
using Core6Music.Web.Interface;
using Core6Music.Web.Areas.Dashboard.ViewModels;
using HashidsNet;
using System.Security.Policy;

namespace Core6Music.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AlbumsController : Controller
    {
        private readonly MusicDateContext _context;
        private readonly IAlbum _album;
        public AlbumsController(MusicDateContext context,IAlbum album)
        {
            _context = context;
            _album = album;
        }

        // GET: Dashboard/Albums
        public async Task<IActionResult> Index()
        {
            AllAlbumViewModels allAlbumViewModels = new AllAlbumViewModels()
            {
                AllAlbum = await _album.GetAllAlbum()
            };
            return View(allAlbumViewModels);
        }

        // GET: Dashboard/Albums/Details/5
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

        // GET: Dashboard/Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name");
            return View();
        }

        // POST: Dashboard/Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ArtistId,albumCategory,Image")] CreateAlbumViewModels createAlbumViewModels)
        {
            if (ModelState.IsValid)
            {
               
                var hash = new Hashids(createAlbumViewModels.Name + createAlbumViewModels.ArtistId, 22);
                var hashId = hash.Encode(1);
              

                Album album = new Album()
                {
                    Id = hashId,
                    Name = createAlbumViewModels.Name,
                    ArtistId = createAlbumViewModels.ArtistId,
                    albumCategory = createAlbumViewModels.albumCategory,
                    Image = _album.SaveImage(createAlbumViewModels.Image)
                };
                if (_album.CreateAlbum(album))
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", createAlbumViewModels.ArtistId);
            return View(createAlbumViewModels);
        }

        // GET: Dashboard/Albums/Edit/5
        public async Task<IActionResult> Edit(string id)
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
                ArtistId = album.ArtistId,
                ImageName = album.Image,
                albumCategory = album.albumCategory
            };
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", album.ArtistId);
            return View(editAlbumViewModels);
        }

        // POST: Dashboard/Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,ArtistId,albumCategory,Image")] EditAlbumViewModels editAlbumViewModels)
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
                        ArtistId = editAlbumViewModels.ArtistId,
                        albumCategory = editAlbumViewModels.albumCategory,
                    };

                    Album EditeImage = await _album.GetAlbum(editAlbumViewModels.Id);
                    if (editAlbumViewModels.Image != null)
                    {
                        
                        if (EditeImage.Image != null)
                        {
                           _album.DeleteImage(EditeImage.Image);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", editAlbumViewModels.ArtistId);
            return View(editAlbumViewModels);
        }

        // GET: Dashboard/Albums/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
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
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Edit", album.Id);
            }
            
          
          
        }

        private bool AlbumExists(string id)
        {
          return _context.Albums.Any(e => e.Id == id);
        }
    }
}
