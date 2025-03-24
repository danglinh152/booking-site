using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Plane model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class PlanesController : Controller
    {
        // GET: admin/airports
        [HttpGet]
        public IActionResult Planes()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/airports/create
        [HttpGet("create")]
        public IActionResult CreatePlane()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/airports/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditPlane(int id)
        {
            // Logic to get airport details by id
            var airport = GetPlaneById(id); // Replace with your actual data retrieval logic

            if (airport == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(airport);
        }

        private Plane GetPlaneById(int id)
        {
            // Replace with your actual data source
            return new Plane
            {

            };
        }
        /* ==============================================================================================*/
    }
}
