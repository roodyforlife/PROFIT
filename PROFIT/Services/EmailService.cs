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
                var receiverEmail = new MailAddress(email, "Receiver");
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, "m@iTT7Rd1UBILsBueWX7")
                };
                var mess = new MailMessage(senderEmail, receiverEmail);
                mess.Subject = "Administration";
                mess.IsBodyHtml = false;
                mess.Body = message;
                // smtp.Send(mess);
                Task.Run(() => smtp.Send(mess));
            }
            catch
            {
            }
        }
    }
}
