using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.ChatService.Resources;
using Core.UserService.Resources;

namespace Core.ChatService
{
    public interface IChatService
    {
        
        Task<ICollection<ChatReadResource>> GetChats(ReadChatResource model);
        Task<ICollection<MessageReadResource>> GetMesseges(ChatReadResource model);
        Task SendMessege(MessegeWriteResource model);
    }
}