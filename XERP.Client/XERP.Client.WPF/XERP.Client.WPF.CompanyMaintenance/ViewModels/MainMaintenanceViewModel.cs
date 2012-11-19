using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using ExtensionMethods;
using SimpleMvvmToolkit;
using XERP.Client.Models;
using XERP.Domain.CompanyDomain.CompanyDataService;
using XERP.Domain.CompanyDomain.Services;

namespace XERP.Client.WPF.CompanyMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newCompanyAutoId;

        private ICompanyServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public MainMaintenanceViewModel(){}

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
            if (XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)//make sure user has rights to UI...
                DoFormsAuthentication();
            else
            {//User is not authenticated...
                RegisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
                FormIsEnabled = false;
            }

            AllowNew = true;
            AllowRowPaste = true;
        }
        #endregion Initialization and Cleanup

        #region Authentication Logic
        private void DoFormsAuthentication()
        {//we need to make sure the system user is allowed access to this UI...
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
        public event EventHandler<NotificationEventArgs> CodeSearchNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> SaveRequiredNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> NewRecordNeededNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
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
                {//no records
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
                if (_companyMaxFieldValueDictionary != null)
                    return _companyMaxFieldValueDictionary;

                _companyMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("Companies");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _companyMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _companyMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedCompany_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {//these properties are not to be persisted or autochanged fields we will igore them...
            if (e.PropertyName == "IsSelected" ||
                e.PropertyName == "IsExpanded" ||
                e.PropertyName == "IsValid" ||
                e.PropertyName == "NotValidMessage" ||
                e.PropertyName == "LastModifiedBy" ||
                e.PropertyName == "LastModifiedByDate")
            {
                return;
            }
            //Key ID Logic...
            if (e.PropertyName == "CompanyID")
            {//make sure it is has changed...
                if (SelectedCompanyMirror.CompanyID != SelectedCompany.CompanyID)
                { //if their are no records it is a key change
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
                        if (Dirty && AllowCommit)//dirty record exists ask if save is required...
                            NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ChangeKeyLogic);
                        else
                            ChangeKeyLogic();

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
                if (prevPropertyValue == null)//both values are null
                    objectsAreEqual = true;
                else//only one value is null
                    objectsAreEqual = false;
            }
            else
            {
                if (prevPropertyValue == null)//only one value is null
                    objectsAreEqual = false;
                else //both values are not null use .Equals...
                    objectsAreEqual = propertyChangedValue.Equals(prevPropertyValue);
            }
            if (!objectsAreEqual)
            {
                //Here we do property change validation if false is returned we will reset the value
                //Back to its mirrored value and return out of the property change w/o updating the repository...
                if (CompanyPropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedCompany);
                    SelectedCompanyMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedCompanyMirror.IsValid = SelectedCompany.IsValid;
                    SelectedCompanyMirror.IsExpanded = SelectedCompany.IsExpanded;
                    SelectedCompanyMirror.NotValidMessage = SelectedCompany.NotValidMessage;
                }
                else
                {//revert back to its previous value... 
                    SelectedCompany.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedCompany.IsValid = SelectedCompanyMirror.IsValid;
                    SelectedCompany.IsExpanded = SelectedCompanyMirror.IsExpanded;
                    SelectedCompany.NotValidMessage = SelectedCompanyMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods

        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedCompany.CompanyID))
            {//check to see if key is part of the current List...
                Company query = CompanyList.Where(item => item.CompanyID == SelectedCompany.CompanyID &&
                                                        item.AutoID != SelectedCompany.AutoID).SingleOrDefault();
                if (query != null)
                {
                    //revert it back...
                    SelectedCompany.CompanyID = SelectedCompanyMirror.CompanyID;
                    //change to the newly selected item...
                    SelectedCompany = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                CompanyList = GetCompanyByID(SelectedCompany.CompanyID);
                if (CompanyList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedCompany.CompanyID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedCompany = CompanyList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedCompany.CompanyID != SelectedCompanyMirror.CompanyID)
                    SelectedCompany.CompanyID = SelectedCompanyMirror.CompanyID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in CompanyList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

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
        private bool CompanyPropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "CompanyID":
                    rBool = CompanyIsValid(SelectedCompany, _itemValidationProperties.CompanyID, out errorMessage);
                    break;
                case "Name":
                    rBool = CompanyIsValid(SelectedCompany, _itemValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedCompany.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedCompany.IsValid = CompanyIsValid(SelectedCompany, out errorMessage);
                if (SelectedCompany.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedCompany.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _itemValidationProperties
        {//we list all fields that require validation...
            CompanyID,
            Name
        }

        //Object.Property Scope Validation...
        private bool CompanyIsValid(Company item, _itemValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _itemValidationProperties.CompanyID:
                    //validate key
                    if (string.IsNullOrEmpty(item.CompanyID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetCompanyState(item);
                    if (entityState == EntityStates.Added && CompanyExists(item.CompanyID))
                    {
                        errorMessage = "Item AllReady Exists...";
                        return false;
                    }
                    break;
                case _itemValidationProperties.Name:
                    //validate Description
                    if (string.IsNullOrEmpty(item.Name))
                    {
                        errorMessage = "Description Is Required.";
                        return false;
                    }
                    break;
            }
            return true;
        }
        //Company Object Scope Validation check the entire object for validity...
        private byte CompanyIsValid(Company item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.CompanyID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetCompanyState(item);
            if (entityState == EntityStates.Added && CompanyExists(item.CompanyID))
            {
                errorMessage = "Item AllReady Exists.";
                return 1;
            }

            //validate Description
            if (string.IsNullOrEmpty(item.Name))
            {
                errorMessage = "Name Is Required.";
                return 1;
            }
            //a value of 2 is pending changes...
            //On Commit we will give it a value of 0...
            return 2;
        }
        #endregion Validation Methods
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

        private EntityStates GetCompanyState(Company item)
        {
            return _serviceAgent.GetCompanyEntityState(item);
        }

        //check to see if the repository has pending changes...
        private bool RepositoryIsDirty()
        {
            return _serviceAgent.CompanyRepositoryIsDirty();
        }

        #region Company CRUD
        private void Refresh()
        {   //refetch current records...
            long selectedAutoID = SelectedCompany.AutoID;
            string autoIDs = "";
            foreach (Company item in CompanyList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {//ditch the extra comma...
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
            BindingList<Company> itemList = new BindingList<Company>(_serviceAgent.GetCompanies().ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<Company> GetCompanies(Company item)
        {
            BindingList<Company> itemList = new BindingList<Company>(_serviceAgent.GetCompanies(item).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<Company> GetCompanyByID(string id)
        {
            BindingList<Company> itemList = new BindingList<Company>(_serviceAgent.GetCompanyByID(id).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private bool CompanyExists(string itemID)
        {
            return _serviceAgent.CompanyExists(itemID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(Company item)
        {
            _serviceAgent.UpdateCompanyRepository(item);
            Dirty = true;
            if (CommitIsAllowed())
                AllowCommit = true;
            else
                AllowCommit = false;
            return AllowCommit;
        }
        //commits repository to the db...
        private bool Commit()
        {   //search non respository UI list for pending saved marked records and mark them as valid...
            var items = (from q in CompanyList where q.IsValid == 2 select q).ToList();
            foreach(Company item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitCompanyRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(Company item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromCompanyRepository(item);
            return true;
        }

        private bool NewCompany(string itemID)
        {
            Company item = new Company();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newCompanyAutoId = _newCompanyAutoId - 1;
            item.AutoID = _newCompanyAutoId;
            item.CompanyID = itemID;
            item.IsValid = 1;
            item.NotValidMessage = "New Record Key Field/s Are Required.";
            CompanyList.Add(item);
            _serviceAgent.AddToCompanyRepository(item);
            SelectedCompany = CompanyList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }
        #endregion Company CRUD
        #endregion ServiceAgent Call Methods
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
                    NewCompanyCommand(); //this will generate a new item and set it as the selected item...
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
            catch (Exception ex)
            {
                NotifyMessage(ex.InnerException.ToString());
            }
        }

        public void SaveCommand()
        {   
            if (GetCompanyState(SelectedCompany) != EntityStates.Detached)
            {
                if (Update(SelectedCompany))
                    Commit();
                else
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }
        public void RefreshCommand()
        {
            Refresh();
        }
        public void DeleteCompanyCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedCompanyList.Count - 1; j >= 0; j--)
                {
                    Company item = (Company)SelectedCompanyList[j];
                    //get Max Index...
                    i = CompanyList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    CompanyList.Remove(item);
                }

                if (CompanyList != null && CompanyList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= CompanyList.Count())
                        ii = CompanyList.Count - 1;

                    SelectedCompany = CompanyList[ii];
                    //we will only enable committ for dirty validated records...
                    if (Dirty == true)
                        AllowCommit = CommitIsAllowed();
                    else
                        AllowCommit = false;
                }
                else//only one record, deleting will result in no records...
                    SetAsEmptySelection();
            }//we try catch item delete as it may be used in another table as a key...
            //As well we will force a refresh to sqare up the UI after the botched delete...
            catch
            {
                NotifyMessage("Company/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewCompanyCommand()
        {
            NewCompany("");
            AllowCommit = false;
        }

        public void NewCompanyCommand(string itemID)
        {
            NewCompany(itemID);
            if (string.IsNullOrEmpty(itemID))
                AllowCommit = false;
            else
                AllowCommit = CommitIsAllowed();
        }

        public void ClearCommand()
        {
            if (Dirty && AllowCommit)
                NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ClearLogic);
            else
                ClearLogic();
        }

        public void SearchCommand()
        {
            if (Dirty && AllowCommit)
                NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.SearchLogic);
            else
                SearchLogic();
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
                SelectedCompany.CompanyTypeID = e.Data.FirstOrDefault().CompanyTypeID;

            UnregisterToReceiveMessages<BindingList<CompanyType>>(MessageTokens.CompanyTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<CompanyCode>>(MessageTokens.CompanyCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<CompanyCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedCompany.CompanyCodeID = e.Data.FirstOrDefault().CompanyCodeID;

            UnregisterToReceiveMessages<BindingList<CompanyType>>(MessageTokens.CompanyTypeSearchToken.ToString(), OnTypeSearchResult);
        }
        #endregion Commands

        #region Helpers
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {// Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
        private void NotifyMessage(string message)
        {// Notify view of an error message w/o throwing an error...
            Notify(MessageNotice, new NotificationEventArgs<Exception>(message));
        }
        
        private void NotifySearch(string message)
        {
            Notify(SearchNotice, new NotificationEventArgs(message));
        }

        private void NotifyTypeSearch(string message)
        {
            Notify(TypeSearchNotice, new NotificationEventArgs(message));
        }

        private void NotifyCodeSearch(string message)
        {
            Notify(CodeSearchNotice, new NotificationEventArgs(message));
        }

        private void NotifyNewRecordNeeded(string message)
        {//Notify view new record may be required...
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
            switch (resultAction)
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
    public static partial class XERPExtensions
    {
        public static object GetPropertyValue(this Company myObj, string propertyName)
        {
            var propInfo = typeof(Company).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this Company myObj, string propertyName)
        {
            var propInfo = typeof(Company).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this Company myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(Company).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}