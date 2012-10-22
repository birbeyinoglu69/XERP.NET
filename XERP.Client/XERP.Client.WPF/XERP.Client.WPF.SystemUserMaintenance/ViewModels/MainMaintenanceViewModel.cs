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
using XERP.Domain.SystemUserDomain.Services;
using XERP.Domain.SystemUserDomain.SystemUserDataService;
//required for extension methods...
using ExtensionMethods;
using XERP.Client.Models;

namespace XERP.Client.WPF.SystemUserMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
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
        public MainMaintenanceViewModel()
        { }

        public void BuildDropDowns()
        {
            SystemUserTypeList = BuildSystemUserTypeDropDown();
            SystemUserCodeList = BuildSystemUserCodeDropDown();
        }


        public MainMaintenanceViewModel(ISystemUserServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            SystemUserList = new BindingList<SystemUser>();
            //disable new row feature...
            SystemUserList.AllowNew = false;
            
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
        //used to enable/disable add and remove security groups for user...
        private bool _allowSecurityEdit;
        public bool AllowSecurityEdit
        {
            get { return _allowSecurityEdit; }
            set
            {
                _allowSecurityEdit = value;
                NotifyPropertyChanged(m => m.AllowSecurityEdit);
            }
        }

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

        private string _systemUserListCount;
        public string SystemUserListCount
        {
            get { return _systemUserListCount; }
            set
            {
                _systemUserListCount = value;
                NotifyPropertyChanged(m => m.SystemUserListCount);
            }
        }
        //Security Groups Available...
        private BindingList<SecurityGroup> _securityGroupAvailableList;
        public BindingList<SecurityGroup> SecurityGroupAvailableList
        {
            get { return _securityGroupAvailableList; }
            set
            {
                _securityGroupAvailableList = value;
                NotifyPropertyChanged(m => m.SecurityGroupAvailableList);
            }
        }

        private System.Collections.IList _selectedSecurityGroupAvailableList;
        public System.Collections.IList SelectedSecurityGroupAvailableList
        {
            get { return _selectedSecurityGroupAvailableList; }
            set
            {
                _selectedSecurityGroupAvailableList = value;
                NotifyPropertyChanged(m => m.SelectedSecurityGroupAvailableList);
            }
        }

        //Security Groups Authorized
        private BindingList<SystemUserSecurity> _systemUserSecurityGroupAuthorizedList;
        public BindingList<SystemUserSecurity> SystemUserSecurityGroupAuthorizedList
        {
            get { return _systemUserSecurityGroupAuthorizedList; }
            set
            {
                _systemUserSecurityGroupAuthorizedList = value;
                NotifyPropertyChanged(m => m.SystemUserSecurityGroupAuthorizedList);
            }
        }

        private System.Collections.IList _selectedSystemUserSecurityGroupAuthorizedList;
        public System.Collections.IList SelectedSystemUserSecurityGroupAuthorizedList
        {
            get { return _selectedSystemUserSecurityGroupAuthorizedList; }
            set
            {
                _selectedSystemUserSecurityGroupAuthorizedList = value;
                NotifyPropertyChanged(m => m.SelectedSystemUserSecurityGroupAuthorizedList);
            }
        }
        
        private BindingList<SystemUser> _systemUserList;
        public BindingList<SystemUser> SystemUserList
        {
            get
            {
                SystemUserListCount = _systemUserList.Count.ToString();
                if (_systemUserList.Count > 0)
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
                return _systemUserList;
            }
            set
            {
                _systemUserList = value;
                NotifyPropertyChanged(m => m.SystemUserList);
            }
        }

        private ObservableCollection<SystemUserType> _systemUserTypeList;
        public ObservableCollection<SystemUserType> SystemUserTypeList
        {
            get { return _systemUserTypeList; }
            set
            {
                _systemUserTypeList = value;
                NotifyPropertyChanged(m => m.SystemUserTypeList);
            }
        }

        private ObservableCollection<SystemUserCode> _systemUserCodeList;
        public ObservableCollection<SystemUserCode> SystemUserCodeList
        {
            get { return _systemUserCodeList; }
            set
            {
                _systemUserCodeList = value;
                NotifyPropertyChanged(m => m.SystemUserCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private SystemUser _selectedSystemUserMirror;
        public SystemUser SelectedSystemUserMirror
        {
            get { return _selectedSystemUserMirror; }
            set { _selectedSystemUserMirror = value; }
        }

        private System.Collections.IList _selectedSystemUserList;
        public System.Collections.IList SelectedSystemUserList
        {
            get { return _selectedSystemUserList; }
            set
            {
                if (_selectedSystemUser != value)
                {
                    _selectedSystemUserList = value;
                    NotifyPropertyChanged(m => m.SelectedSystemUserList);
                }  
            }
        }

        private SystemUser _selectedSystemUser;
        public SystemUser SelectedSystemUser
        {
            get {   return _selectedSystemUser; }
            set
            {
                if (_selectedSystemUser != value)
                {
                    _selectedSystemUser = value;
                    //set the mirrored SelectedSystemUser to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedSystemUserMirror = new SystemUser();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedSystemUser.GetType().GetProperties())
                        {
                            SelectedSystemUserMirror.SetPropertyValue(prop.Name, SelectedSystemUser.GetPropertyValue(prop.Name));
                        }
                        SelectedSystemUserMirror.SystemUserID = _selectedSystemUser.SystemUserID;
                        if (value.AutoID > 0)//autoid of zero would represent an empty or new object...
                        {
                            AllowSecurityEdit = true;
                            //get the Security Groups that pertain to the System User...
                            SecurityGroupAvailableList = GetAvailableSecurityGroups(value.SystemUserID);
                            SystemUserSecurityGroupAuthorizedList =
                                new BindingList<SystemUserSecurity>(GetSystemUserSecurities().ToList());

                        }
                        else
                        {
                            SecurityGroupAvailableList = GetAllSecurityGroups();
                            SystemUserSecurityGroupAuthorizedList = new BindingList<SystemUserSecurity>();
                            //they must save the systemuser before securities can be added...
                            //only a saved systemuser will have an autoid of > 0...
                            AllowSecurityEdit = false;
                        }
                        NotifyPropertyChanged(m => m.SelectedSystemUser);
                        SelectedSystemUser.PropertyChanged += new PropertyChangedEventHandler(SelectedSystemUser_PropertyChanged); 
                    }
                }
            }
        }

        private List<ColumnMetaData> _systemUserColumnMetaDataList;
        public List<ColumnMetaData> SystemUserColumnMetaDataList
        {
            get { return _systemUserColumnMetaDataList; }
            set 
            { 
                _systemUserColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.SystemUserColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _systemUserMaxFieldValueDictionary;
        public Dictionary<string, int> SystemUserMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_systemUserMaxFieldValueDictionary != null)
                {
                    return _systemUserMaxFieldValueDictionary;
                }
                _systemUserMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("SystemUsers");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _systemUserMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _systemUserMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedSystemUser_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "SystemUserID")
            {//make sure it is has changed...
                if (SelectedSystemUserMirror.SystemUserID != SelectedSystemUser.SystemUserID)
                {
                    //if their are no records it is a key change
                    if (SystemUserList != null && SystemUserList.Count == 0
                        && SelectedSystemUser != null && !string.IsNullOrEmpty(SelectedSystemUser.SystemUserID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetSystemUserState(SelectedSystemUser);

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
            
            object propertyChangedValue = SelectedSystemUser.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedSystemUserMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedSystemUser.GetPropertyType(e.PropertyName);
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
                    Update(SelectedSystemUser);
                    //set the mirrored objects field...
                    SelectedSystemUserMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedSystemUser.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedSystemUser.SystemUserID, out errorMessage))
            {
                //check to see if key is part of the current systemUserlist...
                SystemUser query = SystemUserList.Where(systemUser => systemUser.SystemUserID == SelectedSystemUser.SystemUserID &&
                                                        systemUser.AutoID != SelectedSystemUser.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected systemUser...
                    SelectedSystemUser = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                SystemUserList = GetSystemUserByID(SelectedSystemUser.SystemUserID);
                if (SystemUserList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedSystemUser.SystemUserID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedSystemUser = SystemUserList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedSystemUser.SystemUserID != SelectedSystemUserMirror.SystemUserID)
                {
                    SelectedSystemUser.SystemUserID = SelectedSystemUserMirror.SystemUserID;
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
            foreach (SystemUser systemUser in SystemUserList)
            {
                EntityStates entityState = GetSystemUserState(systemUser);
                if (entityState == EntityStates.Modified || 
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(systemUser, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(systemUser.Name, out errorMessage) == false)
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
                case "SystemUserID":
                    rBool = NewKeyIsValid(SelectedSystemUser, out errorMessage);
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

        private bool NewKeyIsValid(SystemUser systemUser, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(systemUser.SystemUserID, out errorMessage) == false)
            {
                return false;
            }
            if (SystemUserExists(systemUser.SystemUserID.ToString()))
            {
                errorMessage = "SystemUserID " + systemUser.SystemUserID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object systemUserID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)systemUserID))
            {
                errorMessage = "SystemUserID Is Required...";
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

        private void SetAsEmptySelection()
        {
            SelectedSystemUser = new SystemUser();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            SystemUserList.Clear();
            SetAsEmptySelection();
        }
        #endregion ViewModel Logic Methods

        #region ServiceAgent Call Methods
        private SecurityGroup GetSecurityGroupByIDReadOnly(string companyID, string securityGroupID)
        {
            return _serviceAgent.GetSecurityGroupByIDReadOnly(companyID, securityGroupID).SingleOrDefault();
        }
        private ObservableCollection<SystemUserType> BuildSystemUserTypeDropDown()
        {
            List<SystemUserType> list = new List<SystemUserType>();
            list = _serviceAgent.GetSystemUserTypes().ToList();
            list.Add(new SystemUserType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<SystemUserType>(list);
        }

        private ObservableCollection<SystemUserCode> BuildSystemUserCodeDropDown()
        {
            List<SystemUserCode> list = new List<SystemUserCode>();
            list = _serviceAgent.GetSystemUserCodes().ToList();
            list.Add(new SystemUserCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<SystemUserCode>(list);
        }

        private BindingList<SecurityGroup> GetAvailableSecurityGroups(string systemUserID)
        {
            return new BindingList<SecurityGroup>(_serviceAgent.GetAvailableSecurityGroups(systemUserID).ToList());
        }

        private BindingList<SecurityGroup> GetAllSecurityGroups()
        {
            return new BindingList<SecurityGroup>(_serviceAgent.GetAllSecurityGroups().ToList());
        }

        private EntityStates GetSystemUserState(SystemUser systemUser)
        {
            return _serviceAgent.GetSystemUserEntityState(systemUser);
        }

        #region CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedSystemUser.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (SystemUser systemUser in SystemUserList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (systemUser.AutoID > 0)
                {
                    autoIDs = autoIDs + systemUser.AutoID.ToString() + ",";
                }
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                SystemUserList = new BindingList<SystemUser>(_serviceAgent.RefreshSystemUser(autoIDs).ToList());
                SelectedSystemUser = (from q in SystemUserList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }
        //These child records are obtained on the GetSystemUsers fetch...
        //So all we are doing here is getting the records from the SystemUser Object Graph...
        private IEnumerable<SystemUserSecurity> GetSystemUserSecurities()
        {
            return SelectedSystemUser.SystemUserSecurities;
        }

        private BindingList<SystemUser> GetSystemUsers()
        {
            BindingList<SystemUser> systemUserList = new BindingList<SystemUser>(_serviceAgent.GetSystemUsers().ToList());
            Dirty = false;
            AllowCommit = false;
            return systemUserList; 
        }

        private BindingList<SystemUser> GetSystemUsers(SystemUser systemUser)
        {
            BindingList<SystemUser> systemUserList = new BindingList<SystemUser>(_serviceAgent.GetSystemUsers(systemUser).ToList());
            Dirty = false;
            AllowCommit = false;
            return systemUserList;
        }

        private BindingList<SystemUser> GetSystemUserByID(string id)
        {
            BindingList<SystemUser> systemUserList = new BindingList<SystemUser>(_serviceAgent.GetSystemUserByID(id).ToList());
            Dirty = false;
            AllowCommit = false;
            return systemUserList; 
        }

        private bool SystemUserExists(string systemUserID)
        {
            return _serviceAgent.SystemUserExists(systemUserID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(SystemUser systemUser)
        {
            _serviceAgent.UpdateSystemUserRepository(systemUser);
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
            _serviceAgent.CommitSystemUserRepository();
            Dirty = false;
            AllowCommit = false;
            if (SelectedSystemUser != null && SelectedSystemUser.AutoID > 0)
            {
                AllowSecurityEdit = true;
            }
            else
            {
                AllowSecurityEdit = false;
            }
            return true;
        }

        private bool Delete(SystemUser systemUser)
        {
            _serviceAgent.DeleteFromSystemUserRepository(systemUser);
            return true;
        }

        private bool Delete(SystemUserSecurity systemUserSecurity)
        {
            _serviceAgent.DeleteFromSystemUserRepository(systemUserSecurity);
            AllowCommit = CommitIsAllowed();
            return true;
        }

        private bool NewSystemUser(string systemUserID)
        {
            SystemUser systemUser = new SystemUser();
            systemUser.SystemUserID = systemUserID;
            //set any default values...
            systemUser.PasswordExpired = false;
            systemUser.Active = true;

            SystemUserList.Add(systemUser);
            _serviceAgent.AddToSystemUserRepository(systemUser);
            SelectedSystemUser = SystemUserList.LastOrDefault();
            

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        private bool NewSystemUserSecurity(SystemUserSecurity systemUserSecurity)
        {
            _serviceAgent.AddToSystemUserRepository(systemUserSecurity);
            AllowCommit = CommitIsAllowed();
            return true;
        }

        #endregion CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void AddSecurityGroupCommand()
        {//we utilize the do while as remove the selected items until they are all removed...
            do
            {
                SecurityGroup securityGroup = (SecurityGroup)SelectedSecurityGroupAvailableList[0];
                SystemUserSecurity systemUserSecurity = new SystemUserSecurity();
                systemUserSecurity.CompanyID = securityGroup.CompanyID;
                systemUserSecurity.SecurityGroupID = securityGroup.SecurityGroupID;
                systemUserSecurity.SystemUserID = SelectedSystemUser.SystemUserID;
                //add item to domain context...
                NewSystemUserSecurity(systemUserSecurity);
                //add item to authorized list
                SystemUserSecurityGroupAuthorizedList.Add(systemUserSecurity);
                //remove item from available list...
                SecurityGroupAvailableList.Remove(securityGroup);
            } while (SelectedSecurityGroupAvailableList.Count > 0); 
        }

        public void RemoveSecurityGroupCommand()
        {
            do
            {
                SystemUserSecurity systemUserSecurity = (SystemUserSecurity)SelectedSystemUserSecurityGroupAuthorizedList[0];
                //remove item from domain context...
                Delete(systemUserSecurity);  
                //remove item from authorized list
                SystemUserSecurityGroupAuthorizedList.Remove(systemUserSecurity);
                //fetch the security group and add it to available list...
                SecurityGroup securityGroup = GetSecurityGroupByIDReadOnly(systemUserSecurity.CompanyID, systemUserSecurity.SecurityGroupID);
                SecurityGroupAvailableList.Add(securityGroup);
            } while (SelectedSystemUserSecurityGroupAuthorizedList.Count > 0); 
        }

        public void PasteRowCommand()
        {
            try
            {
                SystemUserColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewSystemUserCommand(""); //this will generate a new systemUser and set it as the selected systemUser...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedSystemUser.SetPropertyValue(SystemUserColumnMetaDataList[i].Name, columnValue);
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
            if (GetSystemUserState(SelectedSystemUser) != EntityStates.Detached)
            {
                if (Update(SelectedSystemUser))
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
            for (int j = SelectedSystemUserList.Count - 1; j >= 0; j--)
            {
                SystemUser systemUser = (SystemUser)SelectedSystemUserList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = SystemUserList.IndexOf(systemUser) - SelectedSystemUserList.Count;
                }
                
                Delete(systemUser);
                SystemUserList.Remove(systemUser);
            }

            if (SystemUserList != null && SystemUserList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedSystemUser = SystemUserList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }  
        }

        public void NewSystemUserCommand()
        {
            NewSystemUser("");
            AllowCommit = false;
        }

        public void NewSystemUserCommand(string systemUserID)
        {
            NewSystemUser(systemUserID);
            if (string.IsNullOrEmpty(systemUserID))
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
            RegisterToReceiveMessages<BindingList<SystemUser>>(MessageTokens.SystemUserSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<SystemUser>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SystemUserList = e.Data;
                SelectedSystemUser = SystemUserList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<SystemUser>>(MessageTokens.SystemUserSearchToken.ToString(), OnSearchResult);
        }

        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<SystemUserType>>(MessageTokens.SystemUserTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<SystemUserType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SelectedSystemUser.SystemUserTypeID = e.Data.FirstOrDefault().SystemUserTypeID;
            }
            UnregisterToReceiveMessages<BindingList<SystemUserType>>(MessageTokens.SystemUserTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<SystemUserCode>>(MessageTokens.SystemUserCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<SystemUserCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SelectedSystemUser.SystemUserCodeID = e.Data.FirstOrDefault().SystemUserCodeID;
            }
            UnregisterToReceiveMessages<BindingList<SystemUserCode>>(MessageTokens.SystemUserCodeSearchToken.ToString(), OnCodeSearchResult);
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
                    SelectedSystemUser.SystemUserID = SelectedSystemUserMirror.SystemUserID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with SystemUserID provided...
                    NewSystemUserCommand(SelectedSystemUser.SystemUserID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSystemUser.SystemUserID = SelectedSystemUserMirror.SystemUserID;
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
                    _serviceAgent.CommitSystemUserRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSystemUser.SystemUserID = SelectedSystemUserMirror.SystemUserID;
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
        public static object GetPropertyValue(this SystemUser myObj, string propertyName)
        {
            var propInfo = typeof(SystemUser).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyType(this SystemUser myObj, string propertyName)
        {
            var propInfo = typeof(SystemUser).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this SystemUser myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(SystemUser).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}