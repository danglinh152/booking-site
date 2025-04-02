using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Seat model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class SeatsController : Controller
    {
        // GET: admin/seats
        [HttpGet]
        public IActionResult Seats()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/seats/create
        [HttpGet("create")]
        public IActionResult CreateSeat()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/seats/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditSeat(int id)
        {
            // Logic to get seat details by id
            var seat = GetSeatById(id); // Replace with your actual data retrieval logic

            if (seat == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(seat);
        }

        private Seat GetSeatById(int id)
        {
            // Replace with your actual data source
            return new Seat
            {

            };
        }
        /* ==============================================================================================*/
    }
}
