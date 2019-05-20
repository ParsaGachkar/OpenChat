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

        public async Task SendMessege(MessegeReadResource messege)
        {
            await Clients.Client((await userService.ReadUser(messege.ReciverId)).PhoneNumber).SendAsync("MessegeRecive", messege);
        }
        public ChatHub(UserService userService)
        {
            this.userService = userService;
        }
    }
}