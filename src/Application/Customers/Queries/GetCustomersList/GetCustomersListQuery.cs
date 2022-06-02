using MediatR;

namespace Recapi.Application.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQuery : IRequest<CustomersListVm>
    {
    }
}
