using System.Configuration;
using System.Data;
using System.Windows;
using SVE.Mediatek.Model;
using SVE.Mediatek.View;
using SVE.Mediatek.ViewModel;

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
            connectionWindow.DataContext = new ConnectionViewModel();
            connectionWindow.Show();

            //var StaffHandlerWindow = new StaffHandler();
            //StaffHandlerWindow.DataContext = new StaffHandlerViewModel();
            //StaffHandlerWindow.Show();

            //var AbsenceHandlerWindow = new AbsenceHandler();
            //AbsenceHandlerWindow.DataContext = new AbsenceHandlerViewModel("Durand", "Cecile");
            //AbsenceHandlerWindow.Show();

            //var StaffAddWindow = new StaffAdd();
            //StaffAddWindow.DataContext = new StaffAddViewModel();
            //StaffAddWindow.Show();

            //var StaffChangeWindow = new StaffChange();
            //StaffChangeWindow.DataContext = new StaffChangeViewModel(new Staff("Durant", "Alice", "adurant@mediatek.fr", "0612857593", Department.Reception));
            //StaffChangeWindow.Show();


            //var AbsenceAddWindow = new AbscenceAdd();
            //AbsenceAddWindow.DataContext = new AbsenceAddViewModel(new Staff("Durant", "Alice", "adurant@mediatek.fr", "0612857593", Department.Reception));
            //AbsenceAddWindow.Show();

            //var AbsenceChangeWindow = new AbsenceChange();
            //AbsenceChangeWindow.DataContext = new AbsenceChangeViewModel(
            //    new Staff("Durant", "Alice", "adurant@mediatek.fr", "0612857593", Department.Reception),
            //    new Absence(new DateOnly(2024,02,25), new DateOnly(2024, 02, 26), Reason.Maladie));
            //AbsenceChangeWindow.Show();

            //var DelConfirmationWindow = new DelConfirmation();
            //DelConfirmationWindow.DataContext = new DelConfirmationViewModel();
            //DelConfirmationWindow.Show();

            //var ErrorFieldWindow = new ErrorFieldAbsenceAdd();
            //ErrorFieldWindow.DataContext = new ErrorFieldViemModel();
            //ErrorFieldWindow.Show();

            //var ErrorDateWindow = new ErrorDate();
            //ErrorDateWindow.DataContext = new ErrorDateViewModel();
            //ErrorDateWindow.Show();

            //var ErrorConnectionWindow = new ErrorConnection();
            //ErrorConnectionWindow.DataContext = new ErrorConnectionViewModel();
            //ErrorConnectionWindow.Show();
        }
    }

}
