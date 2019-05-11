using System.ComponentModel.DataAnnotations;

namespace Core.AuthService
{

    namespace Resources
    {
        public class UserSendConfiramtionResource
        {
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            public UserSendConfiramtionResource(string phoneNumber)
            {
                this.PhoneNumber = phoneNumber;

            }
        }
    }
}