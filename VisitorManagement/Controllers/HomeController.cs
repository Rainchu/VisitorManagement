using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VisitorManagement.Models;

namespace VisitorManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Lib()
        {
            return View();
        }

        public IActionResult Index()
        {
            ViewBag.Message = HttpContext.Session.GetString("user");

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
