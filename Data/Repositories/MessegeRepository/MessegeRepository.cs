using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Data.Domain;
using Data.Repositories.Abstracts;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

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
            return (await dbContext.Users.FindAsync(model.Id)).UserChats.Select(c => c.Chat).ToList() ?? new Collection<Chat>().ToList();
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.ICollection<Messege>> MessegesFor(Guid id)
        {
            Chat chat = await dbContext.Set<Chat>().Include(m=>m.UserChats).Include(m=>m.Messeges).FirstAsync(c=> c.Id == id);
            return chat.Messeges;
        }

        public Task SeenChat(Messege messege)
        {
            throw new NotImplementedException();
        }

        public async Task SendMessege(Messege messege)
        {
            await dbContext.Set<Messege>().AddAsync(messege);
        }
    }
}