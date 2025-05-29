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
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        public string? PhoneNumber { get; set; }

        public string? IdentityNumber { get; set; }

        public string? SeatNumber { get; set; }

        // One-to-one relationship
        public BookingDetail BookingDetail { get; set; }
    }
}
