using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WisePoll.Data.Models;

namespace WisePoll.Data.Repositories
{
    public class PollFieldsRepository : IPollFieldsRepository
    {
        private readonly ApplicationDbContext _context;

        public PollFieldsRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
    }
}