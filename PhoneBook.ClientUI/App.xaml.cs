using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Ninject;
using PhoneBook.BLL.Interfaces;
using PhoneBook.BLL.Services;
using PhoneBook.ClientUI.Models;
using PhoneBook.ClientUI.ViewModels;
using PhoneBook.ClientUI.Views;
using PhoneBook.DAL;
using PhoneBook.DAL.EF;
using PhoneBook.DAL.Interfaces;
using PhoneBook.DAL.Models;
using PhoneBook.DAL.Repositories;
using System;
using System.IO;
using System.Windows;

namespace PhoneBook.ClientUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IKernel? _kernel;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            _kernel = new StandardKernel();

            _kernel.Bind<ApplicationContext>().ToSelf().InThreadScope()
                .WithConstructorArgument("options", new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString).Options);

            _kernel.Bind<PersonViewModel>().ToSelf().InTransientScope();
            _kernel.Bind<SearchCriteria>().ToSelf().InTransientScope();
            _kernel.Bind<IPersonService>().To<PersonService>().InTransientScope();
            _kernel.Bind<IPersonRepository>().To<EFPersonRepository>().InTransientScope();
            _kernel.Bind<IPersonPhoneRepository>().To<EFPersonPhoneRepository>().InTransientScope();
            _kernel.Bind<IPersonAddressRepository>().To<EFPersonAddressRepository>().InTransientScope();
            _kernel.Bind<IDataSaver>().To<PersonsSaver>().InTransientScope();

            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();

            var appVM = _kernel.Get<MainWindowVM>();

            MainWindow = new MainWindow();
            MainWindow.DataContext = appVM;
            MainWindow.Show();
        }

        public class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
        {
            public ApplicationContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                IConfigurationRoot config = builder.Build();

                string connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
                return new ApplicationContext(optionsBuilder.Options);
            }
        }
    }
}
