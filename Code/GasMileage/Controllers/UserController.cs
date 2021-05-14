using GasMileage.Models;
using Microsoft.AspNetCore.Mvc;

namespace GasMileage.Controllers
{
   public class UserController
      : Controller
   {
      //   F i e l d s   &   P r o p e r t i e s

      private readonly IUserRepository _repository;
      private readonly IEmailRepository _emailRepository;


      //   C o n s t r u c t o r s

      public UserController(IUserRepository repository, IEmailRepository emailRepository)
      {
         _repository = repository;
         _emailRepository = emailRepository;
      } // end UserController( )


      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Register()
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Vehicle");

         return View();
      } // end Register( )

      [HttpPost]
      public IActionResult Register(string username)
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Vehicle");

         if (_repository.UserExists(username) == false)
         {
            string randomPassword = _repository.RandomPassword();
            if (_repository.Create(new Models.User { UserName = username, Password = randomPassword }))
            {
               _emailRepository.Send(username, "Account Created", $"Password: {randomPassword}");
               return View("RegisterSuccess", username);
            }
         }

         ModelState.AddModelError("", "Unable To Register New User");
         return View();
      } // end Register( )


      //   R e a d

      [HttpGet]
      public IActionResult Login()
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Vehicle");

         return View();
      } // end Login( )

      [HttpPost]
      public IActionResult Login(User u)
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Vehicle");

         if (_repository.Login(u))
            return RedirectToAction("Index", "Vehicle");

         ModelState.AddModelError("", "Unable To Log In");
         return View(u);
      } // end Login( )

      public IActionResult Logout()
      {
         _repository.Logout();
         return RedirectToAction("Index", "Home");
      } // end Logout( )


      //   U p d a t e

      [HttpGet]
      public IActionResult ChangePassword()
      {
         if (_repository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         return View();
      } // end ChangePassword( )

      [HttpPost]
      public IActionResult ChangePassword(string oldPassword, string newPassword, string verifyNewPassword)
      {
         if (_repository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         if (oldPassword == null || oldPassword == "")
            ModelState.AddModelError("", "Current Password Is Required");

         if (newPassword == null || newPassword == "")
            ModelState.AddModelError("", "New Password Is Required");

         if (verifyNewPassword == null || verifyNewPassword == "")
            ModelState.AddModelError("", "Verify New Password Is Required");

         if (newPassword != verifyNewPassword)
            ModelState.AddModelError("", "The New Passwords Don't Match");

         if (ModelState.IsValid)
         {
            if (_repository.ChangePassword(oldPassword, newPassword))
               return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Unable To Change Password");
         }

         return View();
      } // end ChangePassword( )


      [HttpGet]
      public IActionResult ResetPassword()
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Vehicle");

         return View();
      } // end ResetPassword( )

      [HttpPost]
      public IActionResult ResetPassword(string username)
      {
         if (_repository.IsUserLoggedIn())
            return RedirectToAction("Index", "Vehicle");

         string tempPassword = _repository.RandomPassword();
         if (_repository.ResetPassword(new User { UserName = username, TempPassword = tempPassword }))
         {
            _emailRepository.Send(username, "Password Reset", $"Password: {tempPassword}");
            return View("ResetPasswordSuccess", username);
         }

         ModelState.AddModelError("", "Unable To Reset Password");
         return View();
      } // end ResetPassword( )


      //   D e l e t e

   }
}
