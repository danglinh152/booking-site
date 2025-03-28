using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Airport model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class AirportsController : Controller
    {
        // GET: admin/airports
        [HttpGet]
        public IActionResult Airports()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/airports/create
        [HttpGet("create")]
        public IActionResult CreateAirport()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/airports/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditAirport(int id)
        {
            // Logic to get airport details by id
            var airport = GetAirportById(id); // Replace with your actual data retrieval logic

            if (airport == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(airport);
        }

        private Airport GetAirportById(int id)
        {
            // Replace with your actual data source
            return new Airport
            {
                AirportID = id,
                Name = "John F. Kennedy International Airport",
                City = "New York",
                Country = "United States"
            };
        }
        /* ==============================================================================================*/
    }
}
