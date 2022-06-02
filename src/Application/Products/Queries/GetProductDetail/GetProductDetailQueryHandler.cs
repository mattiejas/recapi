using AutoMapper;
using Recapi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Recapi.Application.Common.Exceptions;
using Recapi.Application.Common.Interfaces;

namespace Recapi.Application.Products.Queries.GetProductDetail
{
    public class GetProductDetailQueryHandler : MediatR.IRequestHandler<GetProductDetailQuery, ProductDetailVm>
    {
        private readonly IRecapiDbContext _context;
        private readonly IMapper _mapper;

        public GetProductDetailQueryHandler(IRecapiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailVm> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Products
                .ProjectTo<ProductDetailVm>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.ProductId == request.Id, cancellationToken);

            if (vm == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            return vm;
        }
    }
}
