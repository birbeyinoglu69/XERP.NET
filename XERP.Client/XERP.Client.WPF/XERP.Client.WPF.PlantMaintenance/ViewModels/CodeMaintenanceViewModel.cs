using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using ExtensionMethods;
using SimpleMvvmToolkit;
using XERP.Client.Models;
using XERP.Domain.PlantDomain.PlantDataService;
using XERP.Domain.PlantDomain.Services;

namespace XERP.Client.WPF.PlantMaintenance.ViewModels
{
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newPlantCodeAutoId;

        private IPlantServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public CodeMaintenanceViewModel(){}

        public CodeMaintenanceViewModel(IPlantServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            PlantCodeList = new BindingList<PlantCode>();
            //disable new row feature...
            PlantCodeList.AllowNew = false;

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

        private string _plantCodeListCount;
        public string PlantCodeListCount
        {
            get { return _plantCodeListCount; }
            set
            {
                _plantCodeListCount = value;
                NotifyPropertyChanged(m => m.PlantCodeListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region Validation Properties
        private List<ColumnMetaData> _plantCodeColumnMetaDataList;
        public List<ColumnMetaData> PlantCodeColumnMetaDataList
        {
            get { return _plantCodeColumnMetaDataList; }
            set
            {
                _plantCodeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.PlantCodeColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _plantCodeMaxFieldValueDictionary;
        public Dictionary<string, int> PlantCodeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_plantCodeMaxFieldValueDictionary != null)
                {
                    return _plantCodeMaxFieldValueDictionary;
                }
                _plantCodeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("PlantCodes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _plantCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _plantCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties

        #region CRUD Properties
        private BindingList<PlantCode> _plantCodeList;
        public BindingList<PlantCode> PlantCodeList
        {
            get
            {
                PlantCodeListCount = _plantCodeList.Count.ToString();
                if (_plantCodeList.Count > 0)
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
                return _plantCodeList;
            }
            set
            {
                _plantCodeList = value;
                NotifyPropertyChanged(m => m.PlantCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private PlantCode _selectedPlantCodeMirror;
        public PlantCode SelectedPlantCodeMirror
        {
            get { return _selectedPlantCodeMirror; }
            set { _selectedPlantCodeMirror = value; }
        }

        private System.Collections.IList _selectedPlantCodeList;
        public System.Collections.IList SelectedPlantCodeList
        {
            get { return _selectedPlantCodeList; }
            set
            {
                if (_selectedPlantCode != value)
                {
                    _selectedPlantCodeList = value;
                    NotifyPropertyChanged(m => m.SelectedPlantCodeList);
                }
            }
        }

        private PlantCode _selectedPlantCode;
        public PlantCode SelectedPlantCode
        {
            get
            {
                return _selectedPlantCode;
            }
            set
            {
                if (_selectedPlantCode != value)
                {
                    _selectedPlantCode = value;
                    //set the mirrored SelectedPlantCode to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedPlantCodeMirror = new PlantCode();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedPlantCode.GetType().GetProperties())
                        {
                            SelectedPlantCodeMirror.SetPropertyValue(prop.Name, SelectedPlantCode.GetPropertyValue(prop.Name));
                        }
                        SelectedPlantCodeMirror.PlantCodeID = _selectedPlantCode.PlantCodeID;
                        NotifyPropertyChanged(m => m.SelectedPlantCode);

                        SelectedPlantCode.PropertyChanged += new PropertyChangedEventHandler(SelectedPlantCode_PropertyChanged);
                    }
                }
            }
        }
        #endregion CRUD Properties
        #endregion Properties

        #region ViewModel Property Events
        private void SelectedPlantCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "PlantCodeID")
            {//make sure it is has changed...
                if (SelectedPlantCodeMirror.PlantCodeID != SelectedPlantCode.PlantCodeID)
                {
                    //if their are no records it is a key change
                    if (PlantCodeList != null && PlantCodeList.Count == 0
                        && SelectedPlantCode != null && !string.IsNullOrEmpty(SelectedPlantCode.PlantCodeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetPlantCodeState(SelectedPlantCode);

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

            object propertyChangedValue = SelectedPlantCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedPlantCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyCode = SelectedPlantCode.GetPropertyCode(e.PropertyName);
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
                if (PlantCodePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyCode))
                {
                    Update(SelectedPlantCode);
                    SelectedPlantCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedPlantCodeMirror.IsValid = SelectedPlantCode.IsValid;
                    SelectedPlantCodeMirror.IsExpanded = SelectedPlantCode.IsExpanded;
                    SelectedPlantCodeMirror.NotValidMessage = SelectedPlantCode.NotValidMessage;

                }
                else//revert back to its previous value... 
                {
                    SelectedPlantCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedPlantCode.IsValid = SelectedPlantCodeMirror.IsValid;
                    SelectedPlantCode.IsExpanded = SelectedPlantCodeMirror.IsExpanded;
                    SelectedPlantCode.NotValidMessage = SelectedPlantCodeMirror.NotValidMessage;
                }

            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedPlantCode.PlantCodeID))
            {//check to see if key is part of the current companylist...
                PlantCode query = PlantCodeList.Where(company => company.PlantCodeID == SelectedPlantCode.PlantCodeID &&
                                                        company.AutoID != SelectedPlantCode.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back...
                    SelectedPlantCode.PlantCodeID = SelectedPlantCodeMirror.PlantCodeID;
                    //change to the newly selected item...
                    SelectedPlantCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                PlantCodeList = GetPlantCodeByID(SelectedPlantCode.PlantCodeID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (PlantCodeList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedPlantCode.PlantCodeID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedPlantCode = PlantCodeList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedPlantCode.PlantCodeID != SelectedPlantCodeMirror.PlantCodeID)
                    SelectedPlantCode.PlantCodeID = SelectedPlantCodeMirror.PlantCodeID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in PlantCodeList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedPlantCode = new PlantCode();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            PlantCodeList.Clear();
            SetAsEmptySelection();
        }

        private bool PlantCodePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "PlantCodeID":
                    rBool = PlantCodeIsValid(SelectedPlantCode, _companyValidationProperties.PlantCodeID, out errorMessage);
                    break;
                case "Name":
                    rBool = PlantCodeIsValid(SelectedPlantCode, _companyValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedPlantCode.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedPlantCode.IsValid = PlantCodeIsValid(SelectedPlantCode, out errorMessage);
                if (SelectedPlantCode.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedPlantCode.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _companyValidationProperties
        {//we list all fields that require validation...
            PlantCodeID,
            Name
        }

        //Object.Property Scope Validation...
        private bool PlantCodeIsValid(PlantCode item, _companyValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _companyValidationProperties.PlantCodeID:
                    //validate key
                    if (string.IsNullOrEmpty(item.PlantCodeID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetPlantCodeState(item);
                    if (entityState == EntityStates.Added && PlantCodeExists(item.PlantCodeID, ClientSessionSingleton.Instance.CompanyID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = PlantCodeList.Count(q => q.PlantCodeID == item.PlantCodeID);
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
        //PlantCode Object Scope Validation check the entire object for validity...
        private byte PlantCodeIsValid(PlantCode item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.PlantCodeID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetPlantCodeState(item);
            if (entityState == EntityStates.Added && PlantCodeExists(item.PlantCodeID, ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "Item AllReady Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = PlantCodeList.Count(q => q.PlantCodeID == item.PlantCodeID);
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
        private EntityStates GetPlantCodeState(PlantCode itemCode)
        {
            return _serviceAgent.GetPlantCodeEntityState(itemCode);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.PlantCodeRepositoryIsDirty();
        }

        #region PlantCode CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedPlantCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (PlantCode itemCode in PlantCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (itemCode.AutoID > 0)
                    autoIDs = autoIDs + itemCode.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                PlantCodeList = new BindingList<PlantCode>(_serviceAgent.RefreshPlantCode(autoIDs).ToList());
                SelectedPlantCode = (from q in PlantCodeList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<PlantCode> GetPlantCodes(string companyID)
        {
            BindingList<PlantCode> plantCodeList = new BindingList<PlantCode>(_serviceAgent.GetPlantCodes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return plantCodeList;
        }

        private BindingList<PlantCode> GetPlantCodes(PlantCode itemCode, string companyID)
        {
            BindingList<PlantCode> itemCodeList = new BindingList<PlantCode>(_serviceAgent.GetPlantCodes(itemCode, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemCodeList;
        }

        private BindingList<PlantCode> GetPlantCodeByID(string itemCodeID, string companyID)
        {
            BindingList<PlantCode> itemCodeList = new BindingList<PlantCode>(_serviceAgent.GetPlantCodeByID(itemCodeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemCodeList;
        }

        private bool PlantCodeExists(string itemCodeID, string companyID)
        {
            return _serviceAgent.PlantCodeExists(itemCodeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(PlantCode item)
        {
            _serviceAgent.UpdatePlantCodeRepository(item);
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
            var items = (from q in PlantCodeList where q.IsValid == 2 select q).ToList();
            foreach (PlantCode item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitPlantCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(PlantCode itemCode)
        {
            _serviceAgent.DeleteFromPlantCodeRepository(itemCode);
            return true;
        }

        private bool NewPlantCode(string id)
        {
            PlantCode item = new PlantCode();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newPlantCodeAutoId = _newPlantCodeAutoId - 1;
            item.AutoID = _newPlantCodeAutoId;
            item.PlantCodeID = id;
            item.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            item.IsValid = 1;
            item.NotValidMessage = "New Record Key Field/s Are Required.";
            PlantCodeList.Add(item);
            _serviceAgent.AddToPlantCodeRepository(item);
            SelectedPlantCode = PlantCodeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion PlantCode CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                PlantCodeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewPlantCodeCommand(""); //this will generate a new itemCode and set it as the selected itemCode...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedPlantCode.SetPropertyValue(PlantCodeColumnMetaDataList[i].Name, columnValue);
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
            if (GetPlantCodeState(SelectedPlantCode) != EntityStates.Detached)
            {
                if (Update(SelectedPlantCode))
                    Commit();
                else
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeletePlantCodeCommand()
        {
            try
            {//company is fk to 100's of tables deleting it can be tricky...
                int i = 0;
                int ii = 0;
                for (int j = SelectedPlantCodeList.Count - 1; j >= 0; j--)
                {
                    PlantCode item = (PlantCode)SelectedPlantCodeList[j];
                    //get Max Index...
                    i = PlantCodeList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    PlantCodeList.Remove(item);
                }

                if (PlantCodeList != null && PlantCodeList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= PlantCodeList.Count())
                        ii = PlantCodeList.Count - 1;

                    SelectedPlantCode = PlantCodeList[ii];
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
                NotifyMessage("PlantCode/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewPlantCodeCommand()
        {
            NewPlantCode("");
            AllowCommit = false;
        }

        public void NewPlantCodeCommand(string itemCodeID)
        {
            NewPlantCode(itemCodeID);
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
            RegisterToReceiveMessages<BindingList<PlantCode>>(MessageTokens.PlantCodeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<PlantCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                PlantCodeList = e.Data;
                SelectedPlantCode = PlantCodeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<PlantCode>>(MessageTokens.PlantCodeSearchToken.ToString(), OnSearchResult);
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
                    SelectedPlantCode.PlantCodeID = SelectedPlantCodeMirror.PlantCodeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with PlantCodeID provided...
                    NewPlantCodeCommand(SelectedPlantCode.PlantCodeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedPlantCode.PlantCodeID = SelectedPlantCodeMirror.PlantCodeID;
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
                    _serviceAgent.CommitPlantCodeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedPlantCode.PlantCodeID = SelectedPlantCodeMirror.PlantCodeID;
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
        public static object GetPropertyValue(this PlantCode myObj, string propertyName)
        {
            var propInfo = typeof(PlantCode).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyCode(this PlantCode myObj, string propertyName)
        {
            var propInfo = typeof(PlantCode).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this PlantCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(PlantCode).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}