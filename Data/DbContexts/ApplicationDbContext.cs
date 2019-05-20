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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasMany<UserChat>(u=>u.UserChats).WithOne(u=>u.User);

            modelBuilder.Entity<UserChat>().HasOne<User>(uc=>uc.User).WithMany(u=>u.UserChats);
            modelBuilder.Entity<UserChat>().HasOne(u => u.Chat).WithMany(c => c.UserChats);

            modelBuilder.Entity<Chat>().HasMany(c=>c.UserChats).WithOne(c=>c.Chat);

            

        }
    }
}