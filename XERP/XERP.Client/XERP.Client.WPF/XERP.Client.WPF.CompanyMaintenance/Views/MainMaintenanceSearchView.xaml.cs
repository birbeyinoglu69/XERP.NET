using System;
using System.Windows;
using System.Windows.Controls;
using XERP.Client.WPF.CompanyMaintenance.ViewModels;
using SimpleMvvmToolkit;
using XERP.Client.WPF;
namespace XERP.Client.WPF.CompanyMaintenance.Views
{
    public partial class MainMaintenanceSearchView : UserControl
    {
        private ViewModelLocator _vml = new ViewModelLocator();
        private MainSearchViewModel _viewModel;
        private LogIn.MainWindow _logInWindow;
        public MainMaintenanceSearchView()
        {  
            try
            {
                _viewModel = _vml.MainSearchViewModel;
                DataContext = _viewModel;
                _viewModel.ErrorNotice += OnErrorNotice;
                _viewModel.MessageNotice += OnMessageNotice;
                _viewModel.CloseNotice += OnCloseNotice;
                _viewModel.AuthenticatedNotice += OnAuthenticatedNotice;
                InitializeComponent();
                if (!XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)
                {
                    DisplayLogIn();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnErrorNotice(object sender, NotificationEventArgs<Exception> e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        }

        private void OnMessageNotice(object sender, NotificationEventArgs e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        }
        
        private void OnCloseNotice(Object sender, NotificationEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
        
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedList = dgResult.SelectedItems;
        }

        private void OnAuthenticatedNotice(Object sender, NotificationEventArgs e)
        {
            _logInWindow.Close();
        }

        private void DisplayLogIn()
        {
            _logInWindow = new LogIn.MainWindow();
            _logInWindow.ShowDialog();
        }
    }
}
