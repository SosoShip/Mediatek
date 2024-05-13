using System.Configuration;
using System.Data;
using System.Security.Policy;
using System.Windows;
using SVE.Mediatek.Model;
using SVE.Mediatek.View;
using SVE.Mediatek.ViewModel;
using SVE.Mediatek.DAL;
using Microsoft.Extensions.DependencyInjection;
using SVE.Mediatek.DAL.Repository;
using SVE.Mediatek.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.ViewModel.ViewModels;
using SVE.Mediatek.ViewModel.Mappers;

namespace SVE.Mediatek
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
        }


        /// <summary>
        /// Adding and configuring services
        /// </summary>
        /// <returns>Services's Collection : ViewModel, StaffRepository and AbsenceRepository</returns>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //Register the DbContext with the dependency injection service.
            //Each instance of a Repository will automatically inject an instance
            //of mediatekContext into the constructor.
            services.AddDbContext<MediatekContext>(optionsBuilder =>
                optionsBuilder.UseSqlServer("Data Source=.\\" +
                "SQLEXPRESS;Initial Catalog=Mediatek;Integrated Security=true;TrustServerCertificate=true;"));
            //TODO, a la place de l'url, utiliser : Configuration.GetConnectionString("DefaultConnection") 
            // + penser a implementer l'interface IConfiguration

            // Adding services : ViewModels :
            services.AddSingleton<StaffChangeViewModel>();
            services.AddSingleton<StaffAddViewModel>();
            services.AddSingleton<StaffHandlerViewModel>();
            services.AddSingleton<ConnectionViewModel>();
            services.AddSingleton<AbsenceAddViewModel>();
            services.AddSingleton<AbsenceChangeViewModel>();
            services.AddSingleton<AbsenceHandlerViewModel>();
            // Adding services : Repositories :
            services.AddSingleton<IRepository<StaffEntity>, Repository<StaffEntity>>();
            services.AddSingleton<IAbsenceRepository, AbsenceRepository>();
            // Adding services : Mappers :
            services.AddAutoMapper(configuration => configuration.AddProfile<MappingProfile>());

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Opens the login window.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var connectionWindow = new Connection();

            // Initialization of ConnectionViewModel and Binding of the connection window's view-viewModel
            var viewModel = Services.GetService<ConnectionViewModel>();
            viewModel.ShowStaffAction = () => ShowStaff(connectionWindow);
            connectionWindow.DataContext = viewModel;

            connectionWindow.Show();
        }

        public void ShowStaff(Window previousWindow)
        {
            var StaffHandlerWindow = new StaffHandler();

            // Initialization of StaffHandlerViewModel and Binding of the connection window's view-viewModel
            var viewModel = Services.GetService<StaffHandlerViewModel>();
            viewModel.ShowAddStaffAction = () => ShowAddStaff(StaffHandlerWindow);
            viewModel.ShowChangeStaffAction = (staff) => ShowChangeStaff(staff, StaffHandlerWindow);
            viewModel.ShowAbsenceAction = (staff) => ShowAbsence(staff, StaffHandlerWindow);
           
            StaffHandlerWindow.DataContext = viewModel;

            // Displaying of StaffHandlerViewModel
            StaffHandlerWindow.Show();
            previousWindow.Close();
        }
        
        public void ShowAddStaff( Window staffHandler)
        {
            var StaffAddWindow = new StaffAdd();

            // Initialization of StaffAddViewModel and Binding of the connection window's view-viewModel
            var viewModel = Services.GetService<StaffAddViewModel>();
            viewModel.ShowStaffAction= () => ShowStaff(StaffAddWindow);

            StaffAddWindow.DataContext = viewModel;

            // Displaying of StaffAddViewModel
            staffHandler.Close();
            StaffAddWindow.Show(); 
        }

        public void ShowChangeStaff(StaffModel staff, Window staffHandler)
        {
            var staffChangeWindow = new StaffChange();

            // Initialization of StaffChangeViewModel and Binding of the connection window's view-viewModel
            var viewModel = Services.GetService<StaffChangeViewModel>();
            viewModel.TheStaff = staff;
            viewModel.ShowStaffAction = () => ShowStaff(staffChangeWindow);

            staffChangeWindow.DataContext = viewModel;

            // Displaying of StaffAddViewModel
            staffChangeWindow.Show();
            staffHandler.Close();
        }

        public void ShowAbsence(StaffModel staff, Window previousWindow)
        {
            var AbsenceHandlerWindow = new AbsenceHandler();

            // Initialization of AbsenceHandlerViewModel and Binding of the connection window's view-viewModel
            var viewModel = Services.GetService<AbsenceHandlerViewModel>();
            viewModel.TheStaff = staff;
            viewModel.ShowStaffAction = () => ShowStaff(AbsenceHandlerWindow);
            viewModel.ShowAddAbsenceAction = () => ShowAddAbsence(staff, AbsenceHandlerWindow);
            viewModel.ShowChangeAbsenceAction = (selectedAbsence) => ShowChangeAbsence(staff, selectedAbsence, AbsenceHandlerWindow);

            AbsenceHandlerWindow.DataContext = viewModel;

            // Displaying of AbsenceHandlerViewModel
            AbsenceHandlerWindow.Show();
            previousWindow.Close();
        }

        public void ShowAddAbsence(StaffModel staff, Window absenceHandler)
        {
            var AbsenceAddWindow = new AbscenceAdd();

            // Initialization of AbsenceAddViewModel and Binding of the connection window's view-viewModel
            var viewModel = Services.GetService<AbsenceAddViewModel>();
            viewModel.TheStaff = staff;
            viewModel.ShowAbsenceAction = () => ShowAbsence(staff, AbsenceAddWindow);

            AbsenceAddWindow.DataContext = viewModel;

            // Displaying of AbsenceAddViewModel
            AbsenceAddWindow.Show();
            absenceHandler.Close();
        }

        public void ShowChangeAbsence(StaffModel staff, AbsenceModel absence, Window absenceHandler)
        {
            var AbsenceChangeWindow = new AbsenceChange();

            // Initialization of AbsenceChangeViewModel and Binding of the connection window's view-viewModel
            var viewModel = Services.GetService<AbsenceChangeViewModel>();
            viewModel.TheStaff = staff;
            viewModel.TheAbsence = absence;
            viewModel.ShowAbsenceAction = () => ShowAbsence(staff, AbsenceChangeWindow);

            AbsenceChangeWindow.DataContext = viewModel;

            // Displaying of AbsenceChangeViewModel
            AbsenceChangeWindow.Show();
            absenceHandler.Close();
        }
    }
}
