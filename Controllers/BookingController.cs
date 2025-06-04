using Microsoft.AspNetCore.Mvc;
using BookingSite.Models;
using BookingSite.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
namespace BookingSite.Controllers
{
    [AuthorizeRole("Admin")]
    [Route("admin/bookings")]
    public class BookingController : Controller
    {

        private readonly FlightBookingContext context;

        public BookingController(FlightBookingContext context)
        {
            this.context = context;
        }

        // GET: admin/bookings
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookings = await context.Bookings
                .Include(b => b.BookingDetails) // Bao gồm chi tiết booking nếu cần
                .Select(b => new
                {
                    BookingID = b.BookingID,
                    UserID = b.UserID,
                    BookingCode = b.BookingCode,
                    TotalPrice = b.TotalPrice,
                    BookingDate = b.BookingDate,
                    Status = b.Status
                })
                .ToListAsync();

            return View("Bookings", bookings);
        }

        // GET: admin/bookings/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/bookings/update-status
        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] BookingUpdateViewModel model)
        {
            try
            {
                var booking = await context.Bookings.FindAsync(model.BookingID);
                if (booking == null)
                {
                    return NotFound(new { success = false, message = "Booking not found" });
                }

                booking.Status = model.Status;
                await context.SaveChangesAsync();

                return Json(new { success = true, message = "Status updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
