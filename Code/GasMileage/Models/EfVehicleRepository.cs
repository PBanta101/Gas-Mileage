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


      //   D e l e t e

   }
}
