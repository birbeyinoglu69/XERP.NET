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
    public class TypeMaintenanceViewModel : ViewModelBase<TypeMaintenanceViewModel>
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
        public TypeMaintenanceViewModel()
        { }

        public TypeMaintenanceViewModel(ISecurityGroupServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            SecurityGroupTypeList = new BindingList<SecurityGroupType>();
            //disable new row feature...
            SecurityGroupTypeList.AllowNew = false;

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
            //SecurityGroupTypeColumnMetaDataList = new List<ColumnMetaData>();
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

        private string _securityGroupTypeListCount;
        public string SecurityGroupTypeListCount
        {
            get { return _securityGroupTypeListCount; }
            set
            {
                _securityGroupTypeListCount = value;
                NotifyPropertyChanged(m => m.SecurityGroupTypeListCount);
            }
        }

        private BindingList<SecurityGroupType> _securityGroupTypeList;
        public BindingList<SecurityGroupType> SecurityGroupTypeList
        {
            get
            {
                SecurityGroupTypeListCount = _securityGroupTypeList.Count.ToString();
                if (_securityGroupTypeList.Count > 0)
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
                return _securityGroupTypeList;
            }
            set
            {
                _securityGroupTypeList = value;
                NotifyPropertyChanged(m => m.SecurityGroupTypeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private SecurityGroupType _selectedSecurityGroupTypeMirror;
        public SecurityGroupType SelectedSecurityGroupTypeMirror
        {
            get { return _selectedSecurityGroupTypeMirror; }
            set { _selectedSecurityGroupTypeMirror = value; }
        }

        private System.Collections.IList _selectedSecurityGroupTypeList;
        public System.Collections.IList SelectedSecurityGroupTypeList
        {
            get { return _selectedSecurityGroupTypeList; }
            set
            {
                if (_selectedSecurityGroupType != value)
                {
                    _selectedSecurityGroupTypeList = value;
                    NotifyPropertyChanged(m => m.SelectedSecurityGroupTypeList);
                }
            }
        }

        private SecurityGroupType _selectedSecurityGroupType;
        public SecurityGroupType SelectedSecurityGroupType
        {
            get
            {
                return _selectedSecurityGroupType;
            }
            set
            {
                if (_selectedSecurityGroupType != value)
                {
                    _selectedSecurityGroupType = value;
                    //set the mirrored SelectedSecurityGroupType to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedSecurityGroupTypeMirror = new SecurityGroupType();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedSecurityGroupType.GetType().GetProperties())
                        {
                            SelectedSecurityGroupTypeMirror.SetPropertyValue(prop.Name, SelectedSecurityGroupType.GetPropertyValue(prop.Name));
                        }
                        SelectedSecurityGroupTypeMirror.SecurityGroupTypeID = _selectedSecurityGroupType.SecurityGroupTypeID;
                        NotifyPropertyChanged(m => m.SelectedSecurityGroupType);

                        SelectedSecurityGroupType.PropertyChanged += new PropertyChangedEventHandler(SelectedSecurityGroupType_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _securityGroupTypeColumnMetaDataList;
        public List<ColumnMetaData> SecurityGroupTypeColumnMetaDataList
        {
            get { return _securityGroupTypeColumnMetaDataList; }
            set
            {
                _securityGroupTypeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.SecurityGroupTypeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _securityGroupTypeMaxFieldValueDictionary;
        public Dictionary<string, int> SecurityGroupTypeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_securityGroupTypeMaxFieldValueDictionary != null)
                {
                    return _securityGroupTypeMaxFieldValueDictionary;
                }
                _securityGroupTypeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("SecurityGroupTypes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _securityGroupTypeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _securityGroupTypeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedSecurityGroupType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "SecurityGroupTypeID")
            {//make sure it is has changed...
                if (SelectedSecurityGroupTypeMirror.SecurityGroupTypeID != SelectedSecurityGroupType.SecurityGroupTypeID)
                {
                    //if their are no records it is a key change
                    if (SecurityGroupTypeList != null && SecurityGroupTypeList.Count == 0
                        && SelectedSecurityGroupType != null && !string.IsNullOrEmpty(SelectedSecurityGroupType.SecurityGroupTypeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetSecurityGroupTypeState(SelectedSecurityGroupType);

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

            object propertyChangedValue = SelectedSecurityGroupType.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedSecurityGroupTypeMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedSecurityGroupType.GetPropertyType(e.PropertyName);
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
                    Update(SelectedSecurityGroupType);
                    //set the mirrored objects field...
                    SelectedSecurityGroupTypeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedSecurityGroupType.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedSecurityGroupType.SecurityGroupTypeID, out errorMessage))
            {
                //check to see if key is part of the current securityGroupTypelist...
                SecurityGroupType query = SecurityGroupTypeList.Where(securityGroupType => securityGroupType.SecurityGroupTypeID == SelectedSecurityGroupType.SecurityGroupTypeID &&
                                                        securityGroupType.AutoID != SelectedSecurityGroupType.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected securityGroupType...
                    SelectedSecurityGroupType = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                SecurityGroupTypeList = GetSecurityGroupTypeByID(SelectedSecurityGroupType.SecurityGroupTypeID, ClientSessionSingleton.Instance.CompanyID);
                if (SecurityGroupTypeList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedSecurityGroupType.SecurityGroupTypeID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedSecurityGroupType = SecurityGroupTypeList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedSecurityGroupType.SecurityGroupTypeID != SelectedSecurityGroupTypeMirror.SecurityGroupTypeID)
                {
                    SelectedSecurityGroupType.SecurityGroupTypeID = SelectedSecurityGroupTypeMirror.SecurityGroupTypeID;
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
            foreach (SecurityGroupType securityGroupType in SecurityGroupTypeList)
            {
                EntityStates entityState = GetSecurityGroupTypeState(securityGroupType);
                if (entityState == EntityStates.Modified ||
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(securityGroupType, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(securityGroupType.Type, out errorMessage) == false)
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
                case "SecurityGroupTypeID":
                    rBool = NewKeyIsValid(SelectedSecurityGroupType, out errorMessage);
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

        private bool NewKeyIsValid(SecurityGroupType securityGroupType, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(securityGroupType.SecurityGroupTypeID, out errorMessage) == false)
            {
                return false;
            }

            if (SecurityGroupTypeExists(securityGroupType.SecurityGroupTypeID.ToString(), ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "SecurityGroupTypeID " + securityGroupType.SecurityGroupTypeID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object securityGroupTypeID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)securityGroupTypeID))
            {
                errorMessage = "SecurityGroupTypeID Is Required...";
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

        private EntityStates GetSecurityGroupTypeState(SecurityGroupType securityGroupType)
        {
            return _serviceAgent.GetSecurityGroupTypeEntityState(securityGroupType);
        }

        #region SecurityGroupType CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedSecurityGroupType.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (SecurityGroupType securityGroupType in SecurityGroupTypeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (securityGroupType.AutoID > 0)
                {
                    autoIDs = autoIDs + securityGroupType.AutoID.ToString() + ",";
                }
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                SecurityGroupTypeList = new BindingList<SecurityGroupType>(_serviceAgent.RefreshSecurityGroupType(autoIDs).ToList());
                SelectedSecurityGroupType = (from q in SecurityGroupTypeList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<SecurityGroupType> GetSecurityGroupTypes(string companyID)
        {
            BindingList<SecurityGroupType> securityGroupTypeList = new BindingList<SecurityGroupType>(_serviceAgent.GetSecurityGroupTypes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return securityGroupTypeList;
        }

        private BindingList<SecurityGroupType> GetSecurityGroupTypes(SecurityGroupType securityGroupType, string companyID)
        {
            BindingList<SecurityGroupType> securityGroupTypeList = new BindingList<SecurityGroupType>(_serviceAgent.GetSecurityGroupTypes(securityGroupType, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return securityGroupTypeList;
        }

        private BindingList<SecurityGroupType> GetSecurityGroupTypeByID(string securityGroupTypeID, string companyID)
        {
            BindingList<SecurityGroupType> securityGroupTypeList = new BindingList<SecurityGroupType>(_serviceAgent.GetSecurityGroupTypeByID(securityGroupTypeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return securityGroupTypeList;
        }

        private bool SecurityGroupTypeExists(string securityGroupTypeID, string companyID)
        {
            return _serviceAgent.SecurityGroupTypeExists(securityGroupTypeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(SecurityGroupType securityGroupType)
        {
            _serviceAgent.UpdateSecurityGroupTypeRepository(securityGroupType);
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
            _serviceAgent.CommitSecurityGroupTypeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(SecurityGroupType securityGroupType)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromSecurityGroupTypeRepository(securityGroupType);
            return true;
        }

        private bool NewSecurityGroupType(string securityGroupTypeID)
        {
            SecurityGroupType securityGroupType = new SecurityGroupType();
            securityGroupType.SecurityGroupTypeID = securityGroupTypeID;
            securityGroupType.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            SecurityGroupTypeList.Add(securityGroupType);
            _serviceAgent.AddToSecurityGroupTypeRepository(securityGroupType);
            SelectedSecurityGroupType = SecurityGroupTypeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion SecurityGroupType CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedSecurityGroupType = new SecurityGroupType();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            SecurityGroupTypeList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                SecurityGroupTypeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewSecurityGroupTypeCommand(""); //this will generate a new securityGroupType and set it as the selected securityGroupType...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedSecurityGroupType.SetPropertyValue(SecurityGroupTypeColumnMetaDataList[i].Name, columnValue);
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
            if (GetSecurityGroupTypeState(SelectedSecurityGroupType) != EntityStates.Detached)
            {
                if (Update(SelectedSecurityGroupType))
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
            for (int j = SelectedSecurityGroupTypeList.Count - 1; j >= 0; j--)
            {
                SecurityGroupType securityGroupType = (SecurityGroupType)SelectedSecurityGroupTypeList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = SecurityGroupTypeList.IndexOf(securityGroupType) - SelectedSecurityGroupTypeList.Count;
                }

                Delete(securityGroupType);
                SecurityGroupTypeList.Remove(securityGroupType);
            }

            if (SecurityGroupTypeList != null && SecurityGroupTypeList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedSecurityGroupType = SecurityGroupTypeList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }
        }

        public void NewSecurityGroupTypeCommand()
        {
            NewSecurityGroupType("");
            AllowCommit = false;
        }

        public void NewSecurityGroupTypeCommand(string securityGroupTypeID)
        {
            NewSecurityGroupType(securityGroupTypeID);
            if (string.IsNullOrEmpty(securityGroupTypeID))
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
            RegisterToReceiveMessages<BindingList<SecurityGroupType>>(MessageTokens.SecurityGroupTypeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<SecurityGroupType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SecurityGroupTypeList = e.Data;
                SelectedSecurityGroupType = SecurityGroupTypeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<SecurityGroupType>>(MessageTokens.SecurityGroupTypeSearchToken.ToString(), OnSearchResult);
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
                    SelectedSecurityGroupType.SecurityGroupTypeID = SelectedSecurityGroupTypeMirror.SecurityGroupTypeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with SecurityGroupTypeID provided...
                    NewSecurityGroupTypeCommand(SelectedSecurityGroupType.SecurityGroupTypeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSecurityGroupType.SecurityGroupTypeID = SelectedSecurityGroupTypeMirror.SecurityGroupTypeID;
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
                    _serviceAgent.CommitSecurityGroupTypeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSecurityGroupType.SecurityGroupTypeID = SelectedSecurityGroupTypeMirror.SecurityGroupTypeID;
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
        public static object GetPropertyValue(this SecurityGroupType myObj, string propertyName)
        {
            var propInfo = typeof(SecurityGroupType).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyType(this SecurityGroupType myObj, string propertyName)
        {
            var propInfo = typeof(SecurityGroupType).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this SecurityGroupType myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(SecurityGroupType).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}