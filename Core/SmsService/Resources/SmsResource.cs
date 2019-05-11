using System.ComponentModel.DataAnnotations;
namespace Core.SmsService.Resources
{
    public class SmsResource
    {

        public SmsResource(string phoneNumber, string context)
        {
            this.PhoneNumber = phoneNumber;
            this.Context = context;

        }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Context { get; set; }
    }
}