using System.Net;
using System.Net.Mail;

namespace Demo.Presentation.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com",587);    //( Host, Port ) support TLS Encryption
            Client.EnableSsl = true; // to Encrypt the Data Ssl
            Client.Credentials = new NetworkCredential("mewalid24636@gmail.com", "wfwbubdkwpocnvon");  //Username , password   ! I used apppassword !
            Client.Send("mewalid24636@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
