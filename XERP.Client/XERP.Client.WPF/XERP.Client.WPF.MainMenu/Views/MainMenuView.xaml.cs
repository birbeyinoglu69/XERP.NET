using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XERP.Client.WPF.MainMenu.ViewModels;
using SimpleMvvmToolkit;
using XERP.Client.WPF.CompanyMaintenance;
//using XERP.MenuSecurityDomain.Services;
namespace XERP.Client.WPF.MainMenu.Views
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        ViewModelLocator vml = new ViewModelLocator();
        MainMenuViewModel model;
        public MainMenuView()
        {
            try
            {
                InitializeComponent();
                model = vml.MainMenuViewModel;
                DataContext = model;
                model.ErrorNotice += OnErrorNotice;
                model.MenuActionNotice += OnMenuActionNotice;
            }
            catch (Exception ex)
            {

            }
        }

        private void OnMenuActionNotice(Object sender, NotificationEventArgs e)
        {
            XERP.Client.WPF.CompanyMaintenance.MainWindow mainWindow = new XERP.Client.WPF.CompanyMaintenance.MainWindow();
            mainWindow.Show();  
        }


        private void OnErrorNotice(object sender, NotificationEventArgs<Exception> e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        }

        private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            model.TreeNestedMenuItemChanged(e.NewValue);
        }
    }
}
