using Microsoft.AspNetCore.Identity;
using PROFIT.Interfaces;
using PROFIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PROFIT.Services
{
    public class UserService : IUserService
    {
        public User GetInfoFromGoogle(ExternalLoginInfo info)
        {
            string picture = info.Principal.FindFirstValue("picture");
            byte[] data;
            using (WebClient webClient = new WebClient())
            {
                data = webClient.DownloadData(picture);
            }
            User user = new User
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                Name = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                Avatar = data
            };
            return user;
        }
    }
}
