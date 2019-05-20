using System;
using System.Threading.Tasks;
using Data.Domain;
using Data.Repositories.Abstracts;

namespace Data.Repositories.ChatRepository
{
    public interface IChatRepository : IGenericRepository<Chat, Guid>
    {
        
        Task<Chat> CreateChatFor(Guid senderId, Guid reciverId);
        Task<Chat> GetChatFor(Guid senderId, Guid reciverId);
    }
}