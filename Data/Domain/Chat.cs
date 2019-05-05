using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Data.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Domain
{
    [Table("Chats")]
    public class Chat : IEntity<Guid>, IDeleteable<User, Guid>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Guid DeleterId { get; set; }
        [ForeignKey("DeleterId")]
        public User Deleter { get; set; }
        [InverseProperty("Chat")]
        public ICollection<UserChat> UserChats { get; set; }
        public ICollection<User> Users => UserChats.Select(uc => uc.User).ToArray();
        [InverseProperty("Chat")]
        public ICollection<Messege> Messeges { get; set; }

    }
}