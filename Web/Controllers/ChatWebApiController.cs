using System.Linq;
using System;
using System.Threading.Tasks;
using Core.ChatService;
using Core.ChatService.Resources;
using Core.UserService.Resources;
using Microsoft.AspNetCore.Mvc;
using Core.UserService;
using Microsoft.AspNetCore.Authorization;

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
                    new ReadUserResource
                    (
                        (
                            await _userService.ReadUserByPhone(
                                HttpContext.User.Claims.FirstOrDefault(c => c.Type == "phone")?.Value ?? ""
                            )
                        ).Id
                    )
                )
            );
            return base.Ok(chats);
        }
        [HttpGet]
        public async Task<IActionResult> GetMesseges([FromBody] ChatReadResource model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _chatService.GetMesseges(model));
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