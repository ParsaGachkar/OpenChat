using System;
using System.ComponentModel.DataAnnotations;
using Data.Domain.Abstractions;

namespace Data.Domain
{
    public class Messege : IEntity<Guid>, IDeleteable<User, Guid>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Guid DeleterId { get; set; }
        public User Deleter { get; set; }
        [Required]
        public User SenderId { get; set; }
        public User Sender { get; set; }
        [Required]
        public String Context { get; set; }

    }
}