using WisePoll.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WisePoll.Data;

namespace WisePoll.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext db)
        {
            _context = db;
        }

        public async Task RegisterAsync(Users user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public Users FindUserByEmail(Users user)
        {
            return _context.Users.FirstOrDefault(u => u.Email == user.Email);
        }
    }
}