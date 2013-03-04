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
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;
using XERP.Domain.MenuSecurityDomain.ClientModels;

namespace XERP.Client.WPF.MenuItemMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newMenuItemAutoId;

        private IMenuItemServiceAgent _serviceAgent;
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
            MenuItemTypeList = BuildMenuItemTypeDropDown();
            MenuItemCodeList = BuildMenuItemCodeDropDown();
            DBStroredImageList = BuildDBStoredImageDropDown();
            ExecutableProgramList = BuildExecutableProgramDropDown(); 
        }

        public MainMaintenanceViewModel(IMenuItemServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            //make sure of session authentication...
            if (XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)//make sure user has rights to UI... 
                DoFormsAuthentication();
            else
            {//User is not authenticated...
                RegisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
                FormIsEnabled = false;
            }
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            BuildDropDowns();

            MenuItemList = new BindingList<MenuItem>();

            AllowNew = true;

            BuildMenuTree();
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
        public event EventHandler<NotificationEventArgs> TypeSearchNotice;
        public event EventHandler<NotificationEventArgs> CodeSearchNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> SaveRequiredNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> NewRecordNeededNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        #endregion Notifications    

        #region Properties
        #region General Form Function/State Properties
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

        private string _menuItemListCount;
        public string MenuItemListCount
        {
            get { return _menuItemListCount; }
            set
            {
                _menuItemListCount = value;
                NotifyPropertyChanged(m => m.MenuItemListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region DropDown Collections
        private ObservableCollection<MenuItemType> _menuItemTypeList;
        public ObservableCollection<MenuItemType> MenuItemTypeList
        {
            get { return _menuItemTypeList; }
            set
            {
                _menuItemTypeList = value;
                NotifyPropertyChanged(m => m.MenuItemTypeList);
            }
        }

        private ObservableCollection<MenuItemCode> _menuItemCodeList;
        public ObservableCollection<MenuItemCode> MenuItemCodeList
        {
            get { return _menuItemCodeList; }
            set
            {
                _menuItemCodeList = value;
                NotifyPropertyChanged(m => m.MenuItemCodeList);
            }
        }

        private ObservableCollection<ExecutableProgram> _executableProgramList;
        public ObservableCollection<ExecutableProgram> ExecutableProgramList
        {
            get { return _executableProgramList; }
            set
            {
                _executableProgramList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramList);
            }
        }

        private ObservableCollection<DBStoredImage> _dBStoredImageList;
        public ObservableCollection<DBStoredImage> DBStroredImageList
        {
            get { return _dBStoredImageList; }
            set
            {
                _dBStoredImageList = value;
                NotifyPropertyChanged(m => m.DBStroredImageList);
            }
        }
        #endregion DropDown Collections

        #region CRUD Properties
        private BindingList<MenuItem> _menuItemList;
        public BindingList<MenuItem> MenuItemList
        {
            get
            {
                MenuItemListCount = _menuItemList.Count.ToString();
                if (_menuItemList.Count > 0)
                {
                    AllowEdit = true;
                    AllowDelete = true;
                }
                else
                {//no records to edit delete or be dirty...
                    AllowEdit = false;
                    AllowDelete = false;
                    Dirty = false;
                    AllowCommit = false;
                }
                return _menuItemList;
            }
            set
            {
                _menuItemList = value;
                NotifyPropertyChanged(m => m.MenuItemList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private NestedMenuItem _selectedMenuItemMirror;
        public NestedMenuItem SelectedMenuItemMirror
        {
            get { return _selectedMenuItemMirror; }
            set { _selectedMenuItemMirror = value; }
        }

        private NestedMenuItem _selectedMenuItem;
        public NestedMenuItem SelectedMenuItem
        {
            get 
            {
                return _selectedMenuItem; 
            }
            set
            {
                if (_selectedMenuItem != value)
                {
                    _selectedMenuItem = value;
                    //set MenuSecurity Selections
                    SetSecurityGroupLists(value.MenuItemID);
                    //default properties...
                    Dirty = false;
                    MenuItem menuItem = new MenuItem();
                    SelectedMenuItemMirror = new NestedMenuItem();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedMenuItem.GetType().GetProperties())
                        {
                            SelectedMenuItemMirror.SetPropertyValue(prop.Name, SelectedMenuItem.GetPropertyValue(prop.Name));
                        }
                        SelectedMenuItemMirror.MenuItemID = _selectedMenuItem.MenuItemID;
                        NotifyPropertyChanged(m => m.SelectedMenuItem);
                        
                        SelectedMenuItem.PropertyChanged += new PropertyChangedEventHandler(SelectedMenuItem_PropertyChanged); 
                    }
                }
            }
        }
        #endregion CRUD Properties

        #region Nested Item Properties
        //menuitems are converted to this list then they are nested...
        private ObservableCollection<NestedMenuItem> _flatNestedMenuItemList;

        private ObservableCollection<NestedMenuItem> _treeNestedMenuItemList;
        public ObservableCollection<NestedMenuItem> TreeNestedMenuItemList
        {
            get { return _treeNestedMenuItemList; }
            set
            {
                _treeNestedMenuItemList = value;
                NotifyPropertyChanged(m => m.TreeNestedMenuItemList);
            }
        }
        #endregion Nested Item Properties

        #region MenuSecurity Properties
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
        #endregion MenuSecurity Properties

        #region Validation Properties
        private List<ColumnMetaData> _menuItemColumnMetaDataList;
        public List<ColumnMetaData> MenuItemColumnMetaDataList
        {
            get { return _menuItemColumnMetaDataList; }
            set
            {
                _menuItemColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.MenuItemColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _menuItemMaxFieldValueDictionary;
        public Dictionary<string, int> MenuItemMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_menuItemMaxFieldValueDictionary != null)
                    return _menuItemMaxFieldValueDictionary;

                _menuItemMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("MenuItems");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _menuItemMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _menuItemMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedMenuItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "MenuItemID")
            {//make sure it is has changed...
                if (SelectedMenuItemMirror.MenuItemID != SelectedMenuItem.MenuItemID)
                {//convert to the Entity MenuItem...
                    MenuItem menuItem = MenuItemList.SingleOrDefault(q => q.AutoID == SelectedMenuItem.AutoID);
                    EntityStates entityState = GetMenuItemState(menuItem);

                    if (entityState == EntityStates.Unchanged ||
                        entityState == EntityStates.Modified)
                    {//once a key is added it can not be modified...
                        MessageBox.Show("Once A Key Is Added It Can Not Be Modified.");
                        return;
                    }
                }
            }//end KeyID logic...
            
            object propertyChangedValue = SelectedMenuItem.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedMenuItemMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedMenuItem.GetPropertyType(e.PropertyName);
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
                if (MenuItemPropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {//pass in the propertyname and value as we will update the CRUD MenuItem and pass the 
                    //change to the repository...
                    Update(SelectedMenuItem, e.PropertyName, propertyChangedValue);
                    //set the mirrored objects field...
                    SelectedMenuItemMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedMenuItemMirror.IsValid = SelectedMenuItem.IsValid;
                    SelectedMenuItemMirror.IsExpanded = SelectedMenuItem.IsExpanded;
                    SelectedMenuItemMirror.NotValidMessage = SelectedMenuItem.NotValidMessage;
                }
                else
                {
                    SelectedMenuItem.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedMenuItem.IsValid = SelectedMenuItemMirror.IsValid;
                    SelectedMenuItem.IsExpanded = SelectedMenuItemMirror.IsExpanded;
                    SelectedMenuItem.NotValidMessage = SelectedMenuItemMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        //XERP allows for bulk updates. We only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //recurse and check for any invalid rows...
            _rowsValid = true;//initialize to true...
            //recurse tree passing in the root node of the list...
            CheckForInvalidRows(TreeNestedMenuItemList.FirstOrDefault());
            return _rowsValid;
        }

        private bool _rowsValid;
        private void CheckForInvalidRows(NestedMenuItem parent)
        {
            if (_rowsValid)
            {
                foreach (NestedMenuItem child in parent.Children)
                {
                    if (child.IsValid == 1)
                    {
                        _rowsValid = false;
                        return;
                    }
                    if(_rowsValid)    
                        CheckForInvalidRows(child);
                }
            }
        }

        private bool MenuItemPropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "MenuItemID":
                    rBool = MenuItemIsValid(SelectedMenuItem, _menuItemValidationProperties.MenuItemID, out errorMessage);
                    break;
                case "Name":
                    rBool = MenuItemIsValid(SelectedMenuItem, _menuItemValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedMenuItem.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedMenuItem.IsValid = MenuItemIsValid(SelectedMenuItem, out errorMessage);
                if (SelectedMenuItem.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedMenuItem.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _menuItemValidationProperties
        {//we list all fields that require validation...
            MenuItemID,
            Name
        }

        //Object.Property Scope Validation...
        private bool MenuItemIsValid(NestedMenuItem item, _menuItemValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _menuItemValidationProperties.MenuItemID:
                    //validate key
                    if (string.IsNullOrEmpty(item.MenuItemID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    MenuItem menuItem = MenuItemList.SingleOrDefault(q => q.AutoID == item.AutoID);
                    EntityStates entityState = GetMenuItemState(menuItem);
                    if (entityState == EntityStates.Added && MenuItemExists(item.MenuItemID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = MenuItemList.Count(q => q.MenuItemID == item.MenuItemID);
                    if (count > 1)
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    break;
                case _menuItemValidationProperties.Name:
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
        //MenuItem Object Scope Validation check the entire object for validity...
        private byte MenuItemIsValid(NestedMenuItem item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.MenuItemID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            MenuItem menuItem = MenuItemList.SingleOrDefault(q => q.AutoID == item.AutoID);
            EntityStates entityState = GetMenuItemState(menuItem);
            if (entityState == EntityStates.Added && MenuItemExists(item.MenuItemID))
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = MenuItemList.Count(q => q.MenuItemID == item.MenuItemID);
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
        private ObservableCollection<MenuItemType> BuildMenuItemTypeDropDown()
        {
            List<MenuItemType> list = new List<MenuItemType>();
            list = _serviceAgent.GetMenuItemTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new MenuItemType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<MenuItemType>(list);
        }

        private ObservableCollection<MenuItemCode> BuildMenuItemCodeDropDown()
        {
            List<MenuItemCode> list = new List<MenuItemCode>();
            list = _serviceAgent.GetMenuItemCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new MenuItemCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<MenuItemCode>(list);
        }

        private ObservableCollection<DBStoredImage> BuildDBStoredImageDropDown()
        {
            List<DBStoredImage> list = new List<DBStoredImage>();
            list = _serviceAgent.GetDBStoredImagesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new DBStoredImage());
            list.Sort((x, y) => string.Compare(x.ImageID, y.ImageID));

            return new ObservableCollection<DBStoredImage>(list);
        }

        private ObservableCollection<ExecutableProgram> BuildExecutableProgramDropDown()
        {
            List<ExecutableProgram> list = new List<ExecutableProgram>();
            list = _serviceAgent.GetExecutableProgramsReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new ExecutableProgram());
            list.Sort((x, y) => string.Compare(x.Name, y.Name));

            return new ObservableCollection<ExecutableProgram>(list);
        }
        #endregion DropDown Methods

        #region MenuSecurity CRUD
        private void SetSecurityGroupLists(string menuItemID)
        {//used within method as to not fire a property change of the public object...
            //eventually we will set the public objects from these objects...
            List<SecurityGroup> availableSecurityGroupList = new List<SecurityGroup>();
            List<SecurityGroup> assignedSecurityGroupList = new List<SecurityGroup>();

            //used to house the MenuSecurities for selected menu
            List<MenuSecurity> menuSecurities = new List<MenuSecurity>();

            //get all the Security Groups to allow us to omit them if they belong to the Menu Item allready...
            List<SecurityGroup> allSecurityGroups = new List<SecurityGroup>();
            //get all security groups
            allSecurityGroups = _serviceAgent.GetSecurityGroupsReadyOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            //set it to all we will remove the selected ones...
            availableSecurityGroupList = allSecurityGroups;

            //get menuSecurities for selected item...
            menuSecurities = _serviceAgent.GetMenuSecuritiesByMenuItemIDReadOnly(menuItemID, ClientSessionSingleton.Instance.CompanyID).ToList();
            
            //loop MenuSecurity and add it's Security Group to assignedSecurityGroups...
            //then loop AllSecurityGroups and omit the Assigned one from the AvailableSecurityGroups
            foreach (MenuSecurity item in menuSecurities)
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

        private void RemoveAllMenuSecurities(string menuItemID)
        {
            _serviceAgent.RemoveAllMenuSecurities(menuItemID,
                ClientSessionSingleton.Instance.CompanyID);
        }

        private void AddAllMenuSecurities(string menuItemID)
        {
            _serviceAgent.AddAllMenuSecurities(menuItemID,
                ClientSessionSingleton.Instance.CompanyID);
        }

        private void RemoveMenuSecurity(string menuItemID, string securityGroupID)
        {
            _serviceAgent.RemoveMenuSecurity(menuItemID, securityGroupID, 
                ClientSessionSingleton.Instance.CompanyID);                                                          
        }

        private void AddMenuSecurity(string menuItemId, string securityGroupID)
        {
            _serviceAgent.AddMenuSecurity(menuItemId, securityGroupID, 
                ClientSessionSingleton.Instance.CompanyID);        
        }

        #endregion MenuSecurity CRUD

        private EntityStates GetMenuItemState(MenuItem item)
        {
            return _serviceAgent.GetMenuItemEntityState(item);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.MenuItemRepositoryIsDirty();
        }

        #region MenuItem CRUD Methods
        private void Refresh()
        {//refetch records...
            InitializeViewModel();
            Dirty = false;
            AllowCommit = false;
        }

        private BindingList<MenuItem> GetMenuItems(string companyID)
        {
            BindingList<MenuItem> itemList = new BindingList<MenuItem>(_serviceAgent.GetMenuItems(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private BindingList<MenuItem> GetMenuItems(MenuItem item, string companyID)
        {
            BindingList<MenuItem> itemList = new BindingList<MenuItem>(_serviceAgent.GetMenuItems(item, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<MenuItem> GetMenuItemByID(string itemID, string companyID)
        {
            BindingList<MenuItem> itemList = new BindingList<MenuItem>(_serviceAgent.GetMenuItemByID(itemID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private bool MenuItemExists(string itemID)
        {
            return _serviceAgent.MenuItemExists(itemID, ClientSessionSingleton.Instance.CompanyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(NestedMenuItem item, string propertyName, object propertyValue)
        {//get the menuItem from the Repository List...
            MenuItem menuItem = MenuItemList.SingleOrDefault(q => q.AutoID == item.AutoID);
            //Set the edited field when present...
            if(! string.IsNullOrEmpty(propertyName))
                menuItem.SetPropertyValue(propertyName, propertyValue);
            //update the repository with the field change...
            _serviceAgent.UpdateMenuItemRepository(menuItem);
            Dirty = true;
            if (CommitIsAllowed())
                AllowCommit = true;
            else
                AllowCommit = false;
            return AllowCommit;
        }

        //commits repository to the db...
        private bool Commit()
        {   //reset UI state manage fields...
            NestedMenuItem root = TreeNestedMenuItemList.SingleOrDefault(q => (q.ParentMenuID == null || q.ParentMenuID.Equals(string.Empty))
                                                               && q.CompanyID == ClientSessionSingleton.Instance.CompanyID);

            _serviceAgent.CommitMenuItemRepository();
            //recurse tree clear and set seed autoids
            ResetTreeTempFields(root);
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private void ResetTreeTempFields(NestedMenuItem parent)
        {
            foreach (NestedMenuItem child in parent.Children)
            {
                child.IsValid = 0;
                child.NotValidMessage = "";
                //autoID is set a negative value until it is seeded from the db...
                if (child.AutoID < 0)
                {
                    //locate the record to fetch the seeded autoid value...
                    MenuItem menuItem = MenuItemList.SingleOrDefault(q => q.MenuItemID == child.MenuItemID
                                                            && q.CompanyID == ClientSessionSingleton.Instance.CompanyID);
                    child.AutoID = menuItem.AutoID;
                }
                //if it is selected we need to modify SelectedMenuItem as well...
                if (child.IsSelected == true)
                {
                    SelectedMenuItem.IsValid = 0;
                    SelectedMenuItem.NotValidMessage = "";
                }
                ResetTreeTempFields(child);
            }
        }

        private bool Delete(NestedMenuItem item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            MenuItem menuItem = MenuItemList.SingleOrDefault(q => q.AutoID == item.AutoID);
            _serviceAgent.DeleteFromMenuItemRepository(menuItem);
            //remove it from the cache repository list
            MenuItemList.Remove(menuItem);
            return true;
        }

        private bool NewMenuItem(string itemID)
        {//need to fix this...
            MenuItem newItem = new MenuItem();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newMenuItemAutoId = _newMenuItemAutoId - 1;
            newItem.AutoID = _newMenuItemAutoId;
            newItem.MenuItemID = itemID;
            newItem.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            //new items will be added to selected item
            newItem.ParentMenuID = SelectedMenuItem.MenuItemID;
            //add it to the repository list
            MenuItemList.Add(newItem);
            _serviceAgent.AddToMenuItemRepository(newItem);

            //add it to the treeviewList
            NestedMenuItem newNestedItem = new NestedMenuItem(newItem);
            newNestedItem.IsValid = 1;
            newNestedItem.NotValidMessage = "New Record Key Field/s Are Required.";
            SelectedMenuItem.Children.Add(newNestedItem);

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion MenuItem CRUD Methods
        #endregion ServiceAgent Call Methods

        #region NestedMenu Methods
        private void BuildMenuTree()
        {
            TreeNestedMenuItemList = new ObservableCollection<NestedMenuItem>();

            MenuItemList = GetMenuItems(ClientSessionSingleton.Instance.CompanyID);
           
            _flatNestedMenuItemList = new ObservableCollection<NestedMenuItem>();
            foreach (MenuItem menuItem in MenuItemList)
            {
                _flatNestedMenuItemList.Add(new NestedMenuItem(menuItem));
            }
            //Their may be mulitple root nodes...
            List<NestedMenuItem> roots = new List<NestedMenuItem>();
            roots = _flatNestedMenuItemList.
                Where(x => x.ParentMenuID == "")
                .OrderBy(a => a.DisplayOrder)
                .ToList();
            foreach (NestedMenuItem root in roots)
            {
                getNestedChidlren(root);
                TreeNestedMenuItemList.Add(root);
            }
            //SelectedNestedMenuItem = TreeNestedMenuItemList.FirstOrDefault();
            SelectedMenuItem = TreeNestedMenuItemList.FirstOrDefault();
        }

        private void getNestedChidlren(NestedMenuItem parent)
        {
            foreach (NestedMenuItem child in _flatNestedMenuItemList
                .Where(nmi => nmi.ParentMenuID == parent.MenuItemID)
                .OrderBy(a => a.DisplayOrder)
                .ToList())
            {
                parent.Children.Add(child);
                getNestedChidlren(child);
            }
        }
        #endregion NestedMenu Methods
        #endregion Methods

        #region Commands
        public void AssignSelectedSecurityGroupsCommand()
        {
            foreach (var item in SelectedAvailableSecurityGroupList)
            {
                SecurityGroup securityGroup = (SecurityGroup)item;
                AddMenuSecurity(SelectedMenuItem.MenuItemID, securityGroup.SecurityGroupID);
            }
        }
        public void RemoveSelectedSecurityGroupsCommand()
        {
            foreach (var item in SelectedAssignedSecurityGroupList)
            {
                SecurityGroup securityGroup = (SecurityGroup)item;
                RemoveMenuSecurity(SelectedMenuItem.MenuItemID, securityGroup.SecurityGroupID);
            }
        }
        public void AssignAllSecurityGroupsCommand()
        {
            AddAllMenuSecurities(SelectedMenuItem.MenuItemID);
            AvailableSecurityGroupList.Clear();
            AssignedSecurityGroupList = new ObservableCollection<SecurityGroup>
                (_serviceAgent.GetSecurityGroupsReadyOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }
        public void RemoveAllSecurityGroupsCommand()
        {
            RemoveAllMenuSecurities(SelectedMenuItem.MenuItemID);
            AssignedSecurityGroupList.Clear();
            AvailableSecurityGroupList = new ObservableCollection<SecurityGroup>
                (_serviceAgent.GetSecurityGroupsReadyOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }
        public void SaveCommand()
        {
            MenuItem menuItem = MenuItemList.SingleOrDefault(q => q.AutoID == SelectedMenuItem.AutoID);
            if (GetMenuItemState(menuItem) != EntityStates.Detached)
            {
                if (Update(SelectedMenuItem, "", ""))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteMenuItemCommand()
        {//ToDo fix delete logic
            try
            {//delete in repository
                Delete(SelectedMenuItem);

                //remove it from the tree nested list
                NestedMenuItem root = TreeNestedMenuItemList.SingleOrDefault(q => (q.ParentMenuID == null || q.ParentMenuID.Equals(string.Empty))
                                                                               && q.CompanyID == ClientSessionSingleton.Instance.CompanyID);
                //need to itterate tree and find the item and remove it from the tree list...
                RemoveNestedItem(root, SelectedMenuItem.AutoID);  
            }//we try catch company delete as it may be used in another table as a key...
            ////As well we will force a refresh to sqare up the UI after the botched delete...
            catch
            {
               NotifyMessage("MenuItem/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
            //    Refresh();
            }
        }

        public void RemoveNestedItem(NestedMenuItem parent, long autoID)
        {
            foreach (NestedMenuItem child in parent.Children)
            {
                if (child.AutoID == autoID)
                {
                    parent.Children.Remove(child);
                    return;
                }
                RemoveNestedItem(child, autoID);
            }
        }

        public void NewMenuItemCommand()
        {
            NewMenuItem("");
            AllowCommit = false;
        }

        public void NewMenuItemCommand(string itemID)
        {
            NewMenuItem(itemID);
            if (string.IsNullOrEmpty(itemID))//don't allow a save until a itemID is provided...
                AllowCommit = false;
            else
                AllowCommit = CommitIsAllowed();
        }

        #region Right Click FK Searches
        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<MenuItemType>>(MessageTokens.MenuItemTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<MenuItemType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedMenuItem.MenuItemTypeID = e.Data.FirstOrDefault().MenuItemTypeID;

            UnregisterToReceiveMessages<BindingList<MenuItemType>>(MessageTokens.MenuItemTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<MenuItemCode>>(MessageTokens.MenuItemCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<MenuItemCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedMenuItem.MenuItemCodeID = e.Data.FirstOrDefault().MenuItemCodeID;

            UnregisterToReceiveMessages<BindingList<MenuItemType>>(MessageTokens.MenuItemTypeSearchToken.ToString(), OnTypeSearchResult);
        }
        #endregion Right Click FK Searches
        #endregion Commands

        #region Helpers
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {// Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
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
                    SelectedMenuItem.MenuItemID = SelectedMenuItemMirror.MenuItemID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with MenuItemID provided...
                    NewMenuItemCommand(SelectedMenuItem.MenuItemID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedMenuItem.MenuItemID = SelectedMenuItemMirror.MenuItemID;
                    break;
            }
        }

        private void OnSaveResult(MessageBoxResult result, _saveRequiredResultActions resultAction)
        {
            switch (result)
            {
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    //note a commit validation was allready done...
                    _serviceAgent.CommitMenuItemRepository();
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedMenuItem.MenuItemID = SelectedMenuItemMirror.MenuItemID;
                    break;
            }
        }

        private void NotifyMessage(string message)
        {// Notify view of an error message w/o throwing an error...
            Notify(MessageNotice, new NotificationEventArgs<Exception>(message));
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
        public static object GetPropertyValue(this NestedMenuItem myObj, string propertyName)
        {
            var propInfo = typeof(NestedMenuItem).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this NestedMenuItem myObj, string propertyName)
        {
            var propInfo = typeof(NestedMenuItem).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this NestedMenuItem myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(NestedMenuItem).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }

        public static object GetPropertyValue(this MenuItem myObj, string propertyName)
        {
            var propInfo = typeof(MenuItem).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this MenuItem myObj, string propertyName)
        {
            var propInfo = typeof(MenuItem).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this MenuItem myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(MenuItem).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}