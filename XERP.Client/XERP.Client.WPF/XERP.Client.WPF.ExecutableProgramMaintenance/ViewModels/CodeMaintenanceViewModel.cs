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
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;

namespace XERP.Client.WPF.ExecutableProgramMaintenance.ViewModels
{
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
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
        public CodeMaintenanceViewModel()
        { }

        public CodeMaintenanceViewModel(IExecutableProgramServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            ExecutableProgramCodeList = new BindingList<ExecutableProgramCode>();
            //disable new row feature...
            ExecutableProgramCodeList.AllowNew = false;

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
            //ExecutableProgramCodeColumnMetaDataList = new List<ColumnMetaData>();
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

        private string _executableProgramCodeListCount;
        public string ExecutableProgramCodeListCount
        {
            get { return _executableProgramCodeListCount; }
            set
            {
                _executableProgramCodeListCount = value;
                NotifyPropertyChanged(m => m.ExecutableProgramCodeListCount);
            }
        }

        private BindingList<ExecutableProgramCode> _executableProgramCodeList;
        public BindingList<ExecutableProgramCode> ExecutableProgramCodeList
        {
            get
            {
                ExecutableProgramCodeListCount = _executableProgramCodeList.Count.ToString();
                if (_executableProgramCodeList.Count > 0)
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
                return _executableProgramCodeList;
            }
            set
            {
                _executableProgramCodeList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private ExecutableProgramCode _selectedExecutableProgramCodeMirror;
        public ExecutableProgramCode SelectedExecutableProgramCodeMirror
        {
            get { return _selectedExecutableProgramCodeMirror; }
            set { _selectedExecutableProgramCodeMirror = value; }
        }

        private System.Collections.IList _selectedExecutableProgramCodeList;
        public System.Collections.IList SelectedExecutableProgramCodeList
        {
            get { return _selectedExecutableProgramCodeList; }
            set
            {
                if (_selectedExecutableProgramCode != value)
                {
                    _selectedExecutableProgramCodeList = value;
                    NotifyPropertyChanged(m => m.SelectedExecutableProgramCodeList);
                }
            }
        }

        private ExecutableProgramCode _selectedExecutableProgramCode;
        public ExecutableProgramCode SelectedExecutableProgramCode
        {
            get
            {
                return _selectedExecutableProgramCode;
            }
            set
            {
                if (_selectedExecutableProgramCode != value)
                {
                    _selectedExecutableProgramCode = value;
                    //set the mirrored SelectedExecutableProgramCode to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedExecutableProgramCodeMirror = new ExecutableProgramCode();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedExecutableProgramCode.GetType().GetProperties())
                        {
                            SelectedExecutableProgramCodeMirror.SetPropertyValue(prop.Name, SelectedExecutableProgramCode.GetPropertyValue(prop.Name));
                        }
                        SelectedExecutableProgramCodeMirror.ExecutableProgramCodeID = _selectedExecutableProgramCode.ExecutableProgramCodeID;
                        NotifyPropertyChanged(m => m.SelectedExecutableProgramCode);

                        SelectedExecutableProgramCode.PropertyChanged += new PropertyChangedEventHandler(SelectedExecutableProgramCode_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _executableProgramCodeColumnMetaDataList;
        public List<ColumnMetaData> ExecutableProgramCodeColumnMetaDataList
        {
            get { return _executableProgramCodeColumnMetaDataList; }
            set
            {
                _executableProgramCodeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramCodeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _executableProgramCodeMaxFieldValueDictionary;
        public Dictionary<string, int> ExecutableProgramCodeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_executableProgramCodeMaxFieldValueDictionary != null)
                {
                    return _executableProgramCodeMaxFieldValueDictionary;
                }
                _executableProgramCodeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("ExecutableProgramCodes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _executableProgramCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _executableProgramCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedExecutableProgramCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "ExecutableProgramCodeID")
            {//make sure it is has changed...
                if (SelectedExecutableProgramCodeMirror.ExecutableProgramCodeID != SelectedExecutableProgramCode.ExecutableProgramCodeID)
                {
                    //if their are no records it is a key change
                    if (ExecutableProgramCodeList != null && ExecutableProgramCodeList.Count == 0
                        && SelectedExecutableProgramCode != null && !string.IsNullOrEmpty(SelectedExecutableProgramCode.ExecutableProgramCodeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetExecutableProgramCodeState(SelectedExecutableProgramCode);

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

            object propertyChangedValue = SelectedExecutableProgramCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedExecutableProgramCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyCode = SelectedExecutableProgramCode.GetPropertyCode(e.PropertyName);
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
                    Update(SelectedExecutableProgramCode);
                    //set the mirrored objects field...
                    SelectedExecutableProgramCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedExecutableProgramCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedExecutableProgramCode.ExecutableProgramCodeID, out errorMessage))
            {
                //check to see if key is part of the current executableProgramCodelist...
                ExecutableProgramCode query = ExecutableProgramCodeList.Where(executableProgramCode => executableProgramCode.ExecutableProgramCodeID == SelectedExecutableProgramCode.ExecutableProgramCodeID &&
                                                        executableProgramCode.AutoID != SelectedExecutableProgramCode.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected executableProgramCode...
                    SelectedExecutableProgramCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                ExecutableProgramCodeList = GetExecutableProgramCodeByID(SelectedExecutableProgramCode.ExecutableProgramCodeID, ClientSessionSingleton.Instance.CompanyID);
                if (ExecutableProgramCodeList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedExecutableProgramCode.ExecutableProgramCodeID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedExecutableProgramCode = ExecutableProgramCodeList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedExecutableProgramCode.ExecutableProgramCodeID != SelectedExecutableProgramCodeMirror.ExecutableProgramCodeID)
                {
                    SelectedExecutableProgramCode.ExecutableProgramCodeID = SelectedExecutableProgramCodeMirror.ExecutableProgramCodeID;
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
            foreach (ExecutableProgramCode executableProgramCode in ExecutableProgramCodeList)
            {
                EntityStates entityState = GetExecutableProgramCodeState(executableProgramCode);
                if (entityState == EntityStates.Modified ||
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(executableProgramCode, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(executableProgramCode.Code, out errorMessage) == false)
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
                case "ExecutableProgramCodeID":
                    rBool = NewKeyIsValid(SelectedExecutableProgramCode, out errorMessage);
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

        private bool NewKeyIsValid(ExecutableProgramCode executableProgramCode, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(executableProgramCode.ExecutableProgramCodeID, out errorMessage) == false)
            {
                return false;
            }
            if (ExecutableProgramCodeExists(executableProgramCode.ExecutableProgramCodeID.ToString(), ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "ExecutableProgramCodeID " + executableProgramCode.ExecutableProgramCodeID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object executableProgramCodeID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)executableProgramCodeID))
            {
                errorMessage = "ExecutableProgramCodeID Is Required...";
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

        private EntityStates GetExecutableProgramCodeState(ExecutableProgramCode executableProgramCode)
        {
            return _serviceAgent.GetExecutableProgramCodeEntityState(executableProgramCode);
        }

        #region ExecutableProgramCode CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedExecutableProgramCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (ExecutableProgramCode executableProgramCode in ExecutableProgramCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (executableProgramCode.AutoID > 0)
                {
                    autoIDs = autoIDs + executableProgramCode.AutoID.ToString() + ",";
                }
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                ExecutableProgramCodeList = new BindingList<ExecutableProgramCode>(_serviceAgent.RefreshExecutableProgramCode(autoIDs).ToList());
                SelectedExecutableProgramCode = (from q in ExecutableProgramCodeList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<ExecutableProgramCode> GetExecutableProgramCodes(string companyID)
        {
            BindingList<ExecutableProgramCode> executableProgramCodeList = new BindingList<ExecutableProgramCode>(_serviceAgent.GetExecutableProgramCodes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return executableProgramCodeList;
        }

        private BindingList<ExecutableProgramCode> GetExecutableProgramCodes(ExecutableProgramCode executableProgramCode, string companyID)
        {
            BindingList<ExecutableProgramCode> executableProgramCodeList = new BindingList<ExecutableProgramCode>(_serviceAgent.GetExecutableProgramCodes(executableProgramCode, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return executableProgramCodeList;
        }

        private BindingList<ExecutableProgramCode> GetExecutableProgramCodeByID(string executableProgramCodeID, string companyID)
        {
            BindingList<ExecutableProgramCode> executableProgramCodeList = new BindingList<ExecutableProgramCode>(_serviceAgent.GetExecutableProgramCodeByID(executableProgramCodeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return executableProgramCodeList;
        }

        private bool ExecutableProgramCodeExists(string executableProgramCodeID, string companyID)
        {
            return _serviceAgent.ExecutableProgramCodeExists(executableProgramCodeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(ExecutableProgramCode executableProgramCode)
        {
            _serviceAgent.UpdateExecutableProgramCodeRepository(executableProgramCode);
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
            _serviceAgent.CommitExecutableProgramCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(ExecutableProgramCode executableProgramCode)
        {
            _serviceAgent.DeleteFromExecutableProgramCodeRepository(executableProgramCode);
            return true;
        }

        private bool NewExecutableProgramCode(string executableProgramCodeID)
        {
            ExecutableProgramCode executableProgramCode = new ExecutableProgramCode();
            executableProgramCode.ExecutableProgramCodeID = executableProgramCodeID;
            executableProgramCode.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            ExecutableProgramCodeList.Add(executableProgramCode);
            _serviceAgent.AddToExecutableProgramCodeRepository(executableProgramCode);
            SelectedExecutableProgramCode = ExecutableProgramCodeList.LastOrDefault();
            
            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion ExecutableProgramCode CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedExecutableProgramCode = new ExecutableProgramCode();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            ExecutableProgramCodeList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                ExecutableProgramCodeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewExecutableProgramCodeCommand(""); //this will generate a new executableProgramCode and set it as the selected executableProgramCode...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedExecutableProgramCode.SetPropertyValue(ExecutableProgramCodeColumnMetaDataList[i].Name, columnValue);
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
            if (GetExecutableProgramCodeState(SelectedExecutableProgramCode) != EntityStates.Detached)
            {
                if (Update(SelectedExecutableProgramCode))
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
            for (int j = SelectedExecutableProgramCodeList.Count - 1; j >= 0; j--)
            {
                ExecutableProgramCode executableProgramCode = (ExecutableProgramCode)SelectedExecutableProgramCodeList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = ExecutableProgramCodeList.IndexOf(executableProgramCode) - SelectedExecutableProgramCodeList.Count;
                }

                Delete(executableProgramCode);
                ExecutableProgramCodeList.Remove(executableProgramCode);
            }

            if (ExecutableProgramCodeList != null && ExecutableProgramCodeList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedExecutableProgramCode = ExecutableProgramCodeList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }
        }

        public void NewExecutableProgramCodeCommand()
        {
            NewExecutableProgramCode("");
            AllowCommit = false;
        }

        public void NewExecutableProgramCodeCommand(string executableProgramCodeID)
        {
            NewExecutableProgramCode(executableProgramCodeID);
            if (string.IsNullOrEmpty(executableProgramCodeID)) 
            {//don't allow a save until a executableProgramCodeID is provided...
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
            RegisterToReceiveMessages<BindingList<ExecutableProgramCode>>(MessageTokens.ExecutableProgramCodeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<ExecutableProgramCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                ExecutableProgramCodeList = e.Data;
                SelectedExecutableProgramCode = ExecutableProgramCodeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<ExecutableProgramCode>>(MessageTokens.ExecutableProgramCodeSearchToken.ToString(), OnSearchResult);
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
                    SelectedExecutableProgramCode.ExecutableProgramCodeID = SelectedExecutableProgramCodeMirror.ExecutableProgramCodeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with ExecutableProgramCodeID provided...
                    NewExecutableProgramCodeCommand(SelectedExecutableProgramCode.ExecutableProgramCodeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedExecutableProgramCode.ExecutableProgramCodeID = SelectedExecutableProgramCodeMirror.ExecutableProgramCodeID;
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
                    _serviceAgent.CommitExecutableProgramCodeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedExecutableProgramCode.ExecutableProgramCodeID = SelectedExecutableProgramCodeMirror.ExecutableProgramCodeID;
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
        public static object GetPropertyValue(this ExecutableProgramCode myObj, string propertyName)
        {
            var propInfo = typeof(ExecutableProgramCode).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyCode(this ExecutableProgramCode myObj, string propertyName)
        {
            var propInfo = typeof(ExecutableProgramCode).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this ExecutableProgramCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(ExecutableProgramCode).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}