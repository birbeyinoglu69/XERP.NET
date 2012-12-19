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

using XERP.Domain.MenuSecurityDomain.Services;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;

namespace XERP.Client.WPF.ExecutableProgramMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newExecutableProgramAutoId;

        private IExecutableProgramServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public MainMaintenanceViewModel(){}

        public void BuildDropDowns()
        {
            ExecutableProgramTypeList = BuildExecutableProgramTypeDropDown();
            ExecutableProgramCodeList = BuildExecutableProgramCodeDropDown();
        }

        public MainMaintenanceViewModel(IExecutableProgramServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            ExecutableProgramList = new BindingList<ExecutableProgram>();
            //disable new row feature...
            ExecutableProgramList.AllowNew = false;
            
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
            if(ClientSessionSingleton.Instance.ExecutableProgramIDList.Contains(_globalProperties.ExecutableProgramName))
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

        private string _executableProgramListCount;
        public string ExecutableProgramListCount
        {
            get { return _executableProgramListCount; }
            set
            {
                _executableProgramListCount = value;
                NotifyPropertyChanged(m => m.ExecutableProgramListCount);
            }
        }
        
        private BindingList<ExecutableProgram> _executableProgramList;
        public BindingList<ExecutableProgram> ExecutableProgramList
        {
            get
            {
                ExecutableProgramListCount = _executableProgramList.Count.ToString();
                if (_executableProgramList.Count > 0)
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
                return _executableProgramList;
            }
            set
            {
                _executableProgramList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramList);
            }
        }

        private ObservableCollection<ExecutableProgramType> _executableProgramTypeList;
        public ObservableCollection<ExecutableProgramType> ExecutableProgramTypeList
        {
            get { return _executableProgramTypeList; }
            set
            {
                _executableProgramTypeList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramTypeList);
            }
        }

        private ObservableCollection<ExecutableProgramCode> _executableProgramCodeList;
        public ObservableCollection<ExecutableProgramCode> ExecutableProgramCodeList
        {
            get { return _executableProgramCodeList; }
            set
            {
                _executableProgramCodeList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private ExecutableProgram _selectedExecutableProgramMirror;
        public ExecutableProgram SelectedExecutableProgramMirror
        {
            get { return _selectedExecutableProgramMirror; }
            set { _selectedExecutableProgramMirror = value; }
        }

        private System.Collections.IList _selectedExecutableProgramList;
        public System.Collections.IList SelectedExecutableProgramList
        {
            get { return _selectedExecutableProgramList; }
            set
            {
                if (_selectedExecutableProgram != value)
                {
                    _selectedExecutableProgramList = value;
                    NotifyPropertyChanged(m => m.SelectedExecutableProgramList);
                }  
            }
        }

        private ExecutableProgram _selectedExecutableProgram;
        public ExecutableProgram SelectedExecutableProgram
        {
            get 
            {
                return _selectedExecutableProgram; 
            }
            set
            {
                if (_selectedExecutableProgram != value)
                {
                    _selectedExecutableProgram = value;
                    //set the mirrored SelectedExecutableProgram to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedExecutableProgramMirror = new ExecutableProgram();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedExecutableProgram.GetType().GetProperties())
                        {
                            SelectedExecutableProgramMirror.SetPropertyValue(prop.Name, SelectedExecutableProgram.GetPropertyValue(prop.Name));
                        }
                        SelectedExecutableProgramMirror.ExecutableProgramID = _selectedExecutableProgram.ExecutableProgramID;
                        NotifyPropertyChanged(m => m.SelectedExecutableProgram);
                        
                        SelectedExecutableProgram.PropertyChanged += new PropertyChangedEventHandler(SelectedExecutableProgram_PropertyChanged); 
                    }
                }
            }
        }

        private List<ColumnMetaData> _executableProgramColumnMetaDataList;
        public List<ColumnMetaData> ExecutableProgramColumnMetaDataList
        {
            get { return _executableProgramColumnMetaDataList; }
            set 
            { 
                _executableProgramColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _executableProgramMaxFieldValueDictionary;
        public Dictionary<string, int> ExecutableProgramMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_executableProgramMaxFieldValueDictionary != null)
                    return _executableProgramMaxFieldValueDictionary;

                _executableProgramMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("ExecutablePrograms");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _executableProgramMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _executableProgramMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedExecutableProgram_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "ExecutableProgramID")
            {//make sure it is has changed...
                if (SelectedExecutableProgramMirror.ExecutableProgramID != SelectedExecutableProgram.ExecutableProgramID)
                {
                    //if their are no records it is a key change
                    if (ExecutableProgramList != null && ExecutableProgramList.Count == 0
                        && SelectedExecutableProgram != null && !string.IsNullOrEmpty(SelectedExecutableProgram.ExecutableProgramID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetExecutableProgramState(SelectedExecutableProgram);

                    if (entityState == EntityStates.Unchanged ||
                        entityState == EntityStates.Modified)
                    {//once a key is added it can not be modified...
                        if (Dirty  && AllowCommit)//dirty record exists ask if save is required...
                            NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ChangeKeyLogic);
                        else
                            ChangeKeyLogic();

                        return;
                    }
                }
            }//end KeyID logic...
            
            object propertyChangedValue = SelectedExecutableProgram.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedExecutableProgramMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedExecutableProgram.GetPropertyType(e.PropertyName);
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
                if (ExecutableProgramPropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedExecutableProgram);
                    //set the mirrored objects field...
                    SelectedExecutableProgramMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedExecutableProgramMirror.IsValid = SelectedExecutableProgram.IsValid;
                    SelectedExecutableProgramMirror.IsExpanded = SelectedExecutableProgram.IsExpanded;
                    SelectedExecutableProgramMirror.NotValidMessage = SelectedExecutableProgram.NotValidMessage;
                }
                else
                {
                    SelectedExecutableProgram.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedExecutableProgram.IsValid = SelectedExecutableProgramMirror.IsValid;
                    SelectedExecutableProgram.IsExpanded = SelectedExecutableProgramMirror.IsExpanded;
                    SelectedExecutableProgram.NotValidMessage = SelectedExecutableProgramMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedExecutableProgram.ExecutableProgramID))
            {//check to see if key is part of the current companylist...
                ExecutableProgram query = ExecutableProgramList.Where(company => company.ExecutableProgramID == SelectedExecutableProgram.ExecutableProgramID &&
                                                        company.AutoID != SelectedExecutableProgram.AutoID).SingleOrDefault();
                if (query != null)
                {//revert it back
                    SelectedExecutableProgram.ExecutableProgramID = SelectedExecutableProgramMirror.ExecutableProgramID;
                    //change to the newly selected company...
                    SelectedExecutableProgram = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                ExecutableProgramList = GetExecutableProgramByID(SelectedExecutableProgram.ExecutableProgramID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (ExecutableProgramList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedExecutableProgram.ExecutableProgramID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedExecutableProgram = ExecutableProgramList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedExecutableProgram.ExecutableProgramID != SelectedExecutableProgramMirror.ExecutableProgramID)
                    SelectedExecutableProgram.ExecutableProgramID = SelectedExecutableProgramMirror.ExecutableProgramID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in ExecutableProgramList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedExecutableProgram = new ExecutableProgram();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            ExecutableProgramList.Clear();
            SetAsEmptySelection();
        }

        private bool ExecutableProgramPropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "ExecutableProgramID":
                    rBool = ExecutableProgramIsValid(SelectedExecutableProgram, _executableProgramValidationProperties.ExecutableProgramID, out errorMessage);
                    break;
                case "Name":
                    rBool = ExecutableProgramIsValid(SelectedExecutableProgram, _executableProgramValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedExecutableProgram.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedExecutableProgram.IsValid = ExecutableProgramIsValid(SelectedExecutableProgram, out errorMessage);
                if (SelectedExecutableProgram.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedExecutableProgram.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _executableProgramValidationProperties
        {//we list all fields that require validation...
            ExecutableProgramID,
            Name
        }

        //Object.Property Scope Validation...
        private bool ExecutableProgramIsValid(ExecutableProgram item, _executableProgramValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _executableProgramValidationProperties.ExecutableProgramID:
                    //validate key
                    if (string.IsNullOrEmpty(item.ExecutableProgramID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetExecutableProgramState(item);
                    if (entityState == EntityStates.Added && ExecutableProgramExists(item.ExecutableProgramID))
                    {
                        errorMessage = "Item AllReady Exists...";
                        return false;
                    }
                    break;
                case _executableProgramValidationProperties.Name:
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
        //ExecutableProgram Object Scope Validation check the entire object for validity...
        private byte ExecutableProgramIsValid(ExecutableProgram item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.ExecutableProgramID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetExecutableProgramState(item);
            if (entityState == EntityStates.Added && ExecutableProgramExists(item.ExecutableProgramID))
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
        private ObservableCollection<ExecutableProgramType> BuildExecutableProgramTypeDropDown()
        {
            List<ExecutableProgramType> list = new List<ExecutableProgramType>();
            list = _serviceAgent.GetExecutableProgramTypes(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new ExecutableProgramType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<ExecutableProgramType>(list);
        }

        private ObservableCollection<ExecutableProgramCode> BuildExecutableProgramCodeDropDown()
        {
            List<ExecutableProgramCode> list = new List<ExecutableProgramCode>();
            list = _serviceAgent.GetExecutableProgramCodes(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new ExecutableProgramCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<ExecutableProgramCode>(list);
        }

        private EntityStates GetExecutableProgramState(ExecutableProgram item)
        {
            return _serviceAgent.GetExecutableProgramEntityState(item);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.ExecutableProgramRepositoryIsDirty();
        }

        #region ExecutableProgram CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedExecutableProgram.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (ExecutableProgram item in ExecutableProgramList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                ExecutableProgramList = new BindingList<ExecutableProgram>(_serviceAgent.RefreshExecutableProgram(autoIDs).ToList());
                SelectedExecutableProgram = (from q in ExecutableProgramList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<ExecutableProgram> GetExecutablePrograms(string companyID)
        {
            BindingList<ExecutableProgram> itemList = new BindingList<ExecutableProgram>(_serviceAgent.GetExecutablePrograms(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private BindingList<ExecutableProgram> GetExecutablePrograms(ExecutableProgram item, string companyID)
        {
            BindingList<ExecutableProgram> itemList = new BindingList<ExecutableProgram>(_serviceAgent.GetExecutablePrograms(item, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<ExecutableProgram> GetExecutableProgramByID(string itemID, string companyID)
        {
            BindingList<ExecutableProgram> itemList = new BindingList<ExecutableProgram>(_serviceAgent.GetExecutableProgramByID(itemID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private bool ExecutableProgramExists(string itemID)
        {
            return _serviceAgent.ExecutableProgramExists(itemID, ClientSessionSingleton.Instance.CompanyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(ExecutableProgram item)
        {
            _serviceAgent.UpdateExecutableProgramRepository(item);
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
            var items = (from q in ExecutableProgramList where q.IsValid == 2 select q).ToList();
            foreach (ExecutableProgram item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitExecutableProgramRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }


        private bool Delete(ExecutableProgram item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromExecutableProgramRepository(item);
            return true;
        }

        private bool NewExecutableProgram(string itemID)
        {
            ExecutableProgram newItem = new ExecutableProgram();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newExecutableProgramAutoId = _newExecutableProgramAutoId - 1;
            newItem.AutoID = _newExecutableProgramAutoId;
            newItem.ExecutableProgramID = itemID;
            newItem.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            newItem.IsValid = 1;
            newItem.NotValidMessage = "New Record Key Field/s Are Required.";
            ExecutableProgramList.Add(newItem);
            _serviceAgent.AddToExecutableProgramRepository(newItem);
            SelectedExecutableProgram = ExecutableProgramList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }


        #endregion ExecutableProgram CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                ExecutableProgramColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewExecutableProgramCommand(""); //this will generate a new item and set it as the selected item...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedExecutableProgram.SetPropertyValue(ExecutableProgramColumnMetaDataList[i].Name, columnValue);
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
            if (GetExecutableProgramState(SelectedExecutableProgram) != EntityStates.Detached)
            {
                if (Update(SelectedExecutableProgram))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }
        public void RefreshCommand()
        {
            Refresh();
        }
        public void DeleteExecutableProgramCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedExecutableProgramList.Count - 1; j >= 0; j--)
                {
                    ExecutableProgram item = (ExecutableProgram)SelectedExecutableProgramList[j];
                    //get Max Index...
                    i = ExecutableProgramList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    ExecutableProgramList.Remove(item);
                }

                if (ExecutableProgramList != null && ExecutableProgramList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= ExecutableProgramList.Count())
                        ii = ExecutableProgramList.Count - 1;

                    SelectedExecutableProgram = ExecutableProgramList[ii];
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
                NotifyMessage("ExecutableProgram/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewExecutableProgramCommand()
        {
            NewExecutableProgram("");
            AllowCommit = false;
        }

        public void NewExecutableProgramCommand(string itemID)
        {
            NewExecutableProgram(itemID);
            if (string.IsNullOrEmpty(itemID))//don't allow a save until a itemID is provided...
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
            RegisterToReceiveMessages<BindingList<ExecutableProgram>>(MessageTokens.ExecutableProgramSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<ExecutableProgram>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                ExecutableProgramList = e.Data;
                SelectedExecutableProgram = ExecutableProgramList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<ExecutableProgram>>(MessageTokens.ExecutableProgramSearchToken.ToString(), OnSearchResult);
        }

        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<ExecutableProgramType>>(MessageTokens.ExecutableProgramTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<ExecutableProgramType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedExecutableProgram.ExecutableProgramTypeID = e.Data.FirstOrDefault().ExecutableProgramTypeID;

            UnregisterToReceiveMessages<BindingList<ExecutableProgramType>>(MessageTokens.ExecutableProgramTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<ExecutableProgramCode>>(MessageTokens.ExecutableProgramCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<ExecutableProgramCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedExecutableProgram.ExecutableProgramCodeID = e.Data.FirstOrDefault().ExecutableProgramCodeID;

            UnregisterToReceiveMessages<BindingList<ExecutableProgramType>>(MessageTokens.ExecutableProgramTypeSearchToken.ToString(), OnTypeSearchResult);
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
                    SelectedExecutableProgram.ExecutableProgramID = SelectedExecutableProgramMirror.ExecutableProgramID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with ExecutableProgramID provided...
                    NewExecutableProgramCommand(SelectedExecutableProgram.ExecutableProgramID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedExecutableProgram.ExecutableProgramID = SelectedExecutableProgramMirror.ExecutableProgramID;
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
                    _serviceAgent.CommitExecutableProgramRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedExecutableProgram.ExecutableProgramID = SelectedExecutableProgramMirror.ExecutableProgramID;
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
    public static partial class XERPExtensions
    {
        public static object GetPropertyValue(this ExecutableProgram myObj, string propertyName)
        {
            var propInfo = typeof(ExecutableProgram).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this ExecutableProgram myObj, string propertyName)
        {
            var propInfo = typeof(ExecutableProgram).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this ExecutableProgram myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(ExecutableProgram).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}