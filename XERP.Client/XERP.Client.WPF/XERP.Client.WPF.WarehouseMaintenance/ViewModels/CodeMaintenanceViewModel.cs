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

namespace XERP.Client.WPF.WarehouseMaintenance.ViewModels
{
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newWarehouseCodeAutoId;

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

            WarehouseCodeList = new BindingList<WarehouseCode>();
            //disable new row feature...
            WarehouseCodeList.AllowNew = false;

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

        private string _warehouseCodeListCount;
        public string WarehouseCodeListCount
        {
            get { return _warehouseCodeListCount; }
            set
            {
                _warehouseCodeListCount = value;
                NotifyPropertyChanged(m => m.WarehouseCodeListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region Validation Properties
        private List<ColumnMetaData> _warehouseCodeColumnMetaDataList;
        public List<ColumnMetaData> WarehouseCodeColumnMetaDataList
        {
            get { return _warehouseCodeColumnMetaDataList; }
            set
            {
                _warehouseCodeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.WarehouseCodeColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _warehouseCodeMaxFieldValueDictionary;
        public Dictionary<string, int> WarehouseCodeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_warehouseCodeMaxFieldValueDictionary != null)
                {
                    return _warehouseCodeMaxFieldValueDictionary;
                }
                _warehouseCodeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("WarehouseCodes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _warehouseCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _warehouseCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties

        #region CRUD Properties
        private BindingList<WarehouseCode> _warehouseCodeList;
        public BindingList<WarehouseCode> WarehouseCodeList
        {
            get
            {
                WarehouseCodeListCount = _warehouseCodeList.Count.ToString();
                if (_warehouseCodeList.Count > 0)
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
                return _warehouseCodeList;
            }
            set
            {
                _warehouseCodeList = value;
                NotifyPropertyChanged(m => m.WarehouseCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private WarehouseCode _selectedWarehouseCodeMirror;
        public WarehouseCode SelectedWarehouseCodeMirror
        {
            get { return _selectedWarehouseCodeMirror; }
            set { _selectedWarehouseCodeMirror = value; }
        }

        private System.Collections.IList _selectedWarehouseCodeList;
        public System.Collections.IList SelectedWarehouseCodeList
        {
            get { return _selectedWarehouseCodeList; }
            set
            {
                if (_selectedWarehouseCode != value)
                {
                    _selectedWarehouseCodeList = value;
                    NotifyPropertyChanged(m => m.SelectedWarehouseCodeList);
                }
            }
        }

        private WarehouseCode _selectedWarehouseCode;
        public WarehouseCode SelectedWarehouseCode
        {
            get
            {
                return _selectedWarehouseCode;
            }
            set
            {
                if (_selectedWarehouseCode != value)
                {
                    _selectedWarehouseCode = value;
                    //set the mirrored SelectedWarehouseCode to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedWarehouseCodeMirror = new WarehouseCode();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedWarehouseCode.GetType().GetProperties())
                        {
                            SelectedWarehouseCodeMirror.SetPropertyValue(prop.Name, SelectedWarehouseCode.GetPropertyValue(prop.Name));
                        }
                        SelectedWarehouseCodeMirror.WarehouseCodeID = _selectedWarehouseCode.WarehouseCodeID;
                        NotifyPropertyChanged(m => m.SelectedWarehouseCode);

                        SelectedWarehouseCode.PropertyChanged += new PropertyChangedEventHandler(SelectedWarehouseCode_PropertyChanged);
                    }
                }
            }
        }
        #endregion CRUD Properties
        #endregion Properties

        #region ViewModel Property Events
        private void SelectedWarehouseCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "WarehouseCodeID")
            {//make sure it is has changed...
                if (SelectedWarehouseCodeMirror.WarehouseCodeID != SelectedWarehouseCode.WarehouseCodeID)
                {
                    //if their are no records it is a key change
                    if (WarehouseCodeList != null && WarehouseCodeList.Count == 0
                        && SelectedWarehouseCode != null && !string.IsNullOrEmpty(SelectedWarehouseCode.WarehouseCodeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetWarehouseCodeState(SelectedWarehouseCode);

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

            object propertyChangedValue = SelectedWarehouseCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedWarehouseCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyCode = SelectedWarehouseCode.GetPropertyCode(e.PropertyName);
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
                if (WarehouseCodePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyCode))
                {
                    Update(SelectedWarehouseCode);
                    SelectedWarehouseCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedWarehouseCodeMirror.IsValid = SelectedWarehouseCode.IsValid;
                    SelectedWarehouseCodeMirror.IsExpanded = SelectedWarehouseCode.IsExpanded;
                    SelectedWarehouseCodeMirror.NotValidMessage = SelectedWarehouseCode.NotValidMessage;

                }
                else//revert back to its previous value... 
                {
                    SelectedWarehouseCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedWarehouseCode.IsValid = SelectedWarehouseCodeMirror.IsValid;
                    SelectedWarehouseCode.IsExpanded = SelectedWarehouseCodeMirror.IsExpanded;
                    SelectedWarehouseCode.NotValidMessage = SelectedWarehouseCodeMirror.NotValidMessage;
                }

            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedWarehouseCode.WarehouseCodeID))
            {//check to see if key is part of the current companylist...
                WarehouseCode query = WarehouseCodeList.Where(company => company.WarehouseCodeID == SelectedWarehouseCode.WarehouseCodeID &&
                                                        company.AutoID != SelectedWarehouseCode.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back...
                    SelectedWarehouseCode.WarehouseCodeID = SelectedWarehouseCodeMirror.WarehouseCodeID;
                    //change to the newly selected item...
                    SelectedWarehouseCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                WarehouseCodeList = GetWarehouseCodeByID(SelectedWarehouseCode.WarehouseCodeID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (WarehouseCodeList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedWarehouseCode.WarehouseCodeID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedWarehouseCode = WarehouseCodeList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedWarehouseCode.WarehouseCodeID != SelectedWarehouseCodeMirror.WarehouseCodeID)
                    SelectedWarehouseCode.WarehouseCodeID = SelectedWarehouseCodeMirror.WarehouseCodeID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in WarehouseCodeList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedWarehouseCode = new WarehouseCode();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            WarehouseCodeList.Clear();
            SetAsEmptySelection();
        }

        private bool WarehouseCodePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "WarehouseCodeID":
                    rBool = WarehouseCodeIsValid(SelectedWarehouseCode, _companyValidationProperties.WarehouseCodeID, out errorMessage);
                    break;
                case "Name":
                    rBool = WarehouseCodeIsValid(SelectedWarehouseCode, _companyValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedWarehouseCode.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedWarehouseCode.IsValid = WarehouseCodeIsValid(SelectedWarehouseCode, out errorMessage);
                if (SelectedWarehouseCode.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedWarehouseCode.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _companyValidationProperties
        {//we list all fields that require validation...
            WarehouseCodeID,
            Name
        }

        //Object.Property Scope Validation...
        private bool WarehouseCodeIsValid(WarehouseCode item, _companyValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _companyValidationProperties.WarehouseCodeID:
                    //validate key
                    if (string.IsNullOrEmpty(item.WarehouseCodeID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetWarehouseCodeState(item);
                    if (entityState == EntityStates.Added && WarehouseCodeExists(item.WarehouseCodeID, ClientSessionSingleton.Instance.CompanyID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = WarehouseCodeList.Count(q => q.WarehouseCodeID == item.WarehouseCodeID);
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
        //WarehouseCode Object Scope Validation check the entire object for validity...
        private byte WarehouseCodeIsValid(WarehouseCode item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.WarehouseCodeID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetWarehouseCodeState(item);
            if (entityState == EntityStates.Added && WarehouseCodeExists(item.WarehouseCodeID, ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "Item AllReady Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = WarehouseCodeList.Count(q => q.WarehouseCodeID == item.WarehouseCodeID);
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
        private EntityStates GetWarehouseCodeState(WarehouseCode itemCode)
        {
            return _serviceAgent.GetWarehouseCodeEntityState(itemCode);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.WarehouseCodeRepositoryIsDirty();
        }

        #region WarehouseCode CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedWarehouseCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (WarehouseCode itemCode in WarehouseCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (itemCode.AutoID > 0)
                    autoIDs = autoIDs + itemCode.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                WarehouseCodeList = new BindingList<WarehouseCode>(_serviceAgent.RefreshWarehouseCode(autoIDs).ToList());
                SelectedWarehouseCode = (from q in WarehouseCodeList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<WarehouseCode> GetWarehouseCodes(string companyID)
        {
            BindingList<WarehouseCode> warehouseCodeList = new BindingList<WarehouseCode>(_serviceAgent.GetWarehouseCodes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return warehouseCodeList;
        }

        private BindingList<WarehouseCode> GetWarehouseCodes(WarehouseCode itemCode, string companyID)
        {
            BindingList<WarehouseCode> itemCodeList = new BindingList<WarehouseCode>(_serviceAgent.GetWarehouseCodes(itemCode, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemCodeList;
        }

        private BindingList<WarehouseCode> GetWarehouseCodeByID(string itemCodeID, string companyID)
        {
            BindingList<WarehouseCode> itemCodeList = new BindingList<WarehouseCode>(_serviceAgent.GetWarehouseCodeByID(itemCodeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemCodeList;
        }

        private bool WarehouseCodeExists(string itemCodeID, string companyID)
        {
            return _serviceAgent.WarehouseCodeExists(itemCodeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(WarehouseCode item)
        {
            _serviceAgent.UpdateWarehouseCodeRepository(item);
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
            var items = (from q in WarehouseCodeList where q.IsValid == 2 select q).ToList();
            foreach (WarehouseCode item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitWarehouseCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(WarehouseCode itemCode)
        {
            _serviceAgent.DeleteFromWarehouseCodeRepository(itemCode);
            return true;
        }

        private bool NewWarehouseCode(string id)
        {
            WarehouseCode item = new WarehouseCode();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newWarehouseCodeAutoId = _newWarehouseCodeAutoId - 1;
            item.AutoID = _newWarehouseCodeAutoId;
            item.WarehouseCodeID = id;
            item.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            item.IsValid = 1;
            item.NotValidMessage = "New Record Key Field/s Are Required.";
            WarehouseCodeList.Add(item);
            _serviceAgent.AddToWarehouseCodeRepository(item);
            SelectedWarehouseCode = WarehouseCodeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion WarehouseCode CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                WarehouseCodeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewWarehouseCodeCommand(""); //this will generate a new itemCode and set it as the selected itemCode...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedWarehouseCode.SetPropertyValue(WarehouseCodeColumnMetaDataList[i].Name, columnValue);
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
            if (GetWarehouseCodeState(SelectedWarehouseCode) != EntityStates.Detached)
            {
                if (Update(SelectedWarehouseCode))
                    Commit();
                else
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteWarehouseCodeCommand()
        {
            try
            {//company is fk to 100's of tables deleting it can be tricky...
                int i = 0;
                int ii = 0;
                for (int j = SelectedWarehouseCodeList.Count - 1; j >= 0; j--)
                {
                    WarehouseCode item = (WarehouseCode)SelectedWarehouseCodeList[j];
                    //get Max Index...
                    i = WarehouseCodeList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    WarehouseCodeList.Remove(item);
                }

                if (WarehouseCodeList != null && WarehouseCodeList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= WarehouseCodeList.Count())
                        ii = WarehouseCodeList.Count - 1;

                    SelectedWarehouseCode = WarehouseCodeList[ii];
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
                NotifyMessage("WarehouseCode/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewWarehouseCodeCommand()
        {
            NewWarehouseCode("");
            AllowCommit = false;
        }

        public void NewWarehouseCodeCommand(string itemCodeID)
        {
            NewWarehouseCode(itemCodeID);
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
            RegisterToReceiveMessages<BindingList<WarehouseCode>>(MessageTokens.WarehouseCodeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                WarehouseCodeList = e.Data;
                SelectedWarehouseCode = WarehouseCodeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<WarehouseCode>>(MessageTokens.WarehouseCodeSearchToken.ToString(), OnSearchResult);
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
                    SelectedWarehouseCode.WarehouseCodeID = SelectedWarehouseCodeMirror.WarehouseCodeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with WarehouseCodeID provided...
                    NewWarehouseCodeCommand(SelectedWarehouseCode.WarehouseCodeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseCode.WarehouseCodeID = SelectedWarehouseCodeMirror.WarehouseCodeID;
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
                    _serviceAgent.CommitWarehouseCodeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseCode.WarehouseCodeID = SelectedWarehouseCodeMirror.WarehouseCodeID;
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
        public static object GetPropertyValue(this WarehouseCode myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseCode).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyCode(this WarehouseCode myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseCode).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this WarehouseCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(WarehouseCode).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}