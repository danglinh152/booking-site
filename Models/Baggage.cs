namespace BookingSite.Models
{
    public class Baggage
    {
        public int BaggageID { get; set; }
        public int BookingID { get; set; }
        public double Weight { get; set; }
        public double Fee { get; set; }
    }
}
