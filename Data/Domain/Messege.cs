using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Domain.Abstractions;

namespace Data.Domain
{
    [Table("Messeges")]
    public class Messege : IEntity<Guid>, IDeleteable<User, Guid>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Guid? DeleterId { get; set; }
        [ForeignKey("DeleterId")]
        public User Deleter { get; set; }
        [Required]
        public Guid SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        [Required]
        public String Context { get; set; }
        public Guid ChatId { get; set; }
        [ForeignKey("ChatId")]
        public Chat Chat { get; set; }

    }
}