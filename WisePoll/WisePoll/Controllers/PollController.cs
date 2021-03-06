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
            await _pollsService.CreatePollAsync(model);

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
                $"<p>Your friend {CreateUser} invite you to participate at his poll {model.Title}</p>" +
                $"<p>Visit the following address to participate, and Log In with this mail address to participate.</p>" +
                $"<a href=\"{link}\" target='_blank'>Poll link</a>";
             _emailService.SendMail(model.Members, subject, body);

            TempData["link"] = link;
            return RedirectToAction("Success", "Poll");
        }

        [HttpGet]
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
            
            // Check if current connected user have already participated to the poll
            if (!data.PollFields.All(p => p.Users.All(u => u.Email != User.FindFirstValue(ClaimTypes.Email))))
            {
                return RedirectToAction("Result", new { id });   
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
        public async Task<IActionResult> Vote(CreateVotePollViewModel model,int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var data = await _pollsService.GetAsync(id);
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            
            // Check that there no more than one choice checked if its a single choice poll
            if (!data.Multiple && model.PollFields.Count > 1)
            {
                ModelState.AddModelError("PollFields","Please select only one choice");
                return View(data);
            }

            // Check if the checked choices are in the current poll
            if (!data.PollFields.Any(p => model.PollFields.Contains(p.Id)))
            {
                ModelState.AddModelError("PollFields","Please select a correct choice");
                return View(data);
            }
            
            await _pollsService.VotePollAsync(model);
            return RedirectToAction("Result", new { id });
        }

        [HttpGet]
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Desactivate(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var data = await _pollsService.GetAsync(id, true);

            // Check if poll found
            if (data.Id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var userIdString = User.FindFirst("UserId")?.Value;

            if (userIdString == null) return RedirectToAction("Index", "Home");

            if (!int.TryParse(userIdString, out var userId)) return RedirectToAction("Index", "Home");

            if (userId != data.UsersId || !data.Is_active) return RedirectToAction("Index", "Home");

            await _pollsService.DesactivatePollAsync(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Result(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var data = await _pollsService.GetResultsAsync(id);

            // Check if poll found
            if (data.Id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(data);

        }

    }
}
