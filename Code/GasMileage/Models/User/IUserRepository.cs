using System;

namespace GasMileage.Models
{
   public interface IUserRepository
   {
      //   C r e a t e

      public bool Create(User user);


      //   R e a d

      public Guid GetLoggedInUserId();

      public string GetLoggedInUserName();

      public bool IsUserLoggedIn();

      public bool Login(User user);

      public void Logout();

      public bool UserExists(string emailAddress);


      //   U p d a t e

      public bool ChangePassword(string oldPassword, string newPassword);

      public bool ResetPassword(User user);


      //   D e l e t e


      //   M i s c

      public string RandomPassword();

      public string RandomPassword(int length);
   }
}
