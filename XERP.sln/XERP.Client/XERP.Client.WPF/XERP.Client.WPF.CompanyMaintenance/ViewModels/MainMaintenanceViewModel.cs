using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;

// Toolkit namespace
using SimpleMvvmToolkit;

//XERP Namespaces
using XERP.Domain.CompanyDomain.Services;
using XERP.Domain.CompanyDomain.CompanyDataService;
using System.Windows.Controls;
using System.Windows.Input;

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
            SearchLogic
        }

        public MainMaintenanceViewModel()
        { }

        public MainMaintenanceViewModel(ICompanyServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            _dirty = false;

            CompanyTypeList = GetCompanyTypes();
            CompanyCodeList = GetCompanyCodes();
            SelectedCompany = new Company();
            CompanyList = new ObservableCollection<Company>();
            //make sure of session authentication...
            if (XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)
            {
                //make sure user has rights to UI...
                DoFormsAuthentication();
            }
            else
            {//User is not authenticated...
                RegisterToReceiveMessages<bool>("StartUpLogInToken", OnStartUpLogIn);
                UserControlIsEnabledProperty = false;
                //we will do forms authentication once the log in returns a valid System User...
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
            UnregisterToReceiveMessages<bool>("StartUpLogInToken", OnStartUpLogIn);
        }
        
        private void NotifyAuthenticated()
        {
            Notify(AuthenticatedNotice, new NotificationEventArgs());  
        }
        #endregion Authentication Logic

        #region Notifications
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs> SearchNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> SaveRequiredNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        #endregion Notifications    

        #region Properties
        //private only properties...
        private EntityStates _selectedCompanyEntityState;
        private string _companyID;
        private bool _dirty;

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
        
        private ObservableCollection<Company> _companyList;
        public ObservableCollection<Company> CompanyList
        {
            get { return _companyList; }
            set
            {
                _companyList = value;
                NotifyPropertyChanged(m => m.CompanyList);
                CompanyList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CompanyList_CollectionChanged);
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
            get { return _selectedCompany; }
            set
            {
                if (_selectedCompany != value)
                {
                    _selectedCompany = value;
                    if (value != null)
                    {
                        _selectedCompanyEntityState = GetCompanyState(_selectedCompany);
                        //_companyID = _selectedCompany.CompanyID;
                        NotifyPropertyChanged(m => m.SelectedCompany);
                        SelectedCompany.PropertyChanged += new PropertyChangedEventHandler(SelectedCompany_PropertyChanged);
                    }
                }
            }
        }
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedCompany_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CompanyID":
                    OnSelectedKeyChanged();
                    break;
                default:
                        Update(SelectedCompany);
                    break;
            }
        }

        private void CompanyList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Company newItem in e.NewItems)
                    {
                        NewCompany(newItem);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (Company oldItem in e.OldItems)
                    {
                        if (oldItem != null)
                        {
                            Delete(oldItem);
                        }
                    }
                    break;
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void OnSelectedKeyChanged()
        {
            if (CompanyList != null && CompanyList.Count == 0)
            {
                if (SelectedCompany != null && ! string.IsNullOrEmpty(_selectedCompany.CompanyID))
                {
                    ChangeKeyLogic();
                }
                return;
            }
            if ((_companyID != SelectedCompany.CompanyID &&
                _selectedCompanyEntityState != EntityStates.Added))
            {
                if (_dirty)
                {
                    NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ChangeKeyLogic);
                }
                else
                {
                    ChangeKeyLogic();
                }   
            }
            
        }

        private void ChangeKeyLogic()
        {
            if (CompanyExists(SelectedCompany.CompanyID))
            {
                CompanyList = GetCompanyByID(SelectedCompany.CompanyID);
                SelectedCompany = CompanyList.FirstOrDefault();
                _companyID = SelectedCompany.CompanyID;
            }
            else
            {
                Exception e = new Exception();
                NotifyError("No Company Record Exsists Matching CompanyID " + _selectedCompany.CompanyID, e);
                if (_selectedCompany.CompanyID != _companyID)
                {
                    SelectedCompany.CompanyID = _companyID;
                }
                return;
            }
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
            _dirty = false;
            return companyList; 
        }

        private ObservableCollection<Company> GetCompanies(Company company)
        {
            //ClearRepository();
            ObservableCollection<Company> companyList = new ObservableCollection<Company>(_serviceAgent.GetCompanies(company).ToList());
            //AttachToRepository(companyList);
            _dirty = false;
            return companyList;
        }

        private ObservableCollection<Company> GetCompanyByID(string id)
        {
            //ClearRepository();
            ObservableCollection<Company> companyList = new ObservableCollection<Company>(_serviceAgent.GetCompanyByID(id).ToList());
            
            //AttachToRepository(companyList);
            _dirty = false;
            return companyList; 
        }

        private bool CompanyExists(string companyID)
        {
            return _serviceAgent.CompanyExists(companyID);
        }
 
        private void Update(Company company)
        {
            _serviceAgent.UpdateCompanyRepository(company);
            _dirty = true;
        }

        private void Commit()
        {
            _serviceAgent.CommitCompanyRepository();
            _dirty = false;
        }

        private void Delete(Company company)
        {
            _serviceAgent.DeleteFromCompanyRepository(company);
            Commit();
        }

        private void NewCompany(Company company)
        {
            _serviceAgent.AddToCompanyRepository(company);
            SelectedCompany = CompanyList.LastOrDefault();
            _dirty = true;
        }

        #endregion Company CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void SaveCommand()
        {
            if (_selectedCompanyEntityState != EntityStates.Detached)
            {
                Update(SelectedCompany);
                Commit();
            }
        }

        public void DeleteCommand()
        {
            int i = 0;
            if (CompanyList != null && CompanyList.Count > 1)
            {
                i = CompanyList.IndexOf(SelectedCompany) - 1;
                CompanyList.Remove(SelectedCompany);
                SelectedCompany = CompanyList[i];
            }
            else
            {
                CompanyList.Remove(SelectedCompany);
                SelectedCompany = new Company();
            }
        }

        public void NewCompanyCommand()
        {
            Company company = new Company();
            CompanyList.Add(company);
        }

        public void ClearCommand()
        {
            
            CompanyList.Clear();
            SelectedCompany = new Company();
        }

        public void SearchCommand()
        {
            if (_dirty)
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
            RegisterToReceiveMessages<ObservableCollection<Company>>("MainSearchToken", OnSearchResult);
            
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<ObservableCollection<Company>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                CompanyList = e.Data;
                SelectedCompany = CompanyList.FirstOrDefault();
                _dirty = false;
            }
            UnregisterToReceiveMessages<ObservableCollection<Company>>("MainSearchToken", OnSearchResult);
        }

        //public void RightClickContextMenuCommand(object sender, MouseButtonEventArgs e)
        //{
            
        //}
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
        //Notify view to launch search...
        private void NotifySearch(string message)
        {
            Notify(SearchNotice, new NotificationEventArgs(message));
        }

        //Notify view save may be required...
        private void NotifySaveRequired(string message, _saveRequiredResultActions resultAction)
        {
            switch (resultAction)
            {
                case _saveRequiredResultActions.ChangeKeyLogic:
                    Notify(SaveRequiredNotice, new NotificationEventArgs<bool, MessageBoxResult>
                        (message, true, result => { OnSaveResult(result, resultAction); }));
                    break;
                case _saveRequiredResultActions.SearchLogic:
                    Notify(SaveRequiredNotice, new NotificationEventArgs<bool, MessageBoxResult>
                        (message, true, result => { OnSaveResult(result, resultAction); }));
                    break;
            }            
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
                    SelectedCompany.CompanyID = _companyID;
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
            }
        }
        #endregion Helpers
    }
}