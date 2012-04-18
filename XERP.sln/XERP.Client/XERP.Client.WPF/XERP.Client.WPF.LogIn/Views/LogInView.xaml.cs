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
        private ViewModelLocator _viewModelLocator = new ViewModelLocator();
        LogInViewModel _model;
        public LogInView()
        {
            try
            {
                InitializeComponent();
                _model = _viewModelLocator.LogInViewModel;
                DataContext = _model;
                _model.ErrorNotice += OnErrorNotice;
                _model.MessageNotice += OnMessageNotice;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK);
            }
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

    }
}
