using System.Collections.Generic;
using System.Threading.Tasks;
using WisePoll.Services.ViewModels;

namespace WisePoll.Services
{
    public interface IPollsService
    {
        Task<IEnumerable<HomeIndexViewModel>> GetAllByUserIdAsync(int userId);
        Task DesactivatePollAsync(int id);
        Task CreatePollAsync(CreatePollViewModel model);
        Task VotePollAsync(CreateVotePollViewModel model);
        Task<VotePollViewModel> GetAsync(int id,bool isDetached = false);
        Task<ResultPollViewModel> GetResultsAsync(int id,bool isDetached = false);
    }
}