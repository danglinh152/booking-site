using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Seat model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class FareClassesController : Controller
    {
        // GET: admin/seats
        [HttpGet]
        public IActionResult FareClasses()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/seats/create
        [HttpGet("create")]
        public IActionResult CreateFareClass()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/seats/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditFareClass(int id)
        {
            // Logic to get seat details by id
            var FareClass = GetFareClassById(id); // Replace with your actual data retrieval logic

            if (FareClass == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(FareClass);
        }

        private FareClass GetFareClassById(int id)
        {
            // Replace with your actual data source
            return new FareClass
            {

            };
        }
        /* ==============================================================================================*/
    }
}
