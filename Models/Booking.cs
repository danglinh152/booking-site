using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSite.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        public int? UserId { get; set; } // Nullable nếu khách không đăng nhập
        public string BookingCode { get; set; }
        public int FlightId { get; set; }
        public int FareClassId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }


        public User User { get; set; }
        public Flight Flight { get; set; }
        public FareClass FareClass { get; set; }
        public ICollection<Passenger> Passengers { get; set; }
        public ICollection<BookingService> BookingServices { get; set; }
        public Payment Payment { get; set; }

    }
}
