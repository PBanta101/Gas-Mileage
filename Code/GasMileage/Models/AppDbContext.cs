using Microsoft.EntityFrameworkCore;

namespace GasMileage.Models
{
   public class AppDbContext
      : DbContext
   {
      //   F i e l d s   &   P r o p e r t i e s

      public DbSet<Fillup>  Fillups  { get; set; }
      public DbSet<User>    Users    { get; set; }
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

         // modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "BantaP@ERAU.Edu", Password = "Wombat1" });


         //   V e h i c l e s

         modelBuilder.Entity<Vehicle>().HasData(new Vehicle
         {
            Id = 1,
            Year = 1985,
            Make = "Toyota",
            Model = "Pickup",
            Color = "White",
            Vin = "JT4RN...",
            UserId = 1
         });

         modelBuilder.Entity<Vehicle>().HasData(new Vehicle
         {
            Id = 2,
            Year = 1995,
            Make = "Saturn",
            Model = "SW2",
            Color = "Gold"
         });


         //   F i l l u p s

         modelBuilder.Entity<Fillup>().HasData(new Fillup
         {
            Id = 1,
            Date = new System.DateTime(0L),
            Gallons = 10.234f,
            Odometer = 12345,
            TotalCost = 25.52f,
            TripOdometer = 275.6f,
            VehicleId = 1
         });


         modelBuilder.Entity<Fillup>().HasData(new Fillup
         {
            Id = 2,
            Date = new System.DateTime(2021, 05, 01),
            Gallons = 10.234f,
            Odometer = 12700,
            TotalCost = 25.52f,
            TripOdometer = 275.6f,
            VehicleId = 1
         });

      } // end OnModelCreating( )
   } // end class
} // end namespace
