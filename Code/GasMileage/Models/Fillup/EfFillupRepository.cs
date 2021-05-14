using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GasMileage.Models
{
   public class EfFillupRepository
      : IFillupRepository
   {
      //   F i e l d s   &   P r o p e r t i e s

      private readonly AppDbContext       _context;
      private readonly IUserRepository    _userRepository;
      private readonly IVehicleRepository _vehicleRepository;


      //   C o n s t r u c t o r s

      public EfFillupRepository(AppDbContext context, IUserRepository userRepository, IVehicleRepository vehicleRepository)
      {
         _context           = context;
         _userRepository    = userRepository;
         _vehicleRepository = vehicleRepository;
      }


      //   M e t h o d s

      //   C r e a t e

      public Fillup Create(Fillup f)
      {
         _context.Fillups.Add(f);
         _context.SaveChanges();
         RecomputeDaysBetweenFillups(f.VehicleId);
         return f;
      }


      //   R e a d

      public IQueryable<Fillup> GetAllFillups(Guid vehicleId)
      {
         return _context.Fillups.Include(f => f.Vehicle).Where(f => f.VehicleId == vehicleId && f.Vehicle.UserId == _userRepository.GetLoggedInUserId());
      }

      public Fillup GetFillupById(Guid fillupId)
      {
         return _context.Fillups
                        .Include(f => f.Vehicle)
                        .FirstOrDefault(f => f.Id == fillupId && f.Vehicle.UserId == _userRepository.GetLoggedInUserId());
      }


      //   U p d a t e

      public Fillup Update(Fillup f)
      {
         Fillup fillupToUpdate = GetFillupById(f.Id);
         if (fillupToUpdate != null)
         {
            fillupToUpdate.Date         = f.Date;
            fillupToUpdate.Gallons      = f.Gallons;
            fillupToUpdate.Odometer     = f.Odometer;
            fillupToUpdate.TotalCost    = f.TotalCost;
            fillupToUpdate.TripOdometer = f.TripOdometer;
            fillupToUpdate.ZipCode      = f.ZipCode;
            _context.SaveChanges();
            RecomputeDaysBetweenFillups(f.VehicleId);
         }
         return fillupToUpdate;
      }


      //   D e l e t e

      public bool Delete(Fillup f)
      {
         Fillup fillupToDelete = GetFillupById(f.Id);
         if (fillupToDelete == null)
         {
            return false;
         }
         Guid vehicleId = fillupToDelete.VehicleId;
         _context.Fillups.Remove(fillupToDelete);
         _context.SaveChanges();
         RecomputeDaysBetweenFillups(vehicleId);
         return true;
      }


      //   P r i v a t e   M e t h o d s

      private void RecomputeDaysBetweenFillups(Guid vehicleId)
      {
         if (_vehicleRepository.VehicleExists(vehicleId) == false)
         {
            return;
         }

         IEnumerable<Fillup> fillups = _context.Fillups.Where(f => f.VehicleId == vehicleId)
                                                       .OrderBy(f => f.Date)
                                                       .ThenBy(f => f.Odometer);
         if (fillups == null || fillups.Count() == 0)
         {
            return;
         }

         DateTime? lastDate = fillups.ElementAt(0).Date;
         foreach (Fillup f in fillups)
         {
            f.DaysSinceLastFillup = Math.Max(1, (int)((f.Date - lastDate).GetValueOrDefault().TotalDays + 0.5));
            lastDate = f.Date;
         }

         _context.SaveChanges();
      } // end RecomputeDaysBetweenFillups( )
   }
}
