using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasMileage.Models
{
   [Table("Vehicle", Schema = "Mpg")]
   public class Vehicle
   {
      //   F i e l d s   &   P r o p e r t i e s

      public Guid Id { get; set; }

      [Required(ErrorMessage = "Year Is Required")]
      public int Year { get; set; }

      [MaxLength(20, ErrorMessage = "Make Can Only Be 20 Characters Long")]
      [Required(ErrorMessage = "Make Is Required")]
      public string Make { get; set; }

      [MaxLength(20, ErrorMessage = "Model Can Only Be 20 Characters Long")]
      [Required(ErrorMessage = "Model Is Required")]
      public string Model { get; set; }

      [MaxLength(20, ErrorMessage = "Vin Number Can Only Be 20 Characters Long")]
      public string Vin { get; set; }

      [MaxLength(20, ErrorMessage = "Color Can Only Be 20 Characters Long")]
      public string Color { get; set; }

      public IEnumerable<Fillup> Fillups { get; set; }

      public Guid UserId { get; set; }


      //   C o n s t r u c t o r s


      //   M e t h o d s

   }
}
