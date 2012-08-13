using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
// Toolkit namespace
using SimpleMvvmToolkit;
//XERP Namespaces
using XERP.Domain.CompanyDomain.Services;
using XERP.Domain.CompanyDomain.CompanyDataService;
using XERP.Domain.ClientModels;
using XERP.Client;
//required for extension methods...
using ExtensionMethods;
using System.Text;
using XERP.Client.Models;

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
        //required else it generates debug view designer issues 
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
                RegisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
                FormIsEnabled = false;
            }

            AllowNew = true;
            AllowRowPaste = true;
            //CompanyColumnMetaDataList = new List<ColumnMetaData>();
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
            UnregisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
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
        public event EventHandler<NotificationEventArgs> TypeSearchNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> SaveRequiredNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> NewRecordNeededNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        public event EventHandler<NotificationEventArgs> NewRecordCreatedNotice;
        #endregion Notifications    

        #region Properties
        //used to enable/disable rowcopy feature for main datagrid...
        private bool _allowRowCopy;
        public bool AllowRowCopy
        {
            get { return _allowRowCopy; }
            set
            {
                _allowRowCopy = value;
                NotifyPropertyChanged(m => m.AllowRowCopy);
            }
        }

        private bool _allowRowPaste;

        public bool AllowRowPaste
        {
            get { return _allowRowPaste; }
            set 
            { 
                _allowRowPaste = value;
                NotifyPropertyChanged(m => m.AllowRowPaste);
            }
        }


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

        private bool _allowCommit;

        public bool AllowCommit
        {
            get { return _allowCommit; }
            set 
            { 
                _allowCommit = value;
                NotifyPropertyChanged(m => m.AllowCommit);
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
                    AllowRowCopy = true;
                }
                else
                {//no records to edit delete or be dirty...
                    AllowEdit = false;
                    AllowDelete = false;
                    Dirty = false;
                    AllowCommit = false;
                    AllowRowCopy = false;
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

        //this is used to collect previous values as to compare the changed values...
        private Company _selectedCompanyMirror;
        public Company SelectedCompanyMirror
        {
            get { return _selectedCompanyMirror; }
            set { _selectedCompanyMirror = value; }
        }

        private System.Collections.IList _selectedCompanyList;
        public System.Collections.IList SelectedCompanyList
        {
            get { return _selectedCompanyList; }
            set
            {
                if (_selectedCompany != value)
                {
                    _selectedCompanyList = value;
                    NotifyPropertyChanged(m => m.SelectedCompanyList);
                }  
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
                    //set the mirrored SelectedCompany to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedCompanyMirror = new Company();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedCompany.GetType().GetProperties())
                        {
                            SelectedCompanyMirror.SetPropertyValue(prop.Name, SelectedCompany.GetPropertyValue(prop.Name));
                        }
                        SelectedCompanyMirror.CompanyID = _selectedCompany.CompanyID;
                        NotifyPropertyChanged(m => m.SelectedCompany);
                        
                        SelectedCompany.PropertyChanged += new PropertyChangedEventHandler(SelectedCompany_PropertyChanged); 
                    }
                }
            }
        }

        private List<ColumnMetaData> _companyColumnMetaDataList;
        public List<ColumnMetaData> CompanyColumnMetaDataList
        {
            get { return _companyColumnMetaDataList; }
            set 
            { 
                _companyColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.CompanyColumnMetaDataList);
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
                    if (data.ShortChar_1 == "String")
                    {
                        _companyMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _companyMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedCompany_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "CompanyID")
            {//make sure it is has changed...
                if (SelectedCompanyMirror.CompanyID != SelectedCompany.CompanyID)
                {
                    //if their are no records it is a key change
                    if (CompanyList != null && CompanyList.Count == 0
                        && SelectedCompany != null && !string.IsNullOrEmpty(SelectedCompany.CompanyID))
                    {
                        ChangeKeyLogic();
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
                }
            }//end KeyID logic...
            
            object propertyChangedValue = SelectedCompany.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedCompanyMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedCompany.GetPropertyType(e.PropertyName);
            //in some instances the value is not really changing but yet it still is tripping property change..
            //This will ensure that the field has physically been modified...
            //As well when we revert back it constitutes a property change but they will be = and it will bypass the logic...
            bool objectsAreEqual;
            if (propertyChangedValue == null)
            {
                if (prevPropertyValue == null)
                {//both values are null
                    objectsAreEqual = true;
                }
                else
                {//only one value is null
                    objectsAreEqual = false;
                }
            }
            else 
            {
                if (prevPropertyValue == null)
                {//only one value is null
                    objectsAreEqual = false;
                }
                else //both values are not null use .Equals...
                {
                    objectsAreEqual = propertyChangedValue.Equals(prevPropertyValue);
                }
            }
            if (!objectsAreEqual)
            {
                //Here we do property change validation if false is returned we will reset the value
                //Back to its mirrored value and return out of the property change w/o updating the repository...
                if (PropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedCompany);
                    //set the mirrored objects field...
                    SelectedCompanyMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedCompany.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    return;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods

        private void ChangeKeyLogic()
        {
            string errorMessage = "";
            if (KeyChangeIsValid(SelectedCompany.CompanyID, out errorMessage))
            {
                //check to see if key is part of the current companylist...
                Company query = CompanyList.Where(company => company.CompanyID == SelectedCompany.CompanyID &&
                                                        company.AutoID != SelectedCompany.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected company...
                    SelectedCompany = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                CompanyList = GetCompanyByID(SelectedCompany.CompanyID);
                if (CompanyList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedCompany.CompanyID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedCompany = CompanyList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedCompany.CompanyID != SelectedCompanyMirror.CompanyID)
                {
                    SelectedCompany.CompanyID = SelectedCompanyMirror.CompanyID;
                }
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {
            string errorMessage = "";
            bool rBool = true;
            Dirty = false;
            foreach (Company company in CompanyList)
            {
                EntityStates entityState = GetCompanyState(company);
                if (entityState == EntityStates.Modified || 
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(company, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(company.Name, out errorMessage) == false)
                {
                    rBool = false;
                }
            }
            //more bulk validation as required...
            //note bulk validation should coincide with property validation...
            //as we will not allow a commit until all data is valid...
            return rBool;
        }

        private bool PropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage;
            bool rBool = true;
            switch (propertyName)
            {
                case "CompanyID":
                    rBool = NewKeyIsValid(SelectedCompany, out errorMessage);
                    if (rBool == false)
                    {
                        NotifyMessage(errorMessage);
                        return rBool;
                    }
                    break;
                case "Name":
                    rBool = NameIsValid(changedValue, out errorMessage);
                    if (rBool == false)
                    {
                        NotifyMessage(errorMessage);
                        return rBool;
                    }
                    break;
            }
            return true;
        }

        private bool NewKeyIsValid(Company company, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(company.CompanyID, out errorMessage) == false)
            {
                return false;
            }
            if (CompanyExists(company.CompanyID.ToString()))
            {
                errorMessage = "CompanyID " + company.CompanyID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object companyID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)companyID))
            {
                errorMessage = "CompanyID Is Required...";
                return false;
            }
            return true;
        }

        private bool NameIsValid(object value, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)value))
            {
                errorMessage = "Name Is Required...";
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
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedCompany.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (Company company in CompanyList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (company.AutoID > 0)
                {
                    autoIDs = autoIDs + company.AutoID.ToString() + ",";
                }
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                CompanyList = new BindingList<Company>(_serviceAgent.RefreshCompany(autoIDs).ToList());
                SelectedCompany = (from q in CompanyList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<Company> GetCompanies()
        {
            BindingList<Company> companyList = new BindingList<Company>(_serviceAgent.GetCompanies().ToList());
            Dirty = false;
            AllowCommit = false;
            return companyList; 
        }

        private BindingList<Company> GetCompanies(Company company)
        {
            BindingList<Company> companyList = new BindingList<Company>(_serviceAgent.GetCompanies(company).ToList());
            Dirty = false;
            AllowCommit = false;
            return companyList;
        }

        private BindingList<Company> GetCompanyByID(string id)
        {
            BindingList<Company> companyList = new BindingList<Company>(_serviceAgent.GetCompanyByID(id).ToList());
            Dirty = false;
            AllowCommit = false;
            return companyList; 
        }

        private bool CompanyExists(string companyID)
        {
            return _serviceAgent.CompanyExists(companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(Company company)
        {
            _serviceAgent.UpdateCompanyRepository(company);
            Dirty = true;
            if (CommitIsAllowed())
            {
                AllowCommit = true;
                return true;
            }
            else
            {
                AllowCommit = false;
                return false;
            }
        }
        //commits repository to the db...
        private bool Commit()
        {
            _serviceAgent.CommitCompanyRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(Company company)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromCompanyRepository(company);
            return true;
        }

        private bool NewCompany(Company company)
        {
            _serviceAgent.AddToCompanyRepository(company);
            SelectedCompany = CompanyList.LastOrDefault();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        #endregion Company CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedCompany = new Company();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            CompanyList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                CompanyColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
                { return c1.Order.CompareTo(c2.Order); });

                char[] rowSplitter = { '\r', '\n' };
                char[] columnSplitter = { '\t' };
                //get the text from clipboard
                IDataObject dataInClipboard = Clipboard.GetDataObject();
                string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);
                //split it into rows...
                string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);

                foreach (string row in rowsInClipboard)
                {
                    NewCompanyCommand(); //this will generate a new company and set it as the selected company...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedCompany.SetPropertyValue(CompanyColumnMetaDataList[i].Name, columnValue);
                        i++;
                    }
                }
            }
            catch(Exception ex)
            {
                NotifyMessage(ex.InnerException.ToString());
            }
        }

        public void SaveCommand()
        {
            if (GetCompanyState(SelectedCompany) != EntityStates.Detached)
            {
                if (Update(SelectedCompany))
                {
                    Commit();
                }
                else
                {//this should not be hit but just in case we will catch it and then see 
                    //if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
                }
            }
        }
        public void RefreshCommand()
        {
            Refresh();
        }
        public void DeleteCommand()
        {
            int i = 0;
            bool isFirstDelete = true;
            for (int j = SelectedCompanyList.Count - 1; j >= 0; j--)
            {
                Company company = (Company)SelectedCompanyList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = CompanyList.IndexOf(company) - SelectedCompanyList.Count;
                }
                
                Delete(company);
                CompanyList.Remove(company);
            }

            if (CompanyList != null && CompanyList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedCompany = CompanyList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }  
        }

        public void NewCompanyCommand()
        {
            Company company = new Company();
            CompanyList.Add(company);
            NewCompany(company);
            AllowEdit = true;
            //don't allow a save until a companyID is provided...
            AllowCommit = false;
            NotifyNewRecordCreated();
        }
        //overloaded to allow a companyID to be provided...
        public void NewCompanyCommand(string companyID)
        {
            Company company = new Company();
            company.CompanyID = companyID;
            CompanyList.Add(company);
            NewCompany(company);
            AllowEdit = true;
            AllowCommit = CommitIsAllowed();
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
            RegisterToReceiveMessages<BindingList<Company>>(MessageTokens.CompanySearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<Company>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                CompanyList = e.Data;
                SelectedCompany = CompanyList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<Company>>(MessageTokens.CompanySearchToken.ToString(), OnSearchResult);
        }

        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<CompanyType>>(MessageTokens.CompanyTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<CompanyType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SelectedCompany.CompanyTypeID = e.Data.FirstOrDefault().CompanyTypeID;
            }
            UnregisterToReceiveMessages<BindingList<CompanyType>>(MessageTokens.CompanyTypeSearchToken.ToString(), OnTypeSearchResult);
        }
        
        #endregion Commands

        #region Completion Callbacks

        // TODO: Optionally add callback methods for async calls to the service agent

        #endregion Completion Callbacks

        #region Helpers
        //notify the view that a new record was created...
        //allows us to set focus to key field...
        private void NotifyNewRecordCreated()
        {
            Notify(NewRecordCreatedNotice, new NotificationEventArgs());
        }
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

        private void NotifyTypeSearch(string message)
        {
            Notify(TypeSearchNotice, new NotificationEventArgs(message));
        }

        //Notify view new record may be required...
        private void NotifyNewRecordNeeded(string message)
        {
            Notify(NewRecordNeededNotice, new NotificationEventArgs<bool, MessageBoxResult>
            (message, true, result => { OnNewRecordNeededResult(result); }));
        }

        private void OnNewRecordNeededResult(MessageBoxResult result)
        {
            switch (result)
            {
                case MessageBoxResult.No:
                    //revert back...
                    SelectedCompany.CompanyID = SelectedCompanyMirror.CompanyID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with CompanyID provided...
                    NewCompanyCommand(SelectedCompany.CompanyID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedCompany.CompanyID = SelectedCompanyMirror.CompanyID;
                    break;
            }
        }

        //Notify view save may be required...
        private void NotifySaveRequired(string message, _saveRequiredResultActions resultAction)
        {
            Notify(SaveRequiredNotice, new NotificationEventArgs<bool, MessageBoxResult>
            (message, true, result => { OnSaveResult(result, resultAction); }));           
        }

        private void OnSaveResult(MessageBoxResult result, _saveRequiredResultActions resultAction)
        {
            switch (result)
            {
                case MessageBoxResult.No:
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Yes:
                    //note a commit validation was allready done...
                    _serviceAgent.CommitCompanyRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedCompany.CompanyID = SelectedCompanyMirror.CompanyID;
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

namespace ExtensionMethods
{
    using System.Runtime.Serialization.Formatters.Binary; using System.IO;

    public static partial class XERPExtensions
    {
        public static object GetPropertyValue(this Company myObj, string propertyName)
        {
            var propInfo = typeof(Company).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyType(this Company myObj, string propertyName)
        {
            var propInfo = typeof(Company).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this Company myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(Company).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}