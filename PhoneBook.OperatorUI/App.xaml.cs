using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ninject;
using PhoneBook.BLL.Interfaces;
using PhoneBook.BLL.Services;
using PhoneBook.DAL;
using PhoneBook.DAL.EF;
using PhoneBook.DAL.Interfaces;
using PhoneBook.DAL.Repositories;
using PhoneBook.OperatorUI.Models;
using PhoneBook.OperatorUI.ViewModels;
using PhoneBook.OperatorUI.Views;
using System.IO;
using System.Windows;

namespace PhoneBook.OperatorUI
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

            _kernel.Bind<PersonsList>().ToSelf().InSingletonScope();
            _kernel.Bind<IPersonService>().To<PersonService>().InTransientScope();
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();
            _kernel.Bind<IPersonRepository>().To<EFPersonRepository>().InTransientScope();
            _kernel.Bind<IPersonPhoneRepository>().To<EFPersonPhoneRepository>().InTransientScope();
            _kernel.Bind<IPersonAddressRepository>().To<EFPersonAddressRepository>().InTransientScope();
            _kernel.Bind<IDataSaver>().To<PersonsSaver>().InTransientScope();

            var appVM = _kernel.Get<MainWindowVM>();

            MainWindow = new MainWindow();
            MainWindow.DataContext = appVM;
            MainWindow.Show();
        }
    }
}
