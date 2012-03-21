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
    /// Interaction logic for CompanyMaintenanceView.xaml
    /// </summary>
    public partial class CompanyMaintenanceView : UserControl
    {
        ViewModelLocator vml = new ViewModelLocator();
        CompanyMaintenanceViewModel model;
        public CompanyMaintenanceView()
        {
            try
            {
                InitializeComponent();
                model = vml.CompanyMaintenanceViewModel;
                DataContext = model;
                model.ErrorNotice += OnErrorNotice;
                model.SearchNotice += OnSearchNotice;
                model.SaveRequiredNotice += OnSaveRequiredNotice;
            }
            catch (Exception ex)
            {

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
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.ShowDialog(); 
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
   
    }  
}
