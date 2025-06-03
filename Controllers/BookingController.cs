using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Checkin model
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace BookingSite.Controllers
{
    [AuthorizeRole("Admin")]
    [Route("admin/bookings")]
    public class BookingsController : Controller
    {
    
        private readonly FlightBookingContext context;

        public BookingsController(FlightBookingContext context)
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

    }
}
