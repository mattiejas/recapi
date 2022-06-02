using AutoMapper;
using MediatR;
using Recapi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Recapi.Application.Common.Exceptions;
using Recapi.Application.Common.Interfaces;

namespace Recapi.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailVm>
    {
        private readonly IRecapiDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerDetailQueryHandler(IRecapiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDetailVm> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            return _mapper.Map<CustomerDetailVm>(entity);
        }
    }
}
