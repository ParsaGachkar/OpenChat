using System.Threading.Tasks;
using Core.ChatService.Resources;
using Core.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Web.Controllers
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserService userService;

        public async Task SendMessage(MessageReadResource message)
        {
            await Clients.Client((await userService.ReadUser(message.ReciverId)).PhoneNumber).SendAsync("MessageRecive", message);
        }
        public ChatHub(UserService userService)
        {
            this.userService = userService;
        }
    }
}