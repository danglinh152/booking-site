using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSite.Models
{
    public class Passenger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PassengerID { get; set; }
        [Required]
        public int BookingID { get; set; }
        [Required]
        public string FullName { get; set; }
        public TimeSpan DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string? PassportNumber { get; set; } // Nullable
        public string? SeatNumber { get; set; } // Nullable


        public Booking Booking { get; set; }
    }
}
