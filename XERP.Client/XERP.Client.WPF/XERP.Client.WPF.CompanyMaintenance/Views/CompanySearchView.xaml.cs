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
using XERP.Client;
using XERP.Client.WPF.CompanyMaintenance.ViewModels;
using SimpleMvvmToolkit;
namespace XERP.Client.WPF.CompanyMaintenance.Views
{
    /// <summary>
    /// Interaction logic for CompanySearch.xaml
    /// </summary>
    public partial class CompanySearchView : UserControl
    {
        ViewModelLocator vml = new ViewModelLocator();
        CompanySearchViewModel model;
        public CompanySearchView()
        {
            try
            {
                InitializeComponent();
                model = vml.CompanySearchViewModel;
                DataContext = model;
                model.ErrorNotice += OnErrorNotice;
                model.CloseNotice += OnCloseNotice;
            }
            catch (Exception ex)
            {

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
            //System.Collections.IList lst = dgResult.SelectedItems;
            model.SelectedList = dgResult.SelectedItems;
         
        }

    }

    
}
