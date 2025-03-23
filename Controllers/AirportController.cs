using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Airport model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class AirportsController : Controller
    {
        // GET: admin/airports/1
        [HttpGet("{id}")]
        public IActionResult AirportDetails(int id)
        {
            // Logic to get airport details by id
            var airport = GetAirportById(id); // Replace with your actual data retrieval logic

            if (airport == null)
            {
                return NotFound(); // Return a 404 if not found
            }

            return View(airport); // Pass the airport model to the view
        }

        // Example method to simulate data retrieval
        private Airport GetAirportById(int id)
        {
            // Replace with your actual data source
            return new Airport
            {
                AirportId = id,
                Name = "John F. Kennedy International Airport",
                City = "New York",
                Country = "United States"
            };
        }
    }
}
