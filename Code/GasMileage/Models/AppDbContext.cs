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

         // PBanta101@GMail.Com
         modelBuilder.Entity<User>().HasData(
            new User
            {
               Id       = 1,
               UserName = "85FDFD0FB6DFE3AFED031983A1EAEC69ADB8E91CFCEB9FA3EBFAA6984C1E564541CCA57A965FD4C6ACF6632EB0130F42F70E4E52EA038B111B6E16461F2165CD",
               Password = "F1E11932AD24C091A7F52C56296AE5137DF23BCE6E81306D6BE9343CB1C81F68DC967B80490A4E1178273B4A460A6559BF7ACFEFBCD2A292D599869DB28E89CD",
               IsAdmin  = true
            });


         //   V e h i c l e s


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
      } // end OnModelCreating( )
   } // end class
} // end namespace
