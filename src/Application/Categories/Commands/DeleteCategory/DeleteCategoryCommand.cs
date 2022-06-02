using MediatR;
using Recapi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Recapi.Application.Common.Exceptions;
using Recapi.Application.Common.Interfaces;

namespace Recapi.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
        {
            private readonly IRecapiDbContext _context;

            public DeleteCategoryCommandHandler(IRecapiDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Categories
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Category), request.Id);
                }

                _context.Categories.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
