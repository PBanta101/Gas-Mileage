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

      private AppDbContext       _context;
      private IVehicleRepository _vehicleRepository;


      //   C o n s t r u c t o r s

      public EfFillupRepository(AppDbContext context, IVehicleRepository vehicleRepository)
      {
         _context           = context;
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

      public IQueryable<Fillup> GetAllFillups(int vehicleId)
      {
         return _context.Fillups.Where(f => f.VehicleId == vehicleId);
      }

      public Fillup GetFillupById(int fillupId)
      {
         return _context.Fillups
                        .Include(f => f.Vehicle)
                        .FirstOrDefault(f => f.Id == fillupId);
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
         _context.Fillups.Remove(fillupToDelete);
         _context.SaveChanges();
         return true;
      }


      //   P r i v a t e   M e t h o d s

      private void RecomputeDaysBetweenFillups(int vehicleId)
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
