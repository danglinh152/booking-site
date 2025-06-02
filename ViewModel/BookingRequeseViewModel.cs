
public class BookingRequestViewModel
{
  public int FareClassId { get; set; }
  public string TotalPrice { get; set; } // Nếu muốn lưu số, nên dùng decimal
  public int PassengerCount { get; set; }
  public List<PassengerRequestViewModel> Passengers { get; set; }
  public int FlightId { get; set; }
}

public class PassengerRequestViewModel
{
  public int PassengerNumber { get; set; }
  public string Title { get; set; }
  public string FullName { get; set; }
  public string Dob { get; set; } // hoặc DateTime nếu client gửi đúng định dạng
  public string IdentityNumber { get; set; }
  public string PhoneNumber { get; set; }
  public List<ServiceRequestViewModel> SelectedServices { get; set; }
}

public class ServiceRequestViewModel
{
  public int ServiceId { get; set; }
}