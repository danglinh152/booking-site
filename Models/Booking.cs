using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }
        public int? UserID { get; set; } // Nullable nếu khách không đăng nhập
        [Required]
        public string BookingCode { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        public ICollection<BookingDetail> BookingDetails { get; set; }
        public Payment Payment { get; set; }
    }
}
