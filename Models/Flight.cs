using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookingSite.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightID { get; set; }
        [Required]
        public int PlaneID { get; set; }
        [Required]
        public TimeSpan DepartureTime { get; set; }
        [Required]
        public TimeSpan ArrivalTime { get; set; }
        [Required]
        public DateOnly FlightDate { get; set; }
        [Required]
        public int DepartureAirportID { get; set; }
        [Required]
        public int ArrivalAirportID { get; set; }
        public string Status { get; set; }
        [ForeignKey("DepartureAirportID")]
        [ValidateNever]
        public Airport DepartureAirport { get; set; }
        [ForeignKey("ArrivalAirportID")]
        [ValidateNever]
        public Airport ArrivalAirport { get; set; }
        [ForeignKey("PlaneID")]
        [ValidateNever]
        public Plane Plane { get; set; }
        [ValidateNever]
        public ICollection<Booking> Bookings { get; set; }
        [ValidateNever]
        public ICollection<FareClass> FareClasses { get; set; }
    }
}
