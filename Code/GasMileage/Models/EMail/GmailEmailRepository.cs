using System;
using System.Net;
using System.Net.Mail;

namespace GasMileage.Models
{
   public class GmailEmailRepository
      : IEmailRepository
   {
      //   F i e l d s   &   P r o p e r t i e s

      private static readonly string _emailAccount  = System.Environment.GetEnvironmentVariable("MssaCadEmailAccount" );
      private static readonly string _emailPassword = System.Environment.GetEnvironmentVariable("MssaCadEmailPassword");


      //   C o n s t r u c t o r s


      //   M e t h o d s

      public void Send(string to, string subject, string body)
      {

         try
         {
            MailMessage email = new MailMessage
            {
               From       = new MailAddress(_emailAccount),
               Subject    = subject,
               Body       = body,
               IsBodyHtml = true
            };
            email.To.Add(to);

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(_emailAccount, _emailPassword);
            smtp.EnableSsl   = true;
            smtp.Send(email);
         }
         catch (Exception)
         {
         }
      } // end Send( )
   }
}
