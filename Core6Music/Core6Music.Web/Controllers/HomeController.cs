using Core6Music.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Diagnostics;

namespace Core6Music.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IToastNotification _toastNotification;
        public HomeController(ILogger<HomeController> logger,IToastNotification toastNotification)
        {
            _logger = logger;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            _toastNotification.AddSuccessToastMessage("HellO");
            // Success with default options (taking into account the overwritten defaults when initializing in Startup.cs)
            _toastNotification.AddSuccessToastMessage();

            //Info
            _toastNotification.AddInfoToastMessage();

            //Warning
            _toastNotification.AddWarningToastMessage();

            //Error
            _toastNotification.AddErrorToastMessage();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}