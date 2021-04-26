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

      private IVehicleRepository _repository;


      //   C o n s t r u c t o r s

      public VehicleController(IVehicleRepository repository)
      {
         _repository = repository;
      }


      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Add()
      {
         return View(new Vehicle { Year = DateTime.Now.Year });
      }

      [HttpPost]
      public IActionResult Add(Vehicle v)
      {
         if (v.Year < 1886)
         {
            ModelState.AddModelError("", "Year Must Be 1886 Or Later");
         }

         if (v.Year > DateTime.Now.Year + 1)
         {
            ModelState.AddModelError("", $"Year Must Be {DateTime.Now.Year + 1} Or Earlier");
         }

         if (ModelState.IsValid)
         {
            _repository.Create(v);
            return RedirectToAction("Details", new { id = v.Id });
         }

         return View(v);
      }


      //   R e a d

      public IActionResult Index()
      {
         return View(_repository.GetAllVehicles().OrderBy(v => v.Year));
      }

      public IActionResult Details(int id)
      {
         Vehicle v = _repository.GetVehicleById(id);
         if (v == null)
         {
            return RedirectToAction("Index");
         }
         return View(v);
      }


      //   U p d a t e

      [HttpGet]
      public IActionResult Edit(int id)
      {
         Vehicle v = _repository.GetVehicleById(id);
         if (v == null)
         {
            return RedirectToAction("Index", "Vehicle");
         }
         return View(v);
      }

      [HttpPost]
      public IActionResult Edit(Vehicle v)
      {
         if (v.Year < 1886)
         {
            ModelState.AddModelError("", "Year Must Be 1886 Or Later");
         }

         if (v.Year > DateTime.Now.Year + 1)
         {
            ModelState.AddModelError("", $"Year Must Be {DateTime.Now.Year + 1} Or Earlier");
         }

         if (ModelState.IsValid)
         {
            _repository.Update(v);
            return RedirectToAction("Details", new { id = v.Id });
         }

         return View(v);
      }


      //   D e l e t e

      [HttpGet]
      public IActionResult Delete(int id)
      {
         Vehicle v = _repository.GetVehicleById(id);
         if (v == null)
         {
            return RedirectToAction("Index");
         }
         return View(v);
      }

      [HttpPost]
      public IActionResult Delete(Vehicle v)
      {
         _repository.Delete(v.Id);
         return RedirectToAction("Index");
      }
   }
}
