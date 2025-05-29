using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSite.Models
{
    public class BookingService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingServiceID { get; set; }
        [Required]
        public int BookingDetailID { get; set; }
        [Required]
        public int ServiceID { get; set; }
        [Required]
        public int Quantity { get; set; }

        public BookingDetail BookingDetail { get; set; }
        public ExtraService Service { get; set; }
    }
}
