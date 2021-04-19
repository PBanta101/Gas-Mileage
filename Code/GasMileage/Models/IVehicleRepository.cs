using System.Linq;

namespace GasMileage.Models
{
   public interface IVehicleRepository
   {
      //   C r e a t e


      //   R e a d

      public IQueryable<Vehicle> GetAllVehicles();

      public Vehicle GetVehicleById(int id);


      //   U p d a t e


      //   D e l e t e

   }
}
