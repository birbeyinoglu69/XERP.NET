using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;
using System.Linq;
// Toolkit namespace
using SimpleMvvmToolkit;
using System.Drawing;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

//XERP Namespaces

using XERP.Client;

using System.Collections.Generic;
using XERP.Domain.MenuSecurityDomain.Services;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.ClientModels;

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

        public event EventHandler<NotificationEventArgs> MenuActionNotice;
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        #endregion

        #region Properties
        private enum MenuItemSelectionType {LeftTreeMenuSelection, RightDisplayMenuItemSelction};

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

        private NestedMenuItem _selectedTreeNestedMenuItemList;
        public NestedMenuItem SelectedTreeNestedMenuItemList
        {
            get { return _selectedTreeNestedMenuItemList; }
            set
            {
                _selectedTreeNestedMenuItemList = value;
                NotifyPropertyChanged(m => m.SelectedTreeNestedMenuItemList);
                
            }
        }

        private ObservableCollection<NestedMenuItem> _displayedMenuItemList;
        public ObservableCollection<NestedMenuItem> DisplayedMenuItemList
        {
            get { return _displayedMenuItemList; }
            set
            {
                _displayedMenuItemList = value;
                NotifyPropertyChanged(m => m.DisplayedMenuItemList);
            }
        }

        private NestedMenuItem _selectedDisplayedMenuItemList;
        public NestedMenuItem SelectedDisplayedMenuItemList
        {
            get { return _selectedDisplayedMenuItemList; }
            set
            {
                _selectedDisplayedMenuItemList = value;
                NotifyPropertyChanged(m => m.SelectedDisplayedMenuItemList);
                
            }
        }
        #endregion

        #region Methods
        private void MenuItemAction(MenuItemSelectionType menuItemSelectionType)
        {
            switch (menuItemSelectionType)
            {
                case MenuItemSelectionType.RightDisplayMenuItemSelction:
                    if (SelectedDisplayedMenuItemList != null &&
                        SelectedDisplayedMenuItemList.Executable.HasValue &&
                        SelectedDisplayedMenuItemList.Executable.Value == true)
                    {
                        NotifyMenuAction(SelectedDisplayedMenuItemList.ExecutableProgramID);
                    }
                break;
                case MenuItemSelectionType.LeftTreeMenuSelection:
                if (SelectedTreeNestedMenuItemList != null &&
                        SelectedTreeNestedMenuItemList.Executable.HasValue &&
                        SelectedTreeNestedMenuItemList.Executable.Value == true)
                    {
                        NotifyMenuAction(SelectedTreeNestedMenuItemList.ExecutableProgramID);
                    }
                break;
            }
        }

        private ObservableCollection<MenuItem> GetMenuItemsAvailableToUser(string systemUserID)
        {
            return new ObservableCollection<MenuItem>(_serviceAgent.GetMenuItemsAvailableToUser(systemUserID));
        }

        private IEnumerable<NestedMenuItem> SetDisplayedMenuItems(string menuItemID)
        {
            var menuItemChildren = _flatNestedMenuItemList.Where(x => x.ParentMenuID == menuItemID);
            return menuItemChildren;
        }

        private MenuItem GetMenuItemByID(string menuItemID, string companyID)
        {
            return _serviceAgent.GetMenuItemByID(menuItemID, companyID).SingleOrDefault();
        }

        private MenuItem GetMenuItemByAutoID(Int64 autoID)
        {
            return _serviceAgent.GetMenuItemByAutoID(autoID).SingleOrDefault();
        }

        public void TreeNestedMenuItemChanged(object SelectedItem)
        {
            SelectedTreeNestedMenuItemList = (NestedMenuItem)SelectedItem;
            DisplayedMenuItemList = new ObservableCollection<NestedMenuItem>(SetDisplayedMenuItems(SelectedTreeNestedMenuItemList.MenuItemID).ToList());
        }

        private void BuildMenuTree()
        {
            TreeNestedMenuItemList = new ObservableCollection<NestedMenuItem>();
            DisplayedMenuItemList = new ObservableCollection<NestedMenuItem>();

            //for testing we set the value...
            //ClientSessionSingleton.Instance.SystemUserID = "Base";

            _flatMenuItemList = GetMenuItemsAvailableToUser(ClientSessionSingleton.Instance.SystemUserID);
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
        
        #endregion Methods

        #region Helpers
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }

        private void NotifyMenuAction(string message)
        {
            Notify(MenuActionNotice, new NotificationEventArgs(message));
        }

        #endregion Helpers

        #region Commands
        public void TreeMenuItemSelectedCommand()
        {
            MenuItemAction(MenuItemSelectionType.LeftTreeMenuSelection);
        }

        public void MenuItemSelectedCommand()
        {
            MenuItemAction(MenuItemSelectionType.RightDisplayMenuItemSelction);
        }
        #endregion Commands

    }
}