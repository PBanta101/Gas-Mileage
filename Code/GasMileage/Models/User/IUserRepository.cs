using System.Linq;

namespace GasMileage.Models
{
   public interface IUserRepository
   {
      //   C r e a t e

      public User Create(User user);


      //   R e a d

      public int GetLoggedInUserId();

      public string GetLoggedInUserName();

      //public User GetUserByEmailAddress(string emailAddress);

      //public User GetUserById(int id);

      public bool IsUserLoggedIn();

      public bool Login(User user);

      public void Logout();


      //   U p d a t e

      public bool ChangePassword(string oldPassword, string newPassword);

      public bool ResetPassword(User user);

      //public User Update(User u);


      //   D e l e t e


      //   M i s c

      public string RandomPassword();

      public string RandomPassword(int length);
   }
}
