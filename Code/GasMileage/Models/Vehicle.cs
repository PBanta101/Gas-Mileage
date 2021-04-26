using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasMileage.Models
{
   [Table("Vehicle", Schema = "Mpg")]
   public class Vehicle // POCO
   {
      //   F i e l d s   &   P r o p e r t i e s

      public int    Id     { get; set; }

      [Required(ErrorMessage = "Year Is Required")]
      public int    Year   { get; set; }

      [Required(ErrorMessage = "Make Is Required")]
      public string Make   { get; set; }

      [Required(ErrorMessage = "Model Is Required")]
      public string Model  { get; set; }

      public string Vin    { get; set; }

      public string Color  { get; set; }

      public int    UserId { get; set; }


      //   C o n s t r u c t o r s


      //   M e t h o d s

   }
}
