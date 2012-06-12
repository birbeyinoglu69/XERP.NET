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
        private TypeSearchViewModel _model;
        private LogIn.MainWindow _logInWindow;
        public TypeMaintenanceSearchView()
        { 
            
            try
            {
                InitializeComponent();
                _model = _vml.TypeSearchViewModel;
                DataContext = _model;
                _model.ErrorNotice += OnErrorNotice;
                _model.CloseNotice += OnCloseNotice;
                _model.AuthenticatedNotice += OnAuthenticatedNotice;
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
            _model.SelectedList = dgResult.SelectedItems;
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