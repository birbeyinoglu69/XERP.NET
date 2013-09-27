using System;
using System.Windows;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;
using System.Collections.Generic;
// Toolkit namespace
using SimpleMvvmToolkit;
//XERP Namespaces
using XERP.Domain.WarehouseDomain.Services;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
//required for extension methods...
using ExtensionMethods;
using XERP.Client.Models;

namespace XERP.Client.WPF.WarehouseMaintenance.ViewModels
{
    public class TypeMaintenanceViewModel : ViewModelBase<TypeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newWarehouseTypeAutoId;

        private IWarehouseServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public TypeMaintenanceViewModel(){}

        public TypeMaintenanceViewModel(IWarehouseServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            WarehouseTypeList = new BindingList<WarehouseType>();
            //disable new row feature...
            WarehouseTypeList.AllowNew = false;

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

        private string _warehouseTypeListCount;
        public string WarehouseTypeListCount
        {
            get { return _warehouseTypeListCount; }
            set
            {
                _warehouseTypeListCount = value;
                NotifyPropertyChanged(m => m.WarehouseTypeListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region Validation Properties
        private List<ColumnMetaData> _warehouseTypeColumnMetaDataList;
        public List<ColumnMetaData> WarehouseTypeColumnMetaDataList
        {
            get { return _warehouseTypeColumnMetaDataList; }
            set
            {
                _warehouseTypeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.WarehouseTypeColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _warehouseTypeMaxFieldValueDictionary;
        public Dictionary<string, int> WarehouseTypeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_warehouseTypeMaxFieldValueDictionary != null)
                    return _warehouseTypeMaxFieldValueDictionary;

                _warehouseTypeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("WarehouseTypes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _warehouseTypeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);

                }
                return _warehouseTypeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties

        #region CRUD Properties
        private BindingList<WarehouseType> _warehouseTypeList;
        public BindingList<WarehouseType> WarehouseTypeList
        {
            get
            {
                WarehouseTypeListCount = _warehouseTypeList.Count.ToString();
                if (_warehouseTypeList.Count > 0)
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
                return _warehouseTypeList;
            }
            set
            {
                _warehouseTypeList = value;
                NotifyPropertyChanged(m => m.WarehouseTypeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private WarehouseType _selectedWarehouseTypeMirror;
        public WarehouseType SelectedWarehouseTypeMirror
        {
            get { return _selectedWarehouseTypeMirror; }
            set { _selectedWarehouseTypeMirror = value; }
        }

        private System.Collections.IList _selectedWarehouseTypeList;
        public System.Collections.IList SelectedWarehouseTypeList
        {
            get { return _selectedWarehouseTypeList; }
            set
            {
                if (_selectedWarehouseType != value)
                {
                    _selectedWarehouseTypeList = value;
                    NotifyPropertyChanged(m => m.SelectedWarehouseTypeList);
                }
            }
        }

        private WarehouseType _selectedWarehouseType;
        public WarehouseType SelectedWarehouseType
        {
            get
            {
                return _selectedWarehouseType;
            }
            set
            {
                if (_selectedWarehouseType != value)
                {
                    _selectedWarehouseType = value;
                    //set the mirrored SelectedWarehouseType to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedWarehouseTypeMirror = new WarehouseType();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedWarehouseType.GetType().GetProperties())
                        {
                            SelectedWarehouseTypeMirror.SetPropertyValue(prop.Name, SelectedWarehouseType.GetPropertyValue(prop.Name));
                        }
                        SelectedWarehouseTypeMirror.WarehouseTypeID = _selectedWarehouseType.WarehouseTypeID;
                        NotifyPropertyChanged(m => m.SelectedWarehouseType);

                        SelectedWarehouseType.PropertyChanged += new PropertyChangedEventHandler(SelectedWarehouseType_PropertyChanged);
                    }
                }
            }
        }
        #endregion CRUD Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedWarehouseType_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "WarehouseTypeID")
            {//make sure it is has changed...
                if (SelectedWarehouseTypeMirror.WarehouseTypeID != SelectedWarehouseType.WarehouseTypeID)
                {//if their are no records it is a key change
                    if (WarehouseTypeList != null && WarehouseTypeList.Count == 0
                        && SelectedWarehouseType != null && !string.IsNullOrEmpty(SelectedWarehouseType.WarehouseTypeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetWarehouseTypeState(SelectedWarehouseType);

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

            object propertyChangedValue = SelectedWarehouseType.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedWarehouseTypeMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedWarehouseType.GetPropertyType(e.PropertyName);
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
                if (WarehouseTypePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedWarehouseType);
                    //set the mirrored objects field...
                    SelectedWarehouseTypeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedWarehouseTypeMirror.IsValid = SelectedWarehouseType.IsValid;
                    SelectedWarehouseTypeMirror.IsExpanded = SelectedWarehouseType.IsExpanded;
                    SelectedWarehouseTypeMirror.NotValidMessage = SelectedWarehouseType.NotValidMessage;

                }
                else//revert back to its previous value... 
                {
                    SelectedWarehouseType.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedWarehouseType.IsValid = SelectedWarehouseTypeMirror.IsValid;
                    SelectedWarehouseType.IsExpanded = SelectedWarehouseTypeMirror.IsExpanded;
                    SelectedWarehouseType.NotValidMessage = SelectedWarehouseTypeMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedWarehouseType.WarehouseTypeID))
            {//check to see if key is part of the current companylist...
                WarehouseType query = WarehouseTypeList.Where(company => company.WarehouseTypeID == SelectedWarehouseType.WarehouseTypeID &&
                                                        company.AutoID != SelectedWarehouseType.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back...
                    SelectedWarehouseType.WarehouseTypeID = SelectedWarehouseTypeMirror.WarehouseTypeID;
                    //change to the newly selected company...
                    SelectedWarehouseType = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                WarehouseTypeList = GetWarehouseTypeByID(SelectedWarehouseType.WarehouseTypeID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (WarehouseTypeList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedWarehouseType.WarehouseTypeID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedWarehouseType = WarehouseTypeList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedWarehouseType.WarehouseTypeID != SelectedWarehouseTypeMirror.WarehouseTypeID)
                    SelectedWarehouseType.WarehouseTypeID = SelectedWarehouseTypeMirror.WarehouseTypeID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in WarehouseTypeList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedWarehouseType = new WarehouseType();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            WarehouseTypeList.Clear();
            SetAsEmptySelection();
        }

        private bool WarehouseTypePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "WarehouseTypeID":
                    rBool = WarehouseTypeIsValid(SelectedWarehouseType, _companyValidationProperties.WarehouseTypeID, out errorMessage);
                    break;
                case "Name":
                    rBool = WarehouseTypeIsValid(SelectedWarehouseType, _companyValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedWarehouseType.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedWarehouseType.IsValid = WarehouseTypeIsValid(SelectedWarehouseType, out errorMessage);
                if (SelectedWarehouseType.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedWarehouseType.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _companyValidationProperties
        {//we list all fields that require validation...
            WarehouseTypeID,
            Name
        }

        //Object.Property Scope Validation...
        private bool WarehouseTypeIsValid(WarehouseType item, _companyValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _companyValidationProperties.WarehouseTypeID:
                    //validate key
                    if (string.IsNullOrEmpty(item.WarehouseTypeID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetWarehouseTypeState(item);
                    if (entityState == EntityStates.Added && WarehouseTypeExists(item.WarehouseTypeID, ClientSessionSingleton.Instance.CompanyID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = WarehouseTypeList.Count(q => q.WarehouseTypeID == item.WarehouseTypeID);
                    if (count > 1)
                    {
                        errorMessage = "Item All Ready Exists...";
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
        //WarehouseType Object Scope Validation check the entire object for validity...
        private byte WarehouseTypeIsValid(WarehouseType item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.WarehouseTypeID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetWarehouseTypeState(item);
            if (entityState == EntityStates.Added && WarehouseTypeExists(item.WarehouseTypeID, ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = WarehouseTypeList.Count(q => q.WarehouseTypeID == item.WarehouseTypeID);
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

        private EntityStates GetWarehouseTypeState(WarehouseType itemType)
        {
            return _serviceAgent.GetWarehouseTypeEntityState(itemType);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.WarehouseTypeRepositoryIsDirty();
        }

        #region WarehouseType CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedWarehouseType.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (WarehouseType itemType in WarehouseTypeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (itemType.AutoID > 0)
                    autoIDs = autoIDs + itemType.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                WarehouseTypeList = new BindingList<WarehouseType>(_serviceAgent.RefreshWarehouseType(autoIDs).ToList());
                SelectedWarehouseType = (from q in WarehouseTypeList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<WarehouseType> GetWarehouseTypes(string companyID)
        {
            BindingList<WarehouseType> itemTypeList = new BindingList<WarehouseType>(_serviceAgent.GetWarehouseTypes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemTypeList;
        }

        private BindingList<WarehouseType> GetWarehouseTypes(WarehouseType itemType, string companyID)
        {
            BindingList<WarehouseType> itemTypeList = new BindingList<WarehouseType>(_serviceAgent.GetWarehouseTypes(itemType, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemTypeList;
        }

        private BindingList<WarehouseType> GetWarehouseTypeByID(string itemTypeID, string companyID)
        {
            BindingList<WarehouseType> itemTypeList = new BindingList<WarehouseType>(_serviceAgent.GetWarehouseTypeByID(itemTypeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemTypeList;
        }

        private bool WarehouseTypeExists(string itemTypeID, string companyID)
        {
            return _serviceAgent.WarehouseTypeExists(itemTypeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(WarehouseType item)
        {
            _serviceAgent.UpdateWarehouseTypeRepository(item);
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
            var items = (from q in WarehouseTypeList where q.IsValid == 2 select q).ToList();
            foreach (WarehouseType item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitWarehouseTypeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(WarehouseType itemType)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromWarehouseTypeRepository(itemType);
            return true;
        }

        private bool NewWarehouseType(string id)
        {
            WarehouseType item = new WarehouseType();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newWarehouseTypeAutoId = _newWarehouseTypeAutoId - 1;
            item.AutoID = _newWarehouseTypeAutoId;
            item.WarehouseTypeID = id;
            item.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            item.IsValid = 1;
            item.NotValidMessage = "New Record Key Field/s Are Required.";
            WarehouseTypeList.Add(item);
            _serviceAgent.AddToWarehouseTypeRepository(item);
            SelectedWarehouseType = WarehouseTypeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }
        #endregion WarehouseType CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                WarehouseTypeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewWarehouseTypeCommand(""); //this will generate a new itemType and set it as the selected itemType...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedWarehouseType.SetPropertyValue(WarehouseTypeColumnMetaDataList[i].Name, columnValue);
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
            if (GetWarehouseTypeState(SelectedWarehouseType) != EntityStates.Detached)
            {
                if (Update(SelectedWarehouseType))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteWarehouseTypeCommand()
        {
            try
            {//company is fk to 100's of tables deleting it can be tricky...
                int i = 0;
                int ii = 0;
                for (int j = SelectedWarehouseTypeList.Count - 1; j >= 0; j--)
                {
                    WarehouseType item = (WarehouseType)SelectedWarehouseTypeList[j];
                    //get Max Index...
                    i = WarehouseTypeList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    WarehouseTypeList.Remove(item);
                }

                if (WarehouseTypeList != null && WarehouseTypeList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= WarehouseTypeList.Count())
                        ii = WarehouseTypeList.Count - 1;

                    SelectedWarehouseType = WarehouseTypeList[ii];
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
                NotifyMessage("WarehouseType/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewWarehouseTypeCommand()
        {
            NewWarehouseType("");
            AllowCommit = false;
        }

        public void NewWarehouseTypeCommand(string itemTypeID)
        {
            NewWarehouseType(itemTypeID);
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
            RegisterToReceiveMessages<BindingList<WarehouseType>>(MessageTokens.WarehouseTypeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                WarehouseTypeList = e.Data;
                SelectedWarehouseType = WarehouseTypeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<WarehouseType>>(MessageTokens.WarehouseTypeSearchToken.ToString(), OnSearchResult);
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
                    SelectedWarehouseType.WarehouseTypeID = SelectedWarehouseTypeMirror.WarehouseTypeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with WarehouseTypeID provided...
                    NewWarehouseTypeCommand(SelectedWarehouseType.WarehouseTypeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseType.WarehouseTypeID = SelectedWarehouseTypeMirror.WarehouseTypeID;
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
                    _serviceAgent.CommitWarehouseTypeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseType.WarehouseTypeID = SelectedWarehouseTypeMirror.WarehouseTypeID;
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
        public static object GetPropertyValue(this WarehouseType myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseType).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this WarehouseType myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseType).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this WarehouseType myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(WarehouseType).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}