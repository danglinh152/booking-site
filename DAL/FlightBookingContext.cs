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
    public DbSet<Payment> Payments { get; set; }


    public FlightBookingContext(DbContextOptions<FlightBookingContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // BookingDetail → Booking (many-to-one)
        modelBuilder.Entity<BookingDetail>()
            .HasOne(b => b.Booking)
            .WithMany(f => f.BookingDetails)
            .HasForeignKey(b => b.BookingID)
            .OnDelete(DeleteBehavior.Cascade);

        //  BookingDetail → Passenger (one-to-one)
        modelBuilder.Entity<BookingDetail>()
            .HasOne(bd => bd.Passenger)
            .WithOne(p => p.BookingDetail)
            .HasForeignKey<BookingDetail>(bd => bd.PassengerID)
            .OnDelete(DeleteBehavior.Cascade);

        //  BookingDetail → FareClass (one-to-one)
        modelBuilder.Entity<BookingDetail>()
            .HasOne(bd => bd.FareClass)
            .WithOne(p => p.BookingDetail)
            .HasForeignKey<BookingDetail>(bd => bd.FareClassID)
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

        // Configure many-to-many relationship
        modelBuilder.Entity<BookingService>()
            .HasOne(bs => bs.BookingDetail)
            .WithMany(bd => bd.BookingServices)
            .HasForeignKey(bs => bs.BookingDetailID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BookingService>()
            .HasOne(bs => bs.Service)
            .WithMany(s => s.BookingServices)
            .HasForeignKey(bs => bs.ServiceID)
            .OnDelete(DeleteBehavior.Restrict);
    }


}
