using System;
using System.Windows;

namespace TFSTool.UI.Extensions
{
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Get service provider for Current Application
        /// </summary>
        /// <param name="application">Current application</param>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider(this Application application)
        {
            return ((App)application).ServiceProvider;
        }
    }
}
