namespace GasMileage.Models
{
   public interface IEmailRepository
   {
      public void Send(string to, string subject, string body);
   }
}
