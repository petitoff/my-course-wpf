using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BasicLoginSystem.Services;
using BasicLoginSystem.ViewModels;
using BasicLoginSystem.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BasicLoginSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //Rejestracja usług
            services.AddSingleton<UserSessionService>();
            
            // Rejestracja ViewModeli
            services.AddTransient<LoginViewModel>();
            services.AddSingleton<MainViewModel>();

            // Rejestracja widoków
            services.AddTransient<LoginView>();
            services.AddTransient<MainWindow>();
            services.AddTransient<MainView>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}