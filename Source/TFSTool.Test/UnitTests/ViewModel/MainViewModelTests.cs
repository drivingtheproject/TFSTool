using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TFSTool.UI.ViewModel;
using Xunit;

namespace TFSTool.Test.UnitTests.ViewModel
{
    public class MainViewModelTests
    {
        [Fact]
        public void MainViewModel_SelectedView_MustBe_HomeViewModel()
        {
            // Arrange
            var logger = new Mock<ILogger<MainViewModel>>().Object;
            var home = new HomeViewModel(null);
            var userOverview = new UserOverviewViewModel(null,null,null,null);
            var mainViewModel = new MainViewModel(logger, home, userOverview);

            //Asset
            mainViewModel.SelectedIndex.Should().Be(0);
            mainViewModel.SelectedView.Should().NotBeNull().And.Be(home);

        }

        [Fact]
        public void MainViewModel_ChangeSelectedView_MustBe_UserOverviewViewModel()
        {
            // Arrange
            var logger = new Mock<ILogger<MainViewModel>>().Object;
            var home = new HomeViewModel(null);
            var userOverview = new UserOverviewViewModel(null, null, null, null);
            var mainViewModel = new MainViewModel(logger, home, userOverview);

            //Act
            mainViewModel.SelectedIndex = 1;

            //Asset
            mainViewModel.SelectedView.Should().NotBeNull().And.Be(userOverview);

        }
    }
}
