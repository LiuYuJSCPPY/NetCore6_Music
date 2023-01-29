using Core6Music.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Core6Music.Web.DateContext
{
    public class MusicDateContext : IdentityDbContext<MusicUser>
    {
        public MusicDateContext(DbContextOptions<MusicDateContext> options) : base(options)
        {
            
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<FavoriteAlbum> FavoriteAlbums { get; set; }
        public DbSet<FavoriteArtist> FavoriteArtists { get; set; }
        public DbSet<FavoriteSong> FavoriteSongs { get; set; }
        public DbSet<MusicManifest> MusicManifests { get; set; }
        public DbSet<MusicManifestSong> MusicManifestSongs { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongArtist> SongArtists { get; set; }
        public DbSet<ArtistBackImage> ArtistBackImages { get; set;}
        public DbSet<ArtistContextImage> ArtistContextImages { get; set; }
        public DbSet<ArtistHeadImage> ArtistHeadImages { get; set; }


    }
}
