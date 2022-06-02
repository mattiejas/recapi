﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Recapi.Application.Common.Interfaces;
using Recapi.Domain.Entities;

namespace Recapi.Application.Categories.Commands.UpsertCategory
{
    public class UpsertCategoryCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCategoryCommand, int>
        {
            private readonly IRecapiDbContext _context;

            public UpsertCategoryCommandHandler(IRecapiDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertCategoryCommand request, CancellationToken cancellationToken)
            {
                Category entity;

                if (request.Id.HasValue)
                {
                    entity = await _context.Categories.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Category();

                    _context.Categories.Add(entity);
                }

                entity.CategoryName = request.Name;
                entity.Description = request.Description;
                entity.Picture = request.Picture;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.CategoryId;
            }
        }
    }
}
