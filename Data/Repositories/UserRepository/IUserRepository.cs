using System.Threading.Tasks;
using System;
using Data.Domain;
using Data.Repositories.Abstracts;

namespace Data.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        Task<User> ReadByPhone(string phone);
    }
}