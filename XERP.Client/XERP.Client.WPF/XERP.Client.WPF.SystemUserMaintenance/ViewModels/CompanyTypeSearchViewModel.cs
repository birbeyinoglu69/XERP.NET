using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;
//XERP namespace
using XERP.Domain.SystemUserDomain.SystemUserDataService;
using XERP.Domain.SystemUserDomain.Services;


namespace XERP.Client.WPF.SystemUserMaintenance.ViewModels
{
    public class SystemUserSearchViewModel : ViewModelBase<SystemUserSearchViewModel>
    {
        #region Initialization and Cleanup
        //Search Programs will authenticate to the cordinating maintenance form
        private const string _executableProgramName = "SystemUserMaintenance";
        private ISystemUserServiceAgent _serviceAgent;

        public SystemUserSearchViewModel()
        { }

        public SystemUserSearchViewModel(ISystemUserServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            SystemUserTypeList = GetSystemUserTypes();
            SystemUserCodeList = GetSystemUserCodes();

            SearchObject = new SystemUser();    
            ResultList = new ObservableCollection<SystemUser>();
            SelectedList = new ObservableCollection<SystemUser>();
            //make sure of session authentication...
            if (XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)
            {
                //make sure user has rights to UI...
                DoFormsAuthentication();
            }
            else
            {//User is not authenticated...
                RegisterToReceiveMessages<bool>("StartUpLogIn", OnStartUpLogIn);
                UserControlIsEnabledProperty = false;
                //we will do forms authentication once the log in returns a valid System User...
            }
        }
        #endregion Initialization and Cleanup


        private void DoFormsAuthentication()
        {
            //on log in session information is collected about the system user...
            //we need to make the system user is allowed access to this UI...
            if (ClientSessionSingleton.Instance.ExecutableProgramIDList.Contains(_executableProgramName))
            {
                UserControlIsEnabledProperty = true;
            }
            else
            {
                UserControlIsEnabledProperty = false;
            }
        }

        private void OnStartUpLogIn(object sender, NotificationEventArgs<bool> e)
        {//if true is returned login was successful...
            if (e.Data)
            {
                UserControlIsEnabledProperty = true;
                DoFormsAuthentication();
                NotifyAuthenticated();
            }
            else
            {
                UserControlIsEnabledProperty = false;
            }
            UnregisterToReceiveMessages<bool>("StartUpLogIn", OnStartUpLogIn);
        }


        #region Notifications
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs> CloseNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        #endregion Notifications

        private void NotifyAuthenticated()
        {
            Notify(AuthenticatedNotice, new NotificationEventArgs());
        }


        #region Properties
        private bool? _userControlIsEnabledProperty;
        public bool? UserControlIsEnabledProperty
        {
            get { return _userControlIsEnabledProperty; }
            set
            {
                _userControlIsEnabledProperty = value;
                NotifyPropertyChanged(m => m.UserControlIsEnabledProperty);
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

        private ObservableCollection<SystemUser> _resultList;
        public ObservableCollection<SystemUser> ResultList
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

        private ObservableCollection<SystemUserType> _companyTypeList;
        public ObservableCollection<SystemUserType> SystemUserTypeList
        {
            get { return _companyTypeList; }
            set
            {
                _companyTypeList = value;
                NotifyPropertyChanged(m => m.SystemUserTypeList);
            }
        }

        private ObservableCollection<SystemUserCode> _companyCodeList;
        public ObservableCollection<SystemUserCode> SystemUserCodeList
        {
            get { return _companyCodeList; }
            set
            {
                _companyCodeList = value;
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

        private ObservableCollection<SystemUser> GetSystemUsers()
        {
            return new ObservableCollection<SystemUser>(_serviceAgent.GetSystemUsers().ToList());
        }

        private ObservableCollection<SystemUser> GetSystemUsers(SystemUser companyQueryObject)
        {
            return new ObservableCollection<SystemUser>(_serviceAgent.GetSystemUsers(companyQueryObject).ToList());
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
                ObservableCollection<SystemUser> selectedList = new ObservableCollection<SystemUser>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((SystemUser)item);
                }
                MessageBus.Default.Notify("SystemUserSearch", this, new NotificationEventArgs<ObservableCollection<SystemUser>>("", selectedList)); 
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