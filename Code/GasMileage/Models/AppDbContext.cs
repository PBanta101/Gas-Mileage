using Microsoft.EntityFrameworkCore;

namespace GasMileage.Models
{
   public class AppDbContext
      : DbContext
   {
      //   F i e l d s   &   P r o p e r t i e s

      // public DbSet<User> Users { get; set; }

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

      } // end OnModelCreating( )
   } // end class
} // end namespace
