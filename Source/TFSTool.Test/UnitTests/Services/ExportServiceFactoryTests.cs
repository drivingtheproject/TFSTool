using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using TFSTool.Services.Services;
using Xunit;

namespace TFSTool.Test.UnitTests.Services
{
    public class ExportServiceFactoryTests
    {

        [Fact]
        public void XmlExportService_ShouldNot_ThrowException()
        {
            // Arrange
            var logger = new Mock<ILogger<XmlExportService>>();
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(typeof(XmlExportService)))
                .Returns(new XmlExportService(logger.Object));
            var exportServiceFactory = new ExportServiceFactory(serviceProvider.Object);

            //Act
            var service = exportServiceFactory.GetService(ExportType.Xml);

            //Assert 
            service.Should().NotBeNull();
        }

        [Fact]
        public void JsonExportService_Should_ThrowException()
        {
            // Arrange
            var logger = new Mock<ILogger<XmlExportService>>();
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(XmlExportService)))
                .Returns(new XmlExportService(logger.Object));

            var exportServiceFactory = new ExportServiceFactory(serviceProvider.Object);

            //Act and Assert 
            Assert.Throws<NotImplementedException>(
                () => exportServiceFactory.GetService(ExportType.Json));

        }
    }
}
