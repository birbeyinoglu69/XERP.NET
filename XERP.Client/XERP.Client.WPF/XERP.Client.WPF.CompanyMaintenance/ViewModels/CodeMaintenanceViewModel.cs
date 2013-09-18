using System;
using System.Collections.Generic;
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
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newCompanyCodeAutoId;

        private ICompanyServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public CodeMaintenanceViewModel(){}

        public CodeMaintenanceViewModel(ICompanyServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            CompanyCodeList = new BindingList<CompanyCode>();
            //disable new row feature...
            CompanyCodeList.AllowNew = false;

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
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> NewRecordNeededNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        #endregion Notifications

        #region Properties
        #region General Form Function/State Properties
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

        private string _companyCodeListCount;
        public string CompanyCodeListCount
        {
            get { return _companyCodeListCount; }
            set
            {
                _companyCodeListCount = value;
                NotifyPropertyChanged(m => m.CompanyCodeListCount);
            }
        }
        #endregion General Form Function/State Properties

        private BindingList<CompanyCode> _companyCodeList;
        public BindingList<CompanyCode> CompanyCodeList
        {
            get
            {
                CompanyCodeListCount = _companyCodeList.Count.ToString();
                if (_companyCodeList.Count > 0)
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
                return _companyCodeList;
            }
            set
            {
                _companyCodeList = value;
                NotifyPropertyChanged(m => m.CompanyCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private CompanyCode _selectedCompanyCodeMirror;
        public CompanyCode SelectedCompanyCodeMirror
        {
            get { return _selectedCompanyCodeMirror; }
            set { _selectedCompanyCodeMirror = value; }
        }

        private System.Collections.IList _selectedCompanyCodeList;
        public System.Collections.IList SelectedCompanyCodeList
        {
            get { return _selectedCompanyCodeList; }
            set
            {
                if (_selectedCompanyCode != value)
                {
                    _selectedCompanyCodeList = value;
                    NotifyPropertyChanged(m => m.SelectedCompanyCodeList);
                }
            }
        }

        private CompanyCode _selectedCompanyCode;
        public CompanyCode SelectedCompanyCode
        {
            get
            {
                return _selectedCompanyCode;
            }
            set
            {
                if (_selectedCompanyCode != value)
                {
                    _selectedCompanyCode = value;
                    //set the mirrored SelectedCompanyCode to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedCompanyCodeMirror = new CompanyCode();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedCompanyCode.GetType().GetProperties())
                        {
                            SelectedCompanyCodeMirror.SetPropertyValue(prop.Name, SelectedCompanyCode.GetPropertyValue(prop.Name));
                        }
                        SelectedCompanyCodeMirror.CompanyCodeID = _selectedCompanyCode.CompanyCodeID;
                        NotifyPropertyChanged(m => m.SelectedCompanyCode);

                        SelectedCompanyCode.PropertyChanged += new PropertyChangedEventHandler(SelectedCompanyCode_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _companyCodeColumnMetaDataList;
        public List<ColumnMetaData> CompanyCodeColumnMetaDataList
        {
            get { return _companyCodeColumnMetaDataList; }
            set
            {
                _companyCodeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.CompanyCodeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _companyCodeMaxFieldValueDictionary;
        public Dictionary<string, int> CompanyCodeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_companyCodeMaxFieldValueDictionary != null)
                    return _companyCodeMaxFieldValueDictionary;

                _companyCodeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("CompanyCodes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _companyCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _companyCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedCompanyCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {//these properties are not to be persisted we will igore them...
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
            if (e.PropertyName == "CompanyCodeID")
            {//make sure it is has changed...
                if (SelectedCompanyCodeMirror.CompanyCodeID != SelectedCompanyCode.CompanyCodeID)
                {//if their are no records it is a key change
                    if (CompanyCodeList != null && CompanyCodeList.Count == 0
                        && SelectedCompanyCode != null && !string.IsNullOrEmpty(SelectedCompanyCode.CompanyCodeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetCompanyCodeState(SelectedCompanyCode);

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

            object propertyChangedValue = SelectedCompanyCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedCompanyCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedCompanyCode.GetPropertyType(e.PropertyName);
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
            {//Here we do property change validation if false is returned we will reset the value
                //Back to its mirrored value and return out of the property change w/o updating the repository...
                if (CompanyCodePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedCompanyCode);
                    //set the mirrored objects field...
                    SelectedCompanyCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedCompanyCodeMirror.IsValid = SelectedCompanyCode.IsValid;
                    SelectedCompanyCodeMirror.IsExpanded = SelectedCompanyCode.IsExpanded;
                    SelectedCompanyCodeMirror.NotValidMessage = SelectedCompanyCode.NotValidMessage;
                }
                else//revert back to its previous value... 
                {
                    SelectedCompanyCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedCompanyCode.IsValid = SelectedCompanyCodeMirror.IsValid;
                    SelectedCompanyCode.IsExpanded = SelectedCompanyCodeMirror.IsExpanded;
                    SelectedCompanyCode.NotValidMessage = SelectedCompanyCodeMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedCompanyCode.CompanyCodeID))
            {//check to see if key is part of the current list...
                CompanyCode query = CompanyCodeList.Where(item => item.CompanyCodeID == SelectedCompanyCode.CompanyCodeID &&
                                                        item.AutoID != SelectedCompanyCode.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back
                    SelectedCompanyCode.CompanyCodeID = SelectedCompanyCodeMirror.CompanyCodeID;
                    //change to the newly selected item...
                    SelectedCompanyCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                CompanyCodeList = GetCompanyCodeByID(SelectedCompanyCode.CompanyCodeID);
                if (CompanyCodeList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedCompanyCode.CompanyCodeID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedCompanyCode = CompanyCodeList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedCompanyCode.CompanyCodeID != SelectedCompanyCodeMirror.CompanyCodeID)
                    SelectedCompanyCode.CompanyCodeID = SelectedCompanyCodeMirror.CompanyCodeID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in CompanyCodeList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedCompanyCode = new CompanyCode();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            CompanyCodeList.Clear();
            SetAsEmptySelection();
        }

        private bool CompanyCodePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "CompanyCodeID":
                    rBool = CompanyCodeIsValid(SelectedCompanyCode, _itemValidationProperties.CompanyCodeID, out errorMessage);
                    break;
                case "Name":
                    rBool = CompanyCodeIsValid(SelectedCompanyCode, _itemValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedCompanyCode.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedCompanyCode.IsValid = CompanyCodeIsValid(SelectedCompanyCode, out errorMessage);
                if (SelectedCompanyCode.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedCompanyCode.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _itemValidationProperties
        {//we list all fields that require validation...
            CompanyCodeID,
            Name
        }

        //Object.Property Scope Validation...
        private bool CompanyCodeIsValid(CompanyCode item, _itemValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _itemValidationProperties.CompanyCodeID:
                    //validate key
                    if (string.IsNullOrEmpty(item.CompanyCodeID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetCompanyCodeState(item);
                    if (entityState == EntityStates.Added && CompanyCodeExists(item.CompanyCodeID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = CompanyCodeList.Count(q => q.CompanyCodeID == item.CompanyCodeID);
                    if (count > 1)
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    break;
                case _itemValidationProperties.Name:
                    //validate Description
                    if (string.IsNullOrEmpty(item.Description))
                    {
                        errorMessage = "Description Is Required.";
                        return false;
                    }
                    break;
            }
            return true;
        }
        //CompanyCode Object Scope Validation check the entire object for validity...
        private byte CompanyCodeIsValid(CompanyCode item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.CompanyCodeID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetCompanyCodeState(item);
            if (entityState == EntityStates.Added && CompanyCodeExists(item.CompanyCodeID))
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = CompanyCodeList.Count(q => q.CompanyCodeID == item.CompanyCodeID);
            if (count > 1)
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //validate Description
            if (string.IsNullOrEmpty(item.Description))
            {
                errorMessage = "Description Is Required.";
                return 1;
            }
            //a value of 2 is pending changes...
            //On Commit we will give it a value of 0...
            return 2;
        }
        #endregion Validation Methods
        #endregion ViewModel Logic Methods
        #region ServiceAgent Call Methods
        private EntityStates GetCompanyCodeState(CompanyCode item)
        {
            return _serviceAgent.GetCompanyCodeEntityState(item);
        }

        //check to see if the repository has pending changes...
        private bool RepositoryIsDirty()
        {
            return _serviceAgent.CompanyRepositoryIsDirty();
        }

        #region CompanyCode CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedCompanyCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (CompanyCode item in CompanyCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                CompanyCodeList = new BindingList<CompanyCode>(_serviceAgent.RefreshCompanyCode(autoIDs).ToList());
                SelectedCompanyCode = (from q in CompanyCodeList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<CompanyCode> GetCompanyCodes()
        {
            BindingList<CompanyCode> itemList = new BindingList<CompanyCode>(_serviceAgent.GetCompanyCodes().ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<CompanyCode> GetCompanyCodes(CompanyCode item)
        {
            BindingList<CompanyCode> itemList = new BindingList<CompanyCode>(_serviceAgent.GetCompanyCodes(item).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<CompanyCode> GetCompanyCodeByID(string id)
        {
            BindingList<CompanyCode> itemList = new BindingList<CompanyCode>(_serviceAgent.GetCompanyCodeByID(id).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private bool CompanyCodeExists(string itemID)
        {
            return _serviceAgent.CompanyCodeExists(itemID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(CompanyCode item)
        {
            _serviceAgent.UpdateCompanyCodeRepository(item);
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
            var items = (from q in CompanyCodeList where q.IsValid == 2 select q).ToList();
            foreach(CompanyCode item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitCompanyCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(CompanyCode item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromCompanyCodeRepository(item);
            return true;
        }

        private bool NewCompanyCode(string itemID)
        {
            CompanyCode newItem = new CompanyCode();
            _newCompanyCodeAutoId = _newCompanyCodeAutoId - 1;
            newItem.AutoID = _newCompanyCodeAutoId;
            newItem.CompanyCodeID = itemID;
            newItem.IsValid = 1;
            newItem.NotValidMessage = "New Record Key Field/s Are Required.";
            CompanyCodeList.Add(newItem);
            _serviceAgent.AddToCompanyCodeRepository(newItem);
            SelectedCompanyCode = CompanyCodeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion CompanyCode CRUD
        #endregion ServiceAgent Call Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                CompanyCodeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewCompanyCodeCommand(""); //this will generate a new item and set it as the selected item Code...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedCompanyCode.SetPropertyValue(CompanyCodeColumnMetaDataList[i].Name, columnValue);
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
            if (GetCompanyCodeState(SelectedCompanyCode) != EntityStates.Detached)
            {
                if (Update(SelectedCompanyCode))
                    Commit();
                else
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }
        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteCompanyCodeCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedCompanyCodeList.Count - 1; j >= 0; j--)
                {
                    CompanyCode item = (CompanyCode)SelectedCompanyCodeList[j];
                    //get Max Index...
                    i = CompanyCodeList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    CompanyCodeList.Remove(item);
                }

                if (CompanyCodeList != null && CompanyCodeList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= CompanyCodeList.Count())
                        ii = CompanyCodeList.Count - 1;

                    SelectedCompanyCode = CompanyCodeList[ii];
                    //we will only enable committ for dirty validated records...
                    if (Dirty == true)
                        AllowCommit = CommitIsAllowed();
                    else
                        AllowCommit = false;
                }
                else//only one record, deleting will result in no records...
                    SetAsEmptySelection();
            }//we try catch the item to delete as it may be used in another table as a key...
            //As well we will force a refresh to sqare up the UI after the botched delete...
            catch
            {
                NotifyMessage("CompanyCode/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewCompanyCodeCommand()
        {
            NewCompanyCode("");
            AllowCommit = false;
        }

        public void NewCompanyCodeCommand(string newItemID)
        {
            NewCompanyCode(newItemID);
            if (string.IsNullOrEmpty(newItemID))//don't allow a save until a securityGroupCodeID is provided...
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
            RegisterToReceiveMessages<BindingList<CompanyCode>>(MessageTokens.CompanyCodeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<CompanyCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                CompanyCodeList = e.Data;
                SelectedCompanyCode = CompanyCodeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<CompanyCode>>(MessageTokens.CompanyCodeSearchToken.ToString(), OnSearchResult);
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
        {//Notify view to launch search...
            Notify(SearchNotice, new NotificationEventArgs(message));
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
                    SelectedCompanyCode.CompanyCodeID = SelectedCompanyCodeMirror.CompanyCodeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with CompanyCodeID provided...
                    NewCompanyCodeCommand(SelectedCompanyCode.CompanyCodeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedCompanyCode.CompanyCodeID = SelectedCompanyCodeMirror.CompanyCodeID;
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
                    _serviceAgent.CommitCompanyCodeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedCompanyCode.CompanyCodeID = SelectedCompanyCodeMirror.CompanyCodeID;
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
        public static object GetPropertyValue(this CompanyCode myObj, string propertyName)
        {
            var propInfo = typeof(CompanyCode).GetProperty(propertyName);

            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this CompanyCode myObj, string propertyName)
        {
            var propInfo = typeof(CompanyCode).GetProperty(propertyName);

            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this CompanyCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(CompanyCode).GetProperty((string)propertyName);

            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}