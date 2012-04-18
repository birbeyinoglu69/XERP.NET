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
    public partial class TypeMaintenanceView : UserControl
    {
        ViewModelLocator _vml = new ViewModelLocator();
        TypeMaintenanceViewModel _model;
        private LogIn.MainWindow _logInWindow;
        public TypeMaintenanceView()
        {
            try
            {
                InitializeComponent();
                _model = _vml.TypeMaintenanceViewModel;
                DataContext = _model;
                _model.ErrorNotice += OnErrorNotice;
                _model.SearchNotice += OnSearchNotice;
                _model.SaveRequiredNotice += OnSaveRequiredNotice;
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
            TypeSearchWindow searchWindow = new TypeSearchWindow();
            searchWindow.ShowDialog();
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
    }
}