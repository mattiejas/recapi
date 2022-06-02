using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Recapi.Application.Customers.Queries.GetCustomerDetail;
using Recapi.Application.UnitTests.Common;
using Recapi.Persistence;
using Shouldly;
using Xunit;

namespace Recapi.Application.UnitTests.Customers.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomerDetailQueryHandlerTests
    { 
        private readonly RecapiDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerDetailQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }    

        [Fact]
        public async Task GetCustomerDetail()
        {
            var sut = new GetCustomerDetailQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetCustomerDetailQuery { Id = "JASON" }, CancellationToken.None);

            ShouldBeTestExtensions.ShouldBeOfType<CustomerDetailVm>(result);
            ShouldBeStringTestExtensions.ShouldBe(result.Id, "JASON");
        }
    }
}
