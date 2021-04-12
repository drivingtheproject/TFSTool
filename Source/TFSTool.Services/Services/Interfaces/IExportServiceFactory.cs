namespace TFSTool.Services.Services.Interfaces
{
    public interface IExportServiceFactory
    {
        /// <summary>
        /// Get Export service based on export type.
        /// </summary>
        /// <param name="exportType">Export type (ex. Xml, Json, Csv)</param>
        /// <returns></returns>
        public IExportService GetService(ExportType exportType);
    }
}
