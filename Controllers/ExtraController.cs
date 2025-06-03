using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Booking model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class ExtrasController : Controller
    {
        // GET: admin/bookings
        [HttpGet]
        public IActionResult Extras()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/bookings/create
        [HttpGet("create")]
        public IActionResult CreateExtra()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/bookings/edit/1
        // [HttpGet("edit/{id}")]
        // public IActionResult EditExtra(int id)
        // {
        //     // Logic to get booking details by id
        //     var booking = GetExtraById(id); // Replace with your actual data retrieval logic

        //     if (booking == null)
        //     {
        //         return NotFound(); // Return a 404 if not found
        //     }
        //     return View(booking);
        // }

        private ExtraService GetExtraById(int id)
        {
            // Replace with your actual data source
            return new ExtraService
            {

            };
        }
        /* ==============================================================================================*/
    }
}
