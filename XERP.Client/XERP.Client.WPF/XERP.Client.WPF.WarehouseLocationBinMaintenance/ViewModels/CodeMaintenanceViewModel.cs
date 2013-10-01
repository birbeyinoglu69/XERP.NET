using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using ExtensionMethods;
using SimpleMvvmToolkit;
using XERP.Client.Models;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Client.WPF.WarehouseLocationBinMaintenance.ViewModels
{
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newWarehouseLocationBinCodeAutoId;

        private IWarehouseServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public CodeMaintenanceViewModel(){}

        public CodeMaintenanceViewModel(IWarehouseServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            WarehouseLocationBinCodeList = new BindingList<WarehouseLocationBinCode>();
            //disable new row feature...
            WarehouseLocationBinCodeList.AllowNew = false;

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
        public event EventHandler<NotificationEventArgs> NewRecordCreatedNotice;
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

        private string _warehouseLocationBinCodeListCount;
        public string WarehouseLocationBinCodeListCount
        {
            get { return _warehouseLocationBinCodeListCount; }
            set
            {
                _warehouseLocationBinCodeListCount = value;
                NotifyPropertyChanged(m => m.WarehouseLocationBinCodeListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region Validation Properties
        private List<ColumnMetaData> _warehouseLocationBinCodeColumnMetaDataList;
        public List<ColumnMetaData> WarehouseLocationBinCodeColumnMetaDataList
        {
            get { return _warehouseLocationBinCodeColumnMetaDataList; }
            set
            {
                _warehouseLocationBinCodeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationBinCodeColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _warehouseLocationBinCodeMaxFieldValueDictionary;
        public Dictionary<string, int> WarehouseLocationBinCodeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_warehouseLocationBinCodeMaxFieldValueDictionary != null)
                {
                    return _warehouseLocationBinCodeMaxFieldValueDictionary;
                }
                _warehouseLocationBinCodeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("WarehouseLocationBinCodes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _warehouseLocationBinCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _warehouseLocationBinCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties

        #region CRUD Properties
        private BindingList<WarehouseLocationBinCode> _warehouseLocationBinCodeList;
        public BindingList<WarehouseLocationBinCode> WarehouseLocationBinCodeList
        {
            get
            {
                WarehouseLocationBinCodeListCount = _warehouseLocationBinCodeList.Count.ToString();
                if (_warehouseLocationBinCodeList.Count > 0)
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
                return _warehouseLocationBinCodeList;
            }
            set
            {
                _warehouseLocationBinCodeList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationBinCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private WarehouseLocationBinCode _selectedWarehouseLocationBinCodeMirror;
        public WarehouseLocationBinCode SelectedWarehouseLocationBinCodeMirror
        {
            get { return _selectedWarehouseLocationBinCodeMirror; }
            set { _selectedWarehouseLocationBinCodeMirror = value; }
        }

        private System.Collections.IList _selectedWarehouseLocationBinCodeList;
        public System.Collections.IList SelectedWarehouseLocationBinCodeList
        {
            get { return _selectedWarehouseLocationBinCodeList; }
            set
            {
                if (_selectedWarehouseLocationBinCode != value)
                {
                    _selectedWarehouseLocationBinCodeList = value;
                    NotifyPropertyChanged(m => m.SelectedWarehouseLocationBinCodeList);
                }
            }
        }

        private WarehouseLocationBinCode _selectedWarehouseLocationBinCode;
        public WarehouseLocationBinCode SelectedWarehouseLocationBinCode
        {
            get
            {
                return _selectedWarehouseLocationBinCode;
            }
            set
            {
                if (_selectedWarehouseLocationBinCode != value)
                {
                    _selectedWarehouseLocationBinCode = value;
                    //set the mirrored SelectedWarehouseLocationBinCode to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedWarehouseLocationBinCodeMirror = new WarehouseLocationBinCode();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedWarehouseLocationBinCode.GetType().GetProperties())
                        {
                            SelectedWarehouseLocationBinCodeMirror.SetPropertyValue(prop.Name, SelectedWarehouseLocationBinCode.GetPropertyValue(prop.Name));
                        }
                        SelectedWarehouseLocationBinCodeMirror.WarehouseLocationBinCodeID = _selectedWarehouseLocationBinCode.WarehouseLocationBinCodeID;
                        NotifyPropertyChanged(m => m.SelectedWarehouseLocationBinCode);

                        SelectedWarehouseLocationBinCode.PropertyChanged += new PropertyChangedEventHandler(SelectedWarehouseLocationBinCode_PropertyChanged);
                    }
                }
            }
        }
        #endregion CRUD Properties
        #endregion Properties

        #region ViewModel Property Events
        private void SelectedWarehouseLocationBinCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "WarehouseLocationBinCodeID")
            {//make sure it is has changed...
                if (SelectedWarehouseLocationBinCodeMirror.WarehouseLocationBinCodeID != SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID)
                {
                    //if their are no records it is a key change
                    if (WarehouseLocationBinCodeList != null && WarehouseLocationBinCodeList.Count == 0
                        && SelectedWarehouseLocationBinCode != null && !string.IsNullOrEmpty(SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetWarehouseLocationBinCodeState(SelectedWarehouseLocationBinCode);

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

            object propertyChangedValue = SelectedWarehouseLocationBinCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedWarehouseLocationBinCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyCode = SelectedWarehouseLocationBinCode.GetPropertyCode(e.PropertyName);
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
                if (WarehouseLocationBinCodePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyCode))
                {
                    Update(SelectedWarehouseLocationBinCode);
                    SelectedWarehouseLocationBinCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedWarehouseLocationBinCodeMirror.IsValid = SelectedWarehouseLocationBinCode.IsValid;
                    SelectedWarehouseLocationBinCodeMirror.IsExpanded = SelectedWarehouseLocationBinCode.IsExpanded;
                    SelectedWarehouseLocationBinCodeMirror.NotValidMessage = SelectedWarehouseLocationBinCode.NotValidMessage;

                }
                else//revert back to its previous value... 
                {
                    SelectedWarehouseLocationBinCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedWarehouseLocationBinCode.IsValid = SelectedWarehouseLocationBinCodeMirror.IsValid;
                    SelectedWarehouseLocationBinCode.IsExpanded = SelectedWarehouseLocationBinCodeMirror.IsExpanded;
                    SelectedWarehouseLocationBinCode.NotValidMessage = SelectedWarehouseLocationBinCodeMirror.NotValidMessage;
                }

            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID))
            {//check to see if key is part of the current companylist...
                WarehouseLocationBinCode query = WarehouseLocationBinCodeList.Where(company => company.WarehouseLocationBinCodeID == SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID &&
                                                        company.AutoID != SelectedWarehouseLocationBinCode.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back...
                    SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID = SelectedWarehouseLocationBinCodeMirror.WarehouseLocationBinCodeID;
                    //change to the newly selected item...
                    SelectedWarehouseLocationBinCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                WarehouseLocationBinCodeList = GetWarehouseLocationBinCodeByID(SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (WarehouseLocationBinCodeList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedWarehouseLocationBinCode = WarehouseLocationBinCodeList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID != SelectedWarehouseLocationBinCodeMirror.WarehouseLocationBinCodeID)
                    SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID = SelectedWarehouseLocationBinCodeMirror.WarehouseLocationBinCodeID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in WarehouseLocationBinCodeList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedWarehouseLocationBinCode = new WarehouseLocationBinCode();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            WarehouseLocationBinCodeList.Clear();
            SetAsEmptySelection();
        }

        private bool WarehouseLocationBinCodePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "WarehouseLocationBinCodeID":
                    rBool = WarehouseLocationBinCodeIsValid(SelectedWarehouseLocationBinCode, _companyValidationProperties.WarehouseLocationBinCodeID, out errorMessage);
                    break;
                case "Name":
                    rBool = WarehouseLocationBinCodeIsValid(SelectedWarehouseLocationBinCode, _companyValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedWarehouseLocationBinCode.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedWarehouseLocationBinCode.IsValid = WarehouseLocationBinCodeIsValid(SelectedWarehouseLocationBinCode, out errorMessage);
                if (SelectedWarehouseLocationBinCode.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedWarehouseLocationBinCode.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _companyValidationProperties
        {//we list all fields that require validation...
            WarehouseLocationBinCodeID,
            Name
        }

        //Object.Property Scope Validation...
        private bool WarehouseLocationBinCodeIsValid(WarehouseLocationBinCode item, _companyValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _companyValidationProperties.WarehouseLocationBinCodeID:
                    //validate key
                    if (string.IsNullOrEmpty(item.WarehouseLocationBinCodeID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetWarehouseLocationBinCodeState(item);
                    if (entityState == EntityStates.Added && WarehouseLocationBinCodeExists(item.WarehouseLocationBinCodeID, ClientSessionSingleton.Instance.CompanyID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = WarehouseLocationBinCodeList.Count(q => q.WarehouseLocationBinCodeID == item.WarehouseLocationBinCodeID);
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
        //WarehouseLocationBinCode Object Scope Validation check the entire object for validity...
        private byte WarehouseLocationBinCodeIsValid(WarehouseLocationBinCode item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.WarehouseLocationBinCodeID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetWarehouseLocationBinCodeState(item);
            if (entityState == EntityStates.Added && WarehouseLocationBinCodeExists(item.WarehouseLocationBinCodeID, ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "Item AllReady Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = WarehouseLocationBinCodeList.Count(q => q.WarehouseLocationBinCodeID == item.WarehouseLocationBinCodeID);
            if (count > 1)
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
        private EntityStates GetWarehouseLocationBinCodeState(WarehouseLocationBinCode itemCode)
        {
            return _serviceAgent.GetWarehouseLocationBinCodeEntityState(itemCode);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.WarehouseLocationBinCodeRepositoryIsDirty();
        }

        #region WarehouseLocationBinCode CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedWarehouseLocationBinCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (WarehouseLocationBinCode itemCode in WarehouseLocationBinCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (itemCode.AutoID > 0)
                    autoIDs = autoIDs + itemCode.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                WarehouseLocationBinCodeList = new BindingList<WarehouseLocationBinCode>(_serviceAgent.RefreshWarehouseLocationBinCode(autoIDs).ToList());
                SelectedWarehouseLocationBinCode = (from q in WarehouseLocationBinCodeList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<WarehouseLocationBinCode> GetWarehouseLocationBinCodes(string companyID)
        {
            BindingList<WarehouseLocationBinCode> warehouseLocationBinCodeList = new BindingList<WarehouseLocationBinCode>(_serviceAgent.GetWarehouseLocationBinCodes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return warehouseLocationBinCodeList;
        }

        private BindingList<WarehouseLocationBinCode> GetWarehouseLocationBinCodes(WarehouseLocationBinCode itemCode, string companyID)
        {
            BindingList<WarehouseLocationBinCode> itemCodeList = new BindingList<WarehouseLocationBinCode>(_serviceAgent.GetWarehouseLocationBinCodes(itemCode, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemCodeList;
        }

        private BindingList<WarehouseLocationBinCode> GetWarehouseLocationBinCodeByID(string itemCodeID, string companyID)
        {
            BindingList<WarehouseLocationBinCode> itemCodeList = new BindingList<WarehouseLocationBinCode>(_serviceAgent.GetWarehouseLocationBinCodeByID(itemCodeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemCodeList;
        }

        private bool WarehouseLocationBinCodeExists(string itemCodeID, string companyID)
        {
            return _serviceAgent.WarehouseLocationBinCodeExists(itemCodeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(WarehouseLocationBinCode item)
        {
            _serviceAgent.UpdateWarehouseLocationBinCodeRepository(item);
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
            var items = (from q in WarehouseLocationBinCodeList where q.IsValid == 2 select q).ToList();
            foreach (WarehouseLocationBinCode item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitWarehouseLocationBinCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(WarehouseLocationBinCode itemCode)
        {
            _serviceAgent.DeleteFromWarehouseLocationBinCodeRepository(itemCode);
            return true;
        }

        private bool NewWarehouseLocationBinCode(string id)
        {
            WarehouseLocationBinCode item = new WarehouseLocationBinCode();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newWarehouseLocationBinCodeAutoId = _newWarehouseLocationBinCodeAutoId - 1;
            item.AutoID = _newWarehouseLocationBinCodeAutoId;
            item.WarehouseLocationBinCodeID = id;
            item.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            item.IsValid = 1;
            item.NotValidMessage = "New Record Key Field/s Are Required.";
            WarehouseLocationBinCodeList.Add(item);
            _serviceAgent.AddToWarehouseLocationBinCodeRepository(item);
            SelectedWarehouseLocationBinCode = WarehouseLocationBinCodeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion WarehouseLocationBinCode CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                WarehouseLocationBinCodeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewWarehouseLocationBinCodeCommand(""); //this will generate a new itemCode and set it as the selected itemCode...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedWarehouseLocationBinCode.SetPropertyValue(WarehouseLocationBinCodeColumnMetaDataList[i].Name, columnValue);
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
            if (GetWarehouseLocationBinCodeState(SelectedWarehouseLocationBinCode) != EntityStates.Detached)
            {
                if (Update(SelectedWarehouseLocationBinCode))
                    Commit();
                else
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteWarehouseLocationBinCodeCommand()
        {
            try
            {//company is fk to 100's of tables deleting it can be tricky...
                int i = 0;
                int ii = 0;
                for (int j = SelectedWarehouseLocationBinCodeList.Count - 1; j >= 0; j--)
                {
                    WarehouseLocationBinCode item = (WarehouseLocationBinCode)SelectedWarehouseLocationBinCodeList[j];
                    //get Max Index...
                    i = WarehouseLocationBinCodeList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    WarehouseLocationBinCodeList.Remove(item);
                }

                if (WarehouseLocationBinCodeList != null && WarehouseLocationBinCodeList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= WarehouseLocationBinCodeList.Count())
                        ii = WarehouseLocationBinCodeList.Count - 1;

                    SelectedWarehouseLocationBinCode = WarehouseLocationBinCodeList[ii];
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
                NotifyMessage("WarehouseLocationBinCode/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewWarehouseLocationBinCodeCommand()
        {
            NewWarehouseLocationBinCode("");
            AllowCommit = false;
        }

        public void NewWarehouseLocationBinCodeCommand(string itemCodeID)
        {
            NewWarehouseLocationBinCode(itemCodeID);
            if (string.IsNullOrEmpty(itemCodeID)) //don't allow a save until a itemCodeID is provided...
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
            RegisterToReceiveMessages<BindingList<WarehouseLocationBinCode>>(MessageTokens.WarehouseLocationBinCodeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseLocationBinCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                WarehouseLocationBinCodeList = e.Data;
                SelectedWarehouseLocationBinCode = WarehouseLocationBinCodeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<WarehouseLocationBinCode>>(MessageTokens.WarehouseLocationBinCodeSearchToken.ToString(), OnSearchResult);
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
                    SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID = SelectedWarehouseLocationBinCodeMirror.WarehouseLocationBinCodeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with WarehouseLocationBinCodeID provided...
                    NewWarehouseLocationBinCodeCommand(SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID = SelectedWarehouseLocationBinCodeMirror.WarehouseLocationBinCodeID;
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
                    _serviceAgent.CommitWarehouseLocationBinCodeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseLocationBinCode.WarehouseLocationBinCodeID = SelectedWarehouseLocationBinCodeMirror.WarehouseLocationBinCodeID;
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
        public static object GetPropertyValue(this WarehouseLocationBinCode myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseLocationBinCode).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyCode(this WarehouseLocationBinCode myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseLocationBinCode).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this WarehouseLocationBinCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(WarehouseLocationBinCode).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}