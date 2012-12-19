using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SimpleMvvmToolkit;
using XERP.Domain.AddressDomain.AddressDataService;
using XERP.Domain.AddressDomain.Services;


namespace XERP.Client.WPF.AddressMaintenance.ViewModels
{
    public class MainSearchViewModel : ViewModelBase<MainSearchViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private IAddressServiceAgent _serviceAgent;

        public MainSearchViewModel(){}

        public MainSearchViewModel(IAddressServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SearchObject = new Address();    
            ResultList = new BindingList<Address>();
            SelectedList = new BindingList<Address>();
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

        private Address _searchObject;
        public Address SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<Address> _resultList;
        public BindingList<Address> ResultList
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
        #endregion Properties

        #region Methods

        private BindingList<Address> GetAddresses(string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<Address>(_serviceAgent.GetAddresses(companyID).ToList());
        }

        private BindingList<Address> GetAddresses(Address itemQueryObject, string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<Address>(_serviceAgent.GetAddresses(itemQueryObject, companyID).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetAddresses(SearchObject, ClientSessionSingleton.Instance.CompanyID);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<Address> selectedList = new BindingList<Address>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((Address)item);
                }
                MessageBus.Default.Notify(MessageTokens.AddressSearchToken.ToString(), this, new NotificationEventArgs<BindingList<Address>>("", selectedList)); 
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