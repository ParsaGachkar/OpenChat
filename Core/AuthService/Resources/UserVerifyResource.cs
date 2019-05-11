namespace Core.AuthService.Resources
{
    public class UserVerifyResource
    {
        public string PhoneNumber { get; set; }
        public int Code { get; set; }
    }
}