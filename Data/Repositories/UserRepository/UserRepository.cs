using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Domain;
using Data.Repositories.Abstracts;

namespace Data.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User, Guid>, IUserRepository
    {
        public async Task<User> ReadByPhone(string phone)
        {
            await Task.CompletedTask;
            return dbContext.Users.FirstOrDefault(u => u.PhoneNumber == phone);
        }
    }
}