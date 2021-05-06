using System.Linq;

namespace GasMileage.Models
{
   public interface IFillupRepository
   {
      //   C r e a t e

      public Fillup Create(Fillup f);


      //   R e a d

      public IQueryable<Fillup> GetAllFillups(int vehicleId);

      public Fillup GetFillupById(int fillupId);


      //   U p d a t e

      public Fillup Update(Fillup f);


      //   D e l e t e

      public bool Delete(Fillup f);
   }
}
