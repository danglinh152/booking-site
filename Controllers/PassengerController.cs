using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Passenger model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class PassengersController : Controller
    {
        // GET: admin/passengers
        [HttpGet]
        public IActionResult Passengers()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/passengers/create
        [HttpGet("create")]
        public IActionResult CreatePassenger()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/passengers/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditPassenger(int id)
        {
            // Logic to get passenger details by id
            var passenger = GetPassengerById(id); // Replace with your actual data retrieval logic

            if (passenger == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(passenger);
        }

        private Passenger GetPassengerById(int id)
        {
            // Replace with your actual data source
            return new Passenger
            {

            };
        }
        /* ==============================================================================================*/
    }
}
