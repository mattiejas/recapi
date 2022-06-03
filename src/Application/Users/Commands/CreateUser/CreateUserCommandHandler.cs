using System;
using System.Linq;
using Recapi.Application.Common.Exceptions;
using Recapi.Application.Users.Commands.CreateUser;

namespace Application.Users.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Recapi.Application.Common.Interfaces;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IRecapiDbContext _context;
        private readonly IUserManager _userManager;

        public CreateUserCommandHandler(IRecapiDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateUserAsync(request.Username, request.Password);
            
            if (result.Result.Succeeded)
            {
                return result.UserId;
            }

            throw new BadRequestException(result.Result);
        }
    }
}