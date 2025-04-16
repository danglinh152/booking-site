using Microsoft.EntityFrameworkCore;
using BookingSite.Models;

public class FlightBookingContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Plane> Planes { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<FareClass> FareClasses { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<ExtraService> ExtraServices { get; set; }
    public DbSet<BookingService> BookingServices { get; set; }
    public DbSet<Payment> Payments { get; set; }


    public FlightBookingContext(DbContextOptions<FlightBookingContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Booking → Flight (many-to-one)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Flight)
            .WithMany(f => f.Bookings)
            .HasForeignKey(b => b.FlightID)
            .OnDelete(DeleteBehavior.Restrict);

        // Booking → FareClass (many-to-one)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.FareClass)
            .WithMany(fc => fc.Bookings)
            .HasForeignKey(b => b.FareClassID)
            .OnDelete(DeleteBehavior.Restrict);

        // Booking → User (optional many-to-one)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserID)
            .OnDelete(DeleteBehavior.SetNull);

        // Booking → Payment (one-to-one)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Payment)
            .WithOne(p => p.Booking)
            .HasForeignKey<Payment>(p => p.BookingID)
            .OnDelete(DeleteBehavior.Cascade); // chỉ cascade 1 nơi duy nhất

        // Booking → Passengers (one-to-many)
        modelBuilder.Entity<Passenger>()
            .HasOne(p => p.Booking)
            .WithMany(b => b.Passengers)
            .HasForeignKey(p => p.BookingID)
            .OnDelete(DeleteBehavior.Cascade); // hoặc Restrict nếu cần

        // Booking → BookingServices (one-to-many)
        modelBuilder.Entity<BookingService>()
            .HasOne(bs => bs.Booking)
            .WithMany(b => b.BookingServices)
            .HasForeignKey(bs => bs.BookingID)
            .OnDelete(DeleteBehavior.Cascade); // hoặc Restrict
        modelBuilder.Entity<Flight>()
            .HasOne(f => f.DepartureAirport)
            .WithMany(a => a.DepartureFlights)
            .HasForeignKey(f => f.DepartureAirportID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.ArrivalAirport)
            .WithMany(a => a.ArrivalFlights)
            .HasForeignKey(f => f.ArrivalAirportID)
            .OnDelete(DeleteBehavior.Restrict);
    }


}
