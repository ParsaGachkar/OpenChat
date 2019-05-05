using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Data.Domain
{
    public class UserChat
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("ChatId")]
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}