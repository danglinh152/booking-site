using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BookingSite.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BookingSite.ViewModel;


namespace BookingSite.Controllers
{
    public class ProfileController : Controller
    {
        private readonly FlightBookingContext context;

        // Thêm constructor để inject context
        public ProfileController(FlightBookingContext context)
        {
            this.context = context;
        }

        // ViewModel dùng để binding form đổi mật khẩu
        public class ChangePasswordViewModel
        {
            [Required(ErrorMessage = "Mật khẩu hiện tại là bắt buộc")]
            [DataType(DataType.Password)]
            public string? CurrentPassword { get; set; }

            [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
            [MinLength(6, ErrorMessage = "Mật khẩu mới phải từ 6 ký tự trở lên")]
            [DataType(DataType.Password)]
            public string? NewPassword { get; set; }

            [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
            [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp")]
            [DataType(DataType.Password)]
            public string? ConfirmPassword { get; set; }
        }


        // DTO cho checkin
        public class CheckinItem
        {
            public int CheckinID { get; set; }
            public int BookingID { get; set; }
            public string? CheckinTime { get; set; }
            public string? Status { get; set; }
        }

        // Hardcoded danh sách checkin
        private static List<CheckinItem> _checkin = new List<CheckinItem>
        {
            new CheckinItem { CheckinID = 1, BookingID = 101, CheckinTime = "2025-06-01 10:00", Status = "Confirmed" },
            new CheckinItem { CheckinID = 2, BookingID = 102, CheckinTime = "2025-06-02 11:30", Status = "Pending" },
            new CheckinItem { CheckinID = 3, BookingID = 103, CheckinTime = "2025-06-03 09:45", Status = "Cancelled" }
        };

        /* ==================== CHANGE PASSWORD ==================== */

        // GET: /profile/change-passwd
        [Route("profile/change-passwd")]
        [HttpGet]
        public IActionResult ChangePasswd()
        {
            return View();
        }

        // POST: /profile/change-passwd
        [Route("profile/change-passwd")]
        [HttpPost]
        public IActionResult ChangePasswd(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userID = HttpContext.Session.GetString("UserID");
            if (userID == null)
            {
                return RedirectToAction("Login", "User");
            }
            User user = context.Users.Find(int.Parse(userID));
            if (user == null)
            {
                ModelState.AddModelError("", "Người dùng không tồn tại");
                return View(model);
            }

            if (string.IsNullOrEmpty(model.CurrentPassword) || string.IsNullOrEmpty(model.NewPassword))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ mật khẩu hiện tại và mật khẩu mới.");
                return View(model);
            }

            if (HashPassword(model.CurrentPassword) != user.Password)
            {
                ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng");
                return View(model);
            }
            user.Password = HashPassword(model.NewPassword);
            context.SaveChanges();

            TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("ChangePasswd");
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }


        /* ==================== HISTORY BOOKING ==================== */

        // GET: /profile/history-booking
        [Route("profile/history-booking")]
        [HttpGet]
        public IActionResult HistoryBooking()
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "User");
            }
            int userId = int.Parse(userIdStr);
            var bookingList = context.Bookings
                .Where(b => b.UserID == userId)
                .OrderByDescending(b => b.BookingDate)
                .Include(b => b.Flight)
                    .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.Flight)
                    .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.Flight)
                    .ThenInclude(f => f.Plane)
                .ToList();

            var history = bookingList.Select(b => new BookingHistory
            {
                BookingID = b.BookingID,
                BookingCode = b.BookingCode ?? string.Empty,
                TotalPrice = b.TotalPrice,
                BookingDate = b.BookingDate,
                Status = b.Status ?? string.Empty,
                FlightCode = b.Flight != null ? b.Flight.FlightID.ToString() : string.Empty,
                DepartureAirport = (b.Flight != null && b.Flight.DepartureAirport != null) ? b.Flight.DepartureAirport.City : "",
                ArrivalAirport = (b.Flight != null && b.Flight.ArrivalAirport != null) ? b.Flight.ArrivalAirport.City : "",
                PlaneModel = (b.Flight != null && b.Flight.Plane != null) ? b.Flight.Plane.Model : ""
            }).ToList();

            return View(history);
        }


        /* ==================== CHECKIN ==================== */

        [Route("profile/checkin")]
        [HttpGet]
        public IActionResult CheckIn()
        {
            return View();
        }

        [Route("profile/checkin")]
        [HttpPost]
        public IActionResult CheckIn(string BookingCode, string FullName, string IdNumber, string SeatNumber)
        {
            // Validate đơn giản
            if (string.IsNullOrWhiteSpace(BookingCode) ||
                string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrWhiteSpace(IdNumber) ||
                string.IsNullOrWhiteSpace(SeatNumber))
            {
                ModelState.AddModelError("", "Vui lòng điền đầy đủ thông tin check-in.");
                return View();
            }

            // Giả lập tìm BookingID từ BookingCode (thực tế cần truy vấn DB)
            int bookingID = 123; // ví dụ lấy cứng

            var newCheckin = new Checkin()
            {
                BookingID = bookingID,
                CheckinTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = "Checked-in"
            };

            // TODO: Lưu newCheckin vào DB

            TempData["SuccessMessage"] = "Check-in thành công!";

            return RedirectToAction("CheckIn");
        }

        [Route("profile/history-booking/{bookingCode}")]
        [HttpGet]
        public IActionResult HistoryBookingDetail(string bookingCode)
        {
            if (string.IsNullOrEmpty(bookingCode))
            {
                return NotFound("Booking code không hợp lệ.");
            }
            Booking booking = context.Bookings
                .Include(b => b.Flight)
                    .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.Flight)
                    .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.Flight)
                    .ThenInclude(f => f.Plane)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.Passenger)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.FareClass)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.BookingServices)
                        .ThenInclude(bs => bs.Service)
                .FirstOrDefault(b => b.BookingCode == bookingCode);

            return View("HistoryBookingDetail", booking);
        }


    }


}
