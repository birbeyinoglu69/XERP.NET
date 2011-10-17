using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using XERP.ViewModel.MenuMaintenance;

//using XERP.Web.Models.NestedMenuItem;
namespace XERP
{
    public partial class MainPage : UserControl
    {
        private MenuMaintenanceViewModel _dataContext = new MenuMaintenanceViewModel();
        public MainPage()
        {  
            InitializeComponent();
            this.LayoutRoot.DataContext = _dataContext;
            //tv.DataContext = _dataContext.NestedMenuList;
            //txt.DataContext = _dataContext.SelectedMenuItem;
            //txt.DataContext = _dataContext.SelectedMenuItem.MenuItemID;
        }

        private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //MenuMaintenanceViewModel vm = new MenuMaintenanceViewModel();
            _dataContext.SetSelectedMenuItem(e.NewValue);
            
            //txt.Text = _dataContext.SelectedMenuItem.MenuItemID.ToString();
            //txt.Text = "1";
            //vm = null;
            //string s = e.NewValue.ToString();
            //s = ((MI)e.NewValue).ID;
            //s = ((MI)e.NewValue).PID;
        } 
    }
}
