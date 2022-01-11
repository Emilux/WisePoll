using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WisePoll.Data;
using WisePoll.Services;
using WisePoll.Services.ViewModels;

namespace WisePoll.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollsService _pollsService;
        private readonly IEmailService _emailService;
        private readonly ILogger<PollController> _logger;
        public PollController(IPollsService pollsService, ILogger<PollController> logger, IEmailService emailService)
        {
            _pollsService = pollsService;
            _logger = logger;
            _emailService = emailService;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePollViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _pollsService.CreatePollAsync(model);

            //  User Pseudo
            var CreateUser = User.FindFirst("Name")?.Value;

            // @TODO Recuperer l'id du poll créer
            var PollsId = 1;

            var subject = "Wisepoll: Survey invitation: " + model.Title;
            var body =
                "<h2 style=''>" + model.Title + "</h2>" +
                "<p>Your fiend " + CreateUser + " invites you to participate in its poll " + model.Title + "</p>" +
                "<p>Visit the following address to participate, after you are registered with this email address</p>" +
                "<p style=''><a href='#" + this.Url.Action("Vote", "Polls", PollsId) +"'>Link</a>";

            _emailService.SendMail(model.Members, subject, body);

            return RedirectToAction("Success","Poll");
        }


        [Authorize]
        public async Task<IActionResult> Vote(int id)
        {
            if (id == 0 )
            {
                return RedirectToAction("Index", "Home");
            }
            await _pollsService.VotePollAsync(null);
            var data = await _pollsService.GetAsync(id);
            
            if (data.Members.Any(m => m.Email == User.FindFirstValue(ClaimTypes.Email)))
            {
                if (!data.Is_active)
                {
                    return RedirectToAction("Result", new { id });
                }
            
                return View(data);
            }

            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Vote(CreateVotePollViewModel model,int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            return RedirectToAction("Result", new { id });
        }

        public IActionResult Success()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Desactivate(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index","Home");
            }
            
            var data = await _pollsService.GetAsync(id,true);
            
            var userIdString = User.FindFirst("UserId")?.Value;
            
            if ((userIdString == null)) Unauthorized();
            
            if (!int.TryParse(userIdString, out var userId)) Unauthorized();

            if (userId != data.UsersId || !data.Is_active) Unauthorized();
            
            await _pollsService.DesactivatePollAsync(id);
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Result(int id)
        {
            if (id == 0 )
            {
                return RedirectToAction("Index", "Home");
            }
            
            var data = await _pollsService.GetResultsAsync(id);

            if (data.Members.All(m => m.Email != User.FindFirstValue(ClaimTypes.Email)))
                return RedirectToAction("Index", "Home");

            return View(data);

        }

    }
}
