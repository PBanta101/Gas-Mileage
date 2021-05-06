using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasMileage.Models
{
   [Table("User", Schema = "Mpg")]
   public class User
   {
      public int    Id           { get; set; }

      [Column(TypeName = "nvarchar(128)")]
      [Required(ErrorMessage = "UserName Is Required")]
      public string UserName     { get; set; }

      [Column(TypeName = "nvarchar(128)")]
      [Required(ErrorMessage = "Password Is Required")]
      public string Password     { get; set; }

      [Column(TypeName = "nvarchar(128)")]
      public string TempPassword { get; set; }

      public bool?  IsAdmin      { get; set; }
   }
}
