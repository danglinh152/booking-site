using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Models
{
    public class ExtraService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceID { get; set; }
        [Required]
        public string ServiceName { get; set; } //mua hành lý, chọn chổ ngồi, mua đồ ăn
        public string Description { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal Price { get; set; }

        public ICollection<BookingService> BookingServices { get; set; }
    }
}
