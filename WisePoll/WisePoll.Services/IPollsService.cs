using System.Collections.Generic;
using System.Threading.Tasks;
using WisePoll.Services.ViewModels;

namespace WisePoll.Services
{
    public interface IPollsService
    {
        Task<IEnumerable<HomeIndexViewModel>> GetAllAsync();
    }
}