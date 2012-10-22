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

        private IExecutableProgramServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public TypeMaintenanceViewModel()
        { }

        public TypeMaintenanceViewModel(IExecutableProgramServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            ExecutableProgramTypeList = new BindingList<ExecutableProgramType>();
            //disable new row feature...
            ExecutableProgramTypeList.AllowNew = false;

            //make sure of session authentication...
            if (XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)
            {
                //make sure user has rights to UI...
                DoFormsAuthentication();
            }
            else
            {//User is not authenticated...
                RegisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
                FormIsEnabled = false;
            }

            AllowNew = true;
            AllowRowPaste = true;
            //ExecutableProgramTypeColumnMetaDataList = new List<ColumnMetaData>();
        }
        #endregion Initialization and Cleanup

        #region Authentication Logic
        private void DoFormsAuthentication()
        {
            //on log in session information is collected about the system user...
            //we need to make the system user is allowed access to this UI...
            if (ClientSessionSingleton.Instance.ExecutableProgramIDList.Contains(_globalProperties.ExecutableProgramName))
            {
                FormIsEnabled = true;
            }
            else
            {
                FormIsEnabled = false;
            }
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
            {
                FormIsEnabled = false;
            }
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
                {
                    return _executableProgramTypeMaxFieldValueDictionary;
                }
                _executableProgramTypeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("ExecutableProgramTypes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _executableProgramTypeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _executableProgramTypeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedExecutableProgramType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "ExecutableProgramTypeID")
            {//make sure it is has changed...
                if (SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID != SelectedExecutableProgramType.ExecutableProgramTypeID)
                {
                    //if their are no records it is a key change
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
                        if (Dirty && AllowCommit)
                        {//dirty record exists ask if save is required...
                            NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ChangeKeyLogic);
                        }
                        else
                        {
                            ChangeKeyLogic();
                        }
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
                if (prevPropertyValue == null)
                {//both values are null
                    objectsAreEqual = true;
                }
                else
                {//only one value is null
                    objectsAreEqual = false;
                }
            }
            else
            {
                if (prevPropertyValue == null)
                {//only one value is null
                    objectsAreEqual = false;
                }
                else //both values are not null use .Equals...
                {
                    objectsAreEqual = propertyChangedValue.Equals(prevPropertyValue);
                }
            }
            if (!objectsAreEqual)
            {
                //Here we do property change validation if false is returned we will reset the value
                //Back to its mirrored value and return out of the property change w/o updating the repository...
                if (PropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedExecutableProgramType);
                    //set the mirrored objects field...
                    SelectedExecutableProgramTypeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedExecutableProgramType.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    return;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods

        private void ChangeKeyLogic()
        {
            string errorMessage = "";
            if (KeyChangeIsValid(SelectedExecutableProgramType.ExecutableProgramTypeID, out errorMessage))
            {
                //check to see if key is part of the current executableProgramTypelist...
                ExecutableProgramType query = ExecutableProgramTypeList.Where(executableProgramType => executableProgramType.ExecutableProgramTypeID == SelectedExecutableProgramType.ExecutableProgramTypeID &&
                                                        executableProgramType.AutoID != SelectedExecutableProgramType.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected executableProgramType...
                    SelectedExecutableProgramType = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                ExecutableProgramTypeList = GetExecutableProgramTypeByID(SelectedExecutableProgramType.ExecutableProgramTypeID, ClientSessionSingleton.Instance.CompanyID);
                if (ExecutableProgramTypeList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedExecutableProgramType.ExecutableProgramTypeID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedExecutableProgramType = ExecutableProgramTypeList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedExecutableProgramType.ExecutableProgramTypeID != SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID)
                {
                    SelectedExecutableProgramType.ExecutableProgramTypeID = SelectedExecutableProgramTypeMirror.ExecutableProgramTypeID;
                }
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {
            string errorMessage = "";
            bool rBool = true;
            Dirty = false;
            foreach (ExecutableProgramType executableProgramType in ExecutableProgramTypeList)
            {
                EntityStates entityState = GetExecutableProgramTypeState(executableProgramType);
                if (entityState == EntityStates.Modified ||
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(executableProgramType, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(executableProgramType.Type, out errorMessage) == false)
                {
                    rBool = false;
                }
            }
            //more bulk validation as required...
            //note bulk validation should coincide with property validation...
            //as we will not allow a commit until all data is valid...
            return rBool;
        }

        private bool PropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage;
            bool rBool = true;
            switch (propertyName)
            {
                case "ExecutableProgramTypeID":
                    rBool = NewKeyIsValid(SelectedExecutableProgramType, out errorMessage);
                    if (rBool == false)
                    {
                        NotifyMessage(errorMessage);
                        return rBool;
                    }
                    break;
                case "Name":
                    rBool = NameIsValid(changedValue, out errorMessage);
                    if (rBool == false)
                    {
                        NotifyMessage(errorMessage);
                        return rBool;
                    }
                    break;
            }
            return true;
        }

        private bool NewKeyIsValid(ExecutableProgramType executableProgramType, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(executableProgramType.ExecutableProgramTypeID, out errorMessage) == false)
            {
                return false;
            }

            if (ExecutableProgramTypeExists(executableProgramType.ExecutableProgramTypeID.ToString(), ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "ExecutableProgramTypeID " + executableProgramType.ExecutableProgramTypeID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object executableProgramTypeID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)executableProgramTypeID))
            {
                errorMessage = "ExecutableProgramTypeID Is Required...";
                return false;
            }
            return true;
        }

        private bool NameIsValid(object value, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)value))
            {
                errorMessage = "Name Is Required...";
                return false;
            }
            return true;
        }
        #endregion ViewModel Logic Methods

        #region ServiceAgent Call Methods

        private EntityStates GetExecutableProgramTypeState(ExecutableProgramType executableProgramType)
        {
            return _serviceAgent.GetExecutableProgramTypeEntityState(executableProgramType);
        }

        #region ExecutableProgramType CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedExecutableProgramType.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (ExecutableProgramType executableProgramType in ExecutableProgramTypeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (executableProgramType.AutoID > 0)
                {
                    autoIDs = autoIDs + executableProgramType.AutoID.ToString() + ",";
                }
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
            BindingList<ExecutableProgramType> executableProgramTypeList = new BindingList<ExecutableProgramType>(_serviceAgent.GetExecutableProgramTypes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return executableProgramTypeList;
        }

        private BindingList<ExecutableProgramType> GetExecutableProgramTypes(ExecutableProgramType executableProgramType, string companyID)
        {
            BindingList<ExecutableProgramType> executableProgramTypeList = new BindingList<ExecutableProgramType>(_serviceAgent.GetExecutableProgramTypes(executableProgramType, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return executableProgramTypeList;
        }

        private BindingList<ExecutableProgramType> GetExecutableProgramTypeByID(string executableProgramTypeID, string companyID)
        {
            BindingList<ExecutableProgramType> executableProgramTypeList = new BindingList<ExecutableProgramType>(_serviceAgent.GetExecutableProgramTypeByID(executableProgramTypeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return executableProgramTypeList;
        }

        private bool ExecutableProgramTypeExists(string executableProgramTypeID, string companyID)
        {
            return _serviceAgent.ExecutableProgramTypeExists(executableProgramTypeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(ExecutableProgramType executableProgramType)
        {
            _serviceAgent.UpdateExecutableProgramTypeRepository(executableProgramType);
            Dirty = true;
            if (CommitIsAllowed())
            {
                AllowCommit = true;
                return true;
            }
            else
            {
                AllowCommit = false;
                return false;
            }
        }
        //commits repository to the db...
        private bool Commit()
        {
            _serviceAgent.CommitExecutableProgramTypeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(ExecutableProgramType executableProgramType)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromExecutableProgramTypeRepository(executableProgramType);
            return true;
        }

        private bool NewExecutableProgramType(string executableProgramTypeID)
        {
            ExecutableProgramType executableProgramType = new ExecutableProgramType();
            executableProgramType.ExecutableProgramTypeID = executableProgramTypeID;
            executableProgramType.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            ExecutableProgramTypeList.Add(executableProgramType);
            _serviceAgent.AddToExecutableProgramTypeRepository(executableProgramType);
            SelectedExecutableProgramType = ExecutableProgramTypeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion ExecutableProgramType CRUD
        #endregion ServiceAgent Call Methods

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
                    NewExecutableProgramTypeCommand(""); //this will generate a new executableProgramType and set it as the selected executableProgramType...
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
                {
                    Commit();
                }
                else
                {//this should not be hit but just in case we will catch it and then see 
                    //if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
                }
            }
        }
        public void RefreshCommand()
        {
            Refresh();
        }
        public void DeleteCommand()
        {
            int i = 0;
            bool isFirstDelete = true;
            for (int j = SelectedExecutableProgramTypeList.Count - 1; j >= 0; j--)
            {
                ExecutableProgramType executableProgramType = (ExecutableProgramType)SelectedExecutableProgramTypeList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = ExecutableProgramTypeList.IndexOf(executableProgramType) - SelectedExecutableProgramTypeList.Count;
                }

                Delete(executableProgramType);
                ExecutableProgramTypeList.Remove(executableProgramType);
            }

            if (ExecutableProgramTypeList != null && ExecutableProgramTypeList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedExecutableProgramType = ExecutableProgramTypeList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }
        }

        public void NewExecutableProgramTypeCommand()
        {
            NewExecutableProgramType("");
            AllowCommit = false;
        }

        public void NewExecutableProgramTypeCommand(string executableProgramTypeID)
        {
            NewExecutableProgramType(executableProgramTypeID);
            if (string.IsNullOrEmpty(executableProgramTypeID))
            {//don't allow a save until a TypeID is provided...
                AllowCommit = false;
            }
            {
                AllowCommit = CommitIsAllowed();
            }
        }

        public void ClearCommand()
        {
            if (Dirty && AllowCommit)
            {
                NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ClearLogic);
            }
            else
            {
                ClearLogic();
            }
        }

        public void SearchCommand()
        {
            if (Dirty && AllowCommit)
            {
                NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.SearchLogic);
            }
            else
            {
                SearchLogic();
            }
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

        #region Completion Callbacks

        // TODO: Optionally add callback methods for async calls to the service agent

        #endregion Completion Callbacks

        #region Helpers
        //notify the view that a new record was created...
        //allows us to set focus to key field...
        //private void NotifyNewRecordCreated()
        //{
        //    Notify(NewRecordCreatedNotice, new NotificationEventArgs());
        //}
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
        private void NotifyMessage(string message)
        {
            // Notify view of an error message w/o throwing an error...
            Notify(MessageNotice, new NotificationEventArgs<Exception>(message));
        }
        //Notify view to launch search...
        private void NotifySearch(string message)
        {
            Notify(SearchNotice, new NotificationEventArgs(message));
        }

        //Notify view new record may be required...
        private void NotifyNewRecordNeeded(string message)
        {
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
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyType(this ExecutableProgramType myObj, string propertyName)
        {
            var propInfo = typeof(ExecutableProgramType).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this ExecutableProgramType myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(ExecutableProgramType).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}