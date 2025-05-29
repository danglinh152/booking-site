using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSite.Models
{
  public class BookingDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingDetailID { get; set; }

    [Required]
    public int BookingID { get; set; }

    [Required]
    public int FareClassID { get; set; }

    [Required]
    public int PassengerID { get; set; }

    // Navigation properties
    public Booking Booking { get; set; }
    public FareClass FareClass { get; set; }
    public Passenger Passenger { get; set; }
    public ICollection<BookingService> BookingServices { get; set; }
  }
}