using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BookingSite.Models;


namespace BookingSite.Controllers
{
    public class ProfileController : Controller
    {
        // ViewModel dùng để binding form đổi mật khẩu
        public class ChangePasswordViewModel
        {
            [Required(ErrorMessage = "Mật khẩu hiện tại là bắt buộc")]
            [DataType(DataType.Password)]
            public string CurrentPassword { get; set; }

            [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
            [MinLength(6, ErrorMessage = "Mật khẩu mới phải từ 6 ký tự trở lên")]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
            [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
        }

        // DTO cho 1 booking hiển thị lịch sử
        public class BookingHistoryItem
        {
            public string BookingCode { get; set; }
            public string Route { get; set; }
            public string FlightDate { get; set; }
            public string SeatNumbers { get; set; }
            public string Price { get; set; }
            public string Status { get; set; }
        }

        // DTO cho checkin
        public class CheckinItem
        {
            public int CheckinID { get; set; }
            public int BookingID { get; set; }
            public string CheckinTime { get; set; }
            public string Status { get; set; }
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

            // Giả định mật khẩu hiện tại là "abc123"
            string hardcodedCurrentPassword = "abc123";

            if (model.CurrentPassword != hardcodedCurrentPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng");
                return View(model);
            }

            TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("ChangePasswd");
        }


        /* ==================== HISTORY BOOKING ==================== */

        // GET: /profile/history-booking
        [Route("profile/history-booking")]
        [HttpGet]
        public IActionResult HistoryBooking()
        {
            var history = new List<BookingHistoryItem>
            {
                new BookingHistoryItem {
                    BookingCode = "BK123456",
                    Route = "Hà Nội → TP. Hồ Chí Minh",
                    FlightDate = "2025-06-15",
                    SeatNumbers = "12A, 12B",
                    Price = "2,500,000 VND",
                    Status = "Đã xác nhận"
                },
                new BookingHistoryItem {
                    BookingCode = "BK123457",
                    Route = "Đà Nẵng → Hà Nội",
                    FlightDate = "2025-07-01",
                    SeatNumbers = "8C",
                    Price = "1,200,000 VND",
                    Status = "Chờ xử lý"
                },
                new BookingHistoryItem {
                    BookingCode = "BK123458",
                    Route = "TP. Hồ Chí Minh → Nha Trang",
                    FlightDate = "2025-05-20",
                    SeatNumbers = "3D, 3E",
                    Price = "1,800,000 VND",
                    Status = "Đã hủy"
                }
            };

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

    }
}
