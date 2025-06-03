public class CheckInViewModel
{
    public string BookingCode { get; set; }
    public List<PassengerInfo> Passengers { get; set; }
    public int FlightID { get; set; }
    public List<string> TakenSeats { get; set; }
}

public class PassengerInfo
{
    public int PassengerID { get; set; }
    public string FullName { get; set; }
    public string? SeatNumber { get; set; }
}
