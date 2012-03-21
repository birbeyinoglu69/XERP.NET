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
using XERP.Client.WPF.LogIn.ViewModels;
using SimpleMvvmToolkit;
using System.Threading;

namespace XERP.Client.WPF.LogIn.Views
{
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : UserControl
    {
        LogInViewModel model;
        public LogInView()
        {
            InitializeComponent();
            model = (LogInViewModel)DataContext;
            model.ErrorNotice += OnErrorNotice;
            model.MessageNotice += OnMessageNotice;
            model.LogInActionNotice += OnLogInActionNotice;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void OnErrorNotice(object sender, NotificationEventArgs<Exception> e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        }

        private void OnMessageNotice(object sender, NotificationEventArgs e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        }

        private void OnLogInActionNotice(Object sender, NotificationEventArgs e)
        {
            XERP.Client.WPF.MainMenu.MainWindow mainMenuWindow = new XERP.Client.WPF.MainMenu.MainWindow();
            mainMenuWindow.Show();

            App.Current.MainWindow.Close();
            App.Current.MainWindow = mainMenuWindow; 
        }
    }
}
