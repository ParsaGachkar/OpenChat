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
        public User Deleter { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Messege> Messeges { get; set; }

    }
}