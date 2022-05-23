using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Customer.IntegrationTests.ControllersTests
{
    public class PurchaseControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public PurchaseControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        //TODO

    }
}
