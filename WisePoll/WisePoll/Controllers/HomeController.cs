using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WisePoll.Models;
using WisePoll.Services;

namespace WisePoll.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPollsService _pollsService;

        public HomeController(ILogger<HomeController> logger, IPollsService pollsService)
        {
            _logger = logger;
            _pollsService = pollsService;
        }

        public async Task<IActionResult> Index()
        {
            const bool loggedIn = true;
            var data = await _pollsService.GetAllByUserIdAsync(2);
            if (loggedIn)
            {
                return View("LoggedInIndex", data);
            } else
            {
                return View();
            }
            
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
