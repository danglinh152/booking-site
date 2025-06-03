using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookingSite.Models
{
    public class Seat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatID { get; set; }

        [Required]
        public string SeatNumber { get; set; }

        [Required]
        public int FareClassID { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        
        public FareClass FareClass { get; set; }
    }
}
