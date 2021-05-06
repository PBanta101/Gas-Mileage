using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasMileage.Models
{
   [Table("User", Schema = "Mpg")]
   public class User
   {
      public int    Id           { get; set; }

      [Required(ErrorMessage = "UserName Is Required")]
      public string UserName     { get; set; }

      [Required(ErrorMessage = "Password Is Required")]
      public string Password     { get; set; }

      public string TempPassword { get; set; }

      public bool?  IsAdmin      { get; set; }
   }
}
