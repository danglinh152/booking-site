using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Checkin model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class CheckinsController : Controller
    {
        // GET: admin/checkins
        [HttpGet]
        public IActionResult Checkins()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/checkins/create
        [HttpGet("create")]
        public IActionResult CreateCheckin()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/checkins/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditCheckin(int id)
        {
            // Logic to get checkin details by id
            var checkin = GetCheckinById(id); // Replace with your actual data retrieval logic

            if (checkin == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(checkin);
        }

        private Checkin GetCheckinById(int id)
        {
            // Replace with your actual data source
            return new Checkin
            {

            };
        }
        /* ==============================================================================================*/
    }
}