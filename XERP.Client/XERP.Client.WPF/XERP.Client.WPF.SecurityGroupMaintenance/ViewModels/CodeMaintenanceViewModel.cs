using System;
using System.Windows;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;
using System.Collections.Generic;
// Toolkit namespace
using SimpleMvvmToolkit;
//XERP Namespaces
using XERP.Domain.SecurityGroupDomain.Services;
using XERP.Domain.SecurityGroupDomain.SecurityGroupDataService;
//required for extension methods...
using ExtensionMethods;
using XERP.Client.Models;

namespace XERP.Client.WPF.SecurityGroupMaintenance.ViewModels
{
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();

        private ISecurityGroupServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public CodeMaintenanceViewModel()
        { }

        public CodeMaintenanceViewModel(ISecurityGroupServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            SecurityGroupCodeList = new BindingList<SecurityGroupCode>();
            //disable new row feature...
            SecurityGroupCodeList.AllowNew = false;

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
            //SecurityGroupCodeColumnMetaDataList = new List<ColumnMetaData>();
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

        private string _securityGroupCodeListCount;
        public string SecurityGroupCodeListCount
        {
            get { return _securityGroupCodeListCount; }
            set
            {
                _securityGroupCodeListCount = value;
                NotifyPropertyChanged(m => m.SecurityGroupCodeListCount);
            }
        }

        private BindingList<SecurityGroupCode> _securityGroupCodeList;
        public BindingList<SecurityGroupCode> SecurityGroupCodeList
        {
            get
            {
                SecurityGroupCodeListCount = _securityGroupCodeList.Count.ToString();
                if (_securityGroupCodeList.Count > 0)
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
                return _securityGroupCodeList;
            }
            set
            {
                _securityGroupCodeList = value;
                NotifyPropertyChanged(m => m.SecurityGroupCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private SecurityGroupCode _selectedSecurityGroupCodeMirror;
        public SecurityGroupCode SelectedSecurityGroupCodeMirror
        {
            get { return _selectedSecurityGroupCodeMirror; }
            set { _selectedSecurityGroupCodeMirror = value; }
        }

        private System.Collections.IList _selectedSecurityGroupCodeList;
        public System.Collections.IList SelectedSecurityGroupCodeList
        {
            get { return _selectedSecurityGroupCodeList; }
            set
            {
                if (_selectedSecurityGroupCode != value)
                {
                    _selectedSecurityGroupCodeList = value;
                    NotifyPropertyChanged(m => m.SelectedSecurityGroupCodeList);
                }
            }
        }

        private SecurityGroupCode _selectedSecurityGroupCode;
        public SecurityGroupCode SelectedSecurityGroupCode
        {
            get
            {
                return _selectedSecurityGroupCode;
            }
            set
            {
                if (_selectedSecurityGroupCode != value)
                {
                    _selectedSecurityGroupCode = value;
                    //set the mirrored SelectedSecurityGroupCode to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedSecurityGroupCodeMirror = new SecurityGroupCode();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedSecurityGroupCode.GetType().GetProperties())
                        {
                            SelectedSecurityGroupCodeMirror.SetPropertyValue(prop.Name, SelectedSecurityGroupCode.GetPropertyValue(prop.Name));
                        }
                        SelectedSecurityGroupCodeMirror.SecurityGroupCodeID = _selectedSecurityGroupCode.SecurityGroupCodeID;
                        NotifyPropertyChanged(m => m.SelectedSecurityGroupCode);

                        SelectedSecurityGroupCode.PropertyChanged += new PropertyChangedEventHandler(SelectedSecurityGroupCode_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _securityGroupCodeColumnMetaDataList;
        public List<ColumnMetaData> SecurityGroupCodeColumnMetaDataList
        {
            get { return _securityGroupCodeColumnMetaDataList; }
            set
            {
                _securityGroupCodeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.SecurityGroupCodeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _securityGroupCodeMaxFieldValueDictionary;
        public Dictionary<string, int> SecurityGroupCodeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_securityGroupCodeMaxFieldValueDictionary != null)
                {
                    return _securityGroupCodeMaxFieldValueDictionary;
                }
                _securityGroupCodeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("SecurityGroupCodes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _securityGroupCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _securityGroupCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedSecurityGroupCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "SecurityGroupCodeID")
            {//make sure it is has changed...
                if (SelectedSecurityGroupCodeMirror.SecurityGroupCodeID != SelectedSecurityGroupCode.SecurityGroupCodeID)
                {
                    //if their are no records it is a key change
                    if (SecurityGroupCodeList != null && SecurityGroupCodeList.Count == 0
                        && SelectedSecurityGroupCode != null && !string.IsNullOrEmpty(SelectedSecurityGroupCode.SecurityGroupCodeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetSecurityGroupCodeState(SelectedSecurityGroupCode);

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

            object propertyChangedValue = SelectedSecurityGroupCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedSecurityGroupCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyCode = SelectedSecurityGroupCode.GetPropertyCode(e.PropertyName);
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
                    Update(SelectedSecurityGroupCode);
                    //set the mirrored objects field...
                    SelectedSecurityGroupCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedSecurityGroupCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedSecurityGroupCode.SecurityGroupCodeID, out errorMessage))
            {
                //check to see if key is part of the current securityGroupCodelist...
                SecurityGroupCode query = SecurityGroupCodeList.Where(securityGroupCode => securityGroupCode.SecurityGroupCodeID == SelectedSecurityGroupCode.SecurityGroupCodeID &&
                                                        securityGroupCode.AutoID != SelectedSecurityGroupCode.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected securityGroupCode...
                    SelectedSecurityGroupCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                SecurityGroupCodeList = GetSecurityGroupCodeByID(SelectedSecurityGroupCode.SecurityGroupCodeID, ClientSessionSingleton.Instance.CompanyID);
                if (SecurityGroupCodeList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedSecurityGroupCode.SecurityGroupCodeID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedSecurityGroupCode = SecurityGroupCodeList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedSecurityGroupCode.SecurityGroupCodeID != SelectedSecurityGroupCodeMirror.SecurityGroupCodeID)
                {
                    SelectedSecurityGroupCode.SecurityGroupCodeID = SelectedSecurityGroupCodeMirror.SecurityGroupCodeID;
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
            foreach (SecurityGroupCode securityGroupCode in SecurityGroupCodeList)
            {
                EntityStates entityState = GetSecurityGroupCodeState(securityGroupCode);
                if (entityState == EntityStates.Modified ||
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(securityGroupCode, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(securityGroupCode.Code, out errorMessage) == false)
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
                case "SecurityGroupCodeID":
                    rBool = NewKeyIsValid(SelectedSecurityGroupCode, out errorMessage);
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

        private bool NewKeyIsValid(SecurityGroupCode securityGroupCode, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(securityGroupCode.SecurityGroupCodeID, out errorMessage) == false)
            {
                return false;
            }
            if (SecurityGroupCodeExists(securityGroupCode.SecurityGroupCodeID.ToString(), ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "SecurityGroupCodeID " + securityGroupCode.SecurityGroupCodeID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object securityGroupCodeID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)securityGroupCodeID))
            {
                errorMessage = "SecurityGroupCodeID Is Required...";
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

        private EntityStates GetSecurityGroupCodeState(SecurityGroupCode securityGroupCode)
        {
            return _serviceAgent.GetSecurityGroupCodeEntityState(securityGroupCode);
        }

        #region SecurityGroupCode CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedSecurityGroupCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (SecurityGroupCode securityGroupCode in SecurityGroupCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (securityGroupCode.AutoID > 0)
                {
                    autoIDs = autoIDs + securityGroupCode.AutoID.ToString() + ",";
                }
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                SecurityGroupCodeList = new BindingList<SecurityGroupCode>(_serviceAgent.RefreshSecurityGroupCode(autoIDs).ToList());
                SelectedSecurityGroupCode = (from q in SecurityGroupCodeList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<SecurityGroupCode> GetSecurityGroupCodes(string companyID)
        {
            BindingList<SecurityGroupCode> securityGroupCodeList = new BindingList<SecurityGroupCode>(_serviceAgent.GetSecurityGroupCodes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return securityGroupCodeList;
        }

        private BindingList<SecurityGroupCode> GetSecurityGroupCodes(SecurityGroupCode securityGroupCode, string companyID)
        {
            BindingList<SecurityGroupCode> securityGroupCodeList = new BindingList<SecurityGroupCode>(_serviceAgent.GetSecurityGroupCodes(securityGroupCode, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return securityGroupCodeList;
        }

        private BindingList<SecurityGroupCode> GetSecurityGroupCodeByID(string securityGroupCodeID, string companyID)
        {
            BindingList<SecurityGroupCode> securityGroupCodeList = new BindingList<SecurityGroupCode>(_serviceAgent.GetSecurityGroupCodeByID(securityGroupCodeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return securityGroupCodeList;
        }

        private bool SecurityGroupCodeExists(string securityGroupCodeID, string companyID)
        {
            return _serviceAgent.SecurityGroupCodeExists(securityGroupCodeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(SecurityGroupCode securityGroupCode)
        {
            _serviceAgent.UpdateSecurityGroupCodeRepository(securityGroupCode);
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
            _serviceAgent.CommitSecurityGroupCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(SecurityGroupCode securityGroupCode)
        {
            _serviceAgent.DeleteFromSecurityGroupCodeRepository(securityGroupCode);
            return true;
        }

        private bool NewSecurityGroupCode(string securityGroupCodeID)
        {
            SecurityGroupCode securityGroupCode = new SecurityGroupCode();
            securityGroupCode.SecurityGroupCodeID = securityGroupCodeID;
            securityGroupCode.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            SecurityGroupCodeList.Add(securityGroupCode);
            _serviceAgent.AddToSecurityGroupCodeRepository(securityGroupCode);
            SelectedSecurityGroupCode = SecurityGroupCodeList.LastOrDefault();
            
            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion SecurityGroupCode CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedSecurityGroupCode = new SecurityGroupCode();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            SecurityGroupCodeList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                SecurityGroupCodeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewSecurityGroupCodeCommand(""); //this will generate a new securityGroupCode and set it as the selected securityGroupCode...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedSecurityGroupCode.SetPropertyValue(SecurityGroupCodeColumnMetaDataList[i].Name, columnValue);
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
            if (GetSecurityGroupCodeState(SelectedSecurityGroupCode) != EntityStates.Detached)
            {
                if (Update(SelectedSecurityGroupCode))
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
            for (int j = SelectedSecurityGroupCodeList.Count - 1; j >= 0; j--)
            {
                SecurityGroupCode securityGroupCode = (SecurityGroupCode)SelectedSecurityGroupCodeList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = SecurityGroupCodeList.IndexOf(securityGroupCode) - SelectedSecurityGroupCodeList.Count;
                }

                Delete(securityGroupCode);
                SecurityGroupCodeList.Remove(securityGroupCode);
            }

            if (SecurityGroupCodeList != null && SecurityGroupCodeList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedSecurityGroupCode = SecurityGroupCodeList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }
        }

        public void NewSecurityGroupCodeCommand()
        {
            NewSecurityGroupCode("");
            AllowCommit = false;
        }

        public void NewSecurityGroupCodeCommand(string securityGroupCodeID)
        {
            NewSecurityGroupCode(securityGroupCodeID);
            if (string.IsNullOrEmpty(securityGroupCodeID)) 
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
            RegisterToReceiveMessages<BindingList<SecurityGroupCode>>(MessageTokens.SecurityGroupCodeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<SecurityGroupCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SecurityGroupCodeList = e.Data;
                SelectedSecurityGroupCode = SecurityGroupCodeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<SecurityGroupCode>>(MessageTokens.SecurityGroupCodeSearchToken.ToString(), OnSearchResult);
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
                    SelectedSecurityGroupCode.SecurityGroupCodeID = SelectedSecurityGroupCodeMirror.SecurityGroupCodeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with SecurityGroupCodeID provided...
                    NewSecurityGroupCodeCommand(SelectedSecurityGroupCode.SecurityGroupCodeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSecurityGroupCode.SecurityGroupCodeID = SelectedSecurityGroupCodeMirror.SecurityGroupCodeID;
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
                    _serviceAgent.CommitSecurityGroupCodeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSecurityGroupCode.SecurityGroupCodeID = SelectedSecurityGroupCodeMirror.SecurityGroupCodeID;
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
        public static object GetPropertyValue(this SecurityGroupCode myObj, string propertyName)
        {
            var propInfo = typeof(SecurityGroupCode).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyCode(this SecurityGroupCode myObj, string propertyName)
        {
            var propInfo = typeof(SecurityGroupCode).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this SecurityGroupCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(SecurityGroupCode).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}