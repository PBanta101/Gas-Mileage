using System.Linq;

namespace GasMileage.Models
{
   public class EfVehicleRepository
      : IVehicleRepository
   {
      //   F i e l d s   &   P r o p e r t i e s

      private AppDbContext _context;


      //   C o n s t r u c t o r s

      public EfVehicleRepository(AppDbContext context)
      {
         _context = context;
      }


      //   M e t h o d s

      //   C r e a t e

      public Vehicle Create(Vehicle v)
      {
         _context.Vehicles.Add(v);
         _context.SaveChanges();
         return v;
      }


      //   R e a d

      public IQueryable<Vehicle> GetAllVehicles()
      {
         return _context.Vehicles;
      }

      public Vehicle GetVehicleById(int id)
      {
         return _context.Vehicles.FirstOrDefault(v => v.Id == id);
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
            _context.SaveChanges();
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

         _context.Vehicles.Remove(vehicleToDelete);
         _context.SaveChanges();
         return true;
      }
   }
}
