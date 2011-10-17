using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using XERP.Web.Services.MenuItemSecurityGroup;
using System.Collections.ObjectModel;
using XERP.ClientModels.NestedMenuItem;
using System.Collections.Generic;
using System.Linq;
using XERP.Web.Models.MenuItemSecurityGroup;
using System.ComponentModel;
namespace XERP.ViewModel.MenuMaintenance
{
    public class MenuMaintenanceViewModel : INotifyPropertyChanged
    {
        #region PrivateProperties
        //context to communicate with Domain Service...
        private MenuItemSecurityGroupDomainContext _context;
        //used to return the NestedMenuItems
        private ObservableCollection<NestedMenuItem> nestedMenuList;
        //used to collect the flatlist of MenuItems
        private List<NestedMenuItem> flatList = null;
        //MenuItem Entity 2 way binding for CRUD set based on the selected NestedMenuList
        private MenuItem selectedMenuItem;
        #endregion  
        public MenuMaintenanceViewModel()
        {
            nestedMenuList = new ObservableCollection<NestedMenuItem>();
            selectedMenuItem = new MenuItem();
            //nestedMenuList.Add(nmi);
            _context = new MenuItemSecurityGroupDomainContext();
            var menuItems = _context.Load(_context.GetFlatMenuItemsQuery());
            menuItems.Completed += (sender, e) =>
            {
                if (menuItems.HasError)
                {
                    MessageBox.Show(menuItems.Error.Message);
                    menuItems.MarkErrorAsHandled();
                }
                else
                {
                    flatList = new List<NestedMenuItem>();
                    SelectedMenuItem = menuItems.Entities.SingleOrDefault(x => x.AutoID == 5);
                    
                    OutputText = "Hello World";
                    foreach (MenuItem menuItem in menuItems.Entities.ToList())
                    {
                        flatList.Add(new NestedMenuItem(menuItem));
                        
                    }
                    //Their may be mulitple root nodes...
                    List<NestedMenuItem> roots = new List<NestedMenuItem>();
                    roots = flatList.Where(x => x.ParentMenuID == "").ToList();
                    foreach (NestedMenuItem root in roots)
                    {
                        getChidlren(root);
                        nestedMenuList.Add(root);
                    }
                }
            };
        }
        #region PrivateMethods
        private void getChidlren(NestedMenuItem pnmi)
        {
            foreach (NestedMenuItem cnmi in flatList.Where(nmi => nmi.ParentMenuID == pnmi.MenuItemID).ToList())
            {
                pnmi.Children.Add(cnmi);
                getChidlren(cnmi);
            }
        }
        #endregion
        #region PublicMethods
        public void SetSelectedMenuItem(object SelectedItem)
        {
            NestedMenuItem nmi = new NestedMenuItem(new NestedMenuItem((NestedMenuItem)SelectedItem));
            var menuItem = _context.Load(_context.GetByAutoIDQuery(nmi.AutoID));
            menuItem.Completed += (sender, e) =>
            {
                if (menuItem.HasError)
                {
                    MessageBox.Show(menuItem.Error.Message);
                    menuItem.MarkErrorAsHandled();
                }
                else
                {
                    selectedMenuItem = menuItem.Entities.Single();
                }
            };
        }
        #endregion
        #region PublicProperties
        public ObservableCollection<NestedMenuItem> NestedMenuList
        {
            get { return nestedMenuList; }
            set { nestedMenuList = value; } 
        }
        public MenuItem SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set {  
                    selectedMenuItem = value;
                    RaisePropertyChanged("SelectedMenuItem");
                }
            
        }
        #endregion
        //// Declare the PropertyChanged event
        //public event PropertyChangedEventHandler PropertyChanged;
        //// OnPropertyChanged will raise the PropertyChanged event passing the
        //// source property that is being updated.
        //private void onPropertyChanged(object sender, string propertyName)
        //{
        //    if (this.PropertyChanged != null)
        //    {
        //        PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        #region INotifyPropertyChanged Members  
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;  
        private void RaisePropertyChanged(string propertyName)  
        {  
            if (PropertyChanged != null)  
                 PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }  
        #endregion  

                
        private string _outputText;         
        public string OutputText         
        {             
            get             
            {                 
                return _outputText;             
            }              
            set             
            {                 
                _outputText = value;                 
                RaisePropertyChanged("OutputText");             
            }         
        
        }         

    }
}
