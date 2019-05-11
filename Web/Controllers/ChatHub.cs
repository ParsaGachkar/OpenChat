using System.Threading.Tasks;
using Core.ChatService.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Web.Controllers
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessege(MessegeReadResource messege)
        {
            await Clients.Client(messege.Reciver.PhoneNumber).SendAsync("MessegeRecive", messege);
        }
    }
}