using Microsoft.AspNetCore.Identity;
using PROFIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROFIT.Interfaces
{
    public interface IUserService
    {
        public User GetInfoFromGoogle(ExternalLoginInfo info);
    }
}
