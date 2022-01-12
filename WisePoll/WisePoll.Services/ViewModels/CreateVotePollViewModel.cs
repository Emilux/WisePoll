using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WisePoll.Services.Tools;

namespace WisePoll.Services.ViewModels
{
    public class CreateVotePollViewModel
    {
        [Required(ErrorMessage = "Please at least select one choice")]
        public List<int> PollFields { get; set;}
    }
}