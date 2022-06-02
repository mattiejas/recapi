using System.Net;
using System.Threading.Tasks;
using Recapi.Application.Customers.Queries.GetCustomerDetail;
using Recapi.WebUI.IntegrationTests.Common;
using Xunit;

namespace Recapi.WebUI.IntegrationTests.Controllers.Customers
{
    public class GetById : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetById(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenId_ReturnsCustomerViewModel()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var id = "ALFKI";

            var response = await client.GetAsync($"/api/customers/get/{id}");

            response.EnsureSuccessStatusCode();

            var customer = await Utilities.GetResponseContent<CustomerDetailVm>(response);

            Assert.Equal(id, (string) customer.Id);
        }

        [Fact]
        public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            
            var invalidId = "AAAAA";

            var response = await client.GetAsync($"/api/customers/get/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
