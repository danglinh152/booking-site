namespace BookingSite.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Plane
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AircraftID { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public string Manufacturer { get; set; }
}

