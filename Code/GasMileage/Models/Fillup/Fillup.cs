﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasMileage.Models
{
   [Table("Fillup", Schema = "Mpg")]
   public class Fillup
   {
      public Guid Id { get; set; }

      [Column(TypeName = "date")]
      [DataType(DataType.Date)]
      [Required(ErrorMessage = "Date Is Required")]
      [UIHint("date")]
      public DateTime Date { get; set; }

      [Column(TypeName = "decimal(7, 3)")] // 9,999.999
      [Range(0.001, 99999.999, ErrorMessage = "Gallons Is Out Of Range")]
      [Required(ErrorMessage = "Gallons Is Required")]
      [UIHint("number")]
      public float Gallons { get; set; } // 9,999.999

      [Range(0, 1000000, ErrorMessage = "Odometer Is Out Of Range")]
      [Required(ErrorMessage = "Odometer Is Required")]
      [UIHint("number")]
      public int Odometer { get; set; }

      [Column(TypeName = "decimal(7, 2)")] // $99,999.99
      [Range(0.01, 99999.99, ErrorMessage = "Total Cost Is Out Of Range")]
      [Required(ErrorMessage = "Total Cost Is Required")]
      [UIHint("number")]
      public float TotalCost { get; set; } // 99,999.99

      [Column(TypeName = "decimal(6, 1)")] // 99,999.9
      [Range(0.1, 99999.9, ErrorMessage = "Trip Odometer Is Out Of Range")]
      [Required(ErrorMessage = "Trip Odometer Is Required")]
      [UIHint("number")]
      public float TripOdometer { get; set; } // 200,000.0

      [Range(1, 99999, ErrorMessage = "Zip Code Is Out Of Range")]
      [UIHint("number")]
      public int? ZipCode { get; set; }

      // \\ // \\ // \\ // \\ // \\ // \\ // \\ // \\ // \\

      [BindNever]
      public int DaysSinceLastFillup { get; set; }

      [BindNever]
      [Column(TypeName = "decimal(6, 2)")] // 9,999.99
      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public double GallonsPerDay { get; set; }

      [BindNever]
      [Column(TypeName = "decimal(5, 1)")] // 9,999.9
      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public double MilesPerDay { get; set; }

      [BindNever]
      [Column(TypeName = "decimal(5, 2)")] // 999.99
      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public double MilesPerGallon { get; set; }

      [BindNever]
      [Column(TypeName = "decimal(6, 2)")] // $9,999.99
      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public double PricePerDay { get; set; }

      [BindNever]
      [Column(TypeName = "decimal(5, 3)")] // $99.999
      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public double PricePerGallon { get; set; }

      [BindNever]
      [Column(TypeName = "decimal(4, 2)")] // $99.99
      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public double PricePerMile { get; set; }

      // \\ // \\ // \\ // \\ // \\ // \\ // \\ // \\ // \\

      public Guid VehicleId { get; set; }

      public Vehicle Vehicle { get; set; }
   }
}
