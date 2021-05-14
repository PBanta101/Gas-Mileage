using GasMileage.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GasMileage.Controllers
{
   public class FillupController : Controller
   {
      //   F i e l d s   &   P r o p e r t i e s

      private readonly IFillupRepository _repository;
      private readonly IUserRepository   _userRepository;


      //   C o n s t r u c t o r s

      public FillupController(IFillupRepository repository, IUserRepository userRepository)
      {
         _repository     = repository;
         _userRepository = userRepository;
      }


      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Create(Guid vehicleId)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         return View(new Fillup { Date = DateTime.Now.Date, VehicleId = vehicleId });
      }

      [HttpPost]
      public IActionResult Create(Fillup f)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         if (ModelState.IsValid)
         {
            _repository.Create(f);
            return RedirectToAction("Details", new { id = f.Id });
         }

         return View(f);
      }


      //   R e a d

      public IActionResult Details(Guid id)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         Fillup f = _repository.GetFillupById(id);
         if (f == null)
            return RedirectToAction("Index", "Vehicle");

         return View(f);
      }


      //   U p d a t e

      [HttpGet]
      public IActionResult Edit(Guid id)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         Fillup f = _repository.GetFillupById(id);
         return View(f);
      }

      [HttpPost]
      public IActionResult Edit(Fillup f)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         if (ModelState.IsValid)
         {
            _repository.Update(f);
            return RedirectToAction("Details", new { id = f.Id });
         }

         return View(f);
      }


      //   D e l e t e

      [HttpGet]
      public IActionResult Delete(Guid id)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         Fillup f = _repository.GetFillupById(id);
         if (f == null)
            return RedirectToAction("Index", "Vehicle");

         return View(f);
      }

      [HttpPost]
      public IActionResult Delete(Fillup f)
      {
         if (_userRepository.IsUserLoggedIn() == false)
            return RedirectToAction("Index", "Home");

         Guid vehicleId = f.VehicleId;
         _repository.Delete(f);
         return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
      }
   }
}
