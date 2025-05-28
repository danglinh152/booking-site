using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using BookingSite.Models;
using BookingSite.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Controllers
{
    [Route("get-ticket")]
    public class GetTicketController : Controller
    {
        private readonly ILogger<GetTicketController> _logger;
        private readonly FlightBookingContext context;

        public GetTicketController(ILogger<GetTicketController> logger, FlightBookingContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet("")]
        public IActionResult GetTicket()
        {
            var searchJson = TempData["SearchModel"] as string;
            if (string.IsNullOrEmpty(searchJson))
            {
                return Redirect("/");
            }
            var model = JsonSerializer.Deserialize<SearchFlightViewModel>(searchJson);
            model.Airports = context.Airports.ToList();
            model.DepartureCode = context.Airports.Find(model.DepartureID).City + " (" + context.Airports.Find(model.DepartureID).AirportCode + ")";
            model.ArrivalCode = context.Airports.Find(model.ArrivalID).City + " (" + context.Airports.Find(model.ArrivalID).AirportCode + ")";

            DateOnly minDate = model.DepartureDate.Value.AddDays(-5);
            DateOnly maxDate = model.DepartureDate.Value.AddDays(5);

            var flights = context.Flights
            .Where(f => f.FlightDate >= minDate
            && f.FlightDate <= maxDate
            && f.ArrivalAirportID == model.ArrivalID
            && f.DepartureAirportID == model.DepartureID)
            .Include(f => f.FareClasses)
            .Include(f => f.DepartureAirport)
            .Include(f => f.ArrivalAirport)
            .ToList();

            var grouped = flights
            .GroupBy(f => context.Flights.First(fl => fl.FlightID == f.FlightID).FlightDate)
            .OrderBy(g => g.Key)
            .Select(g => new FlightsByDateViewModel
            {
                FlightDate = g.Key,
                Flights = g.ToList(),
                cheapestPrice = g
                    .SelectMany(f => f.FareClasses)
                    .Min(fc => (double?)fc.Price) ?? 0.0
            })
            .ToList();

            var culture = new System.Globalization.CultureInfo("vi-VN");

            model.flightsByDateViewModel = grouped;
            foreach (FlightsByDateViewModel item in model.flightsByDateViewModel)
            {
                var day = culture.DateTimeFormat.GetDayName(item.FlightDate.DayOfWeek);
                item.dateName = day;
            }

            return View(model);
        }

        [HttpGet("get-flight-by-date")]
        public JsonResult getFlightByDate([FromQuery] string date, [FromQuery] int departureID, [FromQuery] int arrivalID, [FromQuery] int passengerNumber)
        {
            try
            {
                DateOnly flightDate = DateOnly.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var flights = context.Flights
                    .Where(f => f.FlightDate == flightDate
                    && f.ArrivalAirportID == arrivalID
                    && f.DepartureAirportID == departureID
                    && f.FareClasses.Any(fc => fc.SeatsAvailable > passengerNumber)
                    )
                    .Include(f => f.FareClasses)
                    .Include(f => f.DepartureAirport)
                    .Include(f => f.ArrivalAirport)
                    .Include(f => f.Plane)
                    .ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                };
                return Json(flights, options);
            }
            catch (FormatException)
            {
                return Json(new { error = "Invalid date format" });
            }
        }



        [HttpPost("")]
        public IActionResult GetTicket(SearchFlightViewModel searchFlightViewModel)
        {

            return View();
        }

        [HttpGet("shopping-cart")]
        public IActionResult ShoppingCart()
        {
            return View();
        }

        [AuthorizeRole("Client")]
        [HttpGet("passenger-info")]
        public IActionResult PassengerInfo()
        {
            return View();
        }

        [HttpGet("itinerary")]
        public IActionResult Itinerary()
        {
            return View();
        }

        [HttpGet("buybaggage")]
        public IActionResult BuyBaggage()
        {
            return View();
        }

        [HttpGet("selectmeal")]
        public IActionResult SelectMeal()
        {
            return View();
        }

        [HttpGet("selectseat")]
        public IActionResult SelectSeat()
        {
            return View();
        }

        [HttpGet("loungeservice")]
        public IActionResult LoungeService()
        {
            return View();
        }
        [HttpGet("payment")]
        public IActionResult Payment()
        {
            return View();
        }
        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestID = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
