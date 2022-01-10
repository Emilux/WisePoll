using WisePoll.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WisePoll.Data.Repositories
{
    public interface IUsersRepository
    {
        Task RegisterAsync(Users user);
        Users FindUserByEmail(Users user);
    }
}
