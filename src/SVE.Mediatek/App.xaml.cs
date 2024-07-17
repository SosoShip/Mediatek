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
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


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

            // Adding services : ViewModels :
            services.AddSingleton<StaffChangeViewModel>();
            services.AddSingleton<StaffAddViewModel>();
            services.AddSingleton<StaffHandlerViewModel>();
            services.AddSingleton<ConnectionViewModel>();
            services.AddSingleton<AbsenceAddViewModel>();
            services.AddSingleton<AbsenceChangeViewModel>();
            services.AddSingleton<AbsenceHandlerViewModel>();
            // Adding services : ManagerInitializer :
            services.AddSingleton<ManagerInitializer>();
            // Adding services : Repositories :
            services.AddSingleton<IStaffRepository, StaffRepository>();
            services.AddSingleton<IAbsenceRepository, AbsenceRepository>();
            services.AddSingleton<IManagerRepository<ManagerEntity>, ManagerRepository>();
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

            // Initialization of a manager
            // In this school app, this manager will remain for life, cannot resign or be replaced.
            var newManager = Services.GetService<ManagerInitializer>();
            Task.Run(() => newManager.InitializeManager()).Wait();
            

            var connectionWindow = new Connection();

            // Initialization of ConnectionViewModel and Binding of the connection window's view-viewModel
            var viewModel = Services.GetService<ConnectionViewModel>();
            viewModel.ShowStaffAction = async () => await ShowStaff(connectionWindow);
            connectionWindow.DataContext = viewModel;

            connectionWindow.Show();
        }

        public async Task ShowStaff(Window previousWindow)
        {
            var StaffHandlerWindow = new StaffHandler();

            // Initialization of StaffHandlerViewModel and Binding of the connection window's view-viewModel
            var viewModel = Services.GetService<StaffHandlerViewModel>();
            await viewModel.DisplaytStaffList();
            viewModel.ShowAddStaffAction = () => ShowAddStaff(StaffHandlerWindow);
            viewModel.ShowChangeStaffAction = (staff) => ShowChangeStaff(staff, StaffHandlerWindow);
            viewModel.ShowAbsenceAction = async (staff) => await ShowAbsence(staff, StaffHandlerWindow);
           
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
            viewModel.ShowStaffAction= async () => await ShowStaff(StaffAddWindow);

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
            viewModel.ShowStaffAction = async () => await ShowStaff(staffChangeWindow);

            staffChangeWindow.DataContext = viewModel;

            // Displaying of StaffAddViewModel
            staffChangeWindow.Show();
            staffHandler.Close();
        }

        public async Task ShowAbsence(StaffModel staff, Window previousWindow)
        {
            var AbsenceHandlerWindow = new AbsenceHandler();

            // Initialization of AbsenceHandlerViewModel and Binding of the connection window's view-viewModel
            var viewModel = Services.GetService<AbsenceHandlerViewModel>();
            viewModel.TheStaff = staff;
            await viewModel.DisplayAbsenceList();
            viewModel.ShowStaffAction = async () => await ShowStaff(AbsenceHandlerWindow);
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
            viewModel.ShowAbsenceAction = async () => await ShowAbsence(staff, AbsenceAddWindow);

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
            viewModel.ShowAbsenceAction = async () => await ShowAbsence(staff, AbsenceChangeWindow);

            AbsenceChangeWindow.DataContext = viewModel;

            // Displaying of AbsenceChangeViewModel
            AbsenceChangeWindow.Show();
            absenceHandler.Close();
        }
    }
}
