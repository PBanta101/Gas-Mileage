using GasMileage.Models;
using Microsoft.AspNetCore.Mvc;

namespace GasMileage.Controllers
{
   public class UserController
      : Controller
   {
      //   F i e l d s   &   P r o p e r t i e s

      private IUserRepository _repository;


      //   C o n s t r u c t o r s

      public UserController(IUserRepository repository)
      {
         _repository = repository;
      }


      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Register()
      {
         return View(new User { Password = _repository.RandomPassword() });
      }

      [HttpPost]
      public IActionResult Register(User u)
      {
         User newUser = _repository.Create(u);
         if (newUser != null)
            return RedirectToAction("Index", "Home");

         ModelState.AddModelError("", "Unable To Register New User");
         return View(u);
      }


      //   R e a d

      [HttpGet]
      public IActionResult Login()
      {
         return View();
      }

      [HttpPost]
      public IActionResult Login(User u)
      {
         if (_repository.Login(u))
            return RedirectToAction("Index", "Home");

         ModelState.AddModelError("", "Unable To Log In");
         return View(u);
      }

      public IActionResult Logout()
      {
         _repository.Logout();
         return RedirectToAction("Index", "Home");
      }


      //   U p d a t e

      [HttpGet]
      public IActionResult ChangePassword()
      {
         return View();
      }

      [HttpPost]
      public IActionResult ChangePassword(UserChangePasswordViewModel ucpvm)
      {
         if (ModelState.IsValid)
         {
            if (_repository.ChangePassword(ucpvm.CurrentPassword, ucpvm.NewPassword))
            {
               return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Unable To Change Password");
            return View(ucpvm);
         }

         return View(ucpvm);
      }


      [HttpGet]
      public IActionResult ResetPassword()
      {
         return View(new User { Password = _repository.RandomPassword() });
      }

      [HttpPost]
      public IActionResult ResetPassword(User u)
      {
         if (ModelState.IsValid)
         {
            u.TempPassword = u.Password;
            u.Password = null;
            if (_repository.ResetPassword(u))
            {
               return RedirectToAction("Index", "Home");
            }

            u.Password = u.TempPassword;
            u.TempPassword = null;

            ModelState.AddModelError("", "Unable To Reset Password");
         }

         return View(u);
      }


      //   D e l e t e

   }
}
