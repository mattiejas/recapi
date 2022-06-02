using System.Linq;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Recapi.Application.Common.Interfaces;

namespace Recapi.Application.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailQuery : IRequest<EmployeeDetailVm>
    {
        public int Id { get; set; }

        public class GetEmployeeDetailQueryHandler : IRequestHandler<GetEmployeeDetailQuery, EmployeeDetailVm>
        {
            private readonly IRecapiDbContext _context;
            private readonly IMapper _mapper;

            public GetEmployeeDetailQueryHandler(IRecapiDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<EmployeeDetailVm> Handle(GetEmployeeDetailQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.Employees
                    .Where(e => e.EmployeeId == request.Id)
                    .ProjectTo<EmployeeDetailVm>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);

                return vm;
            }
        }
    }
}
