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
using XERP.Client.WPF.StartUp.ViewModels;
using SimpleMvvmToolkit;

namespace XERP.Client.WPF.StartUp.Views
{
    /// <summary>
    /// Interaction logic for StartUpView.xaml
    /// </summary>
    public partial class StartUpView : UserControl
    {
        
        private ViewModelLocator _viewModelLocator = new ViewModelLocator();
        private StartUpViewModel _model = new StartUpViewModel();
        private XERP.Client.WPF.LogIn.MainWindow _logInWindow;
        private XERP.Client.WPF.MainMenu.MainWindow _mainMenuWindow;
        private XERP.Client.WPF.StartUp.MainWindow _splashScreenWindow;
        public StartUpView()
        {
            try
            {
                InitializeComponent();
                _model = _viewModelLocator.StartUpViewModel;
                DataContext = _model;
                _model.ErrorNotice += OnErrorNotice;
                _model.LogInNotice += OnLogInNotice;
                _model.MainMenuNotice += OnMainMenuNotice;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK);
            }
        }

        private void OnErrorNotice(object sender, NotificationEventArgs<Exception> e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        }

        private void OnLogInNotice(object sender, NotificationEventArgs e)
        {//hide splash screen until login is instantiated...
         //As the StartUp view model listening for successful login to launch the 
            _splashScreenWindow = (XERP.Client.WPF.StartUp.MainWindow)App.Current.MainWindow;
            _splashScreenWindow.Hide();
            _logInWindow = new XERP.Client.WPF.LogIn.MainWindow();
            _logInWindow.Show();
            
            App.Current.MainWindow = _logInWindow;
            App.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
        private void OnMainMenuNotice(object sender, NotificationEventArgs<bool> e)
        {
            if (e.Data)
            {
                _mainMenuWindow = new XERP.Client.WPF.MainMenu.MainWindow();
                _mainMenuWindow.Show();
                App.Current.MainWindow = _mainMenuWindow;
                _logInWindow.Close();
                _splashScreenWindow.Close(); 
            }
        }  
    }
}
