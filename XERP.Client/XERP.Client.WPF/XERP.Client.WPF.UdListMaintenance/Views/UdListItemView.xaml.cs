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
using System.Data.Services.Client;
using System.ComponentModel;
// Toolkit namespace
using SimpleMvvmToolkit;
//XERP Namespaces
using XERP.Domain.UdListDomain.Services;
using XERP.Domain.UdListDomain.UdListDataService;
//using XERP.Domain.UdListDomain.ClientModels;
using XERP.Domain.ClientModels;
using XERP.Client.Models;
//required for extension methods...
using ExtensionMethods;
using XERP.Client.WPF.UdListMaintenance.ViewModels;
namespace XERP.Client.WPF.UdListMaintenance.Views
{
    /// <summary>
    /// Interaction logic for UdListItemView.xaml
    /// </summary>
    public partial class UdListItemView : UserControl
    {
        ViewModelLocator _vml = ViewModelLocator.Instance;
        MainMaintenanceViewModel _viewModel;
        public UdListItemView()
        {
            try
            {
                this.Resources.MergedDictionaries.Add(XERP.Client.WPF.UdListMaintenance.Resources.SharedDictionaryManager.MenuImagesSharedDictionary);
                this.Resources.MergedDictionaries.Add(XERP.Client.WPF.UdListMaintenance.Resources.SharedDictionaryManager.BaseControlsSharedDictionary);
                _viewModel = _vml.SharedMainMaintenanceViewModel;
                //DataContext = _viewModel;
                //_viewModel.ErrorNotice += OnErrorNotice;
                //_viewModel.MessageNotice += OnMessageNotice;
                //_viewModel.SearchNotice += OnSearchNotice;
                //_viewModel.SaveRequiredNotice += OnSaveRequiredNotice;
                //_viewModel.NewRecordNeededNotice += OnNewRecordNeededNotice;
                //_viewModel.AuthenticatedNotice += OnAuthenticatedNotice;
                _viewModel.WiggleToGhostFieldNotice += OnWiggleToGhostFieldNotice;
                //I use try catch as because the context is inherited it does not have 
                //reference to it at design time...
                //perhaps when I have more time I will go back and fix this for now...
                //we will leave it...
                try
                {
                    //_viewModel.NewItemRecordCreatedNotice += OnNewRecordCreatedNotice;
                }
                catch { }

                InitializeComponent();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        //private void OnErrorNotice(object sender, NotificationEventArgs<Exception> e)
        //{
        //    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        //}

        //private void OnMessageNotice(object sender, NotificationEventArgs e)
        //{
        //    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        //}

        //private void OnNewRecordCreatedNotice(object sender, NotificationEventArgs e)
        //{
        //    if (tabctrlMain.SelectedItem == tabDetail)
        //    {
        //        txtKey.Focus();
        //    }
        //    if (tabctrlMain.SelectedItem == tabList)
        //    {
        //        dgMain.Focus();
        //        if (dgMain.Items.Count > 0 && dgMain.Columns.Count > 0)
        //        {//set the last records first column to have focus...
        //            dgMain.CurrentCell = new DataGridCellInfo(dgMain.Items[dgMain.Items.Count - 1],
        //                dgMain.Columns[0]);
        //            dgMain.BeginEdit();
        //        }
        //    }
        //}

        //private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    //WiggleToGhostField();
        //}

        private void OnWiggleToGhostFieldNotice(object sender, NotificationEventArgs e)
        {//textboxex by default set bindings on lost focus
            //so we have a ghost control on each tab and will wiggle focus to it and back to commit data on saves...
            UIElement elem = Keyboard.FocusedElement as UIElement;
            Keyboard.Focus(ghost);
            Keyboard.Focus(ghost2);
            Keyboard.Focus(elem);
        }

        //private void OnSearchNotice(Object sender, NotificationEventArgs e)
        //{
        //    MainSearchWindow searchWindow = new MainSearchWindow();
        //    searchWindow.Show(); 
        //}

        //private void OnSaveRequiredNotice(Object sender, NotificationEventArgs<bool, MessageBoxResult> e)
        //{
        //    string messageBoxText = e.Message;
        //    string caption = "XERP Warning";
        //    MessageBoxButton button = MessageBoxButton.YesNoCancel;
        //    MessageBoxImage icon = MessageBoxImage.Warning;
        //    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
        //    switch (result)
        //    {
        //        case MessageBoxResult.Yes:
        //            e.Completed(MessageBoxResult.Yes);
        //            break;
        //        case MessageBoxResult.No:
        //            e.Completed(MessageBoxResult.No);
        //            break;
        //        case MessageBoxResult.Cancel:
        //            e.Completed(MessageBoxResult.Cancel);
        //            break;
        //    }
        //}

        //private void OnNewItemRecordNeededNotice(Object sender, NotificationEventArgs<bool, MessageBoxResult> e)
        //{
        //    string messageBoxText = e.Message;
        //    string caption = "XERP Warning";
        //    MessageBoxButton button = MessageBoxButton.YesNoCancel;
        //    MessageBoxImage icon = MessageBoxImage.Warning;
        //    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
        //    switch (result)
        //    {
        //        case MessageBoxResult.Yes:
        //            e.Completed(MessageBoxResult.Yes);
        //            break;
        //        case MessageBoxResult.No:
        //            e.Completed(MessageBoxResult.No);
        //            break;
        //        case MessageBoxResult.Cancel:
        //            e.Completed(MessageBoxResult.Cancel);
        //            break;
        //    }
        //}
        
        private void dgMain_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_viewModel.AllowNewItem)
                {
                    _viewModel.NewUdListItemCommand("");
                }
                else
                {
                    MessageBox.Show("New UdListItem Is Not Enabled...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }  
        }

        //set data grid max length properties...
        private void dgMain_Initialized(object sender, EventArgs e)
        {//we do a try catch supression so that the xaml designer does not throw an error at design time...
            //if it does encounter an error at runtime worse case would be the max length would not work...
            try
            {
                DataGrid datagrid = sender as DataGrid;
                if (datagrid == null) return;
                foreach (DataGridColumn dataGridColumn in datagrid.Columns)
                {
                    if (dataGridColumn is DataGridTextColumn)
                    {
                        DataGridTextColumn dataGridTextColumn = ((DataGridTextColumn)dataGridColumn);
                        Binding binding = (Binding)dataGridTextColumn.Binding;

                        int maxColumnLength = (int)_viewModel.UdListItemMaxFieldValueDictionary[binding.Path.Path.ToString()];
                        Style newStyle = new Style(typeof(TextBox), dataGridTextColumn.EditingElementStyle);
                        newStyle.Setters.Add(new Setter(TextBox.MaxLengthProperty, maxColumnLength));
                        dataGridTextColumn.EditingElementStyle = newStyle;
                    }
                }
            }
            catch { }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//set the focus as the control is loaded...

                txtKey.Focus();
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
                            if (_viewModel.AllowNewItem)
                            {
                                _viewModel.NewUdListItemCommand("");
                                return;
                            }
                            //MessageBox.Show("New UdList Is Not Enabled...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case Key.S:
                            if (_viewModel.AllowCommit)
                            {
                                //WiggleToGhostField();
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

        private void dgMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//sets multiple selections for multiple delete and copy paste ect...
            _viewModel.SelectedUdListItemList = dgMain.SelectedItems;
        }

        private void dgMainPasteRow_Click(object sender, RoutedEventArgs e)
        {//The columns may get moved so we need to predicate the column order before
            //dealing with the paste in the viewmodel...
            _viewModel.UdListItemColumnMetaDataList = new List<ColumnMetaData>();
            foreach (DataGridColumn column in dgMain.Columns)
            {
                string s = column.SortMemberPath.ToString();
                int i = column.DisplayIndex;
                ColumnMetaData columnMetaData = new ColumnMetaData();
                columnMetaData.Name = column.SortMemberPath.ToString();
                columnMetaData.Order = column.DisplayIndex;
                _viewModel.UdListItemColumnMetaDataList.Add(columnMetaData);
            }        
            _viewModel.ItemPasteRowCommand();
        }

        private void dgMain_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (_viewModel.AllowDeleteItem)
                {
                    _viewModel.DeleteItemCommand();
                    return;
                }
                //MessageBox.Show("Delete Is Not Enabled...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //When a child datagrid is selected we will set the treeview selection to the child...
        //In all other cases the selection will be set to the parent...
        //Since IsSelected for the treeview is set on a selectedparent and a selectedchild property change...
        //we will not want to set the isselected property for the parent when the child is specifically 
        //selected from the childs data grid...
        //the property below will allow us to know that the child datagrid was what triggered 
        //The selected properties to be changed...
        private void dgMain_GotFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.UdListItemIsSelected = true;
        }

        private void dgMain_LostFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.UdListItemIsSelected = false;
        }

    }
}  