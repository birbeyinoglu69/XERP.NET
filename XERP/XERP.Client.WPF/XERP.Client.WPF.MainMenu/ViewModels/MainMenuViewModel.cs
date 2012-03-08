using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;
using System.Linq;
// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

//XERP Namespaces
using XERP.MenuSecurityDomain.Services;
using XERP.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Client;
using XERP.MenuSecurityDomain.ClientModels;
using System.Collections.Generic;

namespace XERP.Client.WPF.MainMenu.ViewModels
{
    public class MainMenuViewModel : ViewModelBase<MainMenuViewModel>
    {
        #region Initialization and Cleanup
        private IMenuSecurityServiceAgent _serviceAgent;

        public MainMenuViewModel() { }

        public MainMenuViewModel(IMenuSecurityServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildMenuTree();
        }

        #endregion

        #region Notifications

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        #endregion

        #region Properties
        private ObservableCollection<NestedMenuItem> _flatNestedMenuItemList;
        private ObservableCollection<MenuItem> _flatMenuItemList;

        private ObservableCollection<NestedMenuItem> _treeNestedMenuItemList;
        public ObservableCollection<NestedMenuItem> TreeNestedMenuItemList
        {
            get { return _treeNestedMenuItemList; }
            set
            {
                _treeNestedMenuItemList = value;
                NotifyPropertyChanged(m => m.TreeNestedMenuItemList);
            }
        }

        private ObservableCollection<MenuItem> _displayedMenuItemList;
        public ObservableCollection<MenuItem> DisplayedMenuItemList
        {
            get { return _displayedMenuItemList; }
            set
            {
                _displayedMenuItemList = value;
                NotifyPropertyChanged(m => m.DisplayedMenuItemList);
            }
        }

        //private NestedMenuItem _selectedNestedMenuItem;
        //public NestedMenuItem SelectedNestedMenuItem
        //{
        //    get { return _selectedNestedMenuItem; }
        //    set
        //    {
        //        _selectedNestedMenuItem = value;
        //        NotifyPropertyChanged(m => m.SelectedNestedMenuItem);
        //    }
        //}
        #endregion

        #region Methods
        private ObservableCollection<MenuItem> GetMenuItemsAvailableToUser(string systemUserID)
        {
            return new ObservableCollection<MenuItem>(_serviceAgent.GetMenuItemsAvailableToUser(systemUserID));
        }

        private IEnumerable<MenuItem> SetDisplayedMenuItems(string menuItemID)
        {
            var menuItemChildren = _flatMenuItemList.Where(x => x.ParentMenuID == menuItemID);
            return menuItemChildren;
        }

        private MenuItem GetMenuItemByID(string menuItemID)
        {
            return _serviceAgent.GetMenuItemByID(menuItemID).SingleOrDefault();
        }

        public void TreeNestedMenuItemChanged(object SelectedItem)
        {
            NestedMenuItem nmi = (NestedMenuItem)SelectedItem;          
            DisplayedMenuItemList = new ObservableCollection<MenuItem>(SetDisplayedMenuItems(nmi.MenuItemID).ToList());
        }

        private void BuildMenuTree()
        {
            TreeNestedMenuItemList = new ObservableCollection<NestedMenuItem>();
            DisplayedMenuItemList = new ObservableCollection<MenuItem>();

            _flatMenuItemList = GetMenuItemsAvailableToUser("Base");
            _flatNestedMenuItemList = new ObservableCollection<NestedMenuItem>();
            foreach (MenuItem menuItem in _flatMenuItemList)
            {
                _flatNestedMenuItemList.Add(new NestedMenuItem(menuItem));
            }
            //Their may be mulitple root nodes...
            List<NestedMenuItem> roots = new List<NestedMenuItem>();
            roots = _flatNestedMenuItemList.Where(x => x.ParentMenuID == "").ToList();
            foreach (NestedMenuItem root in roots)
            {
                getNestedChidlren(root);
                TreeNestedMenuItemList.Add(root);
            }

        }

        private void getNestedChidlren(NestedMenuItem pnmi)
        {
            foreach (NestedMenuItem cnmi in _flatNestedMenuItemList.Where(nmi => nmi.ParentMenuID == pnmi.MenuItemID).ToList())
            {
                pnmi.Children.Add(cnmi);
                getNestedChidlren(cnmi);
            }
        }
        #endregion Methods

        #region Completion Callbacks
        
        #endregion

        #region Helpers
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
        #endregion
    }
}