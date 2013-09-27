using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SimpleMvvmToolkit;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;


namespace XERP.Client.WPF.WarehouseLocationMaintenance.ViewModels
{
    public class MainSearchViewModel : ViewModelBase<MainSearchViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private IWarehouseServiceAgent _serviceAgent;

        public MainSearchViewModel(){}

        public MainSearchViewModel(IWarehouseServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            WarehouseLocationTypeList = GetWarehouseLocationTypes();
            WarehouseLocationCodeList = GetWarehouseLocationCodes();

            SearchObject = new WarehouseLocation();    
            ResultList = new BindingList<WarehouseLocation>();
            SelectedList = new BindingList<WarehouseLocation>();
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

        private WarehouseLocation _searchObject;
        public WarehouseLocation SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<WarehouseLocation> _resultList;
        public BindingList<WarehouseLocation> ResultList
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

        private ObservableCollection<WarehouseLocationType> _warehouseLocationTypeList;
        public ObservableCollection<WarehouseLocationType> WarehouseLocationTypeList
        {
            get { return _warehouseLocationTypeList; }
            set
            {
                _warehouseLocationTypeList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationTypeList);
            }
        }

        private ObservableCollection<WarehouseLocationCode> _warehouseLocationCodeList;
        public ObservableCollection<WarehouseLocationCode> WarehouseLocationCodeList
        {
            get { return _warehouseLocationCodeList; }
            set
            {
                _warehouseLocationCodeList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationCodeList);
            }
        }
        #endregion Properties

        #region Methods
        private ObservableCollection<WarehouseLocationType> GetWarehouseLocationTypes()
        {
            return new ObservableCollection<WarehouseLocationType>(_serviceAgent.GetWarehouseLocationTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private ObservableCollection<WarehouseLocationCode> GetWarehouseLocationCodes()
        {
            return new ObservableCollection<WarehouseLocationCode>(_serviceAgent.GetWarehouseLocationCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private BindingList<WarehouseLocation> GetWarehouseLocations(string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<WarehouseLocation>(_serviceAgent.GetWarehouseLocations(companyID).ToList());
        }

        private BindingList<WarehouseLocation> GetWarehouseLocations(WarehouseLocation itemQueryObject, string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<WarehouseLocation>(_serviceAgent.GetWarehouseLocations(itemQueryObject, companyID).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetWarehouseLocations(SearchObject, ClientSessionSingleton.Instance.CompanyID);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<WarehouseLocation> selectedList = new BindingList<WarehouseLocation>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((WarehouseLocation)item);
                }
                MessageBus.Default.Notify(MessageTokens.WarehouseLocationSearchToken.ToString(), this, new NotificationEventArgs<BindingList<WarehouseLocation>>("", selectedList)); 
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