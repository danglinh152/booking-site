using System.ComponentModel.DataAnnotations;
using BookingSite.Models;

namespace BookingSite.ViewModel
{
  public class FlightSegment
  {
    public string DepartureID { get; set; } = "";
    public string ArrivalID { get; set; } = "";

    [DataType(DataType.Date)]
    public DateTime DepartureDate { get; set; }
  }
  public class SearchFlightViewModel
  {
    public List<Airport> Airports { get; set; } = new List<Airport>();

    [Required(ErrorMessage = "Vui lòng chọn loại chuyến bay")]
    public string SearchType
    { get; set; } = ""; // "oneWay", "roundTrip", "multiCity"
    public int DepartureID { get; set; }
    public string DepartureCode { get; set; } = "";
    public string ArrivalCode { get; set; } = "";
    public int ArrivalID { get; set; }
    public DateOnly? DepartureDate { get; set; }

    public DateOnly? ReturnDate { get; set; }
    [Required]
    public int PassengerNumber { get; set; }

    public List<FlightSegment> flightSegments = new List<FlightSegment>();

    public List<FlightsByDateViewModel> flightsByDateViewModel { get; set; }
  }

  public class FlightsByDateViewModel
  {
    public DateOnly FlightDate { get; set; }
    public string dateName { get; set; }
    public List<Flight> Flights { get; set; }
    public double cheapestPrice { get; set; }
  }

}