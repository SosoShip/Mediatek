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
            services.AddDbContext<MediatekContext>(options =>
                options.UseSqlServer("Data Source=.\\" +
                "SQLEXPRESS;Initial Catalog=Mediatek;Integrated Security=true;TrustServerCertificate=true;"));
            //TODO, a la place de l'url, utiliser : Configuration.GetConnectionString("DefaultConnection") 
            // + penser a implementer l'interface IConfiguration

            // Adding services :
            services.AddSingleton<StaffHandlerViewModel>();
            services.AddSingleton<IRepository<StaffEntity>, StaffRepository>();
            services.AddSingleton<IRepository<AbsenceEntity>, AbsenceRepository>();

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Opens the login window.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var connectionWindow = new Connection();
            // Binding of the connection window's view-viewModel
            connectionWindow.DataContext = new ConnectionViewModel { ShowStaffAction = () => ShowStaff(connectionWindow) };
            connectionWindow.Show();
        }

        public void ShowStaff(Window previousWindow)
        {
            var StaffHandlerWindow = new StaffHandler();

            // Initialization of StaffHandlerViewModel
            var viewModel = Services.GetService<StaffHandlerViewModel>();
            viewModel.ShowAddStaffAction = () => ShowAddStaff(StaffHandlerWindow);
            viewModel.ShowChangeStaffAction = (staff) => ShowChangeStaff(staff, StaffHandlerWindow);
            viewModel.ShowAbsenceAction = (staff) => ShowAbsence(staff, StaffHandlerWindow);
           
            // Call of StaffHandlerViewModel
            StaffHandlerWindow.DataContext = viewModel;
            StaffHandlerWindow.Show();
            previousWindow.Close();
        }
        
        public void ShowAddStaff(Window staffHandler)
        {
            var StaffAddWindow = new StaffAdd();
            StaffAddWindow.DataContext = new StaffAddViewModel { ShowStaffAction = () => ShowStaff(StaffAddWindow) };
            StaffAddWindow.Show();
            staffHandler.Close();
        }

        public void ShowChangeStaff(StaffModel staff, Window staffHandler)
        {
            var staffchangewindow = new StaffChange();
            staffchangewindow.DataContext = new StaffChangeViewModel(staff) { ShowStaffAction = () => ShowStaff(staffchangewindow) };
            staffchangewindow.Show();
            staffHandler.Close();
        }

        public void ShowAbsence(StaffModel staff, Window previousWindow)
        {
            var AbsenceHandlerWindow = new AbsenceHandler();
            AbsenceHandlerWindow.DataContext = new AbsenceHandlerViewModel(staff)
            {
                ShowStaffAction = () => ShowStaff(AbsenceHandlerWindow),
                ShowAddAbsenceAction = () => ShowAddAbsence(staff, AbsenceHandlerWindow),
                ShowChangeAbsenceAction = (selectedAbsence) => ShowChangeAbsence(staff, selectedAbsence, AbsenceHandlerWindow)
            };
            AbsenceHandlerWindow.Show();
            previousWindow.Close();
        }

        public void ShowAddAbsence(StaffModel staff, Window absenceHandler)
        {
            var AbsenceAddWindow = new AbscenceAdd();
            AbsenceAddWindow.DataContext = new AbsenceAddViewModel(staff) { ShowAbsenceAction = () => ShowAbsence(staff, AbsenceAddWindow)};
            AbsenceAddWindow.Show();
            absenceHandler.Close();
        }

        public void ShowChangeAbsence(StaffModel staff, AbsenceModel absence, Window absenceHandler)
        {
            var AbsenceChangeWindow = new AbsenceChange();
            AbsenceChangeWindow.DataContext = new AbsenceChangeViewModel(staff, absence) { ShowAbsenceAction = () => ShowAbsence(staff, AbsenceChangeWindow)};
            AbsenceChangeWindow.Show();
            absenceHandler.Close();
        }
    }

}
