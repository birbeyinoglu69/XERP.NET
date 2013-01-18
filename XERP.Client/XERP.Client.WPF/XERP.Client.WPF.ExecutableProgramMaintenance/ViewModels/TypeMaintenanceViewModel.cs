using System;
using System.Windows;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;
using System.Collections.Generic;
// Toolkit namespace
using SimpleMvvmToolkit;
//XERP Namespaces
using XERP.Domain.MenuSecurityDomain.Services;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
//required for extension methods...
using ExtensionMethods;
using XERP.Client.Models;

namespace XERP.Client.WPF.ExecutableProgramMaintenance.ViewModels
{
    public class TypeMaintenanceViewModel : ViewModelBase<TypeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newExecutableProgramTypeAutoId;

        private IExecutableProgramServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public TypeMaintenanceViewModel(){}

        public TypeMaintenanceViewModel(IExecutableProgramServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            ExecutableProgramTypeList = new BindingList<ExecutableProgramType>();
            //disable new row feature...
            ExecutableProgramTypeList.AllowNew = false;

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

        private string _executableProgramTypeListCount;
        public string ExecutableProgramTypeListCount
        {
            get { return _executableProgramTypeListCount; }
            set
            {
                _executableProgramTypeListCount = value;
                NotifyPropertyChanged(m => m.ExecutableProgramTypeListCount);
            }
        }
        #endregion General Form Function/State Properties
        private BindingList<ExecutableProgramType> _executableProgramTypeList;
        public BindingList<ExecutableProgramType> ExecutableProgramTypeList
        {
            get
            {
                ExecutableProgramTypeListCount = _executableProgramTypeList.Count.ToString();
                if (_executableProgramTypeList.Count > 0)
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
                return _executableProgramTypeList;
            }
            set
            {
                _executableProgramTypeList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramTypeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private ExecutableProgramType _selectedExecutableProgramTypeMirror;
        public ExecutableProgramType SelectedExecutableProgramTypeMirror
        {
            get { return _selectedExecutableProgramTypeMirror; }
            set { _selectedExecutableProgramTypeMirror = value; }
        }

        private System.Collections.IList _selectedExecutableProgramTypeList;
        public System.Collections.IList SelectedExecutableProgramTypeList
        {
            get { return _selectedExecutableProgramTypeList; }
            set
            {
                if (_selectedExecutableProgramType != value)
                {
                    _selectedExecutableProgramTypeList = value;
                    NotifyPropertyChanged(m => m.SelectedExecutableProgramTypeList);
                }
            }
        }

        private ExecutableProgramType _selectedExecutableProgramType;
        public ExecutableProgramType SelectedExecutableProgramType
        {
            get
            {
                return _selectedExecutableProgramType;
            }
            set
            {
                if (_selectedExecutableProgramType != value)
                {
                    _selectedExecutableProgramType = value;
                    //set the mirrored SelectedExecutableProgramType to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedExecutableProgramTypeMirror = new ExecutableProgramType();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedExecutableProgramType.GetType().GetProperties())
                        {
                            SelectedExecutableProgramTypeMirror.SetPropertyValue(prop.Name, SelectedExecutableProgramType.GetPropertyValue(prop.Name));
                        }
                        SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID = _selectedExecutableProgramType.ExecutableProgramTypeID;
                        NotifyPropertyChanged(m => m.SelectedExecutableProgramType);

                        SelectedExecutableProgramType.PropertyChanged += new PropertyChangedEventHandler(SelectedExecutableProgramType_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _executableProgramTypeColumnMetaDataList;
        public List<ColumnMetaData> ExecutableProgramTypeColumnMetaDataList
        {
            get { return _executableProgramTypeColumnMetaDataList; }
            set
            {
                _executableProgramTypeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramTypeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _executableProgramTypeMaxFieldValueDictionary;
        public Dictionary<string, int> ExecutableProgramTypeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_executableProgramTypeMaxFieldValueDictionary != null)
                    return _executableProgramTypeMaxFieldValueDictionary;

                _executableProgramTypeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("ExecutableProgramTypes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _executableProgramTypeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);

                }
                return _executableProgramTypeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedExecutableProgramType_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "ExecutableProgramTypeID")
            {//make sure it is has changed...
                if (SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID != SelectedExecutableProgramType.ExecutableProgramTypeID)
                {//if their are no records it is a key change
                    if (ExecutableProgramTypeList != null && ExecutableProgramTypeList.Count == 0
                        && SelectedExecutableProgramType != null && !string.IsNullOrEmpty(SelectedExecutableProgramType.ExecutableProgramTypeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetExecutableProgramTypeState(SelectedExecutableProgramType);

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

            object propertyChangedValue = SelectedExecutableProgramType.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedExecutableProgramTypeMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedExecutableProgramType.GetPropertyType(e.PropertyName);
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
                if (ExecutableProgramTypePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedExecutableProgramType);
                    //set the mirrored objects field...
                    SelectedExecutableProgramTypeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedExecutableProgramTypeMirror.IsValid = SelectedExecutableProgramType.IsValid;
                    SelectedExecutableProgramTypeMirror.IsExpanded = SelectedExecutableProgramType.IsExpanded;
                    SelectedExecutableProgramTypeMirror.NotValidMessage = SelectedExecutableProgramType.NotValidMessage;

                }
                else//revert back to its previous value... 
                {
                    SelectedExecutableProgramType.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedExecutableProgramType.IsValid = SelectedExecutableProgramTypeMirror.IsValid;
                    SelectedExecutableProgramType.IsExpanded = SelectedExecutableProgramTypeMirror.IsExpanded;
                    SelectedExecutableProgramType.NotValidMessage = SelectedExecutableProgramTypeMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedExecutableProgramType.ExecutableProgramTypeID))
            {//check to see if key is part of the current companylist...
                ExecutableProgramType query = ExecutableProgramTypeList.Where(company => company.ExecutableProgramTypeID == SelectedExecutableProgramType.ExecutableProgramTypeID &&
                                                        company.AutoID != SelectedExecutableProgramType.AutoID).SingleOrDefault();
                if (query != null)
                {//revert it back...
                    SelectedExecutableProgramType.ExecutableProgramTypeID = SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID;
                    //change to the newly selected company...
                    SelectedExecutableProgramType = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                ExecutableProgramTypeList = GetExecutableProgramTypeByID(SelectedExecutableProgramType.ExecutableProgramTypeID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (ExecutableProgramTypeList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedExecutableProgramType.ExecutableProgramTypeID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedExecutableProgramType = ExecutableProgramTypeList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedExecutableProgramType.ExecutableProgramTypeID != SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID)
                    SelectedExecutableProgramType.ExecutableProgramTypeID = SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in ExecutableProgramTypeList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedExecutableProgramType = new ExecutableProgramType();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            ExecutableProgramTypeList.Clear();
            SetAsEmptySelection();
        }

        private bool ExecutableProgramTypePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "ExecutableProgramTypeID":
                    rBool = ExecutableProgramTypeIsValid(SelectedExecutableProgramType, _companyValidationProperties.ExecutableProgramTypeID, out errorMessage);
                    break;
                case "Name":
                    rBool = ExecutableProgramTypeIsValid(SelectedExecutableProgramType, _companyValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedExecutableProgramType.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedExecutableProgramType.IsValid = ExecutableProgramTypeIsValid(SelectedExecutableProgramType, out errorMessage);
                if (SelectedExecutableProgramType.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedExecutableProgramType.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _companyValidationProperties
        {//we list all fields that require validation...
            ExecutableProgramTypeID,
            Name
        }

        //Object.Property Scope Validation...
        private bool ExecutableProgramTypeIsValid(ExecutableProgramType item, _companyValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _companyValidationProperties.ExecutableProgramTypeID:
                    //validate key
                    if (string.IsNullOrEmpty(item.ExecutableProgramTypeID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetExecutableProgramTypeState(item);
                    if (entityState == EntityStates.Added && ExecutableProgramTypeExists(item.ExecutableProgramTypeID, ClientSessionSingleton.Instance.CompanyID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = ExecutableProgramTypeList.Count(q => q.ExecutableProgramTypeID == item.ExecutableProgramTypeID);
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
        //ExecutableProgramType Object Scope Validation check the entire object for validity...
        private byte ExecutableProgramTypeIsValid(ExecutableProgramType item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.ExecutableProgramTypeID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetExecutableProgramTypeState(item);
            if (entityState == EntityStates.Added && ExecutableProgramTypeExists(item.ExecutableProgramTypeID, ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = ExecutableProgramTypeList.Count(q => q.ExecutableProgramTypeID == item.ExecutableProgramTypeID);
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

        private EntityStates GetExecutableProgramTypeState(ExecutableProgramType itemType)
        {
            return _serviceAgent.GetExecutableProgramTypeEntityState(itemType);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.ExecutableProgramTypeRepositoryIsDirty();
        }

        #region ExecutableProgramType CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedExecutableProgramType.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (ExecutableProgramType itemType in ExecutableProgramTypeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (itemType.AutoID > 0)
                    autoIDs = autoIDs + itemType.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                ExecutableProgramTypeList = new BindingList<ExecutableProgramType>(_serviceAgent.RefreshExecutableProgramType(autoIDs).ToList());
                SelectedExecutableProgramType = (from q in ExecutableProgramTypeList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<ExecutableProgramType> GetExecutableProgramTypes(string companyID)
        {
            BindingList<ExecutableProgramType> itemTypeList = new BindingList<ExecutableProgramType>(_serviceAgent.GetExecutableProgramTypes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemTypeList;
        }

        private BindingList<ExecutableProgramType> GetExecutableProgramTypes(ExecutableProgramType itemType, string companyID)
        {
            BindingList<ExecutableProgramType> itemTypeList = new BindingList<ExecutableProgramType>(_serviceAgent.GetExecutableProgramTypes(itemType, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemTypeList;
        }

        private BindingList<ExecutableProgramType> GetExecutableProgramTypeByID(string itemTypeID, string companyID)
        {
            BindingList<ExecutableProgramType> itemTypeList = new BindingList<ExecutableProgramType>(_serviceAgent.GetExecutableProgramTypeByID(itemTypeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemTypeList;
        }

        private bool ExecutableProgramTypeExists(string itemTypeID, string companyID)
        {
            return _serviceAgent.ExecutableProgramTypeExists(itemTypeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(ExecutableProgramType item)
        {
            _serviceAgent.UpdateExecutableProgramTypeRepository(item);
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
            var items = (from q in ExecutableProgramTypeList where q.IsValid == 2 select q).ToList();
            foreach (ExecutableProgramType item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitExecutableProgramTypeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }


        private bool Delete(ExecutableProgramType itemType)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromExecutableProgramTypeRepository(itemType);
            return true;
        }

        private bool NewExecutableProgramType(string id)
        {
            ExecutableProgramType item = new ExecutableProgramType();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newExecutableProgramTypeAutoId = _newExecutableProgramTypeAutoId - 1;
            item.AutoID = _newExecutableProgramTypeAutoId;
            item.ExecutableProgramTypeID = id;
            item.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            item.IsValid = 1;
            item.NotValidMessage = "New Record Key Field/s Are Required.";
            ExecutableProgramTypeList.Add(item);
            _serviceAgent.AddToExecutableProgramTypeRepository(item);
            SelectedExecutableProgramType = ExecutableProgramTypeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }


        #endregion ExecutableProgramType CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                ExecutableProgramTypeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewExecutableProgramTypeCommand(""); //this will generate a new itemType and set it as the selected itemType...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedExecutableProgramType.SetPropertyValue(ExecutableProgramTypeColumnMetaDataList[i].Name, columnValue);
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
            if (GetExecutableProgramTypeState(SelectedExecutableProgramType) != EntityStates.Detached)
            {
                if (Update(SelectedExecutableProgramType))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }
        public void RefreshCommand()
        {
            Refresh();
        }
        public void DeleteExecutableProgramTypeCommand()
        {
            try
            {//company is fk to 100's of tables deleting it can be tricky...
                int i = 0;
                int ii = 0;
                for (int j = SelectedExecutableProgramTypeList.Count - 1; j >= 0; j--)
                {
                    ExecutableProgramType item = (ExecutableProgramType)SelectedExecutableProgramTypeList[j];
                    //get Max Index...
                    i = ExecutableProgramTypeList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    ExecutableProgramTypeList.Remove(item);
                }

                if (ExecutableProgramTypeList != null && ExecutableProgramTypeList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= ExecutableProgramTypeList.Count())
                        ii = ExecutableProgramTypeList.Count - 1;

                    SelectedExecutableProgramType = ExecutableProgramTypeList[ii];
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
                NotifyMessage("ExecutableProgramType/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }


        public void NewExecutableProgramTypeCommand()
        {
            NewExecutableProgramType("");
            AllowCommit = false;
        }

        public void NewExecutableProgramTypeCommand(string itemTypeID)
        {
            NewExecutableProgramType(itemTypeID);
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
            RegisterToReceiveMessages<BindingList<ExecutableProgramType>>(MessageTokens.ExecutableProgramTypeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<ExecutableProgramType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                ExecutableProgramTypeList = e.Data;
                SelectedExecutableProgramType = ExecutableProgramTypeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<ExecutableProgramType>>(MessageTokens.ExecutableProgramTypeSearchToken.ToString(), OnSearchResult);
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
                    SelectedExecutableProgramType.ExecutableProgramTypeID = SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with ExecutableProgramTypeID provided...
                    NewExecutableProgramTypeCommand(SelectedExecutableProgramType.ExecutableProgramTypeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedExecutableProgramType.ExecutableProgramTypeID = SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID;
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
                    _serviceAgent.CommitExecutableProgramTypeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedExecutableProgramType.ExecutableProgramTypeID = SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID;
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
        public static object GetPropertyValue(this ExecutableProgramType myObj, string propertyName)
        {
            var propInfo = typeof(ExecutableProgramType).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this ExecutableProgramType myObj, string propertyName)
        {
            var propInfo = typeof(ExecutableProgramType).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this ExecutableProgramType myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(ExecutableProgramType).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}