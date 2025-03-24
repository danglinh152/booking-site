using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Baggage model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class BaggagesController : Controller
    {
        // GET: admin/baggages
        [HttpGet]
        public IActionResult Baggages()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/baggages/create
        [HttpGet("create")]
        public IActionResult CreateBaggage()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/airports/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditBaggage(int id)
        {
            // Logic to get baggage details by id
            var baggage = GetBaggageById(id); // Replace with your actual data retrieval logic

            if (baggage == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(baggage);
        }

        private Baggage GetBaggageById(int id)
        {
            // Replace with your actual data source
            return new Baggage
            {
                BaggageID = id,
                BookingID = 152,
                Weight = 36.36,
                Fee = 36.36
            };
        }
        /* ==============================================================================================*/
    }
}
