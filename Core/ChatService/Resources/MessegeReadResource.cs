using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Domain;

namespace Core.ChatService.Resources
{
    public class MessegeReadResource
    {
        public Guid Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Guid? DeleterId { get; set; }
        public User Deleter { get; set; }
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid ReciverId { get; set; }
        public User Reciver { get; set; }
        public String Context { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}