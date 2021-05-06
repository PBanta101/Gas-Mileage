using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GasMileage.Models
{
   public class EfVehicleRepository
      : IVehicleRepository
   {
      //   F i e l d s   &   P r o p e r t i e s

      private AppDbContext    _context;
      private IUserRepository _userRepository;


      //   C o n s t r u c t o r s

      public EfVehicleRepository(AppDbContext context, IUserRepository userRepository)
      {
         _context        = context;
         _userRepository = userRepository;
      }


      //   M e t h o d s

      //   C r e a t e

      public Vehicle Create(Vehicle v)
      {
         if (_userRepository.IsUserLoggedIn())
         {
            try
            {
               v.UserId = _userRepository.GetLoggedInUserId();
               _context.Vehicles.Add(v);
               _context.SaveChanges();
            }
            catch (Exception e)
            {
               // return null; // Maybe
            }
            return v;
         }

         return null;
      }


      //   R e a d

      public IQueryable<Vehicle> GetAllVehicles()
      {
         if (_userRepository.IsUserLoggedIn())
         {
            return _context.Vehicles.Where(v => v.UserId == _userRepository.GetLoggedInUserId());
         }

         Vehicle[] noVehicles = new Vehicle[0];
         return noVehicles.AsQueryable<Vehicle>();
      }

      public Vehicle GetVehicleById(int id)
      {
         if (_userRepository.IsUserLoggedIn())
         {
            Vehicle v = _context.Vehicles
                                .Include(v => v.Fillups)
                                .FirstOrDefault(v => v.Id == id && v.UserId == _userRepository.GetLoggedInUserId());
            if (v != null)
            {
               v.Fillups = v.Fillups.OrderByDescending(f => f.Odometer);
            }
            return v;
         }

         return null;
      }


      //   U p d a t e

      public Vehicle Update(Vehicle v)
      {
         Vehicle vehicleToUpdate = GetVehicleById(v.Id);
         if (vehicleToUpdate != null)
         {
            vehicleToUpdate.Color = v.Color;
            vehicleToUpdate.Make = v.Make;
            vehicleToUpdate.Model = v.Model;
            vehicleToUpdate.Vin = v.Vin;
            vehicleToUpdate.Year = v.Year;
            try
            {
               _context.SaveChanges();
            }
            catch (Exception e)
            {
               // return null; // Maybe
            }
         }
         return vehicleToUpdate;
      }


      //   D e l e t e

      public bool Delete(int id)
      {
         Vehicle vehicleToDelete = GetVehicleById(id);
         if (vehicleToDelete == null)
         {
            return false;
         }

         try
         {
            _context.Vehicles.Remove(vehicleToDelete);
            _context.SaveChanges();
            return true;
         }
         catch (Exception e)
         {
         }

         return false;
      }
   }
}
