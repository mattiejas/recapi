using System.Linq;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Recapi.Application.Common.Interfaces;

namespace Recapi.Application.Employees.Queries.GetEmployeesList;

public class GetEmployeesListQuery : IRequest<EmployeesListVm>
{
    public class GetEmployeesListQueryHandler : IRequestHandler<GetEmployeesListQuery, EmployeesListVm>
    {
        private readonly IRecapiDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesListQueryHandler(IRecapiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeesListVm> Handle(GetEmployeesListQuery request, CancellationToken cancellationToken)
        {
            var employees = await _context.Employees
                .ProjectTo<EmployeeLookupDto>(_mapper.ConfigurationProvider)
                .OrderBy(e => e.Name)
                .ToListAsync(cancellationToken);

            var vm = new EmployeesListVm
            {
                Employees = employees
            };
                 
            return vm;
        }
    }
}