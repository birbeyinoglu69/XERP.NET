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
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();

        private ISystemUserServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public CodeMaintenanceViewModel()
        { }

        public CodeMaintenanceViewModel(ISystemUserServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            SystemUserCodeList = new BindingList<SystemUserCode>();
            //disable new row feature...
            SystemUserCodeList.AllowNew = false;

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
            //SystemUserCodeColumnMetaDataList = new List<ColumnMetaData>();
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

        private string _systemUserCodeListCount;
        public string SystemUserCodeListCount
        {
            get { return _systemUserCodeListCount; }
            set
            {
                _systemUserCodeListCount = value;
                NotifyPropertyChanged(m => m.SystemUserCodeListCount);
            }
        }

        private BindingList<SystemUserCode> _systemUserCodeList;
        public BindingList<SystemUserCode> SystemUserCodeList
        {
            get
            {
                SystemUserCodeListCount = _systemUserCodeList.Count.ToString();
                if (_systemUserCodeList.Count > 0)
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
                return _systemUserCodeList;
            }
            set
            {
                _systemUserCodeList = value;
                NotifyPropertyChanged(m => m.SystemUserCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private SystemUserCode _selectedSystemUserCodeMirror;
        public SystemUserCode SelectedSystemUserCodeMirror
        {
            get { return _selectedSystemUserCodeMirror; }
            set { _selectedSystemUserCodeMirror = value; }
        }

        private System.Collections.IList _selectedSystemUserCodeList;
        public System.Collections.IList SelectedSystemUserCodeList
        {
            get { return _selectedSystemUserCodeList; }
            set
            {
                if (_selectedSystemUserCode != value)
                {
                    _selectedSystemUserCodeList = value;
                    NotifyPropertyChanged(m => m.SelectedSystemUserCodeList);
                }
            }
        }

        private SystemUserCode _selectedSystemUserCode;
        public SystemUserCode SelectedSystemUserCode
        {
            get
            {
                return _selectedSystemUserCode;
            }
            set
            {
                if (_selectedSystemUserCode != value)
                {
                    _selectedSystemUserCode = value;
                    //set the mirrored SelectedSystemUserCode to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedSystemUserCodeMirror = new SystemUserCode();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedSystemUserCode.GetType().GetProperties())
                        {
                            SelectedSystemUserCodeMirror.SetPropertyValue(prop.Name, SelectedSystemUserCode.GetPropertyValue(prop.Name));
                        }
                        SelectedSystemUserCodeMirror.SystemUserCodeID = _selectedSystemUserCode.SystemUserCodeID;
                        NotifyPropertyChanged(m => m.SelectedSystemUserCode);

                        SelectedSystemUserCode.PropertyChanged += new PropertyChangedEventHandler(SelectedSystemUserCode_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _systemUserCodeColumnMetaDataList;
        public List<ColumnMetaData> SystemUserCodeColumnMetaDataList
        {
            get { return _systemUserCodeColumnMetaDataList; }
            set
            {
                _systemUserCodeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.SystemUserCodeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _systemUserCodeMaxFieldValueDictionary;
        public Dictionary<string, int> SystemUserCodeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_systemUserCodeMaxFieldValueDictionary != null)
                {
                    return _systemUserCodeMaxFieldValueDictionary;
                }
                _systemUserCodeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("SystemUserCodes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _systemUserCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _systemUserCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedSystemUserCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "SystemUserCodeID")
            {//make sure it is has changed...
                if (SelectedSystemUserCodeMirror.SystemUserCodeID != SelectedSystemUserCode.SystemUserCodeID)
                {
                    //if their are no records it is a key change
                    if (SystemUserCodeList != null && SystemUserCodeList.Count == 0
                        && SelectedSystemUserCode != null && !string.IsNullOrEmpty(SelectedSystemUserCode.SystemUserCodeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetSystemUserCodeState(SelectedSystemUserCode);

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

            object propertyChangedValue = SelectedSystemUserCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedSystemUserCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyCode = SelectedSystemUserCode.GetPropertyCode(e.PropertyName);
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
                if (PropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyCode))
                {
                    Update(SelectedSystemUserCode);
                    //set the mirrored objects field...
                    SelectedSystemUserCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedSystemUserCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedSystemUserCode.SystemUserCodeID, out errorMessage))
            {
                //check to see if key is part of the current systemUserCodelist...
                SystemUserCode query = SystemUserCodeList.Where(systemUserCode => systemUserCode.SystemUserCodeID == SelectedSystemUserCode.SystemUserCodeID &&
                                                        systemUserCode.AutoID != SelectedSystemUserCode.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected systemUserCode...
                    SelectedSystemUserCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                SystemUserCodeList = GetSystemUserCodeByID(SelectedSystemUserCode.SystemUserCodeID);
                if (SystemUserCodeList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedSystemUserCode.SystemUserCodeID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedSystemUserCode = SystemUserCodeList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedSystemUserCode.SystemUserCodeID != SelectedSystemUserCodeMirror.SystemUserCodeID)
                {
                    SelectedSystemUserCode.SystemUserCodeID = SelectedSystemUserCodeMirror.SystemUserCodeID;
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
            foreach (SystemUserCode systemUserCode in SystemUserCodeList)
            {
                EntityStates entityState = GetSystemUserCodeState(systemUserCode);
                if (entityState == EntityStates.Modified ||
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(systemUserCode, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(systemUserCode.Code, out errorMessage) == false)
                {
                    rBool = false;
                }
            }
            //more bulk validation as required...
            //note bulk validation should coincide with property validation...
            //as we will not allow a commit until all data is valid...
            return rBool;
        }

        private bool PropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string code)
        {
            string errorMessage;
            bool rBool = true;
            switch (propertyName)
            {
                case "SystemUserCodeID":
                    rBool = NewKeyIsValid(SelectedSystemUserCode, out errorMessage);
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

        private bool NewKeyIsValid(SystemUserCode systemUserCode, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(systemUserCode.SystemUserCodeID, out errorMessage) == false)
            {
                return false;
            }
            if (SystemUserCodeExists(systemUserCode.SystemUserCodeID.ToString()))
            {
                errorMessage = "SystemUserCodeID " + systemUserCode.SystemUserCodeID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object systemUserCodeID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)systemUserCodeID))
            {
                errorMessage = "SystemUserCodeID Is Required...";
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

        private EntityStates GetSystemUserCodeState(SystemUserCode systemUserCode)
        {
            return _serviceAgent.GetSystemUserCodeEntityState(systemUserCode);
        }

        #region SystemUserCode CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedSystemUserCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (SystemUserCode systemUserCode in SystemUserCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (systemUserCode.AutoID > 0)
                {
                    autoIDs = autoIDs + systemUserCode.AutoID.ToString() + ",";
                }
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                SystemUserCodeList = new BindingList<SystemUserCode>(_serviceAgent.RefreshSystemUserCode(autoIDs).ToList());
                SelectedSystemUserCode = (from q in SystemUserCodeList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<SystemUserCode> GetSystemUserCodes()
        {
            BindingList<SystemUserCode> systemUserCodeList = new BindingList<SystemUserCode>(_serviceAgent.GetSystemUserCodes().ToList());
            Dirty = false;
            AllowCommit = false;
            return systemUserCodeList;
        }

        private BindingList<SystemUserCode> GetSystemUserCodes(SystemUserCode systemUserCode)
        {
            BindingList<SystemUserCode> systemUserCodeList = new BindingList<SystemUserCode>(_serviceAgent.GetSystemUserCodes(systemUserCode).ToList());
            Dirty = false;
            AllowCommit = false;
            return systemUserCodeList;
        }

        private BindingList<SystemUserCode> GetSystemUserCodeByID(string id)
        {
            BindingList<SystemUserCode> systemUserCodeList = new BindingList<SystemUserCode>(_serviceAgent.GetSystemUserCodeByID(id).ToList());
            Dirty = false;
            AllowCommit = false;
            return systemUserCodeList;
        }

        private bool SystemUserCodeExists(string systemUserCodeID)
        {
            return _serviceAgent.SystemUserCodeExists(systemUserCodeID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(SystemUserCode systemUserCode)
        {
            _serviceAgent.UpdateSystemUserCodeRepository(systemUserCode);
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
            _serviceAgent.CommitSystemUserCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(SystemUserCode systemUserCode)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromSystemUserCodeRepository(systemUserCode);
            return true;
        }

        private bool NewSystemUserCode(string systemUserCodeID)
        {
            SystemUserCode systemUserCode = new SystemUserCode();
            systemUserCode.SystemUserCodeID = systemUserCodeID;
            SystemUserCodeList.Add(systemUserCode);
            _serviceAgent.AddToSystemUserCodeRepository(systemUserCode);
            SelectedSystemUserCode = SystemUserCodeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion SystemUserCode CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedSystemUserCode = new SystemUserCode();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            SystemUserCodeList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                SystemUserCodeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewSystemUserCodeCommand(""); //this will generate a new systemUserCode and set it as the selected systemUserCode...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedSystemUserCode.SetPropertyValue(SystemUserCodeColumnMetaDataList[i].Name, columnValue);
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
            if (GetSystemUserCodeState(SelectedSystemUserCode) != EntityStates.Detached)
            {
                if (Update(SelectedSystemUserCode))
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
            for (int j = SelectedSystemUserCodeList.Count - 1; j >= 0; j--)
            {
                SystemUserCode systemUserCode = (SystemUserCode)SelectedSystemUserCodeList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = SystemUserCodeList.IndexOf(systemUserCode) - SelectedSystemUserCodeList.Count;
                }

                Delete(systemUserCode);
                SystemUserCodeList.Remove(systemUserCode);
            }

            if (SystemUserCodeList != null && SystemUserCodeList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedSystemUserCode = SystemUserCodeList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }
        }

        public void NewSystemUserCodeCommand()
        {
            NewSystemUserCode("");
            AllowCommit = false;
        }

        public void NewSystemUserCodeCommand(string systemUserCodeID)
        {
            NewSystemUserCode(systemUserCodeID);
            if (string.IsNullOrEmpty(systemUserCodeID))
            {//don't allow a save until a securityGroupCodeID is provided...
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
            RegisterToReceiveMessages<BindingList<SystemUserCode>>(MessageTokens.SystemUserCodeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<SystemUserCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SystemUserCodeList = e.Data;
                SelectedSystemUserCode = SystemUserCodeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<SystemUserCode>>(MessageTokens.SystemUserCodeSearchToken.ToString(), OnSearchResult);
        }

        #endregion Commands

        #region Completion Callbacks

        // TODO: Optionally add callback methods for async calls to the service agent

        #endregion Completion Callbacks

        #region Helpers
        //notify the view that a new record was created...
        //allows us to set focus to key field...
        private void NotifyNewRecordCreated()
        {
            Notify(NewRecordCreatedNotice, new NotificationEventArgs());
        }
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
                    SelectedSystemUserCode.SystemUserCodeID = SelectedSystemUserCodeMirror.SystemUserCodeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with SystemUserCodeID provided...
                    NewSystemUserCodeCommand(SelectedSystemUserCode.SystemUserCodeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSystemUserCode.SystemUserCodeID = SelectedSystemUserCodeMirror.SystemUserCodeID;
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
                    _serviceAgent.CommitSystemUserCodeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSystemUserCode.SystemUserCodeID = SelectedSystemUserCodeMirror.SystemUserCodeID;
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
        public static object GetPropertyValue(this SystemUserCode myObj, string propertyName)
        {
            var propInfo = typeof(SystemUserCode).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyCode(this SystemUserCode myObj, string propertyName)
        {
            var propInfo = typeof(SystemUserCode).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this SystemUserCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(SystemUserCode).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}