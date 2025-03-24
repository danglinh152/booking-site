namespace BookingSite.Models
{
    public class Flight
    {
        public int FlightID { get; set; }
        public int PlaneID { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public int DepartureAirportID { get; set; }
        public int ArrivalAirportID { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public string Status { get; set; }
    }
}
