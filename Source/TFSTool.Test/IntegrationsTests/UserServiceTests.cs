using ExternalTfsService;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace TFSTool.Test.IntegrationsTests.ServiceTest
{
    public class UserServiceTests
    {

        [Fact]
        public async void GetAllUsers()
        {
            //Arrange
            var userService = new TfsUserService();

            // Act           
            var users = await userService.GetAllUsersAsync().ToListAsync();

            //Assert
            users.Should().NotBeNullOrEmpty();
        }
    }
}
