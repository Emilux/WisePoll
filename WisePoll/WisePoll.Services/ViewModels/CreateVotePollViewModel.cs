using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WisePoll.Services.Tools;

namespace WisePoll.Services.ViewModels
{
    public class CreateVotePollViewModel
    {
        public IEnumerable<bool> PollFields { get; set;}
    }
}