using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lk_api.UsersDatabase
{
    public class UsersDbContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;

        public UsersDbContext(DbContextOptions<UsersDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("UsersDbConnection");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
