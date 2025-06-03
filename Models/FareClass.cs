using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Models
{
    public class FareClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FareClassID { get; set; }
        [Required]
        public int FlightID { get; set; }
        [Required]
        public string ClassName { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal Price { get; set; }
        [Required]
        public int SeatsAvailable { get; set; }

        // Một FareClass có nhiều BookingDetail
        public ICollection<BookingDetail>? BookingDetails { get; set; }

        public Flight Flight { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
