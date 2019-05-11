using System;
using System.Threading.Tasks;
using Data.Domain;
using Data.Repositories.Abstracts;

namespace Data.Repositories.MessegeRepository
{
    public class MessegeRepository : GenericRepository<Messege, Guid>, IMessegeRepository
    {
        //TODO: Implement This
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

        public Task SeenChat(Messege messege)
        {
            throw new NotImplementedException();
        }

        public Task SendMessege(Messege messege)
        {
            throw new NotImplementedException();
        }
    }
}