global using Xunit;
using Customer.Domain.Models.Adapters;
using Customer.Domain.Service.Contracts;
using Customer.Domain.Service.Implementations;
using Customer.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Customer.UnitTests
{
    public class CustomerServiceTests
    {
        private Mock<ILogger<CustomerService>> _loggerMock;
        private Mock<DbContextApi> _contextMock;
        private ICustomerService _customerService;

        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<CustomerService>>();
            _contextMock = new Mock<DbContextApi>();
            _customerService = new CustomerService(_loggerMock.Object, _contextMock.Object);
        }

        [Fact]
        public async void AddCustomer()
        {
            //Arrange
            SetUp();            
            var name = new NameAdapter() { Name = "Jose" };

            //Act
            var result = await _customerService.Add(name).ConfigureAwait(false);

            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(result.Name, "Jose");
        }
    }
}