using HackHeroes0._1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HackHeroes0._1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [Authorize]
        public IActionResult Index()
        {                   
            return View();
        }

        public IActionResult NoAccess()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Home/SetTheme")]
        public IActionResult SetTheme(string theme)
        {

            Response.Cookies.Append("theme", theme, new CookieOptions { HttpOnly = true, Path = "/" });

            // Możesz zwrócić status 200 OK, gdy zmiana motywu się powiedzie.
            return Ok();
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