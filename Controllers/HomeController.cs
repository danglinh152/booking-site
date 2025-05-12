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

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("booking/food")]
        public IActionResult Food()
        {
            return View();
        }

        [Route("booking/baggage")]
        public IActionResult Baggage()
        {
            return View();
        }

        [Route("booking")]
        public IActionResult Booking()
        {
            return View();
        }

        [Route("journey-info")]
        public IActionResult JourneyInfor()
        {
            return View();
        }

        [Route("explore")]
        public IActionResult Explore()
        {
            return View();
        }

        [Route("tenpm-club")]
        public IActionResult TenPMClub()
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
