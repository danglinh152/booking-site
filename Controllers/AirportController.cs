using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Airport model

namespace BookingSite.Controllers
{
    [AuthorizeRole("Admin")]
    [Route("admin/airports")]
    public class AirportsController : Controller
    {
        // GET: admin/airports
        private FlightBookingContext context;

        public AirportsController(FlightBookingContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Airports()
        {
            var airports = context.Airports.ToList();
            return View(airports);
        }

        /* ==============================================================================================*/
        // GET: admin/airports/create
        [HttpGet("create")]
        public IActionResult CreateAirport()
        {
            return View();
        }

        /* ==============================================================================================*/

        [HttpPost("create")]
        public IActionResult CreateAirport(Airport airport)
        {

            if (ModelState.IsValid)
            {
                context.Airports.Add(airport);
                context.SaveChanges();
                return RedirectToAction("Airports");
            }
            return View(airport);
        }
        /* ==============================================================================================*/
        // GET: admin/airports/edit/1
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditAirport(int id)
        {
            // Logic to get airport details by id
            var airport = await context.Airports.FindAsync(id); // Replace with your actual data retrieval logic

            if (airport == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(airport);
        }


        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditAirport(int id, Airport updatedAirport)
        {
            var Airport = await context.Airports.FindAsync(id);
            if (Airport == null)
            {
                return NotFound();
            }

            Airport.Country = updatedAirport.Country;
            Airport.City = updatedAirport.City;
            Airport.Country = updatedAirport.Country;

            try
            {
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Airports));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật. Vui lòng thử lại.");
                return View(updatedAirport);
            }
        }


        // DELETE: admin/airport/delete/1
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteAirport(int id)
        {
            var Airport = await context.Airports.FindAsync(id);
            if (Airport == null)
            {
                return NotFound();
            }

            context.Airports.Remove(Airport);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Airports));
        }
        /* ==============================================================================================*/
    }
}
