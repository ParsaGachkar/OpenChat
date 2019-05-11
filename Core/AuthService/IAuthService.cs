using System.Threading.Tasks;
namespace Core.AuthService
{
    public interface IAuthService
    {
        Task SendConfirmation(Resources.UserSendConfiramtionResource model);
        Task<Resources.UserVerifyResponseResource> VerifyConfirmation(Resources.UserVerifyResource model);
        Task<Resources.UserGetResponseResource> GetUser(Resources.UserGetResource model);
    }

    namespace Resources
    {
    }
}