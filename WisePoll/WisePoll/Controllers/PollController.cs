using Microsoft.AspNetCore.Mvc;

namespace WisePoll.Controllers
{
    public class PollController : Controller
    {
        public IActionResult Create()
        {
            return View(); 
        }
        
        public IActionResult Vote()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
        
        public IActionResult Result()
        {
            return View();
        }

    }
}
