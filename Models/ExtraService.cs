using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSite.Models
{
    public class ExtraService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } //mua hành lý, chọn chổ ngồi, mua đồ ăn
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ICollection<BookingService> BookingServices { get; set; }
    }
}
