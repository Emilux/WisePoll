using Microsoft.EntityFrameworkCore;
using WisePoll.Data.Models;

namespace WisePoll.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Members> Members { get; set; }
        public DbSet<Polls> Polls { get; set; }
        public DbSet<PollFields> PollFields { get; set; }
        public DbSet<Users> Users { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
