using System.Configuration;
using System.Data;
using System.Windows;
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
        }
    }

}
