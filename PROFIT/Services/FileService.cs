using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PROFIT.Interfaces;
using PROFIT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace PROFIT.Services
{
    public class FileService : IFileService
    {
        private readonly UserManager<User> _userManager;
        public FileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public void SendConfirmationLink(User user, string email)
        {

        }
    }
}
