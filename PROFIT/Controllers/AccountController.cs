using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PROFIT.Interfaces;
using PROFIT.Models;
using PROFIT.Services;
using PROFIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROFIT.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IFileService _fileService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IFileService fileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    // проверяем, подтвержден ли email
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("Email", "Вы не подтвердили свой email");
                        return View(model);
                    }

                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var user = _userManager.Users.ToList().FirstOrDefault(x => x.Email == User.Identity.Name);
            ViewBag.Account = user;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (_userManager.Users.FirstOrDefault(x => x.Email == model.Email) is not null)
            {
                ModelState.AddModelError("Email", "Аккаунт с таким email уже существует");
            }

            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Name = model.Name, PhoneNumber = model.Phone};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code.Result },
                        protocol: HttpContext.Request.Scheme);
                    _fileService.SendConfirmationLink(model.Email, callbackUrl, "emailSend.html");
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Redirect("../Home/Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Redirect("../Home/Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return Redirect("../Home/Error");
        }

        [HttpPost]
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
