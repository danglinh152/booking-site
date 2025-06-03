namespace BookingSite.ViewModel
{
  public class BookingHistory
  {
    public int BookingID { get; set; }
    public string BookingCode { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime BookingDate { get; set; }
    public string Status { get; set; }
    public string FlightCode { get; set; } // Mã chuyến bay
    public string DepartureAirport { get; set; } // Sân bay khởi hành
    public string ArrivalAirport { get; set; } // Sân bay đến
    public string PlaneModel { get; set; } // Mô hình máy bay
  }
}