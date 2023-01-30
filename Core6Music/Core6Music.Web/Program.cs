using Core6Music.Web.DateContext;
using Microsoft.EntityFrameworkCore;
using Core6Music.Web.Interface;
using Core6Music.Web.Repository;
using Microsoft.AspNetCore.Identity;
using Core6Music.Web.Models;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = false,
    PositionClass = ToastPositions.BottomCenter
});

//Or simply go 
builder.Services.AddMvc().AddNToastNotifyToastr();

builder.Services.AddDbContext<MusicDateContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MusicDateContext")));

builder.Services.AddDefaultIdentity<MusicUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MusicDateContext>();


builder.Services.AddScoped<IArtist, ArtistRepository>();
builder.Services.AddScoped<IAlbum, AlbumRepository>();


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

app.UseNToastNotify();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

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
