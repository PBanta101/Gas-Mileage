using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GasMileage.Models
{
   public class EfUserRepository
      : IUserRepository
   {
      //   F i e l d s   &   P r o p e r t i e s

      private static readonly SHA512 _hash   = SHA512.Create();
      private static readonly Random _random = new Random();

      private readonly AppDbContext _context;
      private readonly ISession     _session;


      //   C o n s t r u c t o r s

      public EfUserRepository(AppDbContext context, IHttpContextAccessor httpContext)
      {
         _context = context;
         _session = httpContext.HttpContext.Session;
      }


      //   M e t h o d s

      //   C r e a t e

      public bool Create(User user)
      {
         if (user.UserName == null || user.UserName == ""
             || user.Password == null || user.Password == "")
            return false;

         string username = user.UserName;
         string lcUsername = username.ToLower();
         string encUsername = encrypt(lcUsername);
         User existingUser = _context.Users.FirstOrDefault(u => u.UserName == encUsername);
         if (existingUser != null)
            return false;

         string password = user.Password;

         user.Id = new Guid(); ///
         user.UserName = encUsername;
         user.Password = encrypt(lcUsername, password);
         user.TempPassword = null;
         user.IsAdmin = false;

         _context.Users.Add(user);
         _context.SaveChanges();

         user.UserName = username;
         user.Password = password;

         return true;
      } // end Create( )


      //   R e a d

      public User GetUserByEmailAddress(string emailAddress)
      {
         User u = _context.Users.FirstOrDefault(u => u.UserName == encrypt(emailAddress.ToLower()));

         if (u != null)
            u.Password = u.TempPassword = null;

         return u;
      } // end GetUserByEmailAddress( )

      public User GetUserById(Guid id)
      {
         User u = _context.Users.Find(id);

         if (u != null)
            u.Password = u.TempPassword = null;

         return u;
      } // end GetUserById( )

      public bool IsUserLoggedIn()
      {
         return _session.GetInt32("userid") != null;
      } // end IsUserLoggedIn( )

      public bool Login(User user)
      {
         if (user.UserName == null || user.UserName == ""
             || user.Password == null || user.Password == "")
            return false;

         string username = user.UserName;
         string lcUsername = username.ToLower();
         string encUsername = encrypt(lcUsername);

         User existingUser = _context.Users.FirstOrDefault(u => u.UserName == encUsername);
         if (existingUser == null)
            return false;

         string password = user.Password;
         string encPassword = encrypt(lcUsername, password);
         if (existingUser.Password != encPassword && existingUser.TempPassword != encPassword)
            return false;

         _session.SetString("userid", existingUser.Id.ToString());
         _session.SetString("username", username);
         return true;
      } // end Login( )

      public void Logout()
      {
         _session.Remove("userid");
         _session.Remove("username");
      } // end Logout( )

      public Guid GetLoggedInUserId()
      {
         string userId = _session.GetString("userid");
         return userId == null ? default : new Guid(userId);
      } // end GetLoggedInUserId( )

      public string GetLoggedInUserName()
      {
         return _session.GetString("username");
      } // end GetLoggedInUserName( )

      public bool UserExists(string emailAddress)
      {
         if (emailAddress == null || emailAddress == "")
            return false;

         return _context.Users.Any(u => u.UserName == encrypt(emailAddress.ToLower()));
      } // end UserExists( )


      //   U p d a t e

      public bool ChangePassword(string oldPassword, string newPassword)
      {
         if (oldPassword == null || oldPassword == ""
             || newPassword == null || newPassword == "")
            return false;

         if (IsUserLoggedIn() == false)
            return false;

         User userToUpdate = _context.Users.Find(GetLoggedInUserId());
         if (userToUpdate == null)
            return false;

         string lcUsername = GetLoggedInUserName().ToLower();
         string encOldPassword = encrypt(lcUsername, oldPassword);
         if (userToUpdate.Password != encOldPassword && userToUpdate.TempPassword != encOldPassword)
            return false;

         userToUpdate.Password = encrypt(lcUsername, newPassword);
         userToUpdate.TempPassword = null;
         _context.SaveChanges();
         return true;
      } // end ChangePassword( )

      public bool ResetPassword(User user)
      {
         if (user == null)
            return false;

         if (user.UserName == null || user.UserName == ""
            || user.TempPassword == null || user.TempPassword == "")
            return false;

         string lcUsername = user.UserName.ToLower();
         string encUsername = encrypt(lcUsername);
         User userToUpdate = _context.Users.FirstOrDefault(u => u.UserName == encUsername);
         if (userToUpdate == null)
            return false;

         userToUpdate.TempPassword = encrypt(lcUsername, user.TempPassword);
         _context.SaveChanges();
         return true;
      }

      public User Update(User u)
      {
         User userToUpdate = GetUserById(u.Id);
         if (userToUpdate != null)
         {
            userToUpdate.IsAdmin = u.IsAdmin;
            _context.SaveChanges();
         }
         return userToUpdate;
      } // end Update( )


      //   D e l e t e

      public bool Delete(Guid id)
      {
         User userToDelete = GetUserById(id);
         if (userToDelete == null)
         {
            return false;
         }
         _context.Users.Remove(userToDelete);
         _context.SaveChanges();
         return true;
      } // end Delete( )

      public bool Delete(User u)
      {
         return Delete(u.Id);
      } // end Delete( )


      //   M i s c

      public string RandomPassword()
      {
         return RandomPassword(2 * _random.Next(3, 5) + 1);
      } // end RandomPassword( )

      public string RandomPassword(int length)
      {
         string result = "";
         for (int i = 0; i < length; i++)
         {
            result += (char)_random.Next(33, 126);
         }
         return result;
      } // end RandomPassword( )


      //   P r i v a t e   M e t h o d s

      private string encrypt(string str)
      {
         byte[] strBytes = Encoding.ASCII.GetBytes(str);
         byte[] encBytes = _hash.ComputeHash(strBytes);
         string encString = BitConverter.ToString(encBytes).Replace("-", "");
         return encString;
      } // end encrypt( )

      private string encrypt(string lcUsername, string password)
      {
         byte[] usernameBytes = Encoding.ASCII.GetBytes(lcUsername);
         byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
         int length = Math.Max(usernameBytes.Length, passwordBytes.Length);
         byte[] saltedBytes = new byte[length];
         for (int b = 0; b < length; b++)
         {
            saltedBytes[b] = (byte)(usernameBytes[b % usernameBytes.Length] ^ passwordBytes[b % passwordBytes.Length]);
         }
         byte[] hashedBytes = _hash.ComputeHash(saltedBytes);
         return BitConverter.ToString(hashedBytes).Replace("-", "");
      } // end encrypt( )
   }
}
