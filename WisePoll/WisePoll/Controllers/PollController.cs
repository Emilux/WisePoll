using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WisePoll.Services;
using WisePoll.Services.ViewModels;
using Microsoft.AspNetCore.Http;

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
        [Authorize]
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
             // await _pollsService.CreatePollAsync(model);

            //  PollsId and User.Pseudo
            var CreateUser = User.FindFirstValue(ClaimTypes.Name);
            var UserId = int.Parse(User.FindFirst("UserId")?.Value);
            int PollsId = (await _pollsService.GetIdPollsByUserIdAsync(UserId)).Id;

            // uriBuilder.Uri.AbsoluteUri doesn't work
            var uriBuilder = new UriBuilder
            {
                Scheme = Request.Scheme,
                Host = Request.Host.ToString(),
                Path = $"/Poll/Vote/{PollsId}".ToString(),
            };
            var link = $"{uriBuilder.Scheme}://{uriBuilder.Host}{uriBuilder.Path}";

            // Mail
            var subject = "Wisepoll: Survey invitation: " + model.Title;
            var body =
                "<h2 style=''>" + model.Title + "</h2>" +
                $"<p>Your fiend {CreateUser} invites you to participate in its poll {model.Title}</p>" +
                "<p>Visit the following address to participate, after you are registered with this email address</p>" +
                $"<a href=\"{link}\" target='_blank'>Poll link</a>";
             _emailService.SendMail(model.Members, subject, body);

            TempData["link"] = link;
            return RedirectToAction("Success", "Poll");
        }


        [Authorize]
        public async Task<IActionResult> Vote(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            
            // Get poll data
            var data = await _pollsService.GetAsync(id);
            
            // Check if poll found
            if (data.Id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            // Check if the connected user is member of the poll
            if (data.Members.All(m => m.Email != User.FindFirstValue(ClaimTypes.Email)))
                return RedirectToAction("Index", "Home");
            
            // Check if the poll is active
            if (!data.Is_active)
            {
                return RedirectToAction("Result", new { id });
            }
            
            return View(data);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Vote(VotePollViewModel model,int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _pollsService.VotePollAsync(model);
            return RedirectToAction("Result", new { id });
        }

        public IActionResult Success()
        {
            var link = TempData["link"] as string;

            if (link != null) 
            { 
                ViewData["link"] = link;
            } else
            {
                ViewData["link"] = "";
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Desactivate(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var data = await _pollsService.GetAsync(id, true);

            var userIdString = User.FindFirst("UserId")?.Value;

            if ((userIdString == null)) Unauthorized();

            if (!int.TryParse(userIdString, out var userId)) Unauthorized();

            if (userId != data.UsersId || !data.Is_active) Unauthorized();

            await _pollsService.DesactivatePollAsync(id);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Result(int id)
        {
            if (id == 0)
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
