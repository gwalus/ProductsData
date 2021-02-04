using Autofac;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoesApp.Data;
using ShoesApp.ViewModel;
using System;
using System.Windows;

namespace ShoesApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite("Data Source = shoes.db");
            });

            services.AddSingleton<WindowViewModel>();
            services.AddSingleton<StatisticsViewModel>();
            services.AddSingleton<ChartsViewModel>();
            services.AddSingleton<ProductsViewModel>();
            services.AddSingleton<MainWindow>();

            services.AddSingleton<IDataRepository, DataRepository>();
            services.AddSingleton<IStatisticsService, StatisticsService>();
            services.AddSingleton<IDialogCoordinator, DialogCoordinator>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}