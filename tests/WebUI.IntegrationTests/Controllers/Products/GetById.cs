using System.Net;
using System.Threading.Tasks;
using Recapi.Application.Products.Queries.GetProductDetail;
using Recapi.WebUI.IntegrationTests.Common;
using Xunit;

namespace Recapi.WebUI.IntegrationTests.Controllers.Products
{
    public class GetById : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetById(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenId_ReturnsProductViewModel()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var id = 1;

            var response = await client.GetAsync($"/api/products/get/{id}");

            response.EnsureSuccessStatusCode();

            var product = await Utilities.GetResponseContent<ProductDetailVm>(response);

            Assert.Equal(id, product.ProductId);
        }

        [Fact]
        public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var invalidId = 0;

            var response = await client.GetAsync($"/api/products/get/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
