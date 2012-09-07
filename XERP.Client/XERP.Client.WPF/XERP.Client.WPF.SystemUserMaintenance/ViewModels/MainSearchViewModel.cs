using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
//XERP namespace
using XERP.Domain.SystemUserDomain.SystemUserDataService;
using XERP.Domain.SystemUserDomain.Services;


namespace XERP.Client.WPF.SystemUserMaintenance.ViewModels
{
    public class MainSearchViewModel : ViewModelBase<MainSearchViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private ISystemUserServiceAgent _serviceAgent;

        public MainSearchViewModel()
        { }

        public MainSearchViewModel(ISystemUserServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            SystemUserTypeList = GetSystemUserTypes();
            SystemUserCodeList = GetSystemUserCodes();

            SearchObject = new SystemUser();    
            ResultList = new BindingList<SystemUser>();
            SelectedList = new BindingList<SystemUser>();
            //make sure of session authentication...
            if (XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)
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

        private SystemUser _searchObject;
        public SystemUser SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<SystemUser> _resultList;
        public BindingList<SystemUser> ResultList
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

        private ObservableCollection<SystemUserType> _systemUserTypeList;
        public ObservableCollection<SystemUserType> SystemUserTypeList
        {
            get { return _systemUserTypeList; }
            set
            {
                _systemUserTypeList = value;
                NotifyPropertyChanged(m => m.SystemUserTypeList);
            }
        }

        private ObservableCollection<SystemUserCode> _systemUserCodeList;
        public ObservableCollection<SystemUserCode> SystemUserCodeList
        {
            get { return _systemUserCodeList; }
            set
            {
                _systemUserCodeList = value;
                NotifyPropertyChanged(m => m.SystemUserCodeList);
            }
        }
        #endregion Properties

        #region Methods
        private ObservableCollection<SystemUserType> GetSystemUserTypes()
        {
            return new ObservableCollection<SystemUserType>(_serviceAgent.GetSystemUserTypesReadOnly().ToList());
        }

        private ObservableCollection<SystemUserCode> GetSystemUserCodes()
        {
            return new ObservableCollection<SystemUserCode>(_serviceAgent.GetSystemUserCodesReadOnly().ToList());
        }

        private BindingList<SystemUser> GetSystemUsers()
        {//note this get is to the singleton repository...
            return new BindingList<SystemUser>(_serviceAgent.GetSystemUsers().ToList());
        }

        private BindingList<SystemUser> GetSystemUsers(SystemUser systemUserQueryObject)
        {//note this get is to the singleton repository...
            return new BindingList<SystemUser>(_serviceAgent.GetSystemUsers(systemUserQueryObject).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetSystemUsers(SearchObject);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<SystemUser> selectedList = new BindingList<SystemUser>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((SystemUser)item);
                }
                MessageBus.Default.Notify(MessageTokens.SystemUserSearchToken.ToString(), this, new NotificationEventArgs<BindingList<SystemUser>>("", selectedList)); 
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