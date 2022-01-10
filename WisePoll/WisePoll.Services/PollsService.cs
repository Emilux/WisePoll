using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        private readonly ILogger<PollsService> _logger;
        public PollsService(IPollsRepository db, ILogger<PollsService> logger)
        {
            _repository = db;
            _logger = logger;
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

        public async Task CreatePollAsync(CreatePollViewModel model)
        {
            var ms = Regex.Replace(model.Members, @"\s+", "").Split(',', StringSplitOptions.TrimEntries).ToList();

            var members = ms.Select(member => new Members
            {
                Email = member,
            });

            var pollFields = model.PollFields.Select(pollField => new PollFields
            {
                Label = pollField
            });

            var membersEnumerable = members.ToList();
            Polls polls = new()
            {
                Title = model.Title,
                Text = model.Text,
                Members = membersEnumerable,
                PollFields = pollFields.ToList(),
                Is_active = true,
                Multiple = true,
                UsersId = 2
            };

            await _repository.AddAsync(polls);
        }

        public async Task<VotePollViewModel> GetAsync(int id)
        {
            var poll = await _repository.GetAsync(id);

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
    }
}