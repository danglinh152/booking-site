using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Flight model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class FlightsController : Controller
    {
        // GET: admin/flights
        [HttpGet]
        public IActionResult Flights()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/flights/create
        [HttpGet("create")]
        public IActionResult CreateFlight()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/flights/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditFlight(int id)
        {
            // Logic to get flight details by id
            var flight = GetFlightById(id); // Replace with your actual data retrieval logic

            if (flight == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(flight);
        }

        private Flight GetFlightById(int id)
        {
            // Replace with your actual data source
            return new Flight
            {

            };
        }
        /* ==============================================================================================*/
    }
}
