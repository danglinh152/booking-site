using System.Diagnostics;
using BookingSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingSite.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Planes()
        {
            return View();
        }

        public IActionResult Flights()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Seats()
        {
            return View();
        }

        public IActionResult Passengers()
        {
            return View();
        }

        public IActionResult Meals()
        {
            return View();
        }

        public IActionResult Checkins()
        {
            return View();
        }

        public IActionResult Baggages()
        {
            return View();
        }

        public IActionResult Bookings()
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
