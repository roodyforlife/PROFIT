using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PROFIT.Interfaces;
using PROFIT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace PROFIT.Services
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void SendConfirmationLink(string email, string link, string file)
        {
            string body = string.Empty;
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "Files/email", file);
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{ConfirmationLink}", $" <a href='{link}'> <div class='button'>Подтвердить email</div></a>");

             EmailService emailService = new EmailService();
            emailService.SendEmailAsync(email, body);
        }
    }
}
