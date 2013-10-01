using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SimpleMvvmToolkit;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;


namespace XERP.Client.WPF.WarehouseLocationBinMaintenance.ViewModels
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
            WarehouseLocationBinTypeList = GetWarehouseLocationBinTypes();
            WarehouseLocationBinCodeList = GetWarehouseLocationBinCodes();

            SearchObject = new WarehouseLocationBin();    
            ResultList = new BindingList<WarehouseLocationBin>();
            SelectedList = new BindingList<WarehouseLocationBin>();
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

        private WarehouseLocationBin _searchObject;
        public WarehouseLocationBin SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<WarehouseLocationBin> _resultList;
        public BindingList<WarehouseLocationBin> ResultList
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

        private ObservableCollection<WarehouseLocationBinType> _warehouseLocationBinTypeList;
        public ObservableCollection<WarehouseLocationBinType> WarehouseLocationBinTypeList
        {
            get { return _warehouseLocationBinTypeList; }
            set
            {
                _warehouseLocationBinTypeList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationBinTypeList);
            }
        }

        private ObservableCollection<WarehouseLocationBinCode> _warehouseLocationBinCodeList;
        public ObservableCollection<WarehouseLocationBinCode> WarehouseLocationBinCodeList
        {
            get { return _warehouseLocationBinCodeList; }
            set
            {
                _warehouseLocationBinCodeList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationBinCodeList);
            }
        }
        #endregion Properties

        #region Methods
        private ObservableCollection<WarehouseLocationBinType> GetWarehouseLocationBinTypes()
        {
            return new ObservableCollection<WarehouseLocationBinType>(_serviceAgent.GetWarehouseLocationBinTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private ObservableCollection<WarehouseLocationBinCode> GetWarehouseLocationBinCodes()
        {
            return new ObservableCollection<WarehouseLocationBinCode>(_serviceAgent.GetWarehouseLocationBinCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private BindingList<WarehouseLocationBin> GetWarehouseLocationBins(string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<WarehouseLocationBin>(_serviceAgent.GetWarehouseLocationBins(companyID).ToList());
        }

        private BindingList<WarehouseLocationBin> GetWarehouseLocationBins(WarehouseLocationBin itemQueryObject, string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<WarehouseLocationBin>(_serviceAgent.GetWarehouseLocationBins(itemQueryObject, companyID).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetWarehouseLocationBins(SearchObject, ClientSessionSingleton.Instance.CompanyID);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<WarehouseLocationBin> selectedList = new BindingList<WarehouseLocationBin>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((WarehouseLocationBin)item);
                }
                MessageBus.Default.Notify(MessageTokens.WarehouseLocationBinSearchToken.ToString(), this, new NotificationEventArgs<BindingList<WarehouseLocationBin>>("", selectedList)); 
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