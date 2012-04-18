using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;
using System.Linq;
// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;
using XERP.Domain.CompanyDomain.Services;
using XERP.Domain.CompanyDomain.CompanyDataService;
using System.Data.Services.Client;
using System.ComponentModel;

namespace XERP.Client.WPF.CompanyMaintenance.ViewModels
{
    public class TypeMaintenanceViewModel : ViewModelBase<TypeMaintenanceViewModel>
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

        public TypeMaintenanceViewModel(ICompanyServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            _dirty = false;

            SelectedCompanyType = new CompanyType();
            CompanyTypeList = new ObservableCollection<CompanyType>();
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
        private EntityStates _selectedCompanyTypeEntityState;
        private string _companyTypeID;
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
        
        private ObservableCollection<CompanyType> _companyTypeList;
        public ObservableCollection<CompanyType> CompanyTypeList
        {
            get { return _companyTypeList; }
            set
            {
                _companyTypeList = value;
                NotifyPropertyChanged(m => m.CompanyTypeList);
                CompanyTypeList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CompanyTypeList_CollectionChanged);
            }
        }

        private CompanyType _selectedCompanyType;
        public CompanyType SelectedCompanyType
        {
            get { return _selectedCompanyType; }
            set
            {
                if (_selectedCompanyType != value)
                {
                    _selectedCompanyType = value;
                    if (value != null)
                    {
                        _selectedCompanyTypeEntityState = GetCompanyTypeState(_selectedCompanyType);
                        NotifyPropertyChanged(m => m.SelectedCompanyType);
                        SelectedCompanyType.PropertyChanged += new PropertyChangedEventHandler(SelectedCompanyType_PropertyChanged);
                    }
                }
            }
        }
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedCompanyType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CompanyTypeID":
                    OnSelectedKeyChanged();
                    break;
                default:
                        Update(SelectedCompanyType);
                    break;
            }
        }

        private void CompanyTypeList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (CompanyType newItem in e.NewItems)
                    {
                        NewCompanyType(newItem);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (CompanyType oldItem in e.OldItems)
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
            if (CompanyTypeList != null && CompanyTypeList.Count == 0)
            {
                if (SelectedCompanyType != null && ! string.IsNullOrEmpty(_selectedCompanyType.CompanyTypeID))
                {
                    ChangeKeyLogic();
                }
                return;
            }
            if ((_companyTypeID != SelectedCompanyType.CompanyTypeID &&
                _selectedCompanyTypeEntityState != EntityStates.Added))
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
            if (CompanyTypeExists(SelectedCompanyType.CompanyTypeID))
            {
                CompanyTypeList = GetCompanyTypeByID(SelectedCompanyType.CompanyTypeID);
                SelectedCompanyType = CompanyTypeList.FirstOrDefault();
                _companyTypeID = SelectedCompanyType.CompanyTypeID;
            }
            else
            {
                Exception e = new Exception();
                NotifyError("No Company Type Record Exsists Matching CompanyTypeID " + _selectedCompanyType.CompanyTypeID, e);
                if (_selectedCompanyType.CompanyTypeID != _companyTypeID)
                {
                    SelectedCompanyType.CompanyTypeID = _companyTypeID;
                }
                return;
            }
        }
        #endregion ViewModel Logic Methods

        #region ServiceAgent Call Methods
        private EntityStates GetCompanyTypeState(CompanyType companyType)
        {
            return _serviceAgent.GetCompanyTypeEntityState(companyType);
        }
        #region CompanyType CRUD

        private ObservableCollection<CompanyType> GetCompanyTypes()
        {
            ObservableCollection<CompanyType> companyTypeList = new ObservableCollection<CompanyType>(_serviceAgent.GetCompanyTypes().ToList());
            _dirty = false;
            return companyTypeList; 
        }

        private ObservableCollection<CompanyType> GetCompanyTypes(CompanyType companyType)
        {
            ObservableCollection<CompanyType> companyTypeList = new ObservableCollection<CompanyType>(_serviceAgent.GetCompanyTypes(companyType).ToList());
            _dirty = false;
            return companyTypeList;
        }

        private ObservableCollection<CompanyType> GetCompanyTypeByID(string id)
        {
            ObservableCollection<CompanyType> companyTypeList = new ObservableCollection<CompanyType>(_serviceAgent.GetCompanyTypeByID(id).ToList());
            _dirty = false;
            return companyTypeList; 
        }

        private bool CompanyTypeExists(string id)
        {
            return _serviceAgent.CompanyTypeExists(id);
        }
 
        private void Update(CompanyType obj)
        {
            string updateError;
            _serviceAgent.UpdateCompanyTypeRepository(obj, out updateError);
            _dirty = true;
        }

        private void Commit()
        {
            _serviceAgent.CommitCompanyTypeRepository();
            _dirty = false;
        }

        private void Delete(CompanyType obj)
        {
            _serviceAgent.DeleteFromCompanyTypeRepository(obj);
            Commit();
        }

        private void NewCompanyType(CompanyType obj)
        {
            _serviceAgent.AddToCompanyTypeRepository(obj);
            SelectedCompanyType = CompanyTypeList.LastOrDefault();
            _dirty = true;
        }

        #endregion CompanyType CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void SaveCommand()
        {
            if (_selectedCompanyTypeEntityState != EntityStates.Detached)
            {
                Update(SelectedCompanyType);
                Commit();
            }
        }

        public void DeleteCommand()
        {
            int i = 0;
            if (CompanyTypeList != null && CompanyTypeList.Count > 1)
            {
                i = CompanyTypeList.IndexOf(SelectedCompanyType) - 1;
                CompanyTypeList.Remove(SelectedCompanyType);
                SelectedCompanyType = CompanyTypeList[i];
            }
            else
            {
                CompanyTypeList.Remove(SelectedCompanyType);
                SelectedCompanyType = new CompanyType();
            }
        }

        public void NewCompanyTypeCommand()
        {
            CompanyType obj = new CompanyType();
            CompanyTypeList.Add(obj);
        }

        public void ClearCommand()
        {
            
            CompanyTypeList.Clear();
            SelectedCompanyType = new CompanyType();
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
            RegisterToReceiveMessages<ObservableCollection<CompanyType>>("TypeSearchToken", OnSearchResult);
            
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<ObservableCollection<CompanyType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                CompanyTypeList = e.Data;
                SelectedCompanyType = CompanyTypeList.FirstOrDefault();
                _dirty = false;
            }
            UnregisterToReceiveMessages<ObservableCollection<CompanyType>>("TypeSearchToken", OnSearchResult);
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
                    _serviceAgent.CommitCompanyTypeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    SelectedCompanyType.CompanyTypeID = _companyTypeID;
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