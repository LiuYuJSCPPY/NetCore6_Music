using Core6Music.Web.DateContext;
using Core6Music.Web.Interface;
using Core6Music.Web.Models;
using System.Media;
using NAudio.Wave;
using TagLib;
using Microsoft.AspNetCore.Mvc;

namespace Core6Music.Web.Repository
{
    
    public class SongRepository : ISong
    {
        private readonly MusicDateContext _musicDateContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SongRepository(MusicDateContext musicDateContext, IWebHostEnvironment webHostEnvironment)
        {
            _musicDateContext= musicDateContext;
            _webHostEnvironment= webHostEnvironment;
        }

        public bool CresteSong([Bind("AlbumId")]Song song,IFormFile Mp3File)
        {
            var FileImage = TagLib.File.Create(Mp3File.FileName);

            Song CreateSong = new Song();
        }

        public bool DeleteSong(int Id)
        {
            throw new NotImplementedException();
        }

        public bool EditSong(int Id,[Bind("AlbumId")] Song song, IFormFile Mp3File)
        {
            throw new NotImplementedException();
        }
    }
}
