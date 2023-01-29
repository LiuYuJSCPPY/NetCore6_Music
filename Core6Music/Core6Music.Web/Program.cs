using Core6Music.Web.DateContext;
using Microsoft.EntityFrameworkCore;
using Core6Music.Web.Interface;
using Core6Music.Web.Repository;
using Microsoft.AspNetCore.Identity;
using Core6Music.Web.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MusicDateContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MusicDateContext")));

builder.Services.AddIdentity<MusicUser, IdentityRole>()
     .AddEntityFrameworkStores<MusicDateContext>()
     .AddDefaultTokenProviders();

builder.Services.AddScoped<IArtist, ArtistRepository>();
builder.Services.AddScoped<IAlbum, AlbumRepository>();
builder.Services.AddScoped<ISong, SongRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
