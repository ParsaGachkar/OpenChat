using System;

namespace Core.ChatService.Resources
{
    public class ChatReadResource
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } = Guid.NewGuid();
    }
}