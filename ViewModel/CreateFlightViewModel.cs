using System.ComponentModel.DataAnnotations;

namespace BookingSite.ViewModel
{
    public class CreateFareClassViewModel
    {
        public string ClassName { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }
    }

    public class CreateFlightViewModel
    {
        public int PlaneID { get; set; }
        public DateOnly FlightDate { get; set; }
        public int DepartureAirportID { get; set; }
        public int ArrivalAirportID { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public string Status { get; set; }

        public List<CreateFareClassViewModel> FareClasses { get; set; }
        public int PlaneCapacity { get; set; } // Để kiểm tra tổng số ghế
    }

}
