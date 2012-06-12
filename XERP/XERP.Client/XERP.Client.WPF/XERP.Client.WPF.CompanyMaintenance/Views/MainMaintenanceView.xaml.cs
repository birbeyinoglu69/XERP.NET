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
using XERP.Client;
using SimpleMvvmToolkit;
namespace XERP.Client.WPF.CompanyMaintenance.Views
{
    /// <summary>
    /// Interaction logic for CompanyMaintenanceView.xaml
    /// </summary>
    public partial class MainMaintenanceView : UserControl
    {
        ViewModelLocator _vml = new ViewModelLocator();
        MainMaintenanceViewModel _viewModel;
        private LogIn.MainWindow _logInWindow;
        public MainMaintenanceView()
        {
            try
            {
                _viewModel = _vml.MainMaintenanceViewModel;
                DataContext = _viewModel;
                _viewModel.ErrorNotice += OnErrorNotice;
                _viewModel.MessageNotice += OnMessageNotice;
                _viewModel.SearchNotice += OnSearchNotice;
                _viewModel.SaveRequiredNotice += OnSaveRequiredNotice;
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

        private void OpenTypeMaintenance_Click(object sender, RoutedEventArgs e)
        {
            TypeMaintenancneWindow typeMaintenanceWindow = new TypeMaintenancneWindow();
            typeMaintenanceWindow.Show();
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {//textboxex by default set bindings on lost focus
            //so we have a ghost control on each tab and will wiggle focus to it and back to commit data on saves...
            UIElement elem = Keyboard.FocusedElement as UIElement;
            Keyboard.Focus(ghost);
            Keyboard.Focus(ghost2);
            Keyboard.Focus(elem);
        }

        private void OnSearchNotice(Object sender, NotificationEventArgs e)
        {
            MainSearchWindow searchWindow = new MainSearchWindow();
            searchWindow.Show(); 
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

        private void OnSaveRequiredNotice(Object sender, NotificationEventArgs<bool, MessageBoxResult> e)
        {
            string messageBoxText = e.Message;
            string caption = "XERP Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    e.Completed(MessageBoxResult.Yes);
                    break;
                case MessageBoxResult.No:
                    e.Completed(MessageBoxResult.No);
                    break;
                case MessageBoxResult.Cancel:
                    e.Completed(MessageBoxResult.Cancel);
                    break;
            }
        }
        //validate that the UniqueID is maintained before allowing another new object...
        private void DataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_viewModel.AllowNew)
                {
                    _viewModel.NewCompanyCommand();
                }
                else
                {
                    MessageBox.Show("CompanyID is Required Before A New Company Can Be Added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //set data grid max length properties...
        private void DataGrid_Initialized(object sender, EventArgs e)
        {
            DataGrid datagrid = sender as DataGrid;
            if (datagrid == null) return;
            foreach (DataGridColumn dataGridColumn in datagrid.Columns)
            {
                if (dataGridColumn is DataGridTextColumn)
                {
                    DataGridTextColumn dataGridTextColumn = ((DataGridTextColumn)dataGridColumn);
                    Binding binding = (Binding)dataGridTextColumn.Binding;
                    
                    int maxColumnLength = (int)_viewModel.CompanyMaxFieldValueDictionary[binding.Path.Path.ToString()];
                    Style newStyle = new Style(typeof(TextBox), dataGridTextColumn.EditingElementStyle);
                    newStyle.Setters.Add(new Setter(TextBox.MaxLengthProperty, maxColumnLength));
                    dataGridTextColumn.EditingElementStyle = newStyle;
                }
            }
        }
    }  
}
