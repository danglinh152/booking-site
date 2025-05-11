using System.Diagnostics;
using BookingSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        
        [Route("Multicity")]
        public IActionResult MultiCity()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Booking")]
        public IActionResult Booking()
        {
            return View();
        }

        [Route("JourneyInfor")]
        public IActionResult JourneyInfor()
        {
            return View();
        }

        [Route("Explore")]
        public IActionResult Explore()
        {
            return View();
        }

        [Route("TenPMClub")]
        public IActionResult TenPMClub()
        {
            return View();
        }
        [Route("Meal")]
        public IActionResult Meal()
        {
            return View();
        }
        [Route("SupportServices")]
        public IActionResult SupportServices()
        {
            return View();
        }
        [Route("Luggage")]
        public IActionResult Luggage()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestID = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
