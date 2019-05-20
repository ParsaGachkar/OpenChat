using System;

namespace Core.UserService.Resources
{
    public class ReadUserResource
    {
        public ReadUserResource(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; internal set; }
        public string PhoneNumber {get;set;}
    }
}