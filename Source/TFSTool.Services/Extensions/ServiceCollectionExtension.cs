using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TFSTool.Services.Services;
using TFSTool.Services.Services.Interfaces;

namespace TFSTool.Services.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Add required services dependencies
        /// </summary>
        /// <param name="services">Service collection</param>       
        /// <returns></returns>
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.TryAddSingleton<XmlExportService>();
            services.TryAddSingleton<IExportServiceFactory, ExportServiceFactory>();
            return services;
        }
    }
}
