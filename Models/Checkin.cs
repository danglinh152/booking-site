namespace BookingSite.Models
{
    public class Checkin
    {
        public int CheckinID { get; set; }
        public int BookingID { get; set; }
        public string CheckinTime { get; set; }
        public string Status { get; set; }
    }
}
