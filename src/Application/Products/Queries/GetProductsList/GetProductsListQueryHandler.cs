using System.Linq;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Recapi.Application.Common.Interfaces;

namespace Recapi.Application.Products.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, ProductsListVm>
    {
        private readonly IRecapiDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsListQueryHandler(IRecapiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductsListVm> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.ProductName)
                .ToListAsync(cancellationToken);

            var vm = new ProductsListVm
            {
                Products = products,
                CreateEnabled = true // TODO: Set based on user permissions.
            };

            return vm;
        }
    }
}
