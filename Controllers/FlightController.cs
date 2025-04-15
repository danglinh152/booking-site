using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingSite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingSite.Controllers
{
    [AuthorizeRole("Admin")]
    [Route("admin/flights")]
    public class FlightsController : Controller
    {
        private FlightBookingContext context;

        public FlightsController(FlightBookingContext context)
        {
            this.context = context;
        }

        // GET: admin/flights
        [HttpGet]
        public IActionResult Flights()
        {
            var flights = context.Flights
                           .Include(f => f.DepartureAirport)
                           .Include(f => f.ArrivalAirport)
                           .Include(f => f.Plane);
            return View(flights.ToList());
        }


        /* ==============================================================================================*/
        // GET: admin/flights/create
        [HttpGet("create")]
        public IActionResult CreateFlight()
        {
            ViewBag.DepartureAirportID = new SelectList(context.Airports, "AirportID", "Name");
            ViewBag.ArrivalAirportID = new SelectList(context.Airports, "AirportID", "Name");
            ViewBag.PlaneID = new SelectList(context.Planes, "PlaneID", "Model");
            return View();
        }

        /* ==============================================================================================*/
        [HttpPost("create")]
        public ActionResult CreateFlight(Flight flight)
        {
            if (ModelState.IsValid)
            {
                context.Flights.Add(flight);
                context.SaveChanges();
                return RedirectToAction("Flights");
            }
            ViewBag.DepartureAirportID = new SelectList(context.Airports, "AirportID", "Name", flight.DepartureAirportID);
            ViewBag.ArrivalAirportID = new SelectList(context.Airports, "AirportID", "Name", flight.ArrivalAirportID);
            ViewBag.PlaneID = new SelectList(context.Planes, "PlaneID", "Model", flight.PlaneID);
            return View(flight);
        }

        /* ==============================================================================================*/
        // GET: admin/flights/edit/1
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditFlight(int id)
        {
            var flight = await context.Flights.FindAsync(id);
            if (flight == null) return NotFound();
            ViewBag.DepartureAirportID = new SelectList(context.Airports, "AirportID", "Name", flight.DepartureAirportID);
            ViewBag.ArrivalAirportID = new SelectList(context.Airports, "AirportID", "Name", flight.ArrivalAirportID);
            ViewBag.PlaneID = new SelectList(context.Planes, "PlaneID", "Model", flight.PlaneID);
            return View(flight);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditFlight(int id, Flight flightUpdate)
        {

            var flight = await context.Flights.FindAsync(id);
            if (flight == null) return NotFound();

            flight.PlaneID = flightUpdate.PlaneID;
            flight.FlightDate = flightUpdate.FlightDate;
            flight.DepartureAirportID = flightUpdate.DepartureAirportID;
            flight.ArrivalAirportID = flightUpdate.ArrivalAirportID;
            flight.DepartureTime = flightUpdate.DepartureTime;
            flight.ArrivalTime = flightUpdate.ArrivalTime;
            flight.Status = flightUpdate.Status;

            try
            {
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Flights));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật. Vui lòng thử lại.");
                return View(flightUpdate);
            }
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            context.Flights.Remove(flight);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Flights));
        }


        private Flight GetFlightById(int id)
        {
            // Replace with your actual data source
            return new Flight
            {

            };
        }
        /* ==============================================================================================*/
    }
}
