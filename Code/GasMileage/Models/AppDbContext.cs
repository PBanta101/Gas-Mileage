using Microsoft.EntityFrameworkCore;
using System;

namespace GasMileage.Models
{
   public class AppDbContext
      : DbContext
   {
      //   F i e l d s   &   P r o p e r t i e s

      public DbSet<Fillup> Fillups { get; set; }
      public DbSet<User> Users { get; set; }
      public DbSet<Vehicle> Vehicles { get; set; }


      //   C o n s t r u c t o r s

      public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
      {
      }


      //   M e t h o d s

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);


         //   U s e r s

         modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();

         // PBanta101@GMail.Com
         modelBuilder.Entity<User>().HasData(
            new User
            {
               Id = new Guid("469faf7d-507d-456a-5d5c-08d9140bf7c6"),
               UserName = "85FDFD0FB6DFE3AFED031983A1EAEC69ADB8E91CFCEB9FA3EBFAA6984C1E564541CCA57A965FD4C6ACF6632EB0130F42F70E4E52EA038B111B6E16461F2165CD",
               Password = "F1E11932AD24C091A7F52C56296AE5137DF23BCE6E81306D6BE9343CB1C81F68DC967B80490A4E1178273B4A460A6559BF7ACFEFBCD2A292D599869DB28E89CD",
               IsAdmin = true
            });


         //   V e h i c l e s

         modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle
            {
               Id = new Guid("0def444a-db95-47ec-fa5b-08d91575881a"),
               Year = 2002,
               Make = "Subaru",
               Model = "Forester",
               Color = "Red",
               UserId = new Guid("469faf7d-507d-456a-5d5c-08d9140bf7c6")
            });


         //   F i l l u p s

         modelBuilder.Entity<Fillup>()
            .Property(f => f.DaysSinceLastFillup)
            .HasDefaultValue(1);

         modelBuilder.Entity<Fillup>()
            .Property(f => f.GallonsPerDay)
            .HasComputedColumnSql("iif( [DaysSinceLastFillup] > 1, [Gallons] / [DaysSinceLastFillup], [Gallons] )");

         modelBuilder.Entity<Fillup>()
            .Property(f => f.MilesPerDay)
            .HasComputedColumnSql("iif( [DaysSinceLastFillup] > 1, [TripOdometer] / [DaysSinceLastFillup], [TripOdometer] )");

         modelBuilder.Entity<Fillup>()
            .Property(f => f.MilesPerGallon)
            .HasComputedColumnSql("iif( [Gallons] > 0, [TripOdometer] / [Gallons], 999.9 )");

         modelBuilder.Entity<Fillup>()
            .Property(f => f.PricePerDay)
            .HasComputedColumnSql("iif( [DaysSinceLastFillup] > 1, [TotalCost] / [DaysSinceLastFillup], [TotalCost] )");

         modelBuilder.Entity<Fillup>()
            .Property(f => f.PricePerGallon)
            .HasComputedColumnSql("iif( [Gallons] > 0, [TotalCost] / [Gallons], 999.9 )");

         modelBuilder.Entity<Fillup>()
            .Property(f => f.PricePerMile)
            .HasComputedColumnSql("iif( [TripOdometer] > 0, [TotalCost] / [TripOdometer], 999.9 )");

         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("c61c29ad-9be1-4eb3-a9b2-08d91576fae3"), Date = new DateTime(2020, 12, 18), Gallons = 11.104f, Odometer = 147360, TotalCost = 21.64f, TripOdometer = 225.7f, ZipCode = 80132, DaysSinceLastFillup =  1, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("3e6b136b-a2b5-4ed9-a9b1-08d91576fae3"), Date = new DateTime(2020, 12, 23), Gallons =  5.881f, Odometer = 147512, TotalCost = 11.87f, TripOdometer = 151.6f, ZipCode = 80132, DaysSinceLastFillup =  5, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("25a96d7a-ea59-47c5-a9b0-08d91576fae3"), Date = new DateTime(2020, 12, 31), Gallons =  5.549f, Odometer = 147653, TotalCost = 11.20f, TripOdometer = 141.0f, ZipCode = 80132, DaysSinceLastFillup =  8, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("bc2a0ff1-e36f-4ce2-a9af-08d91576fae3"), Date = new DateTime(2021,  1,  5), Gallons =  5.134f, Odometer = 147782, TotalCost = 10.37f, TripOdometer = 129.0f, ZipCode = 80132, DaysSinceLastFillup =  5, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("ff53ce12-e9c3-48bd-a9ae-08d91576fae3"), Date = new DateTime(2021,  1,  8), Gallons =  5.324f, Odometer = 147917, TotalCost = 10.91f, TripOdometer = 135.1f, ZipCode = 80132, DaysSinceLastFillup =  3, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("45f6d3a5-dd36-46bb-a9ad-08d91576fae3"), Date = new DateTime(2021,  2,  2), Gallons =  7.408f, Odometer = 148105, TotalCost = 16.29f, TripOdometer = 187.9f, ZipCode = 80132, DaysSinceLastFillup = 25, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("98f47b0a-02cb-4190-a9ac-08d91576fae3"), Date = new DateTime(2021,  3, 12), Gallons =  7.078f, Odometer = 148263, TotalCost = 19.53f, TripOdometer = 157.8f, ZipCode = 80132, DaysSinceLastFillup = 38, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("89c21c3d-acb0-4d57-a9ab-08d91576fae3"), Date = new DateTime(2021,  3, 19), Gallons =  5.739f, Odometer = 148418, TotalCost = 16.06f, TripOdometer = 155.2f, ZipCode = 80132, DaysSinceLastFillup =  7, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("5e9a7626-db13-44f7-a9aa-08d91576fae3"), Date = new DateTime(2021,  4, 11), Gallons =  8.099f, Odometer = 148612, TotalCost = 22.99f, TripOdometer = 193.7f, ZipCode = 80132, DaysSinceLastFillup = 23, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("a745ed52-28c9-4951-a9a9-08d91576fae3"), Date = new DateTime(2021,  4, 25), Gallons = 10.438f, Odometer = 148884, TotalCost = 29.53f, TripOdometer = 271.3f, ZipCode = 80132, DaysSinceLastFillup = 14, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("510a1007-96e4-4069-a9a8-08d91576fae3"), Date = new DateTime(2021,  4, 30), Gallons =  4.677f, Odometer = 149003, TotalCost = 13.23f, TripOdometer = 119.8f, ZipCode = 80132, DaysSinceLastFillup =  5, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("87354378-e1a9-472f-a9a7-08d91576fae3"), Date = new DateTime(2021,  4, 30), Gallons = 10.138f, Odometer = 149278, TotalCost = 25.33f, TripOdometer = 274.8f, ZipCode = 69153, DaysSinceLastFillup =  1, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("3ba9f92b-9c0a-4ab2-a9a6-08d91576fae3"), Date = new DateTime(2021,  5,  1), Gallons = 12.317f, Odometer = 149587, TotalCost = 34.23f, TripOdometer = 308.2f, ZipCode = 68502, DaysSinceLastFillup =  1, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("b5949bba-d0d7-4183-a9a5-08d91576fae3"), Date = new DateTime(2021,  5,  2), Gallons = 12.192f, Odometer = 149846, TotalCost = 33.88f, TripOdometer = 259.0f, ZipCode = 69101, DaysSinceLastFillup =  1, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("44fd61fc-df99-4eee-a9a4-08d91576fae3"), Date = new DateTime(2021,  5,  4), Gallons = 12.652f, Odometer = 150179, TotalCost = 36.17f, TripOdometer = 333.7f, ZipCode = 80132, DaysSinceLastFillup =  2, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("bd3eb530-2d7e-40e3-a9a3-08d91576fae3"), Date = new DateTime(2021,  5,  7), Gallons =  8.672f, Odometer = 150424, TotalCost = 27.31f, TripOdometer = 244.9f, ZipCode = null,  DaysSinceLastFillup =  3, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
         modelBuilder.Entity<Fillup>().HasData( new Fillup { Id = new Guid("c58885ee-02d6-4bad-34db-08d91576652f"), Date = new DateTime(2021,  5,  9), Gallons =  7.703f, Odometer = 150624, TotalCost = 27.31f, TripOdometer = 199.8f, ZipCode = 80132, DaysSinceLastFillup =  2, VehicleId = new Guid("0def444a-db95-47ec-fa5b-08d91575881a")});
      } // end OnModelCreating( )
   } // end class
} // end namespace
