using Microsoft.AspNetCore.Http;
using PROFIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROFIT.Interfaces
{
    public interface IFileService
    {
        public void SendConfirmationLink(string email, string link, string file);
    }
}
