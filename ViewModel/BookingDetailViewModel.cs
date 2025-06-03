namespace BookingSite.ViewModel
{
  public class BookingDetailViewModel
  {
    public int BookingDetailID { get; set; }
    public int BookingID { get; set; }
    public int FareClassID { get; set; }
    public int PassengerID { get; set; }
    public string PassengerName { get; set; } 
    public string FareClassName { get; set; } 
    public decimal Price { get; set; } 
    public string SeatNumber { get; set; } 
    public string BookingCode { get; set; } 
  }
}