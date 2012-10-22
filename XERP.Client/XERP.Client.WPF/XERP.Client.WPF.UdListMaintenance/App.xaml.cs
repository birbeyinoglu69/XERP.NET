using System.Windows;
using XERP.Client.WPF.UdListMaintenance.ViewModels;


namespace XERP.Client.WPF.UdListMaintenance
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.Resources.Add("Locator", XERP.Client.WPF.UdListMaintenance.ViewModelLocator.Instance);
        }
        
    }
}
