using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Recapi.Application.Common.Interfaces;

namespace Recapi.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, CategoriesListVm>
    {
        private readonly IRecapiDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandler(IRecapiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoriesListVm> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CategoriesListVm
            {
                Categories = categories,
                Count = categories.Count
            };

            return vm;
        }
    }
}