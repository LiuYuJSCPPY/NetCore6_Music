﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core6Music.Web.DateContext;
using Core6Music.Web.Models;
using TagLib;
using Core6Music.Web.Areas.Dashboard.ViewModels;
using NToastNotify;


namespace Core6Music.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class SongsController : Controller
    {
        private readonly MusicDateContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IToastNotification _toastNotification;

        public SongsController(MusicDateContext context, IWebHostEnvironment webHostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _toastNotification = toastNotification;
        }

        // GET: Dashboard/Songs
        public async Task<IActionResult> Index()
        {
            var musicDateContext = _context.Songs.Include(s => s.Album);
            return View(await musicDateContext.ToListAsync());
        }

        // GET: Dashboard/Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Dashboard/Songs/Create

        [HttpGet("Dashboard/{AdlumId}/Song/Create")]
        public async Task<IActionResult> Create(string AdlumId)
        {
            Album song =await _context.Albums.FirstOrDefaultAsync(x => x.Id == AdlumId);
            SongViewModel songViewModel = new SongViewModel()
            {
                album = song
            };
            return View(songViewModel);
        }

        // POST: Dashboard/Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost("Dashboard/{AdlumId}/Song/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mp3File")]SongViewModel songViewModel,string AdlumId)
        {
            Album song = await _context.Albums.FirstOrDefaultAsync(x => x.Id == AdlumId);
            songViewModel.album = song;
            IFormFile Mp3File = songViewModel.Mp3File;
            string Mp3Path = Path.Combine(_webHostEnvironment.WebRootPath, "MP3");
            if (!Directory.Exists(Mp3Path))
            {
                Directory.CreateDirectory(Mp3Path);
            }

            string SaveMp3Path = Path.Combine(Mp3Path, Mp3File.FileName);

            using (var steam = new FileStream(SaveMp3Path, FileMode.Create))
            {
                Mp3File.CopyTo(steam);
            }


            var FileName = TagLib.File.Create(SaveMp3Path);
            string[] Artist = FileName.Tag.Artists;
            string Name = FileName.Tag.Title;
            TimeSpan Mp3Time = FileName.Properties.Duration;
            if (FileName.Tag.Title == null && FileName.Tag.Artists == null) 
            {
           
                _toastNotification.AddErrorToastMessage("MP3 檔案創作者是空的");
                return View(songViewModel);
            }

            Song CreateSong = new Song()
            {
                Name = Name,
                AlbumId = AdlumId,
                SongTime = Mp3Time,
                CreateDate = DateTime.Now,
                ArtistName = Artist.ToString(),
                Mp3NameFile = Mp3File.FileName
            };
            

            _context.Add(CreateSong);
                
            await _context.SaveChangesAsync();
               
          
            return View(CreateSong);
        }

      
        // GET: Dashboard/Songs/Edit/5
        public async Task<IActionResult> Edit(int? id, string AlbumId)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", song.AlbumId);
            return View(song);
        }

        // POST: Dashboard/Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile Mp3File, string AlbumId)
        {
            Song song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", song.AlbumId);
            return View(song);
        }


        [HttpPost("Dashboard/{AdlumId}/Song/Delete/id")]
        // GET: Dashboard/Songs/Delete/5
        public async Task<IActionResult> Delete(int? id,string AdlumId)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Dashboard/Songs/Delete/5
        [HttpPost("Dashboard/{AdlumId}/Song/Delete/id")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Songs == null)
            {
                return Problem("Entity set 'MusicDateContext.Songs'  is null.");
            }
           
            
            var song = await _context.Songs.FindAsync(id);
            string DeletePath = Path.Combine(_webHostEnvironment.WebRootPath, "MP3",song.Mp3NameFile);
            if (Directory.Exists(DeletePath))
            {
                Directory.Delete(DeletePath);
            }
            if (song != null)
            {
                _context.Songs.Remove(song);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
          return _context.Songs.Any(e => e.Id == id);
        }
    }
}
