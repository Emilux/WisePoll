using System.Collections.Generic;
using WisePoll.Data.Models;

namespace WisePoll.Services.ViewModels
{
    public class ResultPollViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool Multiple { get; set; }
        public bool Is_active { get; set; }
        public int UsersId { get; set; }
        public List<PollFields> PollFields { get; set;}
        public List<Members> Members { get; set; }
    }
}