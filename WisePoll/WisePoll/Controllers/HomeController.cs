using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        
        /**
         * <summary>method <c>Index</c> Render the index page
         * if the user is logged in show the user polls list
         * </summary>
         */
        public async Task<IActionResult> Index()
        {
            const bool loggedIn = true;
            
            var userIdString = User.FindFirst("UserId")?.Value;
            
            if ((userIdString == null || !loggedIn)) return View();
            
            if (!int.TryParse(userIdString, out var userId)) return View();
            
            var data = await _pollsService.GetAllByUserIdAsync(userId);
            
            return View("LoggedInIndex", data);

        }
        
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
