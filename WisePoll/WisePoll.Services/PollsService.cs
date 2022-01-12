using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WisePoll.Data.Models;
using WisePoll.Data.Repositories;
using WisePoll.Services.ViewModels;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WisePoll.Services
{
    public class PollsService : IPollsService
    {
        private readonly IPollsRepository _repository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<PollsService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PollsService(IPollsRepository db,IUsersRepository usersRepository, ILogger<PollsService> logger,IHttpContextAccessor httpContextAccessor)
        {
            _repository = db;
            _usersRepository = usersRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<IEnumerable<HomeIndexViewModel>> GetAllByUserIdAsync(int userId)
        {
            var polls = await _repository.GetAllByUserIdAsync(userId);

            var model = polls.Select(poll => new HomeIndexViewModel
            {
                Id = poll.Id,
                Title = poll.Title,
                Text = poll.Text,
                Is_active = poll.Is_active,
                Multiple = poll.Multiple,
                Members = poll.Members,
                UsersId = poll.UsersId
            });

            return model;
        }

        public async Task DesactivatePollAsync(int id)
        {
            var model = new Polls
            {
                Id = id,
                Is_active = false
            };
            await _repository.UpdateAsync(model,new List<string>
            {
                "Is_active"
            });
        }
        
        public async Task VotePollAsync(CreateVotePollViewModel model)
        {
            if (model.PollFields != null)
            {
                foreach (var p in model.PollFields)
                {
                    var userIdString = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
                    var isUserIdInt = int.TryParse(userIdString, out var userId);
                    if (isUserIdInt)
                    {
                        await _repository.AddVoteAsync(userId,p);
                    }
                    
                }
            }
        }

        public async Task CreatePollAsync(CreatePollViewModel model)
        {
            var ms = Regex.Replace(model.Members, @"\s+", "").Split(',', StringSplitOptions.TrimEntries).ToList();
            
            // Add all poll members
            var members = ms.Select(member => new Members
            {
                Email = member,
            });
            
            // Add all poll fields
            var pollFields = model.PollFields.Select(pollField => new PollFields
            {
                Label = pollField
            });

            var membersEnumerable = members.ToList();
            var userIdString = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
            var isUserIdInt = int.TryParse(userIdString, out var userId);
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (isUserIdInt && userEmail != null)
            {
                // Add current user to the poll members
                membersEnumerable.Add(new Members
                {
                    Email = userEmail,
                });
                
                // Create the poll model
                Polls polls = new()
                {
                    Title = model.Title,
                    Text = model.Text,
                    Members = membersEnumerable,
                    PollFields = pollFields.ToList(),
                    Is_active = true,
                    Multiple = model.Multiple,
                    UsersId = userId
                };

                await _repository.AddAsync(polls);
            }
        }

        public async Task<VotePollViewModel> GetAsync(int id, bool isDetached = false)
        {
            var poll = await _repository.GetAsync(id,isDetached);
            
            var model = new VotePollViewModel();
            
            if (poll == null) return model;
            
            model.Id = poll.Id;
            model.Is_active = poll.Is_active;
            model.Title = poll.Title;
            model.Text = poll.Text;
            model.Multiple = poll.Multiple;
            model.Members = poll.Members;
            model.PollFields = poll.PollFields;
            model.UsersId = poll.UsersId;

            return model;
        }
        
        public async Task<ResultPollViewModel> GetResultsAsync(int id, bool isDetached = false)
        {
            var poll = await _repository.GetAsync(id,isDetached);
            
            var model = new ResultPollViewModel();
            
            if (poll == null) return model;
            
            model.Id = poll.Id;
            model.Is_active = poll.Is_active;
            model.Title = poll.Title;
            model.Text = poll.Text;
            model.Multiple = poll.Multiple;
            model.Members = poll.Members;
            model.PollFields = poll.PollFields;
            model.UsersId = poll.UsersId;

            return model;
        }

        public async Task<Polls> GetIdPollsByUserIdAsync(int id)
        {
            return await _repository.GetIdPollsByUserIdAsync(id);
        }
    }
}