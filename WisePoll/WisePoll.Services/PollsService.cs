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
            var pollFields = new Polls().PollFields;
            var members = new Polls().Members;
            var newMembersPollFields = members.Select(m => m.PollFields);
            foreach (var newMembersPollField in newMembersPollFields)
            {
                Console.WriteLine(newMembersPollField);
            }
        }

        public async Task CreatePollAsync(CreatePollViewModel model)
        {
            var ms = Regex.Replace(model.Members, @"\s+", "").Split(',', StringSplitOptions.TrimEntries).ToList();
            
            var members = ms.Select(member =>
            {
                var user = _usersRepository.FindUserByEmail(member);
                return new Members
                {
                    Email = member,
                };
            });
            
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
                var user = _usersRepository.FindUserByEmail(userEmail);
                membersEnumerable.Add(new Members
                {
                    Email = userEmail,
                });
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

            var model = new VotePollViewModel
            {
                Id = poll.Id,
                Title = poll.Title,
                Text = poll.Text,
                Is_active = poll.Is_active,
                Multiple = poll.Multiple,
                Members = poll.Members,
                PollFields = poll.PollFields,
                UsersId = poll.UsersId
            };

            return model;
        }
        
        public async Task<ResultPollViewModel> GetResultsAsync(int id, bool isDetached = false)
        {
            var poll = await _repository.GetAsync(id,isDetached);

            var model = new ResultPollViewModel
            {
                Id = poll.Id,
                Title = poll.Title,
                Text = poll.Text,
                Is_active = poll.Is_active,
                Multiple = poll.Multiple,
                Members = poll.Members,
                PollFields = poll.PollFields,
                UsersId = poll.UsersId
            };

            return model;
        }

        public async Task<Polls> GetIdPollsByUserIdAsync(int id)
        {
            return await _repository.GetIdPollsByUserIdAsync(id);
        }
    }
}