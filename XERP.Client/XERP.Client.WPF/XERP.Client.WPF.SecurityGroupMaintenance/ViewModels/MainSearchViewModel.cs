using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SimpleMvvmToolkit;
using XERP.Domain.SecurityGroupDomain.SecurityGroupDataService;
using XERP.Domain.SecurityGroupDomain.Services;


namespace XERP.Client.WPF.SecurityGroupMaintenance.ViewModels
{
    public class MainSearchViewModel : ViewModelBase<MainSearchViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private ISecurityGroupServiceAgent _serviceAgent;

        public MainSearchViewModel(){}

        public MainSearchViewModel(ISecurityGroupServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            SecurityGroupTypeList = GetSecurityGroupTypes();
            SecurityGroupCodeList = GetSecurityGroupCodes();

            SearchObject = new SecurityGroup();    
            ResultList = new BindingList<SecurityGroup>();
            SelectedList = new BindingList<SecurityGroup>();
            //make sure of session authentication...
            if (ClientSessionSingleton.Instance.SessionIsAuthentic) //make sure user has rights to UI...
                DoFormsAuthentication();
            else
            {//User is not authenticated...
                RegisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
                FormIsEnabled = false;
            }
        }
        #endregion Initialization and Cleanup

        #region Authentication
        private void DoFormsAuthentication()
        {//we need to make the system user is allowed access to this UI...
            if (ClientSessionSingleton.Instance.ExecutableProgramIDList.Contains(_globalProperties.ExecutableProgramName))
                FormIsEnabled = true;
            else
                FormIsEnabled = false;
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
                FormIsEnabled = false;

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

        private SecurityGroup _searchObject;
        public SecurityGroup SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<SecurityGroup> _resultList;
        public BindingList<SecurityGroup> ResultList
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

        private ObservableCollection<SecurityGroupType> _securityGroupTypeList;
        public ObservableCollection<SecurityGroupType> SecurityGroupTypeList
        {
            get { return _securityGroupTypeList; }
            set
            {
                _securityGroupTypeList = value;
                NotifyPropertyChanged(m => m.SecurityGroupTypeList);
            }
        }

        private ObservableCollection<SecurityGroupCode> _securityGroupCodeList;
        public ObservableCollection<SecurityGroupCode> SecurityGroupCodeList
        {
            get { return _securityGroupCodeList; }
            set
            {
                _securityGroupCodeList = value;
                NotifyPropertyChanged(m => m.SecurityGroupCodeList);
            }
        }
        #endregion Properties

        #region Methods
        private ObservableCollection<SecurityGroupType> GetSecurityGroupTypes()
        {
            return new ObservableCollection<SecurityGroupType>(_serviceAgent.GetSecurityGroupTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private ObservableCollection<SecurityGroupCode> GetSecurityGroupCodes()
        {
            return new ObservableCollection<SecurityGroupCode>(_serviceAgent.GetSecurityGroupCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private BindingList<SecurityGroup> GetSecurityGroups(string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<SecurityGroup>(_serviceAgent.GetSecurityGroups(companyID).ToList());
        }

        private BindingList<SecurityGroup> GetSecurityGroups(SecurityGroup itemQueryObject, string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<SecurityGroup>(_serviceAgent.GetSecurityGroups(itemQueryObject, companyID).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetSecurityGroups(SearchObject, ClientSessionSingleton.Instance.CompanyID);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<SecurityGroup> selectedList = new BindingList<SecurityGroup>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((SecurityGroup)item);
                }
                MessageBus.Default.Notify(MessageTokens.SecurityGroupSearchToken.ToString(), this, new NotificationEventArgs<BindingList<SecurityGroup>>("", selectedList)); 
            }
            NotifyClose("");
        }
        #endregion Commands

        #region Helpers
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {// Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }

        private void NotifyClose(string message)
        {
            Notify(CloseNotice, new NotificationEventArgs(message));
        }
        #endregion Helpers
    }
}