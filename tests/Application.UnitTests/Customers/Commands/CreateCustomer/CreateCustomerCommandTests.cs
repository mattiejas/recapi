using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Recapi.Application.Customers.Commands.CreateCustomer;
using Recapi.Application.UnitTests.Common;
using Xunit;

namespace Recapi.Application.UnitTests.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandTests : CommandTestBase
    {
        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateCustomerCommand.Handler(_context, mediatorMock.Object);
            var newCustomerId = "QAZQ1";

            // Act
            var result = sut.Handle(new CreateCustomerCommand { Id = newCustomerId }, CancellationToken.None);


            // Assert
            mediatorMock.Verify<Task>(m => m.Publish(It.Is<CustomerCreated>(cc => cc.CustomerId == newCustomerId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
