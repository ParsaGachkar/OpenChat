using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Domain;
using Data.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.ChatRepository
{
    public class ChatRepository : GenericRepository<Chat, Guid>, IChatRepository
    {
        public async Task<Chat> GetChatFor(Guid senderId, Guid reciverId)
        {
            var chat = (await dbContext.Set<Chat>().Include(c=>c.UserChats).ThenInclude(uc=>uc.User)
            .FirstOrDefaultAsync(c=>c.UserChats.Any(uc=>uc.UserId == senderId) && c.UserChats.Any(uc=>uc.UserId == reciverId)));
            return chat;
        }

        public async Task<ICollection<Chat>> GetChats(Guid currentUserId)
        {
             var chat = await dbContext.Set<UserChat>().Include(uc=>uc.Chat).Include(uc=>uc.User)
            .Where(c => c.User.Id == currentUserId).Select(uc=>uc.Chat).Distinct().ToListAsync();
            return chat;
        }

        public async Task<Chat> CreateChatFor(Guid senderId, Guid reciverId)
        {

            Chat chat = new Chat()
            {
                Id = Guid.NewGuid(),
                CreationDateTime = DateTime.Now,
            };
            UserChat sender = new UserChat() { UserId = senderId, ChatId = chat.Id, Id = Guid.NewGuid() ,User = dbContext.Users.Find(senderId)};
            UserChat reciver = new UserChat() { UserId = reciverId, ChatId = chat.Id, Id = Guid.NewGuid(), User = dbContext.Users.Find(reciverId) };
            chat.UserChats = new Collection<UserChat>{
                    sender,
                    reciver
                };
            chat.Messeges = new Collection<Messege>().ToList();
            await base.Create(chat
            );
            return chat;
        }

        public IEnumerable<object> GetChatFor(object currentUserId, object selectedUserId)
        {
            throw new NotImplementedException();
        }
    }
}