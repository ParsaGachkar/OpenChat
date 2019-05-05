using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Domain.Abstractions;

namespace Data.Domain
{
    [Table("Users")]
    public class User : IEntity<Guid>, IDeleteable<User, Guid>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public Guid DeleterId { get; set; }
        public User Deleter { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public String PhoneNumber { get; set; }

    }
}