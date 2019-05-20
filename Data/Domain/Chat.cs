using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Data.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Domain
{
    [Table("Chats")]
    public class Chat : IEntity<Guid>
    {


        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        [InverseProperty("Chat")]
        public ICollection<UserChat> UserChats { get; set; }
        [NotMapped]
        public ICollection<User> Users => UserChats.Select(uc => uc.User).ToList();
        [InverseProperty("Chat")]
        public ICollection<Messege> Messeges { get; set; }
        public Chat()
        {
            UserChats = new Collection<UserChat>();
            Messeges = new Collection<Messege>();
        }

    }
}