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

        // Seed Airport data
        modelBuilder.Entity<Airport>().HasData(
            new Airport
            {
                AirportID = 1,
                Name = "Sân bay quốc tế Tân Sơn Nhất",
                City = "TP. Hồ Chí Minh",
                AirportCode = "SGN"
            },
            new Airport
            {
                AirportID = 2,
                Name = "Sân bay quốc tế Nội Bài",
                City = "Hà Nội",
                AirportCode = "HAN"
            },
            new Airport
            {
                AirportID = 3,
                Name = "Sân bay quốc tế Vân Đồn, Hạ Long",
                City = "Quảng Ninh",
                AirportCode = "VDO"
            },
            new Airport
            {
                AirportID = 4,
                Name = "Sân bay quốc tế Phú Quốc",
                City = "Kiên Giang",
                AirportCode = "PQC"
            },
            new Airport
            {
                AirportID = 5,
                Name = "Sân bay Quốc tế Cát Bi",
                City = "Hải Phòng",
                AirportCode = "HPH"
            },
            new Airport
            {
                AirportID = 6,
                Name = "Sân bay Quốc tế Vinh",
                City = "Nghệ An",
                AirportCode = "VII"
            },
            new Airport
            {
                AirportID = 7,
                Name = "Sân bay Thọ Xuân",
                City = "Thanh Hóa",
                AirportCode = "THD"
            },
            new Airport
            {
                AirportID = 8,
                Name = "Sân bay Quốc tế Cam Ranh",
                City = "Khánh Hòa",
                AirportCode = "CXR"
            },
            new Airport
            {
                AirportID = 9,
                Name = "Sân bay Quốc tế Phú Bài",
                City = "Thừa Thiên Huế",
                AirportCode = "HUI"
            },
            new Airport
            {
                AirportID = 10,
                Name = "Sân bay Quốc tế Phù Cát",
                City = "Bình Định",
                AirportCode = "UIH"
            },
            new Airport
            {
                AirportID = 11,
                Name = "Sân bay Quốc tế Liên Khương",
                City = "Lâm Đồng",
                AirportCode = "DLI"
            },
            new Airport
            {
                AirportID = 12,
                Name = "Sân bay Quốc tế Cần Thơ",
                City = "Cần Thơ",
                AirportCode = "VCA"
            },
            new Airport
            {
                AirportID = 13,
                Name = "Sân bay Điện Biên Phủ",
                City = "Điện Biên",
                AirportCode = "DIN"
            },
            new Airport
            {
                AirportID = 14,
                Name = "Sân bay Đồng Hới",
                City = "Quảng Bình",
                AirportCode = "VDH"
            },
            new Airport
            {
                AirportID = 15,
                Name = "Sân bay Chu Lai",
                City = "Quảng Nam",
                AirportCode = "VCL"
            },
            new Airport
            {
                AirportID = 16,
                Name = "Sân bay Rạch Giá",
                City = "Kiên Giang",
                AirportCode = "VKG"
            },
            new Airport
            {
                AirportID = 17,
                Name = "Sân bay Cà Mau",
                City = "Cà Mau",
                AirportCode = "CAH"
            }
        );

        // Seed ExtraService data
        modelBuilder.Entity<ExtraService>().HasData(
            new ExtraService
            {
                ServiceID = 1,
                ServiceName = "Hành lý 15kg",
                Description = "Thêm hành lý ký gửi 15kg",
                Price = 150000,
                ServiceType = "Luggage"
            },
            new ExtraService
            {
                ServiceID = 2,
                ServiceName = "Hành lý 20kg",
                Description = "Thêm hành lý ký gửi 20kg",
                Price = 200000,
                ServiceType = "Luggage"
            },
            new ExtraService
            {
                ServiceID = 3,
                ServiceName = "Suất ăn tiêu chuẩn",
                Description = "Bữa ăn nóng và nước uống",
                Price = 120000,
                ServiceType = "Meal"
            },
            new ExtraService
            {
                ServiceID = 4,
                ServiceName = "Suất ăn đặc biệt",
                Description = "Bữa ăn đặc biệt và nước uống",
                Price = 200000,
                ServiceType = "Meal"
            }
        );


        // Seed Plane data
        modelBuilder.Entity<Plane>().HasData(
            new Plane
            {
                PlaneID = 1,
                Model = "Airbus A320",
                Manufacturer = "Airbus",
                Capacity = 180
            },
            new Plane
            {
                PlaneID = 2,
                Model = "Airbus A321",
                Manufacturer = "Airbus",
                Capacity = 220
            },
            new Plane
            {
                PlaneID = 3,
                Model = "Boeing 787-9",
                Manufacturer = "Boeing",
                Capacity = 300
            },
            new Plane
            {
                PlaneID = 4,
                Model = "Airbus A350-900",
                Manufacturer = "Airbus",
                Capacity = 350
            },
            new Plane
            {
                PlaneID = 5,
                Model = "ATR 72",
                Manufacturer = "ATR",
                Capacity = 70
            }
        );

        // Seed Flight data
        modelBuilder.Entity<Flight>().HasData(
            // SGN -> HAN Flights
            new Flight
            {
                FlightID = 1,
                PlaneID = 1,  // Airbus A320
                DepartureTime = new TimeSpan(7, 0, 0),  // 07:00
                ArrivalTime = new TimeSpan(9, 10, 0),   // 09:10
                FlightDate = new DateOnly(2025, 6, 5),
                DepartureAirportID = 1,  // SGN
                ArrivalAirportID = 2,    // HAN
                Status = "Scheduled"
            },
            new Flight
            {
                FlightID = 2,
                PlaneID = 2,  // Airbus A321
                DepartureTime = new TimeSpan(13, 30, 0), // 13:30
                ArrivalTime = new TimeSpan(15, 40, 0),   // 15:40
                FlightDate = new DateOnly(2025, 6, 4),
                DepartureAirportID = 1,  // SGN
                ArrivalAirportID = 2,    // HAN
                Status = "Scheduled"
            },

            // HAN -> SGN Flights
            new Flight
            {
                FlightID = 3,
                PlaneID = 1,  // Airbus A320
                DepartureTime = new TimeSpan(10, 0, 0),  // 10:00
                ArrivalTime = new TimeSpan(12, 10, 0),   // 12:10
                FlightDate = new DateOnly(2025, 6, 3),
                DepartureAirportID = 2,  // HAN
                ArrivalAirportID = 1,    // SGN
                Status = "Scheduled"
            },

            // SGN -> DAD Flights
            new Flight
            {
                FlightID = 4,
                PlaneID = 5,  // ATR 72
                DepartureTime = new TimeSpan(8, 0, 0),   // 08:00
                ArrivalTime = new TimeSpan(9, 20, 0),    // 09:20
                FlightDate = new DateOnly(2025, 6, 2),
                DepartureAirportID = 1,  // SGN
                ArrivalAirportID = 3,    // DAD
                Status = "Scheduled"
            },

            // SGN -> PQC Flights
            new Flight
            {
                FlightID = 5,
                PlaneID = 5,  // ATR 72
                DepartureTime = new TimeSpan(6, 0, 0),   // 06:00
                ArrivalTime = new TimeSpan(7, 0, 0),     // 07:00
                FlightDate = new DateOnly(2025, 6, 6),
                DepartureAirportID = 1,  // SGN
                ArrivalAirportID = 4,    // PQC
                Status = "Scheduled"
            }
        );

        // Seed FareClass data
        modelBuilder.Entity<FareClass>().HasData(
            // Flight 1: SGN -> HAN (Morning flight)
            new FareClass
            {
                FareClassID = 1,
                FlightID = 1,
                ClassName = "Economy",
                Price = 1500000,
                SeatsAvailable = 150
            },
            new FareClass
            {
                FareClassID = 2,
                FlightID = 1,
                ClassName = "Business",
                Price = 3500000,
                SeatsAvailable = 20
            },

            // Flight 2: SGN -> HAN (Afternoon flight)
            new FareClass
            {
                FareClassID = 3,
                FlightID = 2,
                ClassName = "Economy",
                Price = 1800000,
                SeatsAvailable = 180
            },
            new FareClass
            {
                FareClassID = 4,
                FlightID = 2,
                ClassName = "Business",
                Price = 4000000,
                SeatsAvailable = 28
            },

            // Flight 3: HAN -> SGN
            new FareClass
            {
                FareClassID = 5,
                FlightID = 3,
                ClassName = "Economy",
                Price = 1600000,
                SeatsAvailable = 150
            },
            new FareClass
            {
                FareClassID = 6,
                FlightID = 3,
                ClassName = "Business",
                Price = 3800000,
                SeatsAvailable = 20
            },

            // Flight 4: SGN -> DAD
            new FareClass
            {
                FareClassID = 7,
                FlightID = 4,
                ClassName = "Economy",
                Price = 1200000,
                SeatsAvailable = 60
            },
            new FareClass
            {
                FareClassID = 8,
                FlightID = 4,
                ClassName = "Business",
                Price = 2500000,
                SeatsAvailable = 10
            },

            // Flight 5: SGN -> PQC
            new FareClass
            {
                FareClassID = 9,
                FlightID = 5,
                ClassName = "Economy",
                Price = 800000,
                SeatsAvailable = 60
            },
            new FareClass
            {
                FareClassID = 10,
                FlightID = 5,
                ClassName = "Business",
                Price = 1800000,
                SeatsAvailable = 10
            }
        );
    }


}
