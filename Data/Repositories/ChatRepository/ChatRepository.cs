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
            var chat = await dbContext.Set<Chat>().Include(c => c.UserChats).Include(c => c.Users)
            .FirstOrDefaultAsync(c => c.Users.Any(u => u.Id == senderId)
                && c.Users.Any(u => u.Id == reciverId));
            return chat;
        }

        public async Task CreateChatFor(Guid senderId, Guid reciverId)
        {

            Chat chat = new Chat()
            {
                Id = Guid.NewGuid(),
                CreationDateTime = DateTime.Now,
            };
            UserChat sender = new UserChat() { UserId = senderId, ChatId = chat.Id, Id = Guid.NewGuid() ,User = dbContext.Users.Find(senderId)};
            UserChat reciver = new UserChat() { UserId = reciverId, ChatId = chat.Id, Id = Guid.NewGuid(), User = dbContext.Users.Find(reciverId) };
            chat.UserChats = new UserChat[]{
                    sender,
                    reciver
                };
            chat.Messeges = new Messege[] { };
            await base.Create(chat
            );
        }

        public IEnumerable<object> GetChatFor(object currentUserId, object selectedUserId)
        {
            throw new NotImplementedException();
        }
    }
}