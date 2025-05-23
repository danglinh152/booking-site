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
    public int ArrivalID { get; set; }
    public DateTime? DepartureDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    [Required]
    public int PassengerNumber { get; set; }

    public List<FlightSegment> flightSegments = new List<FlightSegment>();

  }

}