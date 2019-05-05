using System.Collections.Generic;
using System.Threading.Tasks;
using Core.ChatService.Resources;

namespace Core.ChatService
{
    public interface IChatService
    {
        Task<ICollection<ChatReadResource>> GetChats();
        Task<ICollection<MessegeReadResource>> GetMesseges();
        Task SendMessege(MessegeWriteResource model);
        Task SeenChat(MessegeReadResource model);
        Task EditMessege(MessegeUpdateResource model);
        Task DeleteMessege(MessegeDeleteResource model);
    }
}