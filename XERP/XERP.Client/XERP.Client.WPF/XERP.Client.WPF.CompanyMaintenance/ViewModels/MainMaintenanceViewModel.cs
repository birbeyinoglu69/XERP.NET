using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;
using System.Collections.Generic;

// Toolkit namespace
using SimpleMvvmToolkit;

//XERP Namespaces
using XERP.Domain.CompanyDomain.Services;
using XERP.Domain.CompanyDomain.CompanyDataService;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;

namespace XERP.Client.WPF.CompanyMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private ICompanyServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }

        public MainMaintenanceViewModel()
        { }

        public MainMaintenanceViewModel(ICompanyServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            CompanyTypeList = GetCompanyTypes();
            CompanyCodeList = GetCompanyCodes();
            SetAsEmptySelection();

            CompanyList = new BindingList<Company>();
            //disable new row feature...
            CompanyList.AllowNew = false;
            
            //make sure of session authentication...
            if (XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)
            {
                //make sure user has rights to UI...
                DoFormsAuthentication();
            }
            else
            {//User is not authenticated...
                RegisterToReceiveMessages<bool>("StartUpLogInToken", OnStartUpLogIn);
                FormIsEnabled = false;
            }
        }
        #endregion Initialization and Cleanup

        #region Authentication Logic
        private void DoFormsAuthentication()
        {
            //on log in session information is collected about the system user...
            //we need to make the system user is allowed access to this UI...
            if(ClientSessionSingleton.Instance.ExecutableProgramIDList.Contains(_globalProperties.ExecutableProgramName))
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
            UnregisterToReceiveMessages<bool>("StartUpLogInToken", OnStartUpLogIn);
        }
        
        private void NotifyAuthenticated()
        {
            Notify(AuthenticatedNotice, new NotificationEventArgs());  
        }
        #endregion Authentication Logic

        #region Notifications
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs> MessageNotice;
        public event EventHandler<NotificationEventArgs> SearchNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> SaveRequiredNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        #endregion Notifications    

        #region Properties
        //private only properties...
        private string _previousKeyID;
        //private string _rollbackID = "";
        private string _errorMessage = "";

        private bool _allowNew;
        public bool AllowNew
        {
            get { return _allowNew; }
            set
            {
                _allowNew = value;
                NotifyPropertyChanged(m => m.AllowNew);
            }
        }

        private bool _dirty;
        public bool Dirty
        {
            get { return _dirty; }
            set
            {
                _dirty = value;
                NotifyPropertyChanged(m => m.Dirty);
            }
        }

        private bool _allowDelete;
        public bool AllowDelete
        {
            get { return _allowDelete; }
            set
            {
                _allowDelete = value;
                NotifyPropertyChanged(m => m.AllowDelete);
            }
        }

        private bool _allowEdit;
        public bool AllowEdit
        {
            get { return _allowEdit; }
            set
            {
                _allowEdit = value;
                NotifyPropertyChanged(m => m.AllowEdit);
            }
        }
        

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

        private string _companyListCount;
        public string CompanyListCount
        {
            get { return _companyListCount; }
            set
            {
                _companyListCount = value;
                NotifyPropertyChanged(m => m.CompanyListCount);
            }
        }
        
        private BindingList<Company> _companyList;
        public BindingList<Company> CompanyList
        {
            get
            {
                CompanyListCount = _companyList.Count.ToString();
                if (_companyList.Count > 0)
                {
                    AllowEdit = true;
                    AllowDelete = true;
                }
                else
                {//no records to edit delete or be dirty...
                    AllowEdit = false;
                    AllowDelete = false;
                    Dirty = false;
                }
                return _companyList;
            }
            set
            {
                _companyList = value;
                NotifyPropertyChanged(m => m.CompanyList);
            }
        }

        private ObservableCollection<CompanyType> _companyTypeList;
        public ObservableCollection<CompanyType> CompanyTypeList
        {
            get { return _companyTypeList; }
            set
            {
                _companyTypeList = value;
                NotifyPropertyChanged(m => m.CompanyTypeList);
            }
        }

        private ObservableCollection<CompanyCode> _companyCodeList;
        public ObservableCollection<CompanyCode> CompanyCodeList
        {
            get { return _companyCodeList; }
            set
            {
                _companyCodeList = value;
                NotifyPropertyChanged(m => m.CompanyCodeList);
            }
        }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get 
            {
                return _selectedCompany; 
            }
            set
            {
                if (_selectedCompany != value)
                {
                    _selectedCompany = value;
                    if (value != null)
                    {//default the PreviousKeyID...
                        _previousKeyID = _selectedCompany.CompanyID;
                        NotifyPropertyChanged(m => m.SelectedCompany);
                        SelectedCompany.PropertyChanged += new PropertyChangedEventHandler(SelectedCompany_PropertyChanged);
                    }
                }
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _companyMaxFieldValueDictionary;
        public Dictionary<string, int> CompanyMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                _companyMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("Companies");

                foreach (var data in metaData)
                {
                    _companyMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _companyMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedCompany_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CompanyID":
                    //make sure it really changed...
                    if (_previousKeyID != SelectedCompany.CompanyID)
                    {
                        //if their are no records it is a key change
                        if (CompanyList != null && CompanyList.Count == 0
                            && SelectedCompany != null && !string.IsNullOrEmpty(SelectedCompany.CompanyID))
                        {
                            ChangeKeyLogic();
                            _previousKeyID = SelectedCompany.CompanyID;
                            return;
                        }

                        EntityStates entityState = GetCompanyState(SelectedCompany);

                        if (entityState == EntityStates.Unchanged ||
                            entityState == EntityStates.Modified)
                        {//once a key is added it can not be modified...
                            if (Dirty)
                            {//dirty record exists ask if save is required...
                                NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ChangeKeyLogic);
                            }
                            else
                            {
                                ChangeKeyLogic();
                            }
                            return;   
                        }
                        //if record is newly added then we need to validate the key...
                        //if it is valid we will allow another key to be added...
                        //Once the key is added we will update the entity
                        if (entityState == EntityStates.Added)
                        {
                            if (ValidateNewKey(SelectedCompany, out _errorMessage))
                            {
                                AllowNew = true;
                                Update(SelectedCompany);
                                _previousKeyID = SelectedCompany.CompanyID;
                            }
                            else
                            {
                                NotifyMessage(_errorMessage);
                                //cancel change...
                                SelectedCompany.CompanyID = _previousKeyID;
                            }
                            return;
                        }
                    }
                    //capture value to allow for property change comparison...
                    _previousKeyID = SelectedCompany.CompanyID;    
                    break;
                default:    
                    Update(SelectedCompany);
                    break;
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods

        private void ChangeKeyLogic()
        {
            if (ValidateKeyChange(SelectedCompany, out _errorMessage))
            {
                CompanyList = GetCompanyByID(SelectedCompany.CompanyID);
                if (CompanyList.Count == 0)
                {//set it to an empty company and disable edits...
                    SetAsEmptySelection();
                }
                else
                {
                    SelectedCompany = CompanyList.FirstOrDefault();
                    _previousKeyID = SelectedCompany.CompanyID;
                }
            }
            else
            {
                NotifyMessage(_errorMessage);
                if (SelectedCompany.CompanyID != _previousKeyID)
                {
                    SelectedCompany.CompanyID = _previousKeyID;
                }
            }
        }

        private bool ValidateNewKey(Company company, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty(company.CompanyID))
            {
                errorMessage = "CompanyID Is Required";
                return false;
            }
            if (CompanyExists(company.CompanyID.ToString()))
            {
                errorMessage = "CompanyID " + company.CompanyID + " Allready Exists";
                return false;
            }
            return true;
        }

        private bool ValidateKeyChange(Company company, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty(company.CompanyID))
            {
                errorMessage = "CompanyID Is Required";
                return false;
            }
            return true;
        }
        #endregion ViewModel Logic Methods

        #region ServiceAgent Call Methods
        private ObservableCollection<CompanyType> GetCompanyTypes()
        {
            return new ObservableCollection<CompanyType>(_serviceAgent.GetCompanyTypesReadOnly().ToList());
        }

        private ObservableCollection<CompanyCode> GetCompanyCodes()
        {
            return new ObservableCollection<CompanyCode>(_serviceAgent.GetCompanyCodesReadOnly().ToList());
        }

        private EntityStates GetCompanyState(Company company)
        {
            return _serviceAgent.GetCompanyEntityState(company);
        }
        #region Company CRUD

        private ObservableCollection<Company> GetCompanies()
        {
            ObservableCollection<Company> companyList = new ObservableCollection<Company>(_serviceAgent.GetCompanies().ToList());
            Dirty = false;
            return companyList; 
        }

        private ObservableCollection<Company> GetCompanies(Company company)
        {
            ObservableCollection<Company> companyList = new ObservableCollection<Company>(_serviceAgent.GetCompanies(company).ToList());
            Dirty = false;
            return companyList;
        }

        private BindingList<Company> GetCompanyByID(string id)
        {
            BindingList<Company> companyList = new BindingList<Company>(_serviceAgent.GetCompanyByID(id).ToList());
            Dirty = false;
            return companyList; 
        }

        private bool CompanyExists(string companyID)
        {
            return _serviceAgent.CompanyExists(companyID);
        }
 
        private bool Update(Company company)
        {
            _serviceAgent.UpdateCompanyRepository(company);
            Dirty = true;
            return true;
        }

        private bool Commit()
        {
            //bulk validation required here...
            _serviceAgent.CommitCompanyRepository();
            Dirty = false;
            return true;
        }

        private bool Delete(Company company)
        {
            _serviceAgent.DeleteFromCompanyRepository(company);
            return true;
        }

        private bool NewCompany(Company company)
        {
            _serviceAgent.AddToCompanyRepository(company);
            SelectedCompany = CompanyList.LastOrDefault();
            Dirty = true;
            return true;
        }

        #endregion Company CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void SaveCommand()
        {
            if (GetCompanyState(SelectedCompany) != EntityStates.Detached)
            {
                if (Update(SelectedCompany))
                {
                    Commit();
                }
            }
        }

        public void DeleteCommand()
        {
            int i = 0;
            if (CompanyList != null && CompanyList.Count > 1)
            {
                i = CompanyList.IndexOf(SelectedCompany) - 1;
                //if they delete the first row...
                if (i == -1)
                {
                    i = 0;
                }
                //detach from repository and delete it from db...
                Delete(SelectedCompany);
                //remove it from the the cached forms list...
                CompanyList.Remove(SelectedCompany);
                //reset the selected company...
                SelectedCompany = CompanyList[i];
                //default the functionality
                AllowEdit = true;
                AllowDelete = true;
                AllowNew = true;
                Dirty = false;
                //loop whats left of list to see if their are still unvalid added rows
                //and or dirty rows...
                foreach (Company company in CompanyList)
                {
                    EntityStates entityState = GetCompanyState(company);
                    if (entityState == EntityStates.Added)
                    {
                        if (ValidateNewKey(company, out _errorMessage) == false)
                        {
                            AllowNew = false;
                            Dirty = true;
                        }
                    }
                    if (entityState == EntityStates.Modified ||
                        entityState == EntityStates.Detached)
                    {
                        Dirty = true;
                    }
                }
            }
            else
            {//only one record deleting will result in no records...
                Delete(SelectedCompany);
                CompanyList.Remove(SelectedCompany);
                SetAsEmptySelection();
            }  
        }

        private void SetAsEmptySelection()
        {
            SelectedCompany = new Company();
            AllowEdit = false;
            AllowDelete = false;
            AllowNew = true;
            Dirty = false;
            _previousKeyID = null;
        }

        public void NewCompanyCommand()
        {
            Company company = new Company();
            CompanyList.Add(company);
            NewCompany(company);
            AllowEdit = true;
            AllowNew = false;

        }

        public void ClearCommand()
        {
            if (Dirty)
            {
                NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ClearLogic);
            }
            else
            {
                ClearLogic();
            }  
        }

        public void ClearLogic()
        {
            CompanyList.Clear();
            SetAsEmptySelection();
        }
        public void SearchCommand()
        {
            if (Dirty)
            {
                NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.SearchLogic);
            }
            else
            {
                SearchLogic(); 
            }   
        }

        private void SearchLogic()
        {
            RegisterToReceiveMessages<BindingList<Company>>("MainSearchToken", OnSearchResult);
            
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<Company>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                CompanyList = e.Data;
                SelectedCompany = CompanyList.FirstOrDefault();
                Dirty = false;
            }
            UnregisterToReceiveMessages<BindingList<Company>>("MainSearchToken", OnSearchResult);
        }

        #endregion Commands

        #region Completion Callbacks

        // TODO: Optionally add callback methods for async calls to the service agent

        #endregion Completion Callbacks

        #region Helpers
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
        private void NotifyMessage(string message)
        {
            // Notify view of an error message w/o throwing an error...
            Notify(MessageNotice, new NotificationEventArgs<Exception>(message));
        }
        //Notify view to launch search...
        private void NotifySearch(string message)
        {
            Notify(SearchNotice, new NotificationEventArgs(message));
        }

        //Notify view save may be required...
        private void NotifySaveRequired(string message, _saveRequiredResultActions resultAction)
        {
            //switch (resultAction)
            //{
                //case _saveRequiredResultActions.ChangeKeyLogic:
                    Notify(SaveRequiredNotice, new NotificationEventArgs<bool, MessageBoxResult>
                        (message, true, result => { OnSaveResult(result, resultAction); }));
            //       break;
            //    case _saveRequiredResultActions.SearchLogic:
            //        Notify(SaveRequiredNotice, new NotificationEventArgs<bool, MessageBoxResult>
            //            (message, true, result => { OnSaveResult(result, resultAction); }));
            //        break;
            //    case _saveRequiredResultActions.ClearLogic:
            //        Notify(SaveRequiredNotice, new NotificationEventArgs<bool, MessageBoxResult>
            //            (message, true, result => { OnSaveResult(result, resultAction); }));
            //        break;
            //}            
        }

        private void OnSaveResult(MessageBoxResult result, _saveRequiredResultActions resultAction)
        {
            switch (result)
            {
                case MessageBoxResult.No:
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Yes:
                    _serviceAgent.CommitCompanyRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //don't do anything at all...
                    //SelectedCompany.CompanyID = _rollbackID;
                    break;
            }
        }
        
        private void CaseSaveResultActions(_saveRequiredResultActions resultAction)
        {
            switch ( resultAction)
            {
                case _saveRequiredResultActions.ChangeKeyLogic:
                    ChangeKeyLogic();
                    break;
                case _saveRequiredResultActions.SearchLogic:
                    SearchLogic();
                    break;
                case _saveRequiredResultActions.ClearLogic:
                    ClearLogic();
                    break;
            }
        }
        #endregion Helpers
    }
}