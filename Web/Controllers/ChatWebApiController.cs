using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("/api/chat")]
    public class ChatWebApiController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetMesseges()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessege()
        {
            throw new NotImplementedException();
        }

    }
}