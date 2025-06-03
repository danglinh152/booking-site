using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Checkin model
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace BookingSite.Controllers
{
    [Route("profile/checkins")]
    public class CheckinsController : Controller
    {
        private FlightBookingContext context;

        public CheckinsController(FlightBookingContext context)
        {
            this.context = context;
        }
        // // GET: admin/checkins
        // [HttpGet]
        // public IActionResult Checkins()
        // {
        //     return View();
        // }

        // // Step 1: Nhập mã booking
        // public IActionResult Index() => View();
        [HttpPost("CheckIn")]
        public async Task<IActionResult> CheckIn(string bookingCode)
        {
            var booking = await context.Bookings
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Passenger)
                .FirstOrDefaultAsync(b => b.BookingCode == bookingCode);

            if (booking == null)
            {
                return Json(new { success = false, message = "Booking code not found." });
            }

            if (booking.BookingDetails.All(bd => !string.IsNullOrEmpty(bd.Passenger.SeatNumber)))
            {
                return Json(new { success = false, message = "All passengers have already checked in." });
            }

            if (booking.Status != "Paid")
            {
                return Json(new { success = false, message = "Booking must be paid to check in" });
            }
            // Lấy danh sách ghế đã sử dụng một lần để tối ưu
            var usedSeats = await context.Passengers
                .Where(p => p.SeatNumber != null)
                .Select(p => p.SeatNumber)
                .ToListAsync();

            foreach (var detail in booking.BookingDetails)
            {
                if (string.IsNullOrEmpty(detail.Passenger.SeatNumber))
                {
                    // Lấy danh sách ghế từ FareClass của BookingDetail, chỉ lấy ghế còn trống
                    var seats = await context.Seats
                        .Where(s => s.FareClassID == detail.FareClassID && s.IsAvailable)
                        .ToListAsync();

                    // Kiểm tra ghế chưa được sử dụng (kết hợp với IsAvailable)
                    var availableSeat = seats.FirstOrDefault(s => !usedSeats.Contains(s.SeatNumber));
                    if (availableSeat != null)
                    {
                        detail.Passenger.SeatNumber = availableSeat.SeatNumber;
                        availableSeat.IsAvailable = false; // Cập nhật trạng thái ghế thành không khả dụng
                    }
                    else
                    {
                        return Json(new { success = false, message = "No available seats for this fare class." });
                    }
                }
            }
            

            booking.Status = "CheckedIn";
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Check-in successful.";
            return RedirectToAction("CheckinSuccess");
        }

        [HttpGet("get-passengers")]
        public async Task<IActionResult> GetPassengers(string bookingCode)
        {
            var booking = await context.Bookings
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Passenger)
                .FirstOrDefaultAsync(b => b.BookingCode == bookingCode);

            if (booking == null)
            {
                return Json(new { error = "Booking code not found." });
            }

            var passengers = booking.BookingDetails.Select(bd => new 
            { 
                fullName = bd.Passenger.FullName, 
                identityNumber = bd.Passenger.IdentityNumber,
            }).ToList();

            return Json(new { passengers});
        }
        // Trang thành công
        public IActionResult CheckinSuccess()
        {
            return View("CheckinSuccess");
        }

    }
}