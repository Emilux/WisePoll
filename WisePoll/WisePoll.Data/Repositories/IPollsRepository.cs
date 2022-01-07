using System.Collections.Generic;
using System.Threading.Tasks;
using WisePoll.Data.Models;

namespace WisePoll.Data.Repositories
{
    public interface IPollsRepository
    {
        Task<List<Polls>> GetAllAsync();
        Task<Polls> GetAsync(int id);
        Task AddAsync(Polls polls);
        Task DeleteAsync(int id);
        Task UpdateAsync(Polls polls);
    }
}