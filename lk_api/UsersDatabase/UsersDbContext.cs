using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lk_api.UsersDatabase
{
    public class UsersDbContext : IdentityDbContext<User>
    {

        private string _connectionString;


        public UsersDbContext(DbContextOptions<UsersDbContext> options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
