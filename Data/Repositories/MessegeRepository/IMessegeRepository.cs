using System;
using Data.Domain;
using Data.Repositories.Abstracts;

namespace Data.Repositories.MessegeRepository
{
    public interface IMessegeRepository : IGenericRepository<Messege, Guid>
    {
        System.Threading.Tasks.Task<System.Collections.Generic.ICollection<Chat>> GetChatsFor(User model);
        System.Threading.Tasks.Task<System.Collections.Generic.ICollection<Messege>> MessegesFor(Chat model);
    }
}