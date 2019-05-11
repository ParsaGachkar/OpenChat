using System.Linq;
using Core.AuthService.Resources;
using Microsoft.AspNetCore.SignalR;

namespace Web.Controllers
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Claims?.FirstOrDefault(c => c.Type == CustomClaim.PhoneNumber)?.Value;
        }
    }
}