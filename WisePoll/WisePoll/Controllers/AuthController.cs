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

        public AuthController(IAuthService service)
        {
            _authService = service;
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AuthRegisterViewModel model)
        {
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

            if (_authService.CheckSingleEmail(user) != null)
            {
                return View(model);
            }

            await _authService.RegisterAsync(user);

            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthLoginViewModel model, string returnUrl)
        {
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
