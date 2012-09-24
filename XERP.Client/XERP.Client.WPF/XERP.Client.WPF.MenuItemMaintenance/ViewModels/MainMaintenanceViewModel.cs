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
using XERP.Domain.MenuSecurityDomain.Services;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
//required for extension methods...
using ExtensionMethods;
using XERP.Client.Models;

namespace XERP.Client.WPF.MenuItemMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        
        private IMenuItemServiceAgent _serviceAgent;
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
            MenuItemTypeList = BuildMenuItemTypeDropDown();
            MenuItemCodeList = BuildMenuItemCodeDropDown();
        }

        public MainMaintenanceViewModel(IMenuItemServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            MenuItemList = new BindingList<MenuItem>();
            //disable new row feature...
            MenuItemList.AllowNew = false;
            
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
                return _menuItemList;
            }
            set
            {
                _menuItemList = value;
                NotifyPropertyChanged(m => m.MenuItemList);
            }
        }

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

        //this is used to collect previous values as to compare the changed values...
        private MenuItem _selectedMenuItemMirror;
        public MenuItem SelectedMenuItemMirror
        {
            get { return _selectedMenuItemMirror; }
            set { _selectedMenuItemMirror = value; }
        }

        private System.Collections.IList _selectedMenuItemList;
        public System.Collections.IList SelectedMenuItemList
        {
            get { return _selectedMenuItemList; }
            set
            {
                if (_selectedMenuItem != value)
                {
                    _selectedMenuItemList = value;
                    NotifyPropertyChanged(m => m.SelectedMenuItemList);
                }  
            }
        }

        private MenuItem _selectedMenuItem;
        public MenuItem SelectedMenuItem
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
                    //set the mirrored SelectedMenuItem to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedMenuItemMirror = new MenuItem();
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

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _menuItemMaxFieldValueDictionary;
        public Dictionary<string, int> MenuItemMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_menuItemMaxFieldValueDictionary != null)
                {
                    return _menuItemMaxFieldValueDictionary;
                }
                _menuItemMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("MenuItems");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _menuItemMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _menuItemMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedMenuItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "MenuItemID")
            {//make sure it is has changed...
                if (SelectedMenuItemMirror.MenuItemID != SelectedMenuItem.MenuItemID)
                {
                    //if their are no records it is a key change
                    if (MenuItemList != null && MenuItemList.Count == 0
                        && SelectedMenuItem != null && !string.IsNullOrEmpty(SelectedMenuItem.MenuItemID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetMenuItemState(SelectedMenuItem);

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
            
            object propertyChangedValue = SelectedMenuItem.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedMenuItemMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedMenuItem.GetPropertyType(e.PropertyName);
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
                    Update(SelectedMenuItem);
                    //set the mirrored objects field...
                    SelectedMenuItemMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedMenuItem.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedMenuItem.MenuItemID, out errorMessage))
            {
                //check to see if key is part of the current menuItemlist...
                MenuItem query = MenuItemList.Where(menuItem => menuItem.MenuItemID == SelectedMenuItem.MenuItemID &&
                                                        menuItem.AutoID != SelectedMenuItem.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected menuItem...
                    SelectedMenuItem = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                MenuItemList = GetMenuItemByID(SelectedMenuItem.MenuItemID, ClientSessionSingleton.Instance.CompanyID);
                if (MenuItemList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedMenuItem.MenuItemID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedMenuItem = MenuItemList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedMenuItem.MenuItemID != SelectedMenuItemMirror.MenuItemID)
                {
                    SelectedMenuItem.MenuItemID = SelectedMenuItemMirror.MenuItemID;
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
            foreach (MenuItem menuItem in MenuItemList)
            {
                EntityStates entityState = GetMenuItemState(menuItem);
                if (entityState == EntityStates.Modified || 
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(menuItem, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(menuItem.Name, out errorMessage) == false)
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
                case "MenuItemID":
                    rBool = NewKeyIsValid(SelectedMenuItem, out errorMessage);
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

        private bool NewKeyIsValid(MenuItem menuItem, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(menuItem.MenuItemID, out errorMessage) == false)
            {
                return false;
            }
            if (MenuItemExists(menuItem.MenuItemID.ToString()))
            {
                errorMessage = "MenuItemID " + menuItem.MenuItemID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object menuItemID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)menuItemID))
            {
                errorMessage = "MenuItemID Is Required...";
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
        private ObservableCollection<MenuItemType> BuildMenuItemTypeDropDown()
        {
            List<MenuItemType> list = new List<MenuItemType>();
            list = _serviceAgent.GetMenuItemTypes(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new MenuItemType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<MenuItemType>(list);
        }

        private ObservableCollection<MenuItemCode> BuildMenuItemCodeDropDown()
        {
            List<MenuItemCode> list = new List<MenuItemCode>();
            list = _serviceAgent.GetMenuItemCodes(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new MenuItemCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<MenuItemCode>(list);
        }

        private EntityStates GetMenuItemState(MenuItem menuItem)
        {
            return _serviceAgent.GetMenuItemEntityState(menuItem);
        }

        #region MenuItem CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedMenuItem.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (MenuItem menuItem in MenuItemList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (menuItem.AutoID > 0)
                {
                    autoIDs = autoIDs + menuItem.AutoID.ToString() + ",";
                }
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                MenuItemList = new BindingList<MenuItem>(_serviceAgent.RefreshMenuItem(autoIDs).ToList());
                SelectedMenuItem = (from q in MenuItemList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<MenuItem> GetMenuItems(string companyID)
        {
            BindingList<MenuItem> menuItemList = new BindingList<MenuItem>(_serviceAgent.GetMenuItems(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return menuItemList; 
        }

        private BindingList<MenuItem> GetMenuItems(MenuItem menuItem, string companyID)
        {
            BindingList<MenuItem> menuItemList = new BindingList<MenuItem>(_serviceAgent.GetMenuItems(menuItem, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return menuItemList;
        }

        private BindingList<MenuItem> GetMenuItemByID(string menuItemID, string companyID)
        {
            BindingList<MenuItem> menuItemList = new BindingList<MenuItem>(_serviceAgent.GetMenuItemByID(menuItemID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return menuItemList; 
        }

        private bool MenuItemExists(string menuItemID)
        {
            return _serviceAgent.MenuItemExists(menuItemID, ClientSessionSingleton.Instance.CompanyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(MenuItem menuItem)
        {
            _serviceAgent.UpdateMenuItemRepository(menuItem);
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
            _serviceAgent.CommitMenuItemRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(MenuItem menuItem)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromMenuItemRepository(menuItem);
            return true;
        }

        private bool NewMenuItem(string menuItemID)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.MenuItemID = menuItemID;
            menuItem.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            MenuItemList.Add(menuItem);
            _serviceAgent.AddToMenuItemRepository(menuItem);
            SelectedMenuItem = MenuItemList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion MenuItem CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedMenuItem = new MenuItem();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            MenuItemList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                MenuItemColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewMenuItemCommand(""); //this will generate a new menuItem and set it as the selected menuItem...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedMenuItem.SetPropertyValue(MenuItemColumnMetaDataList[i].Name, columnValue);
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
            if (GetMenuItemState(SelectedMenuItem) != EntityStates.Detached)
            {
                if (Update(SelectedMenuItem))
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
            for (int j = SelectedMenuItemList.Count - 1; j >= 0; j--)
            {
                MenuItem menuItem = (MenuItem)SelectedMenuItemList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = MenuItemList.IndexOf(menuItem) - SelectedMenuItemList.Count;
                }
                
                Delete(menuItem);
                MenuItemList.Remove(menuItem);
            }

            if (MenuItemList != null && MenuItemList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedMenuItem = MenuItemList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }  
        }

        public void NewMenuItemCommand()
        {
            NewMenuItem("");
            AllowCommit = false;
        }

        public void NewMenuItemCommand(string menuItemID)
        {
            NewMenuItem(menuItemID);
            if (string.IsNullOrEmpty(menuItemID))
            {//don't allow a save until a menuItemID is provided...
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
            RegisterToReceiveMessages<BindingList<MenuItem>>(MessageTokens.MenuItemSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<MenuItem>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                MenuItemList = e.Data;
                SelectedMenuItem = MenuItemList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<MenuItem>>(MessageTokens.MenuItemSearchToken.ToString(), OnSearchResult);
        }

        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<MenuItemType>>(MessageTokens.MenuItemTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<MenuItemType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SelectedMenuItem.MenuItemTypeID = e.Data.FirstOrDefault().MenuItemTypeID;
            }
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
            {
                SelectedMenuItem.MenuItemCodeID = e.Data.FirstOrDefault().MenuItemCodeID;
            }
            UnregisterToReceiveMessages<BindingList<MenuItemType>>(MessageTokens.MenuItemTypeSearchToken.ToString(), OnTypeSearchResult);
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
                    _serviceAgent.CommitMenuItemRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedMenuItem.MenuItemID = SelectedMenuItemMirror.MenuItemID;
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
        public static object GetPropertyValue(this MenuItem myObj, string propertyName)
        {
            var propInfo = typeof(MenuItem).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyType(this MenuItem myObj, string propertyName)
        {
            var propInfo = typeof(MenuItem).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this MenuItem myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(MenuItem).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}