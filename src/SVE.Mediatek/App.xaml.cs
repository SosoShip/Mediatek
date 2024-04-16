using System.Configuration;
using System.Data;
using System.Security.Policy;
using System.Windows;
using SVE.Mediatek.Model;
using SVE.Mediatek.View;
using SVE.Mediatek.ViewModel;
using SVE.Mediatek.DAL;

namespace SVE.Mediatek
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
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
            StaffHandlerWindow.DataContext = new StaffHandlerViewModel 
            { 
                ShowAddStaffAction = () => ShowAddStaff(StaffHandlerWindow),
                ShowChangeStaffAction = (staff) => ShowChangeStaff(staff, StaffHandlerWindow),
                ShowAbsenceAction = (staff) => ShowAbsence(staff, StaffHandlerWindow)
            };
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
