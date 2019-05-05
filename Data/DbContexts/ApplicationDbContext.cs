using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}