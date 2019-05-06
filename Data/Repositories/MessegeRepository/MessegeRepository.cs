using System;
using Data.Domain;
using Data.Repositories.Abstracts;

namespace Data.Repositories.MessegeRepository
{
    public class MessegeRepository : GenericRepository<Messege, Guid>, IMessegeRepository
    {
        public Messege Read(object id)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<System.Collections.Generic.ICollection<Chat>> GetChatsFor(User model)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<System.Collections.Generic.ICollection<Messege>> MessegesFor(Chat model)
        {
            throw new NotImplementedException();
        }
    }
}