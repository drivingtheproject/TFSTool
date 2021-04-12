using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using TFSTool.Services.Services;
using TFSTool.UI.Model;
using Xunit;

namespace TFSTool.Test.UnitTests.Services
{
    public class ExportServiceTests
    {

        [Theory, AutoData]
        public async void TestXmlExport(List<UserModel> users)
        {
            // Arrange
            var logger = new Mock<ILogger<XmlExportService>>();
            var exportService = new XmlExportService(logger.Object);

            // Act
            var result = await exportService.ExportAsync<UserModel>("data.xml", users);

            // Assert
            result.Should().BeTrue();
        }               
    }
}
