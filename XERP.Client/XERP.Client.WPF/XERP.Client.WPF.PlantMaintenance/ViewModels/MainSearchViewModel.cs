using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SimpleMvvmToolkit;
using XERP.Domain.PlantDomain.PlantDataService;
using XERP.Domain.PlantDomain.Services;


namespace XERP.Client.WPF.PlantMaintenance.ViewModels
{
    public class MainSearchViewModel : ViewModelBase<MainSearchViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private IPlantServiceAgent _serviceAgent;

        public MainSearchViewModel(){}

        public MainSearchViewModel(IPlantServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            PlantTypeList = GetPlantTypes();
            PlantCodeList = GetPlantCodes();

            SearchObject = new Plant();    
            ResultList = new BindingList<Plant>();
            SelectedList = new BindingList<Plant>();
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

        private Plant _searchObject;
        public Plant SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<Plant> _resultList;
        public BindingList<Plant> ResultList
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

        private ObservableCollection<PlantType> _plantTypeList;
        public ObservableCollection<PlantType> PlantTypeList
        {
            get { return _plantTypeList; }
            set
            {
                _plantTypeList = value;
                NotifyPropertyChanged(m => m.PlantTypeList);
            }
        }

        private ObservableCollection<PlantCode> _plantCodeList;
        public ObservableCollection<PlantCode> PlantCodeList
        {
            get { return _plantCodeList; }
            set
            {
                _plantCodeList = value;
                NotifyPropertyChanged(m => m.PlantCodeList);
            }
        }
        #endregion Properties

        #region Methods
        private ObservableCollection<PlantType> GetPlantTypes()
        {
            return new ObservableCollection<PlantType>(_serviceAgent.GetPlantTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private ObservableCollection<PlantCode> GetPlantCodes()
        {
            return new ObservableCollection<PlantCode>(_serviceAgent.GetPlantCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private BindingList<Plant> GetPlants(string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<Plant>(_serviceAgent.GetPlants(companyID).ToList());
        }

        private BindingList<Plant> GetPlants(Plant itemQueryObject, string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<Plant>(_serviceAgent.GetPlants(itemQueryObject, companyID).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetPlants(SearchObject, ClientSessionSingleton.Instance.CompanyID);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<Plant> selectedList = new BindingList<Plant>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((Plant)item);
                }
                MessageBus.Default.Notify(MessageTokens.PlantSearchToken.ToString(), this, new NotificationEventArgs<BindingList<Plant>>("", selectedList)); 
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