using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Domain.Abstractions;

namespace Data.Domain
{
    [Table("Users")]
    public class User : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
       
        [DataType(DataType.PhoneNumber)]
        [Required]
        public String PhoneNumber { get; set; }
        [InverseProperty("User")]
        public ICollection<UserChat> UserChats { get; set; }
        
        public User()
        {
            UserChats = new Collection<UserChat>();
        }
    }
}