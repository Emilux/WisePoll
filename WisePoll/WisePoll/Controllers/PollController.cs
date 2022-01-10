using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using WisePoll.Services;
using WisePoll.Services.ViewModels;

namespace WisePoll.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollsService _pollsService;
        private readonly ILogger<PollController> _logger;
        public PollController(IPollsService pollsService, ILogger<PollController> logger)
        {
            _pollsService = pollsService;
            _logger = logger;

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePollViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _pollsService.CreatePollAsync(model);
            return Redirect("Index");
        }
        public async Task<IActionResult> Vote(int id)
        {
            var data = await _pollsService.GetAsync(id);

            if (!data.Is_active)
            {
                return RedirectToAction("Result", new { id });
            }
            
            return View(data);
        }

        public IActionResult Success()
        {
            return View();
        }

        public async Task<IActionResult> Desactivate(int id)
        {
            await _pollsService.DesactivatePollAsync(id);
            return RedirectToAction("Index","Home");
        }

        public IActionResult Result()
        {
            return View();
        }

    }
}
