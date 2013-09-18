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
using XERP.Domain.SystemUserDomain.SystemUserDataService;
using XERP.Domain.SystemUserDomain.Services;

namespace XERP.Client.WPF.SystemUserMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newSystemUserAutoId;

        private ISystemUserServiceAgent _serviceAgent;
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
            SystemUserTypeList = BuildSystemUserTypeDropDown();
            SystemUserCodeList = BuildSystemUserCodeDropDown();
            CompanyList = BuildCompanyDropDown();
            PlantList = BuildPlantDropDown();
            AddressList = BuildAddressDropDown();
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
        #endregion General Form Function/State Properties

        #region DropDown Collections
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

        private ObservableCollection<Company> _companyList;
        public ObservableCollection<Company> CompanyList
        {
            get { return _companyList; }
            set
            {
                _companyList = value;
                NotifyPropertyChanged(m => m.CompanyList);
            }
        }

        private ObservableCollection<Plant> _plantList;
        public ObservableCollection<Plant> PlantList
        {
            get { return _plantList; }
            set
            {
                _plantList = value;
                NotifyPropertyChanged(m => m.PlantList);
            }
        }

        private ObservableCollection<Address> _addressList;
        public ObservableCollection<Address> AddressList
        {
            get { return _addressList; }
            set
            {
                _addressList = value;
                NotifyPropertyChanged(m => m.AddressList);
            }
        }
        #endregion DropDown Collections

        #region CRUD Properties
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
            get 
            {
                return _selectedSystemUser; 
            }
            set
            {
                if (_selectedSystemUser != value)
                {
                    _selectedSystemUser = value;
                    //set Security Selections
                    SetSecurityGroupLists(value.SystemUserID);
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
                        NotifyPropertyChanged(m => m.SelectedSystemUser);
                        
                        SelectedSystemUser.PropertyChanged += new PropertyChangedEventHandler(SelectedSystemUser_PropertyChanged); 
                    }
                }
            }
        }
        #endregion CRUD Propeties

        #region Validation Properties
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

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _systemUserMaxFieldValueDictionary;
        public Dictionary<string, int> SystemUserMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_systemUserMaxFieldValueDictionary != null)
                    return _systemUserMaxFieldValueDictionary;

                _systemUserMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("SystemUsers");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _systemUserMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _systemUserMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties

        #region UserSecurity Properties
        private ObservableCollection<SecurityGroup> _availableSecurityGroupList;
        public ObservableCollection<SecurityGroup> AvailableSecurityGroupList
        {
            get { return _availableSecurityGroupList; }
            set
            {
                _availableSecurityGroupList = value;
                NotifyPropertyChanged(m => m.AvailableSecurityGroupList);
            }
        }

        private ObservableCollection<SecurityGroup> _assignedSecurityGroupList;
        public ObservableCollection<SecurityGroup> AssignedSecurityGroupList
        {
            get { return _assignedSecurityGroupList; }
            set
            {
                _assignedSecurityGroupList = value;
                NotifyPropertyChanged(m => m.AssignedSecurityGroupList);
            }
        }

        private System.Collections.IList _selectedAvailableSecurityGroupList;
        public System.Collections.IList SelectedAvailableSecurityGroupList
        {
            get { return _selectedAvailableSecurityGroupList; }
            set
            {
                _selectedAvailableSecurityGroupList = value;
                NotifyPropertyChanged(m => m.SelectedAvailableSecurityGroupList);
            }
        }

        private System.Collections.IList _selectedAssignedSecurityGroupList;
        public System.Collections.IList SelectedAssignedSecurityGroupList
        {
            get { return _selectedAssignedSecurityGroupList; }
            set
            {
                _selectedAssignedSecurityGroupList = value;
                NotifyPropertyChanged(m => m.SelectedAssignedSecurityGroupList);
            }
        }
        #endregion UserSecurity Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedSystemUser_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
                        if (Dirty  && AllowCommit)//dirty record exists ask if save is required...
                            NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ChangeKeyLogic);
                        else
                            ChangeKeyLogic();

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
                if (SystemUserPropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedSystemUser);
                    //set the mirrored objects field...
                    SelectedSystemUserMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedSystemUserMirror.IsValid = SelectedSystemUser.IsValid;
                    SelectedSystemUserMirror.IsExpanded = SelectedSystemUser.IsExpanded;
                    SelectedSystemUserMirror.NotValidMessage = SelectedSystemUser.NotValidMessage;
                }
                else
                {
                    SelectedSystemUser.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedSystemUser.IsValid = SelectedSystemUserMirror.IsValid;
                    SelectedSystemUser.IsExpanded = SelectedSystemUserMirror.IsExpanded;
                    SelectedSystemUser.NotValidMessage = SelectedSystemUserMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedSystemUser.SystemUserID))
            {//check to see if key is part of the current companylist...
                SystemUser query = SystemUserList.Where(company => company.SystemUserID == SelectedSystemUser.SystemUserID &&
                                                        company.AutoID != SelectedSystemUser.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back
                    SelectedSystemUser.SystemUserID = SelectedSystemUserMirror.SystemUserID;
                    //change to the newly selected company...
                    SelectedSystemUser = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                SystemUserList = GetSystemUserByID(SelectedSystemUser.SystemUserID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (SystemUserList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedSystemUser.SystemUserID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedSystemUser = SystemUserList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedSystemUser.SystemUserID != SelectedSystemUserMirror.SystemUserID)
                    SelectedSystemUser.SystemUserID = SelectedSystemUserMirror.SystemUserID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in SystemUserList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
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

        private bool SystemUserPropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "SystemUserID":
                    rBool = SystemUserIsValid(SelectedSystemUser, _systemUserValidationProperties.SystemUserID, out errorMessage);
                    break;
                case "Name":
                    rBool = SystemUserIsValid(SelectedSystemUser, _systemUserValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedSystemUser.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedSystemUser.IsValid = SystemUserIsValid(SelectedSystemUser, out errorMessage);
                if (SelectedSystemUser.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedSystemUser.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _systemUserValidationProperties
        {//we list all fields that require validation...
            SystemUserID,
            Name
        }

        //Object.Property Scope Validation...
        private bool SystemUserIsValid(SystemUser item, _systemUserValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _systemUserValidationProperties.SystemUserID:
                    //validate key
                    if (string.IsNullOrEmpty(item.SystemUserID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetSystemUserState(item);
                    if (entityState == EntityStates.Added && SystemUserExists(item.SystemUserID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = SystemUserList.Count(q => q.SystemUserID == item.SystemUserID);
                    if (count > 1)
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    break;
                case _systemUserValidationProperties.Name:
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
        //SystemUser Object Scope Validation check the entire object for validity...
        private byte SystemUserIsValid(SystemUser item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.SystemUserID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetSystemUserState(item);
            if (entityState == EntityStates.Added && SystemUserExists(item.SystemUserID))
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = SystemUserList.Count(q => q.SystemUserID == item.SystemUserID);
            if (count > 1)
            {
                errorMessage = "Item All Ready Exists.";
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
        #region DropDown Methods
        private ObservableCollection<SystemUserType> BuildSystemUserTypeDropDown()
        {
            List<SystemUserType> list = new List<SystemUserType>();
            list = _serviceAgent.GetSystemUserTypesReadOnly().ToList();
            list.Add(new SystemUserType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<SystemUserType>(list);
        }

        private ObservableCollection<SystemUserCode> BuildSystemUserCodeDropDown()
        {
            List<SystemUserCode> list = new List<SystemUserCode>();
            list = _serviceAgent.GetSystemUserCodesReadOnly().ToList();
            list.Add(new SystemUserCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<SystemUserCode>(list);
        }

        private ObservableCollection<Company> BuildCompanyDropDown()
        {
            List<Company> list = new List<Company>();
            list = _serviceAgent.GetCompaniesReadOnly().ToList();
            list.Add(new Company());
            list.Sort((x, y) => string.Compare(x.Name, y.Name));

            return new ObservableCollection<Company>(list);
        }

        private ObservableCollection<Plant> BuildPlantDropDown()
        {
            List<Plant> list = new List<Plant>();
            list = _serviceAgent.GetPlantsReadOnly().ToList();
            list.Add(new Plant());
            list.Sort((x, y) => string.Compare(x.Name, y.Name));

            return new ObservableCollection<Plant>(list);
        }

        private ObservableCollection<Address> BuildAddressDropDown()
        {
            List<Address> list = new List<Address>();
            list = _serviceAgent.GetAddressesReadOnly().ToList();
            list.Add(new Address());
            list.Sort((x, y) => string.Compare(x.Name, y.Name));

            return new ObservableCollection<Address>(list);
        }
        #endregion DropDown Methods

        private EntityStates GetSystemUserState(SystemUser item)
        {
            return _serviceAgent.GetSystemUserEntityState(item);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.SystemUserRepositoryIsDirty();
        }

        #region SystemUser CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedSystemUser.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (SystemUser item in SystemUserList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                SystemUserList = new BindingList<SystemUser>(_serviceAgent.RefreshSystemUser(autoIDs).ToList());
                SelectedSystemUser = (from q in SystemUserList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<SystemUser> GetSystemUsers(string companyID)
        {
            BindingList<SystemUser> itemList = new BindingList<SystemUser>(_serviceAgent.GetSystemUsers().ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private BindingList<SystemUser> GetSystemUsers(SystemUser item, string companyID)
        {
            BindingList<SystemUser> itemList = new BindingList<SystemUser>(_serviceAgent.GetSystemUsers(item).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<SystemUser> GetSystemUserByID(string itemID, string companyID)
        {
            BindingList<SystemUser> itemList = new BindingList<SystemUser>(_serviceAgent.GetSystemUserByID(itemID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private bool SystemUserExists(string itemID)
        {
            return _serviceAgent.SystemUserExists(itemID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(SystemUser item)
        {
            _serviceAgent.UpdateSystemUserRepository(item);
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
            var items = (from q in SystemUserList where q.IsValid == 2 select q).ToList();
            foreach (SystemUser item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitSystemUserRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }


        private bool Delete(SystemUser item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromSystemUserRepository(item);
            return true;
        }

        private bool NewSystemUser(string itemID)
        {
            SystemUser newItem = new SystemUser();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newSystemUserAutoId = _newSystemUserAutoId - 1;
            newItem.AutoID = _newSystemUserAutoId;
            newItem.SystemUserID = itemID;
            
            newItem.IsValid = 1;
            newItem.NotValidMessage = "New Record Key Field/s Are Required.";
            SystemUserList.Add(newItem);
            _serviceAgent.AddToSystemUserRepository(newItem);
            SelectedSystemUser = SystemUserList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }


        #endregion SystemUser CRUD

        #region SystemUserSecurity CRUD
        private void SetSecurityGroupLists(string menuItemID)
        {//used within method as to not fire a property change of the public object...
            //eventually we will set the public objects from these objects...
            List<SecurityGroup> availableSecurityGroupList = new List<SecurityGroup>();
            List<SecurityGroup> assignedSecurityGroupList = new List<SecurityGroup>();

            //used to house the SystemUserSecurities for selected menu
            List<SystemUserSecurity> menuSecurities = new List<SystemUserSecurity>();

            //get all the Security Groups to allow us to omit them if they belong to the SystemUser Item allready...
            List<SecurityGroup> allSecurityGroups = new List<SecurityGroup>();
            //get all security groups
            allSecurityGroups = _serviceAgent.GetSecurityGroupsReadyOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            //set it to all we will remove the selected ones...
            availableSecurityGroupList = allSecurityGroups;

            //get menuSecurities for selected item...
            menuSecurities = _serviceAgent.GetSystemUserSecuritiesBySystemUserIDReadOnly(menuItemID, ClientSessionSingleton.Instance.CompanyID).ToList();

            //loop SystemUserSecurity and add it's Security Group to assignedSecurityGroups...
            //then loop AllSecurityGroups and omit the Assigned one from the AvailableSecurityGroups
            foreach (SystemUserSecurity item in menuSecurities)
            {
                assignedSecurityGroupList.Add(item.SecurityGroup);
                foreach (SecurityGroup omitItem in allSecurityGroups)
                {
                    if (omitItem.SecurityGroupID == item.SecurityGroupID)
                    {
                        availableSecurityGroupList.Remove(omitItem);
                        break;
                    }
                }

            }

            //set the public objects from method objects...
            AvailableSecurityGroupList = new ObservableCollection<SecurityGroup>(availableSecurityGroupList);
            AssignedSecurityGroupList = new ObservableCollection<SecurityGroup>(assignedSecurityGroupList);
        }

        private void RemoveAllSystemUserSecurities(string menuItemID)
        {
            _serviceAgent.RemoveAllSystemUserSecurities(menuItemID,
                ClientSessionSingleton.Instance.CompanyID);
        }

        private void AddAllSystemUserSecurities(string menuItemID)
        {
            _serviceAgent.AddAllSystemUserSecurities(menuItemID,
                ClientSessionSingleton.Instance.CompanyID);
        }

        private void RemoveSystemUserSecurity(string menuItemID, string securityGroupID)
        {
            _serviceAgent.RemoveSystemUserSecurity(menuItemID, securityGroupID,
                ClientSessionSingleton.Instance.CompanyID);
        }

        private void AddSystemUserSecurity(string menuItemId, string securityGroupID)
        {
            _serviceAgent.AddSystemUserSecurity(menuItemId, securityGroupID,
                ClientSessionSingleton.Instance.CompanyID);
        }

        #endregion SystemUserSecurity CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void AssignSelectedSecurityGroupsCommand()
        {
            foreach (var item in SelectedAvailableSecurityGroupList)
            {
                SecurityGroup securityGroup = (SecurityGroup)item;
                AddSystemUserSecurity(SelectedSystemUser.SystemUserID, securityGroup.SecurityGroupID);
            }
        }
        public void RemoveSelectedSecurityGroupsCommand()
        {
            foreach (var item in SelectedAssignedSecurityGroupList)
            {
                SecurityGroup securityGroup = (SecurityGroup)item;
                RemoveSystemUserSecurity(SelectedSystemUser.SystemUserID, securityGroup.SecurityGroupID);
            }
        }
        public void AssignAllSecurityGroupsCommand()
        {
            AddAllSystemUserSecurities(SelectedSystemUser.SystemUserID);
            AvailableSecurityGroupList.Clear();
            AssignedSecurityGroupList = new ObservableCollection<SecurityGroup>
                (_serviceAgent.GetSecurityGroupsReadyOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }
        public void RemoveAllSecurityGroupsCommand()
        {
            RemoveAllSystemUserSecurities(SelectedSystemUser.SystemUserID);
            AssignedSecurityGroupList.Clear();
            AvailableSecurityGroupList = new ObservableCollection<SecurityGroup>
                (_serviceAgent.GetSecurityGroupsReadyOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
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
                    NewSystemUserCommand(""); //this will generate a new item and set it as the selected item...
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
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteSystemUserCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedSystemUserList.Count - 1; j >= 0; j--)
                {
                    SystemUser item = (SystemUser)SelectedSystemUserList[j];
                    //get Max Index...
                    i = SystemUserList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    SystemUserList.Remove(item);
                }

                if (SystemUserList != null && SystemUserList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= SystemUserList.Count())
                        ii = SystemUserList.Count - 1;

                    SelectedSystemUser = SystemUserList[ii];
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
                NotifyMessage("SystemUser/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewSystemUserCommand()
        {
            NewSystemUser("");
            AllowCommit = false;
        }

        public void NewSystemUserCommand(string itemID)
        {
            NewSystemUser(itemID);
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

        #region Right Click FK Searches
        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<SystemUserType>>(MessageTokens.SystemUserTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<SystemUserType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedSystemUser.SystemUserTypeID = e.Data.FirstOrDefault().SystemUserTypeID;

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
                SelectedSystemUser.SystemUserCodeID = e.Data.FirstOrDefault().SystemUserCodeID;

            UnregisterToReceiveMessages<BindingList<SystemUserType>>(MessageTokens.SystemUserTypeSearchToken.ToString(), OnTypeSearchResult);
        }
        #endregion Right Click FK Searches
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

        private void NotifySearch(string message)
        {//Notify view to launch search...
            Notify(SearchNotice, new NotificationEventArgs(message));
        }

        #region Right Click FK Helpers
        private void NotifyTypeSearch(string message)
        {
            Notify(TypeSearchNotice, new NotificationEventArgs(message));
        }

        private void NotifyCodeSearch(string message)
        {
            Notify(CodeSearchNotice, new NotificationEventArgs(message));
        }
        #endregion Right Click FK Helpers
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
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this SystemUser myObj, string propertyName)
        {
            var propInfo = typeof(SystemUser).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this SystemUser myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(SystemUser).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}