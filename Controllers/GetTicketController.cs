using System.Diagnostics;
using System.Text.Json;
using BookingSite.Models;
using BookingSite.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookingSite.Controllers
{
    [Route("get-ticket")]
    public class GetTicketController : Controller
    {
        private readonly ILogger<GetTicketController> _logger;

        public GetTicketController(ILogger<GetTicketController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")] // Đây là action mặc định cho /get-ticket
        public IActionResult GetTicket()
        {
            // var searchJson = TempData["SearchModel"] as string;
            // var model = JsonSerializer.Deserialize<SearchFlightViewModel>(searchJson);

            return View();
        }

        [HttpPost("")]
        public IActionResult GetTicket(SearchFlightViewModel searchFlightViewModel)
        {

            return View();
        }

        [HttpGet("shoppingcart")]
        public IActionResult ShoppingCart()
        {
            return View();
        }

        [HttpGet("passengerinfo")]
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
