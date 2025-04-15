using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BookingSite.Models;
using Microsoft.EntityFrameworkCore; // Ensure this points to the correct location of your Plane model

namespace BookingSite.Controllers
{
    [AuthorizeRole("Admin")]
    [Route("admin/planes")]
    public class PlanesController : Controller
    {
        private FlightBookingContext context;

        public PlanesController(FlightBookingContext context)
        {
            this.context = context;
        }
        // GET: admin/airports
        [HttpGet]
        public IActionResult Planes()
        {
            var planes = context.Planes.ToList();
            return View(planes);
        }

        /* ==============================================================================================*/
        // GET: admin/airports/create
        [HttpGet("create")]
        public IActionResult CreatePlane()
        {
            return View();
        }

        /* ==============================================================================================*/
        [HttpPost("create")]
        public IActionResult CreatePlane(Plane plane)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Model error: " + error.ErrorMessage);
                }
                return View(plane);
            }
            if (ModelState.IsValid)
            {
                context.Planes.Add(plane);
                context.SaveChanges();
                return RedirectToAction("Planes");
            }
            return View(plane);
        }

        /* ==============================================================================================*/
        // GET: admin/airports/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditPlane(int id)
        {
            // Logic to get airport details by id
            var plane = context.Planes.Find(id); // Replace with your actual data retrieval logic

            if (plane == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(plane);
        }


        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditPlane(int id, Plane updatePlane)
        {
            if (id != updatePlane.PlaneID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(updatePlane);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Planes));
                }
                catch (DbUpdateException)
                {
                    return StatusCode(500, "Lỗi cập nhật dữ liệu");
                }
            }
            return View(updatePlane);
        }
        [HttpGet("delete/{id}")]
        public ActionResult Delete(int id)
        {
            var plane = context.Planes.Find(id);
            if (plane == null) return NotFound();
            return View(plane);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeletePlane(int id)
        {
            var plane = await context.Planes.FindAsync(id);
            if (plane == null)
            {
                return NotFound();
            }
            context.Planes.Remove(plane);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Planes));
        }


        /* ==============================================================================================*/
    }
}
