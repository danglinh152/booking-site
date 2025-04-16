using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSite.Models{
    public class FareClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FareClassId { get; set; }
        public int FlightId { get; set; }
        public string ClassName { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }


        public Flight Flight { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
