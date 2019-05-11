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

            modelBuilder.Entity<UserChat>().HasOne(u => u.User).WithMany(c => c.UserChats);
            modelBuilder.Entity<UserChat>().HasOne(u => u.Chat).WithMany(c => c.UserChats);

            modelBuilder.Entity<User>().HasOne(u => u.Deleter).WithOne().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Messege>().HasOne(m => m.Sender).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Messege>().HasOne(m => m.Reciver).WithMany().OnDelete(DeleteBehavior.Restrict);

        }
    }
}