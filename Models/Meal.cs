using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Models
{
    public class Meal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MealID { get; set; }
        [Required]
        public int BookingID { get; set; }
        [Required]
        public string MealType { get; set; }
        [Required]
        [Precision(10, 2)]
        public double Price { get; set; }
    }
}
