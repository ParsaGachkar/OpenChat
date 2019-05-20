using System;

namespace Core.ChatService.Resources
{
    public class MessegeWriteResource
    {
         public Guid ReciverId { get; set; }
         public Guid SenderId { get; set; }
         public String Context { get; set; }
    }
}