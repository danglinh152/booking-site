using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSite.Models
{
    public class Checkin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CheckinID { get; set; }
        [Required]
        public int BookingID { get; set; }
        [Required]
        public string CheckinTime { get; set; }
        public string Status { get; set; }
    }
}
