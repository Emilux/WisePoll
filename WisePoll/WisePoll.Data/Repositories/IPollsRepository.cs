using System.Collections.Generic;
using System.Threading.Tasks;
using WisePoll.Data.Models;

namespace WisePoll.Data.Repositories
{
    public interface IPollsRepository
    {
        Task<List<Polls>> GetAllAsync();
        Task<List<Polls>> GetAllByUserIdAsync(int userId);
        Task<Polls> GetAsync(int id,bool isDetached = false);
        Task AddAsync(Polls polls);
        Task DeleteAsync(int id);
        Task UpdateAsync(Polls polls);
        Task UpdateAsync(Polls polls, List<string> properties);
        Task<Polls> GetIdPollsByUserIdAsync(int id);
        Task AddVoteAsync(int userId,int pollFieldsId);
    }
}