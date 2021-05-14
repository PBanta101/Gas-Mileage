using GasMileage.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GasMileage.Controllers
{
   public class VehicleController
      : Controller
   {
      //   F i e l d s   &   P r o p e r t i e s

      private readonly IVehicleRepository _repository;
      private readonly IUserRepository _userRepository;


      //   C o n s t r u c t o r s

      public VehicleController(IVehicleRepository repository, IUserRepository userRepository)
      {
         _repository = repository;
         _userRepository = userRepository;
      } // end VehicleController( )


      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Add()
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         return View(new Vehicle { Year = DateTime.Now.Year });
      } // end Add( )

      [HttpPost]
      public IActionResult Add(Vehicle v)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         if (v.Year < 1885 || v.Year > DateTime.Now.Year + 1)
            ModelState.AddModelError("", $"Year Must Be Between 1885 And {DateTime.Now.Year + 1}");

         if (ModelState.IsValid)
         {
            _repository.Create(v);
            return RedirectToAction("Details", new { id = v.Id });
         }

         return View(v);
      } // end Add( )


      //   R e a d

      public IActionResult Index()
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         return View(_repository.GetAllVehicles().OrderBy(v => v.Year).ThenBy(v => v.Make).ThenBy(v => v.Model));
      } // end Index( )

      public IActionResult Details(Guid id)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         Vehicle v = _repository.GetVehicleById(id);
         if (v == null)
            return RedirectToAction("Index");

         return View(v);
      } // end Details( )


      //   U p d a t e

      [HttpGet]
      public IActionResult Edit(Guid id)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         Vehicle v = _repository.GetVehicleById(id);
         if (v == null)
            return RedirectToAction("Index", "Vehicle");

         return View(v);
      } // end Edit( )

      [HttpPost]
      public IActionResult Edit(Vehicle v)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         if (v.Year < 1885 || v.Year > DateTime.Now.Year + 1)
            ModelState.AddModelError("", $"Year Must Be Between 1885 And {DateTime.Now.Year + 1}");

         if (ModelState.IsValid)
         {
            _repository.Update(v);
            return RedirectToAction("Details", new { id = v.Id });
         }

         return View(v);
      } // end Edit( )


      //   D e l e t e

      [HttpGet]
      public IActionResult Delete(Guid id)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         Vehicle v = _repository.GetVehicleById(id);
         if (v == null)
            return RedirectToAction("Index");

         return View(v);
      } // end Delete( )

      [HttpPost]
      public IActionResult Delete(Vehicle v)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         _repository.Delete(v.Id);
         return RedirectToAction("Index");
      } // end Delete( )
   }
}
