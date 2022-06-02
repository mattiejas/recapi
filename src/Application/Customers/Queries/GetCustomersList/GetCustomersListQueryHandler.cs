using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Recapi.Application.Common.Interfaces;

namespace Recapi.Application.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, CustomersListVm>
    {
        private readonly IRecapiDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersListQueryHandler(IRecapiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomersListVm> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers
                .ProjectTo<CustomerLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CustomersListVm
            {
                Customers = customers
            };

            return vm;
        }
    }
}
