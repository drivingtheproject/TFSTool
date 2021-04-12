using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TFSTool.Services.Services.Interfaces;

namespace TFSTool.Services.Services
{
    public class XmlExportService : IExportService
    {
        private readonly ILogger<XmlExportService> _logger;
        public XmlExportService(ILogger<XmlExportService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Export data to xml.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="fileName">File Name</param>
        /// <param name="data">Data to be exported</param>
        /// <returns></returns>
        public async Task<bool> ExportAsync<T>(string fileName, IEnumerable<T> data)
        {
            var result = false;
            try
            {
                result = await Task.Factory.StartNew(() =>
                {

                    using StreamWriter outputFile = new StreamWriter(fileName);
                    var x = new XmlSerializer(data.GetType());
                    x.Serialize(outputFile, data);
                    return true;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail to export : {ex.Message}");
            }

            return result;

        }
    }
}