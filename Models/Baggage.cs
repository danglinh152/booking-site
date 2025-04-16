using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSite.Models
{
    public class Baggage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BaggageID { get; set; }
        [Required]
        public int BookingID { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double Fee { get; set; }
    }
}
