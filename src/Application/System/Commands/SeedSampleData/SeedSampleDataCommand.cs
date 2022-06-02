using MediatR;
using Recapi.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Recapi.Application.Common.Interfaces;

namespace Recapi.Application.System.Commands.SeedSampleData
{
    public class SeedSampleDataCommand : IRequest
    {
    }

    public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
    {
        private readonly IRecapiDbContext _context;
        private readonly IUserManager _userManager;

        public SeedSampleDataCommandHandler(IRecapiDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new SampleDataSeeder(_context, _userManager);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
