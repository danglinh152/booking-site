using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Checkin model
using BookingSite.Data; 
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BookingSite.ViewModels; /
namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
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

        [HttpPost]
        public IActionResult FindBooking(string bookingCode)
        {
            var booking = context.Bookings.FirstOrDefault(b => b.BookingCode == bookingCode);
            if (booking == null)
            {
                TempData["Error"] = "Mã đặt chỗ không tồn tại.";
                return RedirectToAction("Index");
            }

            var bookingDetails = context.BookingDetails
                .Include(bd => bd.Passenger)
                .Where(bd => bd.BookingID == booking.BookingID)
                .Select(bd => new PassengerInfo
                {
                    PassengerID = bd.PassengerID,
                    FullName = bd.Passenger.FullName,
                    SeatNumber = bd.Passenger.SeatNumber
                }).ToList();

            var flightID = booking.FlightID ?? 0;

            var takenSeats = context.Passengers
                .Where(p => context.BookingDetails.Any(bd => bd.PassengerID == p.PassengerID &&
                                                            context.FareClasses.Any(f => f.FareClassID == bd.FareClassID &&
                                                                                            f.FlightID == flightID)) &&
                            p.SeatNumber != null)
                .Select(p => p.SeatNumber)
                .ToList();

            var model = new CheckInViewModel
            {
                BookingCode = bookingCode,
                Passengers = bookingDetails,
                FlightID = flightID,
                TakenSeats = takenSeats
            };

            return View("SeatSelection", model);
        }

        // Step 2: Cập nhật ghế cho hành khách
        [HttpPost]
        public IActionResult CheckInPassenger(int passengerID, string seatNumber)
        {
            var passenger = context.Passengers.Find(passengerID);
            if (passenger == null) return NotFound();

            passenger.SeatNumber = seatNumber;
            context.SaveChanges();

            TempData["Success"] = "Check-in thành công!";
            return RedirectToAction("Index");
        }
    

    }
}