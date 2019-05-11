using System.Collections.Generic;
using System.Threading.Tasks;
using Core.ChatService.Resources;
using Core.UserService.Resources;

namespace Core.ChatService
{
    public interface IChatService
    {
        Task<ICollection<ChatReadResource>> GetChats(ReadUserResource model);
        Task<ICollection<MessegeReadResource>> GetMesseges(ChatReadResource model);
        Task SendMessege(MessegeWriteResource model);
        Task EditMessege(MessegeUpdateResource model);
        Task DeleteMessege(MessegeDeleteResource model);
        Task SeenChat(ChatReadResource model);
    }
}