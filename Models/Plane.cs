
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookingSite.Models
{
    public class Plane
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlaneID { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [ValidateNever]
        public ICollection<Flight> Flights { get; set; }
    }
}


