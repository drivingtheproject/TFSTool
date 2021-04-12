using AutoMapper;
using ExternalTfsService;
using ExternalTfsService.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSTool.Services.Services;
using TFSTool.Services.Services.Interfaces;
using TFSTool.UI.Model;
using TFSTool.UI.ViewModel;
using Xunit;

namespace TFSTool.Test.UnitTests.ViewModel
{
    public class UserOverviewViewModelTests
    {
        private readonly Mock<ILogger<UserOverviewViewModel>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ITfsUserService> _userService;
        private readonly Mock<IExportServiceFactory> _exportFactory;

        public UserOverviewViewModelTests()
        {

            var users = GetSampleUsers();

            _logger = new Mock<ILogger<UserOverviewViewModel>>();
            _mapper = new Mock<IMapper>();
            _mapper.Setup(x => x.Map<IEnumerable<TfsUser>, IEnumerable<UserModel>>(It.IsAny<IEnumerable<TfsUser>>()))
            .Returns(users.ModelUsers);

            _userService = new Mock<ITfsUserService>();
            _userService.Setup(x => x.GetAllUsersAsync()).Returns(users.TfsUsers.ToAsyncEnumerable());

            var exportService = new Mock<IExportService>();
            exportService.Setup(x => x.ExportAsync<UserModel>(It.IsAny<string>(), It.IsAny<List<UserModel>>())).Returns(Task.FromResult(true));

            _exportFactory = new Mock<IExportServiceFactory>();
            _exportFactory.Setup(x => x.GetService(It.IsAny<ExportType>())).Returns(exportService.Object);
        }

        [Fact]
        public void Model_Command_CanExecute_Test()
        {
            // Arrange           
            var userOverview = new UserOverviewViewModel(_userService.Object, _exportFactory.Object, _mapper.Object, _logger.Object);

            //Assert
            userOverview.DeleteUsersCommand.CanExecute(null).Should().BeFalse();
            userOverview.ReverseUsersCommand.CanExecute(null).Should().BeFalse();
            userOverview.ExportUsersCommand.CanExecute(null).Should().BeFalse();
            userOverview.GetUsersCommand.CanExecute(null).Should().BeTrue();
        }


        [Fact]
        public void Model_CanExecute_Reverse_Export_Command()
        {
            // Arrange           
            var userOverview = new UserOverviewViewModel(_userService.Object, _exportFactory.Object, _mapper.Object, _logger.Object);

            //Act
            userOverview.GetUsersCommand.Execute(null);

            //Assert
            userOverview.DeleteUsersCommand.CanExecute(null).Should().BeFalse();
            userOverview.ReverseUsersCommand.CanExecute(null).Should().BeTrue();
            userOverview.ExportUsersCommand.CanExecute(null).Should().BeTrue();
            userOverview.GetUsersCommand.CanExecute(null).Should().BeTrue();
        }

        [Fact]
        public void Model_Execute_GetUsers_ShouldHave_Users()
        {
            // Arrange           
            var userOverview = new UserOverviewViewModel(_userService.Object, _exportFactory.Object, _mapper.Object, _logger.Object);

            //Act
            userOverview.GetUsersCommand.Execute(null);

            //Assert
            userOverview.Users.Should().NotBeNullOrEmpty();
        }


        [Fact]
        public void Model_CanExecute_DeleteCommand()
        {
            // Arrange           
            var userOverview = new UserOverviewViewModel(_userService.Object, _exportFactory.Object, _mapper.Object, _logger.Object);

            //Act
            userOverview.SelectedUser = new UserModel();

            //Assert
            userOverview.DeleteUsersCommand.CanExecute(null).Should().BeTrue();
        }

        [Fact]
        public void Model_Execute_Delete_Must_DeleteUsers()
        {
            // Arrange          
            var users = GetSampleUsers();
            var userOverview = new UserOverviewViewModel(_userService.Object, _exportFactory.Object, _mapper.Object, _logger.Object);

            //Act
            userOverview.GetUsersCommand.Execute(null);
            userOverview.DeleteUsersCommand.Execute(new List<UserModel>(userOverview.Users));

            //Assert
            userOverview.Users.Should().BeNullOrEmpty();
            userOverview.SelectedUser.Should().BeNull();
            userOverview.DeleteUsersCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void Model_Execute_Reverse_Must_ReverseUsers()
        {
            // Arrange            
            var userOverview = new UserOverviewViewModel(_userService.Object, _exportFactory.Object, _mapper.Object, _logger.Object);

            //Act
            userOverview.GetUsersCommand.Execute(null);
            userOverview.ReverseUsersCommand.Execute(null);

            //Assert
            userOverview.Users.Should().NotBeNullOrEmpty();
            userOverview.Users[0].Id.Should().Be(3);
        }

        [Fact]
        public void Model_Execute_Export_Must_Successfull()
        {
            // Arrange            
            var userOverview = new UserOverviewViewModel(_userService.Object, _exportFactory.Object, _mapper.Object, _logger.Object);

            //Act            
            userOverview.GetUsersCommand.Execute(null);
            userOverview.IsEnabled = false;
            userOverview.ExportUsersCommand.Execute(null);

            //Assert
            userOverview.IsEnabled.Should().BeTrue();            
        }

        private (List<TfsUser> TfsUsers, List<UserModel> ModelUsers) GetSampleUsers()
        {
            var mockTfsÚserData = new List<TfsUser>() { new TfsUser(1, "test1", "ab1@bxc.com"), new TfsUser(2, "test2", "ab2@bxc.com"), new TfsUser(3, "test3", "ab3@bxc.com") };
            var mockÚserData = new List<UserModel>() { new UserModel { Id = 1, Name = "test1", Email = "ab1@bxc.com" }, new UserModel { Id = 2, Name = "test2", Email = "ab2@bxc.com" }, new UserModel { Id = 3, Name = "test3", Email = "ab3@bxc.com" } };
            return (mockTfsÚserData, mockÚserData);
        }
    }
}
