using System;
using TFSTool.Services.Services.Interfaces;

namespace TFSTool.Services.Services
{
    public class ExportServiceFactory : IExportServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ExportServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Get Export service based on export type.
        /// </summary>
        /// <param name="exportType">Export type (Xml, Json, Csv)</param>
        /// <returns></returns>
        public IExportService GetService(ExportType exportType)
        {
            return exportType switch
            {
                ExportType.Xml => (IExportService)_serviceProvider.GetService(typeof(XmlExportService)),
                _ => throw new NotImplementedException($"{exportType} type export is not supported."),
            };
        }
    }
}
