using Microsoft.AspNetCore.Mvc;
using Sprint4dotnet.Controllers;
using Sprint4dotnet.Models;
using Xunit;
using Moq;
using Sprint4dotnet.Services;



namespace Sprint4dotnet.ClientsControllerTest
{
    public class ClientsControllerTests
    {
        [Fact]
        public async Task Register_ShouldReturnOk_WhenClientIsValid()
        {
            // Arrange
            var client = new Client { Name = "John Doe", Email = "john@example.com", Password = "securepassword" };
            var mockService = new Mock<IClientService>();
            mockService.Setup(s => s.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
           .Returns(Task.CompletedTask);
            var controller = new ClientsController(mockService.Object);

            // Act
            var result = await controller.Register(client);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }

}
