using GasMileage.Models;
using Microsoft.AspNetCore.Mvc;

namespace GasMileage.Controllers
{
   public class UserController
      : Controller
   {
      //   F i e l d s   &   P r o p e r t i e s

      private readonly IEmailRepository _emailRepository;
      private readonly IUserRepository _repository;


      //   C o n s t r u c t o r s

      public UserController(IUserRepository repository, IEmailRepository emailRepository)
      {
         _emailRepository = emailRepository;
         _repository = repository;
      }


      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Register()
      {
         return View(new User { Password = "tihSbmuD" });
      }

      [HttpPost]
      public IActionResult Register(User u)
      {
         u.Password = _repository.RandomPassword();
         User newUser = _repository.Create(u);
         if (newUser != null)
         {
            _emailRepository.Send(u.UserName, "Account Created", $"Password: {u.Password}");
            return View("RegisterSuccess", u);
         }

         u.Password = "tihSbmuD";
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
         return View(new User { Password = "tihSbmuD" });
      }

      [HttpPost]
      public IActionResult ResetPassword(User u)
      {
         u.TempPassword = _repository.RandomPassword();
         if (_repository.ResetPassword(u))
         {
            _emailRepository.Send(u.UserName, "Password Reset", $"Password: {u.TempPassword}");
            return View("ResetPasswordSuccess", u);
         }

         ModelState.AddModelError("", "Unable To Reset Password");
         return View(u);
      }


      //   D e l e t e

   }
}
