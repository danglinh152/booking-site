namespace BookingSite.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int PassengerID { get; set; }
        public int FlightID { get; set; }
        public string BookingDate { get; set; }
        public string Status { get; set; }
    }
}
