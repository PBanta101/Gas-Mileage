using GasMileage.Models;
using Microsoft.AspNetCore.Mvc;

namespace GasMileage.Controllers
{
   public class VehicleController
      : Controller
   {
      //   C r e a t e

      [HttpGet]
      public IActionResult Add()
      {
         return View();
      }

      [HttpPost]
      public IActionResult Add(Vehicle v)
      {
         // Adds this vehicle (v) to the database.
         return RedirectToAction("Details");
         // return View("Details");
      }


      //   R e a d

      // Read a Vehicle out of the Database and display it on a web page.
      public IActionResult Details(int id)
      {
         // 1. Go to the database and retrieve Vehicle with the given id.
         Vehicle v = new Vehicle();
         v.Id = 1;
         v.Year = 1985;
         v.Make = "Toyota";
         v.Model = "PickUp";
         v.Vin = "JT4RN...";
         v.Color = "White";
         v.UserId = 2;

         // 2. Display that Vehicle on a View.
         return View(v);
      }


      //   U p d a t e

      [HttpGet]
      public IActionResult Edit(int id)
      {
         // 1. Read a Vehicle out of a Database.
         Vehicle v = new Vehicle();
         v.Id = 1;
         v.Year = 1985;
         v.Make = "Toyota";
         v.Model = "PickUp";
         v.Vin = "JT4RN...";
         v.Color = "White";
         v.UserId = 2;

         // 2. Display it on a web page so that the user can change some fields / values.
         return View(v);
      }

      [HttpPost]
      public IActionResult Edit(Vehicle v)
      {
         // Update the vehicle (v) in the database - ignore for now
         return RedirectToAction("Details");
      }


      //   D e l e t e
   }
}
