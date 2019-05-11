using System.Linq;
using System;
using System.Threading.Tasks;
using Core.AuthService;
using Core.AuthService.Resources;
using Core.UserService;
using Microsoft.AspNetCore.Mvc;
using Core.UserService.Resources;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Route("/api/user")]
    [Authorize]
    public class UsersWebApiController : Controller
    {

        [HttpGet("/auth/{phone}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserAuthStart(string phone)
        {
            await _authService.SendConfirmation(new UserSendConfiramtionResource(phone));
            if (ModelState.IsValid)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("/auth/{phone}/{code}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserAuthVerify(string phone, int code)
        {
            var result = await _authService.VerifyConfirmation(new UserVerifyResource(phone, code));
            if (ModelState.IsValid)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var result = await _userService.ReadUserByPhone((HttpContext.User.Claims.FirstOrDefault(c => c.Type == "phone")?.Value) ?? "");
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> SetUserInfo([FromBody] EditUserResource model)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }
            await _userService.EditUser(model);
            return Ok();
        }
        [HttpGet("/{phone}")]
        public async Task<IActionResult> GetUserInfoSpecific(string phone)
        {
            var result = await _userService.ReadUserByPhone(phone ?? "");
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UsersWebApiController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
    }
}