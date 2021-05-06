using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GasMileage.Models
{
   public class EfFillupRepository
      : IFillupRepository
   {
      //   F i e l d s   &   P r o p e r t i e s

      private AppDbContext _context;


      //   C o n s t r u c t o r s

      public EfFillupRepository(AppDbContext context)
      {
         _context = context;
      }


      //   M e t h o d s

      //   C r e a t e

      public Fillup Create(Fillup f)
      {
         _context.Fillups.Add(f);
         _context.SaveChanges();
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
   }
}
