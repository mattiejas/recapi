using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Recapi.Application.Customers.Queries.GetCustomersList;
using Recapi.Application.UnitTests.Common;
using Recapi.Persistence;
using Shouldly;
using Xunit;

namespace Recapi.Application.UnitTests.Customers.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomersListQueryHandlerTests
    {
        private readonly RecapiDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCustomersTest()
        {
            var sut = new GetCustomersListQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetCustomersListQuery(), CancellationToken.None);

            ShouldBeTestExtensions.ShouldBeOfType<CustomersListVm>(result);

            ShouldBeTestExtensions.ShouldBe(result.Customers.Count, 3);
        }
    }
}