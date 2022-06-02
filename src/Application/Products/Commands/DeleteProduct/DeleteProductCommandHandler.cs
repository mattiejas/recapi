﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Recapi.Application.Common.Exceptions;
using Recapi.Application.Common.Interfaces;
using Recapi.Domain.Entities;

namespace Recapi.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IRecapiDbContext _context;

        public DeleteProductCommandHandler(IRecapiDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            var hasOrders = _context.OrderDetails.Any(od => od.ProductId == entity.ProductId);
            if (hasOrders)
            {
                // TODO: Add functional test for this behaviour.
                throw new DeleteFailureException(nameof(Product), request.Id, "There are existing orders associated with this product.");
            }

            _context.Products.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
