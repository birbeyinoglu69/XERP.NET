using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
//XERP namespace
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;


namespace XERP.Client.WPF.MenuItemMaintenance.ViewModels
{
    public class MainSearchViewModel : ViewModelBase<MainSearchViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private IMenuItemServiceAgent _serviceAgent;

        public MainSearchViewModel()
        { }

        public MainSearchViewModel(IMenuItemServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            MenuItemTypeList = GetMenuItemTypes();
            MenuItemCodeList = GetMenuItemCodes();

            SearchObject = new MenuItem();    
            ResultList = new BindingList<MenuItem>();
            SelectedList = new BindingList<MenuItem>();
            //make sure of session authentication...
            if (ClientSessionSingleton.Instance.SessionIsAuthentic)
            {
                //make sure user has rights to UI...
                DoFormsAuthentication();
            }
            else
            {//User is not authenticated...
                RegisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
                FormIsEnabled = false;
                //we will do forms authentication once the log in returns a valid System User...
            }
        }
        #endregion Initialization and Cleanup

        #region Authentication
        private void DoFormsAuthentication()
        {
            //on log in session information is collected about the system user...
            //we need to make the system user is allowed access to this UI...
            if (ClientSessionSingleton.Instance.ExecutableProgramIDList.Contains(_globalProperties.ExecutableProgramName))
            {
                FormIsEnabled = true;
            }
            else
            {
                FormIsEnabled = false;
            }
        }

        private void OnStartUpLogIn(object sender, NotificationEventArgs<bool> e)
        {//if true is returned login was successful...
            if (e.Data)
            {
                FormIsEnabled = true;
                DoFormsAuthentication();
                NotifyAuthenticated();
            }
            else
            {
                FormIsEnabled = false;
            }
            UnregisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
        }
        #endregion Authentication

        #region Notifications
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs> MessageNotice;
        public event EventHandler<NotificationEventArgs> CloseNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        #endregion Notifications

        private void NotifyAuthenticated()
        {
            Notify(AuthenticatedNotice, new NotificationEventArgs());
        }


        #region Properties
        

        private bool? _formIsEnabled;
        public bool? FormIsEnabled
        {
            get { return _formIsEnabled; }
            set
            {
                _formIsEnabled = value;
                NotifyPropertyChanged(m => m.FormIsEnabled);
            }
        }

        private MenuItem _searchObject;
        public MenuItem SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<MenuItem> _resultList;
        public BindingList<MenuItem> ResultList
        {
            get { return _resultList; }
            set
            {
                _resultList = value;
                NotifyPropertyChanged(m => m.ResultList);
            }
        }

        private System.Collections.IList _selectedList;
        public System.Collections.IList SelectedList
        {
            get { return _selectedList; }
            set
            {
                _selectedList = value;
                NotifyPropertyChanged(m => m.SelectedList);
            }
        }

        private ObservableCollection<MenuItemType> _menuItemTypeList;
        public ObservableCollection<MenuItemType> MenuItemTypeList
        {
            get { return _menuItemTypeList; }
            set
            {
                _menuItemTypeList = value;
                NotifyPropertyChanged(m => m.MenuItemTypeList);
            }
        }

        private ObservableCollection<MenuItemCode> _menuItemCodeList;
        public ObservableCollection<MenuItemCode> MenuItemCodeList
        {
            get { return _menuItemCodeList; }
            set
            {
                _menuItemCodeList = value;
                NotifyPropertyChanged(m => m.MenuItemCodeList);
            }
        }
        #endregion Properties

        #region Methods
        private ObservableCollection<MenuItemType> GetMenuItemTypes()
        {
            return new ObservableCollection<MenuItemType>(_serviceAgent.GetMenuItemTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private ObservableCollection<MenuItemCode> GetMenuItemCodes()
        {
            return new ObservableCollection<MenuItemCode>(_serviceAgent.GetMenuItemCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private BindingList<MenuItem> GetMenuItems(string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<MenuItem>(_serviceAgent.GetMenuItems(companyID).ToList());
        }

        private BindingList<MenuItem> GetMenuItems(MenuItem menuItemQueryObject, string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<MenuItem>(_serviceAgent.GetMenuItems(menuItemQueryObject, companyID).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetMenuItems(SearchObject, ClientSessionSingleton.Instance.CompanyID);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<MenuItem> selectedList = new BindingList<MenuItem>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((MenuItem)item);
                }
                MessageBus.Default.Notify(MessageTokens.MenuItemSearchToken.ToString(), this, new NotificationEventArgs<BindingList<MenuItem>>("", selectedList)); 
            }
            NotifyClose("");
        }
        #endregion Commands

        #region Helpers
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }

        private void NotifyClose(string message)
        {
            Notify(CloseNotice, new NotificationEventArgs(message));
        }
        #endregion Helpers
    }
}