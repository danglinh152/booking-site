namespace BookingSite.Models
{
    public class Airport
    {
        public int AirportId { get; set; } // ID của sân bay
        public string Name { get; set; } // Tên sân bay
        public string City { get; set; } // Thành phố nơi sân bay tọa lạc
        public string Country { get; set; } // Quốc gia nơi sân bay tọa lạc
    }
}
