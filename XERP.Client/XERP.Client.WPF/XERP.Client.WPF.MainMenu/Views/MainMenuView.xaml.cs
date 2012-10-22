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
using XERP.Client.WPF.MainMenu.ViewModels;
using SimpleMvvmToolkit;
//using XERP.Client.WPF.CompanyMaintenance;
//using XERP.Client.WPF.MainMenu;
//using XERP.MenuSecurityDomain.Services;
namespace XERP.Client.WPF.MainMenu.Views
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        private ViewModelLocator _viewModelLocator = new ViewModelLocator();
        private WindowLocator _windowLocator = new WindowLocator();
        private MainMenuViewModel _viewModel = new MainMenuViewModel();
        public MainMenuView()
        {
            try
            {
                InitializeComponent();
                _viewModel = _viewModelLocator.MainMenuViewModel;
                DataContext = _viewModel;
                _viewModel.ErrorNotice += OnErrorNotice;
                _viewModel.MenuActionNotice += OnMenuActionNotice;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK);
            }
        }

        private void OnMenuActionNotice(Object sender, NotificationEventArgs e)
        {
            try
            {
                if (e.Message == "")
                {//it is not implemented yet
                    UINotImplemented uINotImplementedWindow = new UINotImplemented();
                    uINotImplementedWindow.Show();
                }
                else
                {
                    string showWindowErrorMessage;
                    _windowLocator.ShowWindow(e.Message, out showWindowErrorMessage);
                    if (!string.IsNullOrEmpty(showWindowErrorMessage))
                    {
                        MessageBox.Show(showWindowErrorMessage, "Error", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK);
            }
        }


        private void OnErrorNotice(object sender, NotificationEventArgs<Exception> e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        }

        private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _viewModel.TreeNestedMenuItemChanged(e.NewValue);
        }
    }
}
