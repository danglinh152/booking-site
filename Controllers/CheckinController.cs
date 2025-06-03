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


        // [HttpPost("CheckIn")]
        // public async Task<IActionResult> CheckIn(string bookingCode)
        // {
        //     var booking = await context.Bookings
        //         .Include(b => b.BookingDetails)
        //         .ThenInclude(bd => bd.Passenger)
        //         .FirstOrDefaultAsync(b => b.BookingCode == bookingCode);

        //     if (booking == null)
        //     {
        //         return Json(new { success = false, message = "Booking code not found." });
        //     }

        //     if (booking.BookingDetails.All(bd => !string.IsNullOrEmpty(bd.Passenger.SeatNumber)))
        //     {
        //         return Json(new { success = false, message = "All passengers have already checked in." });
        //     }

        //     if (booking.Status != "Paid")
        //     {
        //         return Json(new { success = false, message = "Booking must be paid to check in" });
        //     }
        //     // Lấy danh sách ghế đã sử dụng một lần để tối ưu
        //     var usedSeats = await context.Passengers
        //         .Where(p => p.SeatNumber != null)
        //         .Select(p => p.SeatNumber)
        //         .ToListAsync();

        //     foreach (var detail in booking.BookingDetails)
        //     {
        //         if (string.IsNullOrEmpty(detail.Passenger.SeatNumber))
        //         {
        //             // Lấy danh sách ghế từ FareClass của BookingDetail, chỉ lấy ghế còn trống
        //             var seats = await context.Seats
        //                 .Where(s => s.FareClassID == detail.FareClassID && s.IsAvailable)
        //                 .ToListAsync();

        //             // Kiểm tra ghế chưa được sử dụng (kết hợp với IsAvailable)
        //             var availableSeat = seats.FirstOrDefault(s => !usedSeats.Contains(s.SeatNumber));
        //             if (availableSeat != null)
        //             {
        //                 detail.Passenger.SeatNumber = availableSeat.SeatNumber;
        //                 availableSeat.IsAvailable = false; // Cập nhật trạng thái ghế thành không khả dụng
        //             }
        //             else
        //             {
        //                 return Json(new { success = false, message = "No available seats for this fare class." });
        //             }
        //         }
        //     }
            

        //     booking.Status = "CheckedIn";
        //     await context.SaveChangesAsync();

        //     TempData["SuccessMessage"] = "Check-in successful.";
        //     return RedirectToAction("CheckinSuccess");
        // }

        public IActionResult CheckIn()
        {
            return View();
        }

        [HttpPost("CheckIn")]
        public async Task<IActionResult> CheckIn(string bookingCode)
        {
            var booking = await context.Bookings
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Passenger)
                .Include(b => b.Flight)
                .FirstOrDefaultAsync(b => b.BookingCode == bookingCode);

            if (booking == null)
            {
                TempData["ErrorMessage"] = "Booking code not found.";
                return View("CheckIn");
            }

            if (booking.BookingDetails.All(bd => !string.IsNullOrEmpty(bd.Passenger.SeatNumber)))
            {
                TempData["ErrorMessage"] = "All passengers have already checked in.";
                return View("CheckIn");
            }

            if (booking.Status != "Paid")
            {
                TempData["ErrorMessage"] = "Booking must be paid to check in.";
                return View("CheckIn");
            }
            
            // // Chuyển đổi TimeSpan thành TimeOnly
            // var departureTimeOnly = new TimeOnly(flight.DepartureTime.Hours, flight.DepartureTime.Minutes, flight.DepartureTime.Seconds);
            // var departureDateTime = flight.FlightDate.ToDateTime(departureTimeOnly);
            // var currentDateTime = DateTime.Now; // 11:40 PM, 03/06/2025

            // var earliestCheckIn = departureDateTime.AddHours(-24);
            // var latestCheckIn = departureDateTime.AddHours(-2);

            // if (currentDateTime < earliestCheckIn)
            // {
            //     TempData["ErrorMessage"] = "Check-in is only available 24 hours before departure.";
            //     return View("CheckIn");
            // }

            // if (currentDateTime > latestCheckIn)
            // {
            //     TempData["ErrorMessage"] = "Check-in has closed 2 hours before departure.";
            //     return View("CheckIn");
            // }

            var usedSeats = await context.Passengers
                .Where(p => p.SeatNumber != null)
                .Select(p => p.SeatNumber)
                .ToListAsync();

            foreach (var detail in booking.BookingDetails)
            {
                if (string.IsNullOrEmpty(detail.Passenger.SeatNumber))
                {
                    var seats = await context.Seats
                        .Where(s => s.FareClassID == detail.FareClassID && s.IsAvailable)
                        .ToListAsync();

                    var availableSeat = seats.FirstOrDefault(s => !usedSeats.Contains(s.SeatNumber));
                    if (availableSeat != null)
                    {
                        detail.Passenger.SeatNumber = availableSeat.SeatNumber;
                        availableSeat.IsAvailable = false;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "No available seats for this fare class.";
                        return View("CheckIn");
                    }
                }
            }

            booking.Status = "CheckedIn";
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Check-in successful.";
            TempData["BookingCode"] = bookingCode; // Lưu bookingCode để sử dụng trong Success
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
        [HttpGet("CheckinSuccess")]
        public async Task<IActionResult> CheckinSuccess()
        {
            var bookingCode = TempData["BookingCode"]?.ToString();
            if (string.IsNullOrEmpty(bookingCode))
            {
                return RedirectToAction("CheckIn"); // Quay lại trang CheckIn nếu không có bookingCode
            }

            var booking = await context.Bookings
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Passenger)
                .Include(b => b.Flight)
                .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.Flight)
                .ThenInclude(f => f.ArrivalAirport)
                .FirstOrDefaultAsync(b => b.BookingCode == bookingCode);

            if (booking == null)
            {
                return RedirectToAction("CheckIn"); // Quay lại trang CheckIn nếu không tìm thấy booking
            }

            return View("CheckinSuccess", booking); // Trả về view Success với model Booking
        }

    }
}