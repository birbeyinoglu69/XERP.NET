using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using XERP.Client.WPF.MenuItemMaintenance.ViewModels;
using SimpleMvvmToolkit;
using System.Collections.Generic;
using XERP.Client.Models;
using XERP.Client.WPF;
using XERP.Domain.MenuSecurityDomain.ClientModels;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
namespace XERP.Client.WPF.MenuItemMaintenance.Views
{
    /// <summary>
    /// Interaction logic for MenuItemMaintenanceView.xaml
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
                this.Resources.MergedDictionaries.Add(XERP.Client.WPF.MenuItemMaintenance.Resources.SharedDictionaryManager.MenuImagesSharedDictionary);
                this.Resources.MergedDictionaries.Add(XERP.Client.WPF.MenuItemMaintenance.Resources.SharedDictionaryManager.BaseControlsSharedDictionary);
                _viewModel = _vml.MainMaintenanceViewModel;
                DataContext = _viewModel;
                _viewModel.ErrorNotice += OnErrorNotice;
                _viewModel.MessageNotice += OnMessageNotice;
                //_viewModel.SearchNotice += OnSearchNotice;
                _viewModel.TypeSearchNotice += OnTypeSearchNotice;
                _viewModel.CodeSearchNotice += OnCodeSearchNotice;
                _viewModel.SaveRequiredNotice += OnSaveRequiredNotice;
                _viewModel.NewRecordNeededNotice += OnNewRecordNeededNotice;
                _viewModel.AuthenticatedNotice += OnAuthenticatedNotice;
                //_viewModel.NewRecordCreatedNotice += OnNewRecordCreatedNotice;

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
            TypeMaintenanceWindow maintenanceWindow = new TypeMaintenanceWindow();
            maintenanceWindow.Show();
        }

        private void OpenCodeMaintenance_Click(object sender, RoutedEventArgs e)
        {
            CodeMaintenanceWindow maintenanceWindow = new CodeMaintenanceWindow();
            maintenanceWindow.Show();
        }

        private void OpenMenuIconMaintenance_Click(object sender, RoutedEventArgs e)
        {
            //CodeMaintenanceWindow maintenanceWindow = new CodeMaintenanceWindow();
            //maintenanceWindow.Show();
            MessageBox.Show("Not Implemented Yet");
        }

        private void OpenExecutableCodeMaintenance_Click(object sender, RoutedEventArgs e)
        {
            //CodeMaintenanceWindow maintenanceWindow = new CodeMaintenanceWindow();
            //maintenanceWindow.Show();
            MessageBox.Show("Not Implemented Yet");
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            WiggleToGhostField();
            if (_viewModel.AllowCommit == true)
            {
                _viewModel.SaveCommand();
            }
        }

        private void WiggleToGhostField()
        {//textboxex by default set bindings on lost focus
            //so we have a ghost control on each tab and will wiggle focus to it and back to commit data on saves...
            UIElement elem = Keyboard.FocusedElement as UIElement;
            Keyboard.Focus(ghost);
            //Keyboard.Focus(ghost2);
            Keyboard.Focus(elem);
        }


        private void OnTypeSearchNotice(Object sender, NotificationEventArgs e)
        {
            TypeSearchWindow searchWindow = new TypeSearchWindow();
            searchWindow.ShowDialog();
        }

        private void OnCodeSearchNotice(Object sender, NotificationEventArgs e)
        {
            CodeSearchWindow searchWindow = new CodeSearchWindow();
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

        private void OnNewRecordNeededNotice(Object sender, NotificationEventArgs<bool, MessageBoxResult> e)
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//set the focus as the control is loaded...
            //txtKey.Focus();
        }

        //Hot keys Control S--Save Control N--New, Delete Key--Delete
        private void UserControl_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N || e.Key == Key.S)
            {
                if (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    switch (e.Key)
                    {
                        case Key.N:
                            if (_viewModel.AllowNew)
                            {
                                _viewModel.NewMenuItemCommand("");
                                return;
                            }
                            //MessageBox.Show("New MenuItem Is Not Enabled...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case Key.S:
                            if (mnuSave.IsEnabled)
                            {
                                WiggleToGhostField();
                                _viewModel.SaveCommand();
                                return;
                            }
                            //MessageBox.Show("Save Is Not Enabled...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //_viewModel.TreeNestedMenuItemChanged(e.NewValue);   
        }

        private void btnAssignSelectedSecurityGroups_Click(object sender, RoutedEventArgs e)
        {//set the selected items for the _viewModel
            _viewModel.SelectedAvailableSecurityGroupList = lbxAvailableSecurityGroups.SelectedItems;
            //do the required MenuSecurity CRUD
            _viewModel.AssignSelectedSecurityGroupsCommand();
            //need to remove from list and add to the other list...

            for (int j = lbxAvailableSecurityGroups.SelectedItems.Count - 1; j >= 0; j--)
            {//move item from one list to the other...
                SecurityGroup item = (SecurityGroup)lbxAvailableSecurityGroups.SelectedItems[j];
                _viewModel.AvailableSecurityGroupList.Remove((SecurityGroup)item);
                _viewModel.AssignedSecurityGroupList.Add((SecurityGroup)item);
            }
            lbxAvailableSecurityGroups.SelectedItems.Clear();
        }

        private void btnRemoveSelectedSecurityGroups_Click(object sender, RoutedEventArgs e)
        {//set the selected items for the _viewModel
            _viewModel.SelectedAssignedSecurityGroupList = lbxAssignedSecurityGroups.SelectedItems;
            //do the required MenuSecurity CRUD
            _viewModel.RemoveSelectedSecurityGroupsCommand();

            for (int j = lbxAssignedSecurityGroups.SelectedItems.Count - 1; j >= 0; j--)
            {//move item from one list to the other...
                SecurityGroup item = (SecurityGroup)lbxAssignedSecurityGroups.SelectedItems[j];
                _viewModel.AssignedSecurityGroupList.Remove((SecurityGroup)item);
                _viewModel.AvailableSecurityGroupList.Add((SecurityGroup)item); 
            }
            //need to remove from list and add to the other list...
            lbxAssignedSecurityGroups.SelectedItems.Clear();
        }

    }  
}
