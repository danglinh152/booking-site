using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Booking model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class BookingsController : Controller
    {
        // GET: admin/bookings
        [HttpGet]
        public IActionResult Bookings()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/bookings/create
        [HttpGet("create")]
        public IActionResult CreateBooking()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/bookings/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditBooking(int id)
        {
            // Logic to get booking details by id
            var booking = GetBookingById(id); // Replace with your actual data retrieval logic

            if (booking == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(booking);
        }

        private Booking GetBookingById(int id)
        {
            // Replace with your actual data source
            return new Booking
            {

            };
        }
        /* ==============================================================================================*/
    }
}
