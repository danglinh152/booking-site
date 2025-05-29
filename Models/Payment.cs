using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentID { get; set; }
        [Required]
        public int BookingID { get; set; }
        [Precision(10, 2)]
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public TimeSpan PaymentDate { get; set; }

        public Booking Booking { get; set; }
    }
}
