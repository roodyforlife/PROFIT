using MimeKit;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PROFIT.Services
{
    public class EmailService
    {
        public void SendEmailAsync(string email, string message)
        {
            try
            {
                var senderEmail = new MailAddress("roodyhacker123@gmail.com", "PROFIT");
                var receiverEmail = new MailAddress(email, "PROFIT");
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, "isfyvcguycstanzb")
                };
                var mess = new MailMessage(senderEmail, receiverEmail);
                mess.Subject = "Administration";
                mess.IsBodyHtml = true;
                mess.Body = message;
                Task.Run(() => smtp.Send(mess));
            }
            catch
            {
            }
        }
    }
}
