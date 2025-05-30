using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using BookingSite.Models;
using BookingSite.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookingSite.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FlightBookingContext context;

        public HomeController(ILogger<HomeController> logger, FlightBookingContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var airports = context.Airports.ToList();
            SearchFlightViewModel searchFlightViewModel = new SearchFlightViewModel();
            searchFlightViewModel.Airports = airports;
            return View(searchFlightViewModel);
        }

        [HttpPost("")]
        public IActionResult Search(SearchFlightViewModel searchFlightViewModel)
        {
            bool error = false;
            if (searchFlightViewModel.DepartureID == 0)
            {
                error = true;
                ModelState.AddModelError(nameof(searchFlightViewModel.DepartureID), "*Chọn điểm đi");
            }
            if (searchFlightViewModel.ArrivalID == 0)
            {
                error = true;
                ModelState.AddModelError(nameof(searchFlightViewModel.ArrivalID), "*Chọn điểm đến");
            }
            if (searchFlightViewModel.DepartureDate == null)
            {
                error = true;
                ModelState.AddModelError(nameof(searchFlightViewModel.DepartureDate), "*Chọn ngày đi");
            }
            if (searchFlightViewModel.SearchType == "roundTrip" && searchFlightViewModel.ReturnDate == null)
            {
                error = true;
                ModelState.AddModelError(nameof(searchFlightViewModel.ReturnDate), "*Chọn ngày về");
            }
            if (error == true)
            {
                searchFlightViewModel.Airports = context.Airports.ToList();
                return View("Index", searchFlightViewModel);
            }

            var json = JsonSerializer.Serialize(searchFlightViewModel);
            TempData["SearchModel"] = json;
            return RedirectToAction("GetTicket", "GetTicket");
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

        [Route("Explore")]
        public IActionResult Explore()
        {
            return View();
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("tenpm-club")]
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
        [Route("ChooseSeat")]
        public IActionResult ChooseSeat()
        {
            return View();
        }
        [Route("Insurance")]
        public IActionResult Insurance()
        {
            return View();
        }
        [Route("Lounge")]
        public IActionResult Lounge()
        {
            return View();
        }
        [Route("Hotel")]
        public IActionResult Hotel()
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
