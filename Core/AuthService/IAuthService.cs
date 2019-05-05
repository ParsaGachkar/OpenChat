using System.Threading.Tasks;
namespace Core.AuthService
{
    public interface IAuthService
    {
        Task SendConfirmation();
        Task VerifyConfirmation();
        Task GetUser();
    }
}