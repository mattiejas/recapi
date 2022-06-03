using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Recapi.Application.Common.Exceptions;
using Recapi.Application.Common.Interfaces;
using Recapi.Application.Common.Models;
using NotImplementedException = System.NotImplementedException;

namespace Recapi.Application.Users.Commands.LoginCommand;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string?>
{
    private readonly IUserManager _userManager;

    public LoginCommandHandler(IUserManager userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<string?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validateResult = await _userManager.ValidateCredentialsAsync(request.Username, request.Password);

        if (!validateResult.Result.Succeeded) return null;
        
        var generateResult = await _userManager.GenerateTokenAsync(validateResult.UserId);
        if (!generateResult.Result.Succeeded)
        {
            throw new BadRequestException(generateResult.Result);
        }
        
        return generateResult.Token;
    }
}