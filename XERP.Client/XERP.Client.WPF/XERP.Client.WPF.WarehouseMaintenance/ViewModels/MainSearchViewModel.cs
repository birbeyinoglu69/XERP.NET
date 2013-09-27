using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SimpleMvvmToolkit;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;


namespace XERP.Client.WPF.WarehouseMaintenance.ViewModels
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
            WarehouseTypeList = GetWarehouseTypes();
            WarehouseCodeList = GetWarehouseCodes();

            SearchObject = new Warehouse();    
            ResultList = new BindingList<Warehouse>();
            SelectedList = new BindingList<Warehouse>();
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

        private Warehouse _searchObject;
        public Warehouse SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<Warehouse> _resultList;
        public BindingList<Warehouse> ResultList
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

        private ObservableCollection<WarehouseType> _warehouseTypeList;
        public ObservableCollection<WarehouseType> WarehouseTypeList
        {
            get { return _warehouseTypeList; }
            set
            {
                _warehouseTypeList = value;
                NotifyPropertyChanged(m => m.WarehouseTypeList);
            }
        }

        private ObservableCollection<WarehouseCode> _warehouseCodeList;
        public ObservableCollection<WarehouseCode> WarehouseCodeList
        {
            get { return _warehouseCodeList; }
            set
            {
                _warehouseCodeList = value;
                NotifyPropertyChanged(m => m.WarehouseCodeList);
            }
        }
        #endregion Properties

        #region Methods
        private ObservableCollection<WarehouseType> GetWarehouseTypes()
        {
            return new ObservableCollection<WarehouseType>(_serviceAgent.GetWarehouseTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private ObservableCollection<WarehouseCode> GetWarehouseCodes()
        {
            return new ObservableCollection<WarehouseCode>(_serviceAgent.GetWarehouseCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private BindingList<Warehouse> GetWarehouses(string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<Warehouse>(_serviceAgent.GetWarehouses(companyID).ToList());
        }

        private BindingList<Warehouse> GetWarehouses(Warehouse itemQueryObject, string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<Warehouse>(_serviceAgent.GetWarehouses(itemQueryObject, companyID).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetWarehouses(SearchObject, ClientSessionSingleton.Instance.CompanyID);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<Warehouse> selectedList = new BindingList<Warehouse>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((Warehouse)item);
                }
                MessageBus.Default.Notify(MessageTokens.WarehouseSearchToken.ToString(), this, new NotificationEventArgs<BindingList<Warehouse>>("", selectedList)); 
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