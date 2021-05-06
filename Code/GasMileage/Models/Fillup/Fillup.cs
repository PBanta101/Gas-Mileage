using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasMileage.Models
{
   [Table("Fillup", Schema = "Mpg")]
   public class Fillup
   {
      public int      Id                  { get; set; }

      [DataType(DataType.Date)]
      [UIHint("date")]
      public DateTime Date                { get; set; }

      [Column(TypeName = "decimal(7, 3)")]
      [UIHint("number")]
      public float    Gallons             { get; set; } // 9,999.999

      [UIHint("number")]
      public int      Odometer            { get; set; }

      [Column(TypeName = "decimal(7, 2)")]
      [UIHint("number")]
      public float    TotalCost           { get; set; } // 99,999.99

      [Column(TypeName = "decimal(7, 1)")]
      [UIHint("number")]
      public float    TripOdometer        { get; set; } // 200,000.0

      public int      DaysSinceLastFillup { get; set; }

      public int      VehicleId           { get; set; }

      public Vehicle  Vehicle             { get; set; }
   }
}
