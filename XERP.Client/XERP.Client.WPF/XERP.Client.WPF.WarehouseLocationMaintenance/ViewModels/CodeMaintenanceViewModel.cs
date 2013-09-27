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

namespace XERP.Client.WPF.WarehouseLocationMaintenance.ViewModels
{
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newWarehouseLocationCodeAutoId;

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

            WarehouseLocationCodeList = new BindingList<WarehouseLocationCode>();
            //disable new row feature...
            WarehouseLocationCodeList.AllowNew = false;

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

        private string _warehouseLocationCodeListCount;
        public string WarehouseLocationCodeListCount
        {
            get { return _warehouseLocationCodeListCount; }
            set
            {
                _warehouseLocationCodeListCount = value;
                NotifyPropertyChanged(m => m.WarehouseLocationCodeListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region Validation Properties
        private List<ColumnMetaData> _warehouseLocationCodeColumnMetaDataList;
        public List<ColumnMetaData> WarehouseLocationCodeColumnMetaDataList
        {
            get { return _warehouseLocationCodeColumnMetaDataList; }
            set
            {
                _warehouseLocationCodeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationCodeColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _warehouseLocationCodeMaxFieldValueDictionary;
        public Dictionary<string, int> WarehouseLocationCodeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_warehouseLocationCodeMaxFieldValueDictionary != null)
                {
                    return _warehouseLocationCodeMaxFieldValueDictionary;
                }
                _warehouseLocationCodeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("WarehouseLocationCodes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _warehouseLocationCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _warehouseLocationCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties

        #region CRUD Properties
        private BindingList<WarehouseLocationCode> _warehouseLocationCodeList;
        public BindingList<WarehouseLocationCode> WarehouseLocationCodeList
        {
            get
            {
                WarehouseLocationCodeListCount = _warehouseLocationCodeList.Count.ToString();
                if (_warehouseLocationCodeList.Count > 0)
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
                return _warehouseLocationCodeList;
            }
            set
            {
                _warehouseLocationCodeList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private WarehouseLocationCode _selectedWarehouseLocationCodeMirror;
        public WarehouseLocationCode SelectedWarehouseLocationCodeMirror
        {
            get { return _selectedWarehouseLocationCodeMirror; }
            set { _selectedWarehouseLocationCodeMirror = value; }
        }

        private System.Collections.IList _selectedWarehouseLocationCodeList;
        public System.Collections.IList SelectedWarehouseLocationCodeList
        {
            get { return _selectedWarehouseLocationCodeList; }
            set
            {
                if (_selectedWarehouseLocationCode != value)
                {
                    _selectedWarehouseLocationCodeList = value;
                    NotifyPropertyChanged(m => m.SelectedWarehouseLocationCodeList);
                }
            }
        }

        private WarehouseLocationCode _selectedWarehouseLocationCode;
        public WarehouseLocationCode SelectedWarehouseLocationCode
        {
            get
            {
                return _selectedWarehouseLocationCode;
            }
            set
            {
                if (_selectedWarehouseLocationCode != value)
                {
                    _selectedWarehouseLocationCode = value;
                    //set the mirrored SelectedWarehouseLocationCode to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedWarehouseLocationCodeMirror = new WarehouseLocationCode();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedWarehouseLocationCode.GetType().GetProperties())
                        {
                            SelectedWarehouseLocationCodeMirror.SetPropertyValue(prop.Name, SelectedWarehouseLocationCode.GetPropertyValue(prop.Name));
                        }
                        SelectedWarehouseLocationCodeMirror.WarehouseLocationCodeID = _selectedWarehouseLocationCode.WarehouseLocationCodeID;
                        NotifyPropertyChanged(m => m.SelectedWarehouseLocationCode);

                        SelectedWarehouseLocationCode.PropertyChanged += new PropertyChangedEventHandler(SelectedWarehouseLocationCode_PropertyChanged);
                    }
                }
            }
        }
        #endregion CRUD Properties
        #endregion Properties

        #region ViewModel Property Events
        private void SelectedWarehouseLocationCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "WarehouseLocationCodeID")
            {//make sure it is has changed...
                if (SelectedWarehouseLocationCodeMirror.WarehouseLocationCodeID != SelectedWarehouseLocationCode.WarehouseLocationCodeID)
                {
                    //if their are no records it is a key change
                    if (WarehouseLocationCodeList != null && WarehouseLocationCodeList.Count == 0
                        && SelectedWarehouseLocationCode != null && !string.IsNullOrEmpty(SelectedWarehouseLocationCode.WarehouseLocationCodeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetWarehouseLocationCodeState(SelectedWarehouseLocationCode);

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

            object propertyChangedValue = SelectedWarehouseLocationCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedWarehouseLocationCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyCode = SelectedWarehouseLocationCode.GetPropertyCode(e.PropertyName);
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
                if (WarehouseLocationCodePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyCode))
                {
                    Update(SelectedWarehouseLocationCode);
                    SelectedWarehouseLocationCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedWarehouseLocationCodeMirror.IsValid = SelectedWarehouseLocationCode.IsValid;
                    SelectedWarehouseLocationCodeMirror.IsExpanded = SelectedWarehouseLocationCode.IsExpanded;
                    SelectedWarehouseLocationCodeMirror.NotValidMessage = SelectedWarehouseLocationCode.NotValidMessage;

                }
                else//revert back to its previous value... 
                {
                    SelectedWarehouseLocationCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedWarehouseLocationCode.IsValid = SelectedWarehouseLocationCodeMirror.IsValid;
                    SelectedWarehouseLocationCode.IsExpanded = SelectedWarehouseLocationCodeMirror.IsExpanded;
                    SelectedWarehouseLocationCode.NotValidMessage = SelectedWarehouseLocationCodeMirror.NotValidMessage;
                }

            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedWarehouseLocationCode.WarehouseLocationCodeID))
            {//check to see if key is part of the current companylist...
                WarehouseLocationCode query = WarehouseLocationCodeList.Where(company => company.WarehouseLocationCodeID == SelectedWarehouseLocationCode.WarehouseLocationCodeID &&
                                                        company.AutoID != SelectedWarehouseLocationCode.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back...
                    SelectedWarehouseLocationCode.WarehouseLocationCodeID = SelectedWarehouseLocationCodeMirror.WarehouseLocationCodeID;
                    //change to the newly selected item...
                    SelectedWarehouseLocationCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                WarehouseLocationCodeList = GetWarehouseLocationCodeByID(SelectedWarehouseLocationCode.WarehouseLocationCodeID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (WarehouseLocationCodeList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedWarehouseLocationCode.WarehouseLocationCodeID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedWarehouseLocationCode = WarehouseLocationCodeList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedWarehouseLocationCode.WarehouseLocationCodeID != SelectedWarehouseLocationCodeMirror.WarehouseLocationCodeID)
                    SelectedWarehouseLocationCode.WarehouseLocationCodeID = SelectedWarehouseLocationCodeMirror.WarehouseLocationCodeID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in WarehouseLocationCodeList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedWarehouseLocationCode = new WarehouseLocationCode();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            WarehouseLocationCodeList.Clear();
            SetAsEmptySelection();
        }

        private bool WarehouseLocationCodePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "WarehouseLocationCodeID":
                    rBool = WarehouseLocationCodeIsValid(SelectedWarehouseLocationCode, _companyValidationProperties.WarehouseLocationCodeID, out errorMessage);
                    break;
                case "Name":
                    rBool = WarehouseLocationCodeIsValid(SelectedWarehouseLocationCode, _companyValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedWarehouseLocationCode.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedWarehouseLocationCode.IsValid = WarehouseLocationCodeIsValid(SelectedWarehouseLocationCode, out errorMessage);
                if (SelectedWarehouseLocationCode.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedWarehouseLocationCode.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _companyValidationProperties
        {//we list all fields that require validation...
            WarehouseLocationCodeID,
            Name
        }

        //Object.Property Scope Validation...
        private bool WarehouseLocationCodeIsValid(WarehouseLocationCode item, _companyValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _companyValidationProperties.WarehouseLocationCodeID:
                    //validate key
                    if (string.IsNullOrEmpty(item.WarehouseLocationCodeID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetWarehouseLocationCodeState(item);
                    if (entityState == EntityStates.Added && WarehouseLocationCodeExists(item.WarehouseLocationCodeID, ClientSessionSingleton.Instance.CompanyID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = WarehouseLocationCodeList.Count(q => q.WarehouseLocationCodeID == item.WarehouseLocationCodeID);
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
        //WarehouseLocationCode Object Scope Validation check the entire object for validity...
        private byte WarehouseLocationCodeIsValid(WarehouseLocationCode item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.WarehouseLocationCodeID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetWarehouseLocationCodeState(item);
            if (entityState == EntityStates.Added && WarehouseLocationCodeExists(item.WarehouseLocationCodeID, ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "Item AllReady Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = WarehouseLocationCodeList.Count(q => q.WarehouseLocationCodeID == item.WarehouseLocationCodeID);
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
        private EntityStates GetWarehouseLocationCodeState(WarehouseLocationCode itemCode)
        {
            return _serviceAgent.GetWarehouseLocationCodeEntityState(itemCode);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.WarehouseLocationCodeRepositoryIsDirty();
        }

        #region WarehouseLocationCode CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedWarehouseLocationCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (WarehouseLocationCode itemCode in WarehouseLocationCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (itemCode.AutoID > 0)
                    autoIDs = autoIDs + itemCode.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                WarehouseLocationCodeList = new BindingList<WarehouseLocationCode>(_serviceAgent.RefreshWarehouseLocationCode(autoIDs).ToList());
                SelectedWarehouseLocationCode = (from q in WarehouseLocationCodeList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<WarehouseLocationCode> GetWarehouseLocationCodes(string companyID)
        {
            BindingList<WarehouseLocationCode> warehouseLocationCodeList = new BindingList<WarehouseLocationCode>(_serviceAgent.GetWarehouseLocationCodes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return warehouseLocationCodeList;
        }

        private BindingList<WarehouseLocationCode> GetWarehouseLocationCodes(WarehouseLocationCode itemCode, string companyID)
        {
            BindingList<WarehouseLocationCode> itemCodeList = new BindingList<WarehouseLocationCode>(_serviceAgent.GetWarehouseLocationCodes(itemCode, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemCodeList;
        }

        private BindingList<WarehouseLocationCode> GetWarehouseLocationCodeByID(string itemCodeID, string companyID)
        {
            BindingList<WarehouseLocationCode> itemCodeList = new BindingList<WarehouseLocationCode>(_serviceAgent.GetWarehouseLocationCodeByID(itemCodeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemCodeList;
        }

        private bool WarehouseLocationCodeExists(string itemCodeID, string companyID)
        {
            return _serviceAgent.WarehouseLocationCodeExists(itemCodeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(WarehouseLocationCode item)
        {
            _serviceAgent.UpdateWarehouseLocationCodeRepository(item);
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
            var items = (from q in WarehouseLocationCodeList where q.IsValid == 2 select q).ToList();
            foreach (WarehouseLocationCode item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitWarehouseLocationCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(WarehouseLocationCode itemCode)
        {
            _serviceAgent.DeleteFromWarehouseLocationCodeRepository(itemCode);
            return true;
        }

        private bool NewWarehouseLocationCode(string id)
        {
            WarehouseLocationCode item = new WarehouseLocationCode();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newWarehouseLocationCodeAutoId = _newWarehouseLocationCodeAutoId - 1;
            item.AutoID = _newWarehouseLocationCodeAutoId;
            item.WarehouseLocationCodeID = id;
            item.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            item.IsValid = 1;
            item.NotValidMessage = "New Record Key Field/s Are Required.";
            WarehouseLocationCodeList.Add(item);
            _serviceAgent.AddToWarehouseLocationCodeRepository(item);
            SelectedWarehouseLocationCode = WarehouseLocationCodeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion WarehouseLocationCode CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                WarehouseLocationCodeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewWarehouseLocationCodeCommand(""); //this will generate a new itemCode and set it as the selected itemCode...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedWarehouseLocationCode.SetPropertyValue(WarehouseLocationCodeColumnMetaDataList[i].Name, columnValue);
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
            if (GetWarehouseLocationCodeState(SelectedWarehouseLocationCode) != EntityStates.Detached)
            {
                if (Update(SelectedWarehouseLocationCode))
                    Commit();
                else
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteWarehouseLocationCodeCommand()
        {
            try
            {//company is fk to 100's of tables deleting it can be tricky...
                int i = 0;
                int ii = 0;
                for (int j = SelectedWarehouseLocationCodeList.Count - 1; j >= 0; j--)
                {
                    WarehouseLocationCode item = (WarehouseLocationCode)SelectedWarehouseLocationCodeList[j];
                    //get Max Index...
                    i = WarehouseLocationCodeList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    WarehouseLocationCodeList.Remove(item);
                }

                if (WarehouseLocationCodeList != null && WarehouseLocationCodeList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= WarehouseLocationCodeList.Count())
                        ii = WarehouseLocationCodeList.Count - 1;

                    SelectedWarehouseLocationCode = WarehouseLocationCodeList[ii];
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
                NotifyMessage("WarehouseLocationCode/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewWarehouseLocationCodeCommand()
        {
            NewWarehouseLocationCode("");
            AllowCommit = false;
        }

        public void NewWarehouseLocationCodeCommand(string itemCodeID)
        {
            NewWarehouseLocationCode(itemCodeID);
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
            RegisterToReceiveMessages<BindingList<WarehouseLocationCode>>(MessageTokens.WarehouseLocationCodeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseLocationCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                WarehouseLocationCodeList = e.Data;
                SelectedWarehouseLocationCode = WarehouseLocationCodeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<WarehouseLocationCode>>(MessageTokens.WarehouseLocationCodeSearchToken.ToString(), OnSearchResult);
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
                    SelectedWarehouseLocationCode.WarehouseLocationCodeID = SelectedWarehouseLocationCodeMirror.WarehouseLocationCodeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with WarehouseLocationCodeID provided...
                    NewWarehouseLocationCodeCommand(SelectedWarehouseLocationCode.WarehouseLocationCodeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseLocationCode.WarehouseLocationCodeID = SelectedWarehouseLocationCodeMirror.WarehouseLocationCodeID;
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
                    _serviceAgent.CommitWarehouseLocationCodeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseLocationCode.WarehouseLocationCodeID = SelectedWarehouseLocationCodeMirror.WarehouseLocationCodeID;
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
        public static object GetPropertyValue(this WarehouseLocationCode myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseLocationCode).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyCode(this WarehouseLocationCode myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseLocationCode).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this WarehouseLocationCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(WarehouseLocationCode).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}