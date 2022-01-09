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

            await _authService.RegisterAsync(model);

            return RedirectToAction("index", "Home");
        }
    }
}
