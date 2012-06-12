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
        LogInViewModel _veiwModel;
        public LogInView()
        {
            try
            {
                _veiwModel = _viewModelLocator.LogInViewModel;
                DataContext = _veiwModel;
                _veiwModel.ErrorNotice += OnErrorNotice;
                _veiwModel.MessageNotice += OnMessageNotice;
                
                InitializeComponent();  
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

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //textboxe by default set bindings on lost focus
                //so we have a ghost control on each tab and will wiggle focus to it and back to commit data on saves...
                UIElement elem = Keyboard.FocusedElement as UIElement;
                Keyboard.Focus(ghost);
                Keyboard.Focus(elem);
                _veiwModel.AuthenticateCommand();
            }
        }

    }
}
