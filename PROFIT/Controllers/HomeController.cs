using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PROFIT.Data;
using PROFIT.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PROFIT.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly DataBaseContext _db;

        public HomeController(UserManager<User> userManager, DataBaseContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            var user = _userManager.Users.ToList().FirstOrDefault(x => x.Email == User.Identity.Name);
            ViewBag.Account = user;
            return View();
        }
    }
}
