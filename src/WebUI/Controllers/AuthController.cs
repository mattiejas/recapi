using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recapi.Application.Users.Commands.CreateUser;
using Recapi.Application.Users.Commands.LoginCommand;

namespace Recapi.WebUI.Controllers
{
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand createUserCommand)
        {
            await Mediator.Send(createUserCommand);
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            var token = await Mediator.Send(loginCommand);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }
    }
}