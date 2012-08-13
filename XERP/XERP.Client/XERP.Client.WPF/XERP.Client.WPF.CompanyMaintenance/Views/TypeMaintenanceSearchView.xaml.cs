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
using XERP.Client.WPF.CompanyMaintenance.ViewModels;
using SimpleMvvmToolkit;

namespace XERP.Client.WPF.CompanyMaintenance.Views
{
    /// <summary>
    /// Interaction logic for TypeMaintenanceSearchView.xaml
    /// </summary>
    public partial class TypeMaintenanceSearchView : UserControl
    {
        private ViewModelLocator _vml = new ViewModelLocator();
        private TypeSearchViewModel _viewmodel;
        private LogIn.MainWindow _logInWindow;
        public TypeMaintenanceSearchView()
        { 
            
            try
            { 
                _viewmodel = _vml.TypeSearchViewModel;
                DataContext = _viewmodel;
                _viewmodel.ErrorNotice += OnErrorNotice;
                _viewmodel.CloseNotice += OnCloseNotice;
                _viewmodel.AuthenticatedNotice += OnAuthenticatedNotice;
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
            _viewmodel.SelectedList = dgResult.SelectedItems;
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