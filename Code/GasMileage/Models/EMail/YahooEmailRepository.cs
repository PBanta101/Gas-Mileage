using System;
using System.Net;
using System.Net.Mail;

namespace GasMileage.Models
{
   public class YahooEmailRepository
      : IEmailRepository
   {
      private readonly string _emailAccount = System.Environment.GetEnvironmentVariable("MssaCadEmailAccount");
      private readonly string _emailPassword = System.Environment.GetEnvironmentVariable("MssaCadEmailPassword");

      public void Send(string to, string subject, string body)
      {
         try
         {
            MailMessage email = new MailMessage
            {
               From = new MailAddress(_emailAccount),
               Subject = subject,
               Body = body,
               IsBodyHtml = true
            };
            email.To.Add(to);

            SmtpClient smtp = new SmtpClient("smtp.mail.yahoo.com", 587);
            smtp.Credentials = new NetworkCredential(_emailAccount, _emailPassword);
            smtp.EnableSsl = true;
            smtp.Send(email);
         }
         catch (Exception)
         {
         }
      }
   }
}
