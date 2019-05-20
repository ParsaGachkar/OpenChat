using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Threading.Tasks;
using Core.ChatService;
using Core.ChatService.Resources;
using Core.UserService.Resources;
using Microsoft.AspNetCore.Mvc;
using Core.UserService;
using Microsoft.AspNetCore.Authorization;
using Core.AuthService.Resources;

namespace Web.Controllers
{
    [Route("/api/chat")]
    [Authorize]
    public class ChatWebApiController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var chats =
            (await _chatService.GetChats
                (
                    new ReadChatResource(){
                        currentUserId = 
                        (await _userService.ReadUserByPhone(User.Claims.First(c=>c.Type == CustomClaim.PhoneNumber).Value)).Id,
                    }
                )
            );
            return base.Ok(chats);
        }
        [HttpGet("messeges/{id}")]
        public async Task<IActionResult> GetMesseges(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _chatService.GetMesseges(new ChatReadResource(){Id = id}));
        }
        [HttpPost]
        public async Task<IActionResult> SendMessege([FromBody] MessegeWriteResource model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _chatService.SendMessege(model);
            return Ok();
        }
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatWebApiController(IChatService chatService, IUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }
    }
}