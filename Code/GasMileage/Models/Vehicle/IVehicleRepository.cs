using System.Linq;

namespace GasMileage.Models
{
   public interface IVehicleRepository
   {
      //   C r e a t e

      public Vehicle Create(Vehicle v);


      //   R e a d

      public IQueryable<Vehicle> GetAllVehicles();

      public Vehicle GetVehicleById(int id);

      public bool VehicleExists(int id);


      //   U p d a t e

      public Vehicle Update(Vehicle v);


      //   D e l e t e

      public bool Delete(int id);

   }
}
