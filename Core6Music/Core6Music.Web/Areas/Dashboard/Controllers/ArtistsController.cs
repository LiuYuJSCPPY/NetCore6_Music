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
    public class ArtistsController : Controller
    {
        private readonly MusicDateContext _context;
        private readonly IArtist _artist;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IToastNotification _toastNotification;

        public ArtistsController(MusicDateContext context, IArtist artist, IWebHostEnvironment webHostEnvironment,IToastNotification toastNotification)
        {
            _context = context;
            _artist = artist;
            _webHostEnvironment = webHostEnvironment;
            _toastNotification = toastNotification;
        }

        // GET: Dashboard/Artists
        public async Task<IActionResult> Index()
        {
            AllArtistsViewModels allArtistsView= new AllArtistsViewModels();
          
            allArtistsView.Artists = await _artist.GetAllArtistAsync();
              return View(allArtistsView);
        }

        // GET: Dashboard/Artists/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }
            var artist = await _artist.GetArtistAsync(id);
            string backImage = _context.ArtistBackImages.FirstOrDefault(x => x.ArtistId == artist.Id).ImageName;
           
            DetailArtistVIewModels detailsAlbumViewModels = new DetailArtistVIewModels()
            {
                Id = id,
                Name = artist.Name,
                Context = artist.Context,
                FaceBook = artist.FaceBook,
                Instagram = artist.Instagram,
                Twitter = artist.Twitter,
                Wikipedia = artist.Wikipedia,
                BackImageName = backImage,
               
            };
           
            if (artist == null)
            {
                return NotFound();
            }

            return View(detailsAlbumViewModels);
        }

        // GET: Dashboard/Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Context,FaceBook,Instagram,Twitter,Wikipedia,ArtistBackImage,ArtistContextImage,ArtistHeadImage")] CreateArtistsViewModel createArtistsViewModel)
        {
            
            var hasdid = new Hashids(createArtistsViewModel.Name,22);
            var hash = hasdid.Encode(1);
            bool Restult = false;
            bool RestultArtist = false;
            if (ModelState.IsValid)
            {
                Artist CreateArtist = new Artist();
                CreateArtist.Name = createArtistsViewModel.Name;
                CreateArtist.Id = hash;
                CreateArtist.Context = createArtistsViewModel.Context;
                CreateArtist.FaceBook = createArtistsViewModel.FaceBook;
                CreateArtist.Instagram = createArtistsViewModel.Instagram;
                CreateArtist.Twitter = createArtistsViewModel.Twitter;
                CreateArtist.Wikipedia = createArtistsViewModel.Wikipedia;

               Restult =  _artist.CreateArtist(CreateArtist);
              
            }
            if (Restult && createArtistsViewModel.ArtistBackImage != null)
            {
                string ImageName = _artist.SaveImage(createArtistsViewModel.ArtistBackImage, "BackImage");
                ArtistBackImage CareateArtistBackImage  = new ArtistBackImage();
                CareateArtistBackImage.ArtistId = hash;
                CareateArtistBackImage.ImageName = ImageName;
                _context.Add(CareateArtistBackImage);
                await _context.SaveChangesAsync();

            }
            if (Restult && createArtistsViewModel.ArtistContextImage!= null)
            {
                string ContextImageName = _artist.SaveImage(createArtistsViewModel.ArtistContextImage, "ContextImage");
                ArtistContextImage CareateArtistBackImage = new ArtistContextImage()
                {
                    ArtistId = hash,
                    ImageName = ContextImageName,
                };
               
                _context.Add(CareateArtistBackImage);
               await _context.SaveChangesAsync();

            }
            if (Restult && createArtistsViewModel.ArtistHeadImage != null)
            {
                string HeadImageName = _artist.SaveImage(createArtistsViewModel.ArtistHeadImage, "HeadImage");
                ArtistHeadImage CareateArtistBackImage = new ArtistHeadImage()
                {
                    ArtistId = hash,
                    ImageName = HeadImageName,
                };
               
                _context.Add(CareateArtistBackImage);
                await _context.SaveChangesAsync();

            }
            if (Restult)
            {
                _toastNotification.AddSuccessToastMessage("新增成功!");
                return RedirectToAction(nameof(Index));
            }else
            {
                _toastNotification.AddErrorToastMessage("新增失敗");
                return View(createArtistsViewModel);
            }
          
        }

        // GET: Dashboard/Artists/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }
            var artist = await _artist.GetArtistAsync(id);
            string backImage = _context.ArtistBackImages.FirstOrDefault(x => x.ArtistId == artist.Id).ImageName;
            
            EditArtistViewModel detailArtistVIewModels = new EditArtistViewModel()
            {
                Id = id,
                Name = artist.Name,
                Context = artist.Context,
                FaceBook = artist.FaceBook,
                Instagram = artist.Instagram,
                Twitter = artist.Twitter,
                Wikipedia = artist.Wikipedia,
                BackImageName = backImage,
               
            };
           
            if (artist == null)
            {
                return NotFound();
            }
            return View(detailArtistVIewModels);
        }

        // POST: Dashboard/Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Context,FaceBook,Instagram,Twitter,Wikipedia,ArtistBackImage,ArtistContextImage,ArtistHeadImage")] EditArtistViewModel editArtistViewModel)
        {
            bool Result = false;
            if (id != editArtistViewModel.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                Artist EditArtist = new Artist()
                {
                    Id = editArtistViewModel.Id,
                    Name = editArtistViewModel.Name,
                    Context = editArtistViewModel.Context,
                    FaceBook = editArtistViewModel.FaceBook,
                    Instagram = editArtistViewModel.Instagram,
                    Twitter = editArtistViewModel.Twitter,
                    Wikipedia = editArtistViewModel.Wikipedia
                };
                try
                {
                    
                    Result = await _artist.UpateArtist(editArtistViewModel.Id,EditArtist);


                    if (Result && editArtistViewModel.ArtistBackImage != null)
                    {
                        ArtistBackImage artistBackImage = _context.ArtistBackImages.FirstOrDefault(x => x.ArtistId == EditArtist.Id);
                        if (artistBackImage != null)
                        {
                            string DeletePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/Artist/BackImage", artistBackImage.ImageName);
                            System.IO.File.Delete(DeletePath);
                           
                        }
                        else
                        {
                            string SaveImageName = _artist.SaveImage(editArtistViewModel.ArtistBackImage, "BackImage");
                            ArtistBackImage CareateArtistBackImage = new ArtistBackImage();
                            CareateArtistBackImage.ArtistId = EditArtist.Id;
                            CareateArtistBackImage.ImageName = SaveImageName;
                            _context.Add(CareateArtistBackImage);
                            await _context.SaveChangesAsync();
                        }
                       

                        string EditImageName = _artist.SaveImage(editArtistViewModel.ArtistBackImage, "BackImage");
                        ArtistBackImage EditArtistBackImage = new ArtistBackImage();
                        EditArtistBackImage.Id = artistBackImage.Id;
                        EditArtistBackImage.ArtistId = EditArtist.Id;
                        EditArtistBackImage.ImageName = EditImageName;
                        _context.Update(EditArtistBackImage);
                        await _context.SaveChangesAsync();

                    }



                    if (Result && editArtistViewModel.ArtistContextImage != null)
                    {
                        ArtistContextImage artistContextImage = _context.ArtistContextImages.FirstOrDefault(x => x.ArtistId == EditArtist.Id);
                        if(artistContextImage != null)
                        {
                            string DeletePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/Artist/ContextImage", artistContextImage.ImageName);
                            System.IO.File.Delete(DeletePath);
                            _context.Remove(artistContextImage);
                            await _context.SaveChangesAsync();
                        }
                        
                         else
                        {
                            string SaveImageName = _artist.SaveImage(editArtistViewModel.ArtistContextImage, "ContextImage");
                            ArtistContextImage CareateArtistContextImage = new ArtistContextImage();
                            CareateArtistContextImage.ArtistId = EditArtist.Id;
                            CareateArtistContextImage.ImageName = SaveImageName;
                            _context.Add(CareateArtistContextImage);
                            await _context.SaveChangesAsync();
                        }


                        string EditImageName = _artist.SaveImage(editArtistViewModel.ArtistContextImage, "ContextImage");
                        ArtistContextImage EditArtistContextImage = new ArtistContextImage();
                        EditArtistContextImage.Id = artistContextImage.Id;
                        EditArtistContextImage.ArtistId = EditArtist.Id;
                        EditArtistContextImage.ImageName = EditImageName;
                        _context.Update(EditArtistContextImage);
                        await _context.SaveChangesAsync();

                    }


                    if (Result && editArtistViewModel.ArtistHeadImage != null)
                    {
                        ArtistHeadImage artistHeadImage = _context.ArtistHeadImages.FirstOrDefault(x => x.ArtistId == EditArtist.Id);
                        if(artistHeadImage != null)
                        {
                            string DeletePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/Artist/HeadImage", artistHeadImage.ImageName);
                            System.IO.File.Delete(DeletePath);
                            _context.Remove(artistHeadImage);
                            await _context.SaveChangesAsync();
                        }
                         else
                        {
                            string SaveImageName = _artist.SaveImage(editArtistViewModel.ArtistHeadImage, "HeadImage");
                            ArtistHeadImage CareateArtistHeadImage = new ArtistHeadImage();
                            CareateArtistHeadImage.ArtistId = EditArtist.Id;
                            CareateArtistHeadImage.ImageName = SaveImageName;
                            _context.Add(CareateArtistHeadImage);
                            await _context.SaveChangesAsync();
                        }


                        string EditImageName = _artist.SaveImage(editArtistViewModel.ArtistBackImage, "HeadImage");
                        ArtistHeadImage EditArtistHeadImage = new ArtistHeadImage();
                        EditArtistHeadImage.Id = artistHeadImage.Id;
                        EditArtistHeadImage.ArtistId = EditArtist.Id;
                        EditArtistHeadImage.ImageName = EditImageName;
                        _context.Update(EditArtistHeadImage);
                        await _context.SaveChangesAsync();

                    }
                   

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (Result)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(editArtistViewModel);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Dashboard/Artists/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Dashboard/Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Artists == null)
            {
                return Problem("Entity set 'MusicDateContext.Artists'  is null.");
            }
            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                _artist.DeleteImage(artist.Id);
                _artist.DeleteArtist(artist.Id);
            }
           
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(string id)
        {
          return _context.Artists.Any(e => e.Id == id);
        }
    }
}
