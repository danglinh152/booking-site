using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookingSite.Models
{
    public class Airport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AirportID { get; set; } // ID của sân bay
        public string Name { get; set; } // Tên sân bay
        public string City { get; set; } // Thành phố nơi sân bay tọa lạc
        public string Country { get; set; } // Quốc gia nơi sân bay tọa lạc

        [InverseProperty("DepartureAirport")]
        [ValidateNever]
        public ICollection<Flight> DepartureFlights { get; set; }

        [InverseProperty("ArrivalAirport")]
        [ValidateNever]
        public ICollection<Flight> ArrivalFlights { get; set; }
    }
}
