using MediatR;

namespace Recapi.Application.Users.Commands.LoginCommand;

public class LoginCommand : IRequest<string?>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}