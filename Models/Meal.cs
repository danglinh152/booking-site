namespace BookingSite.Models
{
    public class Meal
    {
        public int MealID { get; set; }
        public int BookingID { get; set; }
        public string MealType { get; set; }
        public double Price { get; set; }
    }
}
