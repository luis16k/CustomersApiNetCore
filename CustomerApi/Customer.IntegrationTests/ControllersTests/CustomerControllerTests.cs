using Customer.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Customer.IntegrationTests.ControllersTests
{
    public class CustomerControllerI : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public CustomerControllerI(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_GetCustomers_Should_Return_OK()
        {

            var response = await _client.GetAsync("customer/all");

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_SearchCostumerByName_Should_Return_OK()
        {
            var response = await _client.GetAsync("customer/name/Jose");
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_SearchCostumerById_Should_Return_OK()
        {
            var response = await _client.GetAsync("customer/id/1");
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
