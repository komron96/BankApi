using Microsoft.EntityFrameworkCore;
namespace DataAccess
{

    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
    }

}