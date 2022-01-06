using Microsoft.AspNetCore.Mvc;

namespace WisePoll.Controllers
{
    public class PollController : Controller
    {
        public IActionResult Index()
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

    }
}
