using System;
using System.Windows;
using System.Collections.ObjectModel;
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
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
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
        public MainMaintenanceViewModel()
        { }

        public MainMaintenanceViewModel(ISecurityGroupServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            
            SecurityGroupTypeList = GetSecurityGroupTypes();
            SecurityGroupCodeList = GetSecurityGroupCodes();
            SetAsEmptySelection();

            SecurityGroupList = new BindingList<SecurityGroup>();
            //disable new row feature...
            SecurityGroupList.AllowNew = false;
            
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
        }
        #endregion Initialization and Cleanup

        #region Authentication Logic
        private void DoFormsAuthentication()
        {
            //on log in session information is collected about the system user...
            //we need to make the system user is allowed access to this UI...
            if(ClientSessionSingleton.Instance.ExecutableProgramIDList.Contains(_globalProperties.ExecutableProgramName))
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
        public event EventHandler<NotificationEventArgs> TypeSearchNotice;
        public event EventHandler<NotificationEventArgs> CodeSearchNotice;
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

        private string _securityGroupListCount;
        public string SecurityGroupListCount
        {
            get { return _securityGroupListCount; }
            set
            {
                _securityGroupListCount = value;
                NotifyPropertyChanged(m => m.SecurityGroupListCount);
            }
        }
        
        private BindingList<SecurityGroup> _securityGroupList;
        public BindingList<SecurityGroup> SecurityGroupList
        {
            get
            {
                SecurityGroupListCount = _securityGroupList.Count.ToString();
                if (_securityGroupList.Count > 0)
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
                return _securityGroupList;
            }
            set
            {
                _securityGroupList = value;
                NotifyPropertyChanged(m => m.SecurityGroupList);
            }
        }

        private ObservableCollection<SecurityGroupType> _securityGroupTypeList;
        public ObservableCollection<SecurityGroupType> SecurityGroupTypeList
        {
            get { return _securityGroupTypeList; }
            set
            {
                _securityGroupTypeList = value;
                NotifyPropertyChanged(m => m.SecurityGroupTypeList);
            }
        }

        private ObservableCollection<SecurityGroupCode> _securityGroupCodeList;
        public ObservableCollection<SecurityGroupCode> SecurityGroupCodeList
        {
            get { return _securityGroupCodeList; }
            set
            {
                _securityGroupCodeList = value;
                NotifyPropertyChanged(m => m.SecurityGroupCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private SecurityGroup _selectedSecurityGroupMirror;
        public SecurityGroup SelectedSecurityGroupMirror
        {
            get { return _selectedSecurityGroupMirror; }
            set { _selectedSecurityGroupMirror = value; }
        }

        private System.Collections.IList _selectedSecurityGroupList;
        public System.Collections.IList SelectedSecurityGroupList
        {
            get { return _selectedSecurityGroupList; }
            set
            {
                if (_selectedSecurityGroup != value)
                {
                    _selectedSecurityGroupList = value;
                    NotifyPropertyChanged(m => m.SelectedSecurityGroupList);
                }  
            }
        }

        private SecurityGroup _selectedSecurityGroup;
        public SecurityGroup SelectedSecurityGroup
        {
            get 
            {
                return _selectedSecurityGroup; 
            }
            set
            {
                if (_selectedSecurityGroup != value)
                {
                    _selectedSecurityGroup = value;
                    //set the mirrored SelectedSecurityGroup to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedSecurityGroupMirror = new SecurityGroup();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedSecurityGroup.GetType().GetProperties())
                        {
                            SelectedSecurityGroupMirror.SetPropertyValue(prop.Name, SelectedSecurityGroup.GetPropertyValue(prop.Name));
                        }
                        SelectedSecurityGroupMirror.SecurityGroupID = _selectedSecurityGroup.SecurityGroupID;
                        NotifyPropertyChanged(m => m.SelectedSecurityGroup);
                        
                        SelectedSecurityGroup.PropertyChanged += new PropertyChangedEventHandler(SelectedSecurityGroup_PropertyChanged); 
                    }
                }
            }
        }

        private List<ColumnMetaData> _securityGroupColumnMetaDataList;
        public List<ColumnMetaData> SecurityGroupColumnMetaDataList
        {
            get { return _securityGroupColumnMetaDataList; }
            set 
            { 
                _securityGroupColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.SecurityGroupColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _securityGroupMaxFieldValueDictionary;
        public Dictionary<string, int> SecurityGroupMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_securityGroupMaxFieldValueDictionary != null)
                {
                    return _securityGroupMaxFieldValueDictionary;
                }
                _securityGroupMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("SecurityGroups");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _securityGroupMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _securityGroupMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedSecurityGroup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "SecurityGroupID")
            {//make sure it is has changed...
                if (SelectedSecurityGroupMirror.SecurityGroupID != SelectedSecurityGroup.SecurityGroupID)
                {
                    //if their are no records it is a key change
                    if (SecurityGroupList != null && SecurityGroupList.Count == 0
                        && SelectedSecurityGroup != null && !string.IsNullOrEmpty(SelectedSecurityGroup.SecurityGroupID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetSecurityGroupState(SelectedSecurityGroup);

                    if (entityState == EntityStates.Unchanged ||
                        entityState == EntityStates.Modified)
                    {//once a key is added it can not be modified...
                        if (Dirty  && AllowCommit)
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
            
            object propertyChangedValue = SelectedSecurityGroup.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedSecurityGroupMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedSecurityGroup.GetPropertyType(e.PropertyName);
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
                    Update(SelectedSecurityGroup);
                    //set the mirrored objects field...
                    SelectedSecurityGroupMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedSecurityGroup.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedSecurityGroup.SecurityGroupID, out errorMessage))
            {
                //check to see if key is part of the current securityGrouplist...
                SecurityGroup query = SecurityGroupList.Where(securityGroup => securityGroup.SecurityGroupID == SelectedSecurityGroup.SecurityGroupID &&
                                                        securityGroup.AutoID != SelectedSecurityGroup.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected securityGroup...
                    SelectedSecurityGroup = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                SecurityGroupList = GetSecurityGroupByID(SelectedSecurityGroup.SecurityGroupID);
                if (SecurityGroupList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedSecurityGroup.SecurityGroupID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedSecurityGroup = SecurityGroupList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedSecurityGroup.SecurityGroupID != SelectedSecurityGroupMirror.SecurityGroupID)
                {
                    SelectedSecurityGroup.SecurityGroupID = SelectedSecurityGroupMirror.SecurityGroupID;
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
            foreach (SecurityGroup securityGroup in SecurityGroupList)
            {
                EntityStates entityState = GetSecurityGroupState(securityGroup);
                if (entityState == EntityStates.Modified || 
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(securityGroup, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(securityGroup.Name, out errorMessage) == false)
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
                case "SecurityGroupID":
                    rBool = NewKeyIsValid(SelectedSecurityGroup, out errorMessage);
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

        private bool NewKeyIsValid(SecurityGroup securityGroup, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(securityGroup.SecurityGroupID, out errorMessage) == false)
            {
                return false;
            }
            if (SecurityGroupExists(securityGroup.SecurityGroupID.ToString()))
            {
                errorMessage = "SecurityGroupID " + securityGroup.SecurityGroupID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object securityGroupID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)securityGroupID))
            {
                errorMessage = "SecurityGroupID Is Required...";
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
        private ObservableCollection<SecurityGroupType> GetSecurityGroupTypes()
        {
            return new ObservableCollection<SecurityGroupType>(_serviceAgent.GetSecurityGroupTypesReadOnly().ToList());
        }

        private ObservableCollection<SecurityGroupCode> GetSecurityGroupCodes()
        {
            return new ObservableCollection<SecurityGroupCode>(_serviceAgent.GetSecurityGroupCodesReadOnly().ToList());
        }

        private EntityStates GetSecurityGroupState(SecurityGroup securityGroup)
        {
            return _serviceAgent.GetSecurityGroupEntityState(securityGroup);
        }

        #region SecurityGroup CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedSecurityGroup.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (SecurityGroup securityGroup in SecurityGroupList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (securityGroup.AutoID > 0)
                {
                    autoIDs = autoIDs + securityGroup.AutoID.ToString() + ",";
                }
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                SecurityGroupList = new BindingList<SecurityGroup>(_serviceAgent.RefreshSecurityGroup(autoIDs).ToList());
                SelectedSecurityGroup = (from q in SecurityGroupList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<SecurityGroup> GetSecurityGroups()
        {
            BindingList<SecurityGroup> securityGroupList = new BindingList<SecurityGroup>(_serviceAgent.GetSecurityGroups().ToList());
            Dirty = false;
            AllowCommit = false;
            return securityGroupList; 
        }

        private BindingList<SecurityGroup> GetSecurityGroups(SecurityGroup securityGroup)
        {
            BindingList<SecurityGroup> securityGroupList = new BindingList<SecurityGroup>(_serviceAgent.GetSecurityGroups(securityGroup).ToList());
            Dirty = false;
            AllowCommit = false;
            return securityGroupList;
        }

        private BindingList<SecurityGroup> GetSecurityGroupByID(string id)
        {
            BindingList<SecurityGroup> securityGroupList = new BindingList<SecurityGroup>(_serviceAgent.GetSecurityGroupByID(id).ToList());
            Dirty = false;
            AllowCommit = false;
            return securityGroupList; 
        }

        private bool SecurityGroupExists(string securityGroupID)
        {
            return _serviceAgent.SecurityGroupExists(securityGroupID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(SecurityGroup securityGroup)
        {
            _serviceAgent.UpdateSecurityGroupRepository(securityGroup);
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
            _serviceAgent.CommitSecurityGroupRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(SecurityGroup securityGroup)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromSecurityGroupRepository(securityGroup);
            return true;
        }

        private bool NewSecurityGroup(SecurityGroup securityGroup)
        {
            _serviceAgent.AddToSecurityGroupRepository(securityGroup);
            SelectedSecurityGroup = SecurityGroupList.LastOrDefault();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        #endregion SecurityGroup CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedSecurityGroup = new SecurityGroup();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            SecurityGroupList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                SecurityGroupColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewSecurityGroupCommand(); //this will generate a new securityGroup and set it as the selected securityGroup...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedSecurityGroup.SetPropertyValue(SecurityGroupColumnMetaDataList[i].Name, columnValue);
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
            if (GetSecurityGroupState(SelectedSecurityGroup) != EntityStates.Detached)
            {
                if (Update(SelectedSecurityGroup))
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
            for (int j = SelectedSecurityGroupList.Count - 1; j >= 0; j--)
            {
                SecurityGroup securityGroup = (SecurityGroup)SelectedSecurityGroupList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = SecurityGroupList.IndexOf(securityGroup) - SelectedSecurityGroupList.Count;
                }
                
                Delete(securityGroup);
                SecurityGroupList.Remove(securityGroup);
            }

            if (SecurityGroupList != null && SecurityGroupList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedSecurityGroup = SecurityGroupList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }  
        }

        public void NewSecurityGroupCommand()
        {
            SecurityGroup securityGroup = new SecurityGroup();
            SecurityGroupList.Add(securityGroup);
            NewSecurityGroup(securityGroup);
            AllowEdit = true;
            //don't allow a save until a securityGroupID is provided...
            AllowCommit = false;
            NotifyNewRecordCreated();
        }
        //overloaded to allow a securityGroupID to be provided...
        public void NewSecurityGroupCommand(string securityGroupID)
        {
            SecurityGroup securityGroup = new SecurityGroup();
            securityGroup.SecurityGroupID = securityGroupID;
            SecurityGroupList.Add(securityGroup);
            NewSecurityGroup(securityGroup);
            AllowEdit = true;
            AllowCommit = CommitIsAllowed();
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
            RegisterToReceiveMessages<BindingList<SecurityGroup>>(MessageTokens.SecurityGroupSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<SecurityGroup>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SecurityGroupList = e.Data;
                SelectedSecurityGroup = SecurityGroupList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<SecurityGroup>>(MessageTokens.SecurityGroupSearchToken.ToString(), OnSearchResult);
        }

        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<SecurityGroupType>>(MessageTokens.SecurityGroupTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<SecurityGroupType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SelectedSecurityGroup.SecurityGroupTypeID = e.Data.FirstOrDefault().SecurityGroupTypeID;
            }
            UnregisterToReceiveMessages<BindingList<SecurityGroupType>>(MessageTokens.SecurityGroupTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<SecurityGroupCode>>(MessageTokens.SecurityGroupCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<SecurityGroupCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SelectedSecurityGroup.SecurityGroupCodeID = e.Data.FirstOrDefault().SecurityGroupCodeID;
            }
            UnregisterToReceiveMessages<BindingList<SecurityGroupType>>(MessageTokens.SecurityGroupTypeSearchToken.ToString(), OnTypeSearchResult);
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

        private void NotifyTypeSearch(string message)
        {
            Notify(TypeSearchNotice, new NotificationEventArgs(message));
        }

        private void NotifyCodeSearch(string message)
        {
            Notify(CodeSearchNotice, new NotificationEventArgs(message));
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
                    SelectedSecurityGroup.SecurityGroupID = SelectedSecurityGroupMirror.SecurityGroupID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with SecurityGroupID provided...
                    NewSecurityGroupCommand(SelectedSecurityGroup.SecurityGroupID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSecurityGroup.SecurityGroupID = SelectedSecurityGroupMirror.SecurityGroupID;
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
                    _serviceAgent.CommitSecurityGroupRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSecurityGroup.SecurityGroupID = SelectedSecurityGroupMirror.SecurityGroupID;
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
        public static object GetPropertyValue(this SecurityGroup myObj, string propertyName)
        {
            var propInfo = typeof(SecurityGroup).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyType(this SecurityGroup myObj, string propertyName)
        {
            var propInfo = typeof(SecurityGroup).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this SecurityGroup myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(SecurityGroup).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}