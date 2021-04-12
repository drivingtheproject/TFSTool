using System.Collections.Generic;
using System.Threading.Tasks;

namespace TFSTool.Services.Services.Interfaces
{
    public interface IExportService
    {
        /// <summary>
        /// Export data to selected format.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="fileName">File Name</param>
        /// <param name="data">Data to be exported</param>
        /// <returns></returns>
        Task<bool> ExportAsync<T>(string fileName, IEnumerable<T> data);
    }
}
