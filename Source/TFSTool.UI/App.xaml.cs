using AutoMapper;
using ExternalTfsService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Windows;
using TFSTool.Services.Extensions;
using TFSTool.UI.Model.Mappings;
using TFSTool.UI.View;
using TFSTool.UI.ViewModel;

namespace TFSTool.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Build Configurations
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            // Configure Logger
            AddLoggerConfiguration();
          
            // Configure Services
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();            
     
            // Start Window
            var startUpWindow = ServiceProvider.GetRequiredService<MainWindow>();
            startUpWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {

            // Register logging.
            services.AddLogging(configure => configure.AddSerilog());
            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            // Register AutoMapper.
            var mappingConfiguration = new MapperConfiguration(mc => { mc.AddProfile(new UserModelMappingProfile()); });
            services.AddSingleton(mappingConfiguration.CreateMapper());
           
            // Register all ViewModels.
            services.AddTransient<UserOverviewViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<MainViewModel>();

            // Register all views.
            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(HomeView));
            services.AddTransient(typeof(UserOverviewView));

            // Register all services.
            services.AddSingleton<ITfsUserService, TfsUserService>();
            services.AddServiceDependencies();                                
        }

        private void AddLoggerConfiguration()
        {           
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("TFSTool.log")
            .CreateLogger();           
        }

    }
}
