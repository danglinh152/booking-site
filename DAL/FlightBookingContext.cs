using Microsoft.EntityFrameworkCore;
using BookingSite.Models;

public class FlightBookingContext : DbContext 
{
  public DbSet<User> Users { get; set; }

  public FlightBookingContext(DbContextOptions<FlightBookingContext> options) : base(options)
  {
  }
}
