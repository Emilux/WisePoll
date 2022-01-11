using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WisePoll.Data.Models;
using WisePoll.Data.Repositories;
using WisePoll.Services;
using WisePoll.Services.ViewModels;

namespace WisePoll.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _mailService;

        public AuthController(IAuthService authservice, IEmailService emailService)
        {
            _authService = authservice;
            _mailService = emailService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Home");
            }
            return View();
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Home");
            }
            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AuthRegisterViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new Users()
            {
                Pseudo = model.Pseudo,
                Email = model.Email,
                Password = model.Password
            };

            if (_authService.GetUserByMail(user) != null)
            {
                ModelState.AddModelError("email", "Email is all ready used");
                return View(model);
            }

            await _authService.RegisterAsync(user);

            await Login(new AuthLoginViewModel
            {
                Email = model.Email,
                Password = model.Password,
                StayLog = false
            }, null) ;

            _mailService.SendMail();

            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthLoginViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Home");
            }

            if (ModelState.IsValid)
            {

                var user = new Users()
                {
                    Email = model.Email,
                    Password = model.Password,
                };

                bool StayLog = model.StayLog;

                var result = await _authService.AuthenticateAsync(user, StayLog);
                if (result)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            ModelState.AddModelError("", "Invalid Email or Password");
            return View(model);
        }
    }
}
