using System;
using System.Windows;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;
using System.Collections.Generic;
// Toolkit namespace
using SimpleMvvmToolkit;
//XERP Namespaces
using XERP.Domain.SystemUserDomain.Services;
using XERP.Domain.SystemUserDomain.SystemUserDataService;
//required for extension methods...
using ExtensionMethods;
using XERP.Client.Models;

namespace XERP.Client.WPF.SystemUserMaintenance.ViewModels
{
    public class TypeMaintenanceViewModel : ViewModelBase<TypeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newSystemUserTypeAutoId;

        private ISystemUserServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public TypeMaintenanceViewModel(){}

        public TypeMaintenanceViewModel(ISystemUserServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            SystemUserTypeList = new BindingList<SystemUserType>();
            //disable new row feature...
            SystemUserTypeList.AllowNew = false;

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

        private string _systemUserTypeListCount;
        public string SystemUserTypeListCount
        {
            get { return _systemUserTypeListCount; }
            set
            {
                _systemUserTypeListCount = value;
                NotifyPropertyChanged(m => m.SystemUserTypeListCount);
            }
        }

        private BindingList<SystemUserType> _systemUserTypeList;
        public BindingList<SystemUserType> SystemUserTypeList
        {
            get
            {
                SystemUserTypeListCount = _systemUserTypeList.Count.ToString();
                if (_systemUserTypeList.Count > 0)
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
                return _systemUserTypeList;
            }
            set
            {
                _systemUserTypeList = value;
                NotifyPropertyChanged(m => m.SystemUserTypeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private SystemUserType _selectedSystemUserTypeMirror;
        public SystemUserType SelectedSystemUserTypeMirror
        {
            get { return _selectedSystemUserTypeMirror; }
            set { _selectedSystemUserTypeMirror = value; }
        }

        private System.Collections.IList _selectedSystemUserTypeList;
        public System.Collections.IList SelectedSystemUserTypeList
        {
            get { return _selectedSystemUserTypeList; }
            set
            {
                if (_selectedSystemUserType != value)
                {
                    _selectedSystemUserTypeList = value;
                    NotifyPropertyChanged(m => m.SelectedSystemUserTypeList);
                }
            }
        }

        private SystemUserType _selectedSystemUserType;
        public SystemUserType SelectedSystemUserType
        {
            get
            {
                return _selectedSystemUserType;
            }
            set
            {
                if (_selectedSystemUserType != value)
                {
                    _selectedSystemUserType = value;
                    //set the mirrored SelectedSystemUserType to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedSystemUserTypeMirror = new SystemUserType();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedSystemUserType.GetType().GetProperties())
                        {
                            SelectedSystemUserTypeMirror.SetPropertyValue(prop.Name, SelectedSystemUserType.GetPropertyValue(prop.Name));
                        }
                        SelectedSystemUserTypeMirror.SystemUserTypeID = _selectedSystemUserType.SystemUserTypeID;
                        NotifyPropertyChanged(m => m.SelectedSystemUserType);

                        SelectedSystemUserType.PropertyChanged += new PropertyChangedEventHandler(SelectedSystemUserType_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _systemUserTypeColumnMetaDataList;
        public List<ColumnMetaData> SystemUserTypeColumnMetaDataList
        {
            get { return _systemUserTypeColumnMetaDataList; }
            set
            {
                _systemUserTypeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.SystemUserTypeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _systemUserTypeMaxFieldValueDictionary;
        public Dictionary<string, int> SystemUserTypeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_systemUserTypeMaxFieldValueDictionary != null)
                    return _systemUserTypeMaxFieldValueDictionary;

                _systemUserTypeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("SystemUserTypes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _systemUserTypeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);

                }
                return _systemUserTypeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedSystemUserType_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "SystemUserTypeID")
            {//make sure it is has changed...
                if (SelectedSystemUserTypeMirror.SystemUserTypeID != SelectedSystemUserType.SystemUserTypeID)
                {//if their are no records it is a key change
                    if (SystemUserTypeList != null && SystemUserTypeList.Count == 0
                        && SelectedSystemUserType != null && !string.IsNullOrEmpty(SelectedSystemUserType.SystemUserTypeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetSystemUserTypeState(SelectedSystemUserType);

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

            object propertyChangedValue = SelectedSystemUserType.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedSystemUserTypeMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedSystemUserType.GetPropertyType(e.PropertyName);
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
                if (SystemUserTypePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedSystemUserType);
                    //set the mirrored objects field...
                    SelectedSystemUserTypeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedSystemUserTypeMirror.IsValid = SelectedSystemUserType.IsValid;
                    SelectedSystemUserTypeMirror.IsExpanded = SelectedSystemUserType.IsExpanded;
                    SelectedSystemUserTypeMirror.NotValidMessage = SelectedSystemUserType.NotValidMessage;

                }
                else//revert back to its previous value... 
                {
                    SelectedSystemUserType.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedSystemUserType.IsValid = SelectedSystemUserTypeMirror.IsValid;
                    SelectedSystemUserType.IsExpanded = SelectedSystemUserTypeMirror.IsExpanded;
                    SelectedSystemUserType.NotValidMessage = SelectedSystemUserTypeMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedSystemUserType.SystemUserTypeID))
            {//check to see if key is part of the current companylist...
                SystemUserType query = SystemUserTypeList.Where(company => company.SystemUserTypeID == SelectedSystemUserType.SystemUserTypeID &&
                                                        company.AutoID != SelectedSystemUserType.AutoID).SingleOrDefault();
                if (query != null)
                {//revert it back...
                    SelectedSystemUserType.SystemUserTypeID = SelectedSystemUserTypeMirror.SystemUserTypeID;
                    //change to the newly selected company...
                    SelectedSystemUserType = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                SystemUserTypeList = GetSystemUserTypeByID(SelectedSystemUserType.SystemUserTypeID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (SystemUserTypeList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedSystemUserType.SystemUserTypeID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedSystemUserType = SystemUserTypeList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedSystemUserType.SystemUserTypeID != SelectedSystemUserTypeMirror.SystemUserTypeID)
                    SelectedSystemUserType.SystemUserTypeID = SelectedSystemUserTypeMirror.SystemUserTypeID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in SystemUserTypeList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedSystemUserType = new SystemUserType();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            SystemUserTypeList.Clear();
            SetAsEmptySelection();
        }

        private bool SystemUserTypePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "SystemUserTypeID":
                    rBool = SystemUserTypeIsValid(SelectedSystemUserType, _companyValidationProperties.SystemUserTypeID, out errorMessage);
                    break;
                case "Name":
                    rBool = SystemUserTypeIsValid(SelectedSystemUserType, _companyValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedSystemUserType.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedSystemUserType.IsValid = SystemUserTypeIsValid(SelectedSystemUserType, out errorMessage);
                if (SelectedSystemUserType.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedSystemUserType.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _companyValidationProperties
        {//we list all fields that require validation...
            SystemUserTypeID,
            Name
        }

        //Object.Property Scope Validation...
        private bool SystemUserTypeIsValid(SystemUserType item, _companyValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _companyValidationProperties.SystemUserTypeID:
                    //validate key
                    if (string.IsNullOrEmpty(item.SystemUserTypeID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetSystemUserTypeState(item);
                    if (entityState == EntityStates.Added && SystemUserTypeExists(item.SystemUserTypeID, ClientSessionSingleton.Instance.CompanyID))
                    {
                        errorMessage = "Item AllReady Exists...";
                        return false;
                    }
                    break;
                case _companyValidationProperties.Name:
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
        //SystemUserType Object Scope Validation check the entire object for validity...
        private byte SystemUserTypeIsValid(SystemUserType item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.SystemUserTypeID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetSystemUserTypeState(item);
            if (entityState == EntityStates.Added && SystemUserTypeExists(item.SystemUserTypeID, ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "Item AllReady Exists.";
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

        private EntityStates GetSystemUserTypeState(SystemUserType itemType)
        {
            return _serviceAgent.GetSystemUserTypeEntityState(itemType);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.SystemUserTypeRepositoryIsDirty();
        }

        #region SystemUserType CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedSystemUserType.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (SystemUserType itemType in SystemUserTypeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (itemType.AutoID > 0)
                    autoIDs = autoIDs + itemType.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                SystemUserTypeList = new BindingList<SystemUserType>(_serviceAgent.RefreshSystemUserType(autoIDs).ToList());
                SelectedSystemUserType = (from q in SystemUserTypeList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<SystemUserType> GetSystemUserTypes(string companyID)
        {
            BindingList<SystemUserType> itemTypeList = new BindingList<SystemUserType>(_serviceAgent.GetSystemUserTypes().ToList());
            Dirty = false;
            AllowCommit = false;
            return itemTypeList;
        }

        private BindingList<SystemUserType> GetSystemUserTypes(SystemUserType itemType, string companyID)
        {
            BindingList<SystemUserType> itemTypeList = new BindingList<SystemUserType>(_serviceAgent.GetSystemUserTypes(itemType).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemTypeList;
        }

        private BindingList<SystemUserType> GetSystemUserTypeByID(string itemTypeID, string companyID)
        {
            BindingList<SystemUserType> itemTypeList = new BindingList<SystemUserType>(_serviceAgent.GetSystemUserTypeByID(itemTypeID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemTypeList;
        }

        private bool SystemUserTypeExists(string itemTypeID, string companyID)
        {
            return _serviceAgent.SystemUserTypeExists(itemTypeID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(SystemUserType item)
        {
            _serviceAgent.UpdateSystemUserTypeRepository(item);
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
            var items = (from q in SystemUserTypeList where q.IsValid == 2 select q).ToList();
            foreach (SystemUserType item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitSystemUserTypeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }


        private bool Delete(SystemUserType itemType)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromSystemUserTypeRepository(itemType);
            return true;
        }

        private bool NewSystemUserType(string id)
        {
            SystemUserType item = new SystemUserType();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newSystemUserTypeAutoId = _newSystemUserTypeAutoId - 1;
            item.AutoID = _newSystemUserTypeAutoId;
            item.SystemUserTypeID = id;
            item.IsValid = 1;
            item.NotValidMessage = "New Record Key Field/s Are Required.";
            SystemUserTypeList.Add(item);
            _serviceAgent.AddToSystemUserTypeRepository(item);
            SelectedSystemUserType = SystemUserTypeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }


        #endregion SystemUserType CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                SystemUserTypeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewSystemUserTypeCommand(""); //this will generate a new itemType and set it as the selected itemType...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedSystemUserType.SetPropertyValue(SystemUserTypeColumnMetaDataList[i].Name, columnValue);
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
            if (GetSystemUserTypeState(SelectedSystemUserType) != EntityStates.Detached)
            {
                if (Update(SelectedSystemUserType))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }
        public void RefreshCommand()
        {
            Refresh();
        }
        public void DeleteSystemUserTypeCommand()
        {
            try
            {//company is fk to 100's of tables deleting it can be tricky...
                int i = 0;
                int ii = 0;
                for (int j = SelectedSystemUserTypeList.Count - 1; j >= 0; j--)
                {
                    SystemUserType item = (SystemUserType)SelectedSystemUserTypeList[j];
                    //get Max Index...
                    i = SystemUserTypeList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    SystemUserTypeList.Remove(item);
                }

                if (SystemUserTypeList != null && SystemUserTypeList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= SystemUserTypeList.Count())
                        ii = SystemUserTypeList.Count - 1;

                    SelectedSystemUserType = SystemUserTypeList[ii];
                    //we will only enable committ for dirty validated records...
                    if (Dirty == true)
                        AllowCommit = CommitIsAllowed();
                    else
                        AllowCommit = false;
                }
                else//only one record, deleting will result in no records...
                    SetAsEmptySelection();
            }//we try catch company delete as it may be used in another table as a key...
            //As well we will force a refresh to sqare up the UI after the botched delete...
            catch
            {
                NotifyMessage("SystemUserType/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }


        public void NewSystemUserTypeCommand()
        {
            NewSystemUserType("");
            AllowCommit = false;
        }

        public void NewSystemUserTypeCommand(string itemTypeID)
        {
            NewSystemUserType(itemTypeID);
            if (string.IsNullOrEmpty(itemTypeID))//don't allow a save until a TypeID is provided...
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
            RegisterToReceiveMessages<BindingList<SystemUserType>>(MessageTokens.SystemUserTypeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<SystemUserType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SystemUserTypeList = e.Data;
                SelectedSystemUserType = SystemUserTypeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<SystemUserType>>(MessageTokens.SystemUserTypeSearchToken.ToString(), OnSearchResult);
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
                    SelectedSystemUserType.SystemUserTypeID = SelectedSystemUserTypeMirror.SystemUserTypeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with SystemUserTypeID provided...
                    NewSystemUserTypeCommand(SelectedSystemUserType.SystemUserTypeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSystemUserType.SystemUserTypeID = SelectedSystemUserTypeMirror.SystemUserTypeID;
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
                    _serviceAgent.CommitSystemUserTypeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSystemUserType.SystemUserTypeID = SelectedSystemUserTypeMirror.SystemUserTypeID;
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
        public static object GetPropertyValue(this SystemUserType myObj, string propertyName)
        {
            var propInfo = typeof(SystemUserType).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this SystemUserType myObj, string propertyName)
        {
            var propInfo = typeof(SystemUserType).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this SystemUserType myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(SystemUserType).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}