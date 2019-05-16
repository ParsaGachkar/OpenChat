using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Data.Domain;
using Data.Repositories.Abstracts;
using System.Collections.ObjectModel;

namespace Data.Repositories.MessegeRepository
{
    public class MessegeRepository : GenericRepository<Messege, Guid>, IMessegeRepository
    {
        public Messege Read(object id)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.ICollection<Chat>> GetChatsFor(User model)
        {
            return (await dbContext.Users.FindAsync(model.Id)).UserChats.Select(c => c.Chat).ToArray() ?? new Chat[] { };
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.ICollection<Messege>> MessegesFor(Chat model)
        {
            return (await dbContext.Set<Chat>().FindAsync(model.Id)).Messeges;
        }

        public Task SeenChat(Messege messege)
        {
            throw new NotImplementedException();
        }

        public async Task SendMessege(Messege messege)
        {
            await this.Create(messege);
        }
    }
}