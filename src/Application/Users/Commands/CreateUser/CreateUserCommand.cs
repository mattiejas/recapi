using MediatR;

namespace Recapi.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}