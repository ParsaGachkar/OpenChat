using System.ComponentModel.DataAnnotations;

namespace Core.AuthService.Resources
{
    public class UserVerifyResource
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public int Code { get; set; }
        public UserVerifyResource(string phoneNumber, int code)
        {
            this.PhoneNumber = phoneNumber;
            this.Code = code;

        }
    }
}