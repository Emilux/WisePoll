using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using WisePoll.Data.Models;

namespace WisePoll.Data.Repositories
{
    public class PollsRepository : IPollsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PollsRepository> _logger;

        public PollsRepository(ApplicationDbContext dbContext, ILogger<PollsRepository> logger)
        {
            _context = dbContext;
            _logger = logger;
        }

        public Task<List<Polls>> GetAllAsync()
        {
            return _context.Polls
                .Include(poll => poll.Members)
                .Include(poll => poll.PollFields)
                .ThenInclude(pollField => pollField.Users)
                .OrderBy(m => m.Is_active)
                .ToListAsync();
        }
        
        public Task<List<Polls>> GetAllByUserIdAsync(int userId)
        {
            return _context.Polls
                .Include(poll => poll.Members)
                .Include(poll => poll.PollFields)
                .ThenInclude(pollField => pollField.Users)
                .Where(m => m.UsersId == userId)
                .OrderByDescending(m => m.Is_active)
                .ToListAsync();
        }

        public async Task<Polls> GetAsync(int id,bool isDetached = false)
        {
            var polls = await _context.Polls
                .Include(poll => poll.Members)
                .Include(poll => poll.PollFields)
                .ThenInclude(pollField => pollField.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (isDetached)
            {
                _context.Entry(polls).State = EntityState.Detached;
            }
            
            return polls;
        }

        public async Task AddAsync(Polls polls)
        {
            if (polls == null)
                throw new ArgumentException(null, nameof(polls));
            
            await _context.Polls.AddAsync(polls);
            await _context.SaveChangesAsync();
        }

        public async Task AddVoteAsync(int userId,int pollFieldsId)
        {
            var users = await _context.Users
                .Include(p => p.PollFields)
                .SingleAsync(p => p.Id == userId);
            var pollFields = await _context.PollFields
                .Include(p => p.Users)
                .SingleAsync(p => p.Id == pollFieldsId);
            
            users.PollFields.Add(pollFields);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException(nameof(id));
            
            Polls poll = new() {Id = id};
            
            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Polls polls)
        {
            if (polls is not {Id: > 0} )
                throw new ArgumentNullException(nameof(polls));

            _context.Polls.Update(polls);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Polls polls, List<string> properties)
        {
            _context.Entry(polls).State = EntityState.Detached;
            foreach (var p in properties)
            {
                _context.Entry(polls).Property(p).IsModified = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Polls> GetIdPollsByUserIdAsync(int userId)
        {
            return await _context.Polls
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync(m => m.UsersId == userId);
        }
    }
}