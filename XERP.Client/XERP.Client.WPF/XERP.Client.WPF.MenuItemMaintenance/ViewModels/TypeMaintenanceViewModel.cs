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

namespace XERP.Client.WPF.MenuItemMaintenance.ViewModels
{
    public class TypeMaintenanceViewModel : ViewModelBase<TypeMaintenanceViewModel>
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
        public TypeMaintenanceViewModel()
        { }

        public TypeMaintenanceViewModel(IMenuItemServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            MenuItemTypeList = new BindingList<MenuItemType>();
            //disable new row feature...
            MenuItemTypeList.AllowNew = false;

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
            //MenuItemTypeColumnMetaDataList = new List<ColumnMetaData>();
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

        private string _menuItemTypeListCount;
        public string MenuItemTypeListCount
        {
            get { return _menuItemTypeListCount; }
            set
            {
                _menuItemTypeListCount = value;
                NotifyPropertyChanged(m => m.MenuItemTypeListCount);
            }
        }

        private BindingList<MenuItemType> _menuItemTypeList;
        public BindingList<MenuItemType> MenuItemTypeList
        {
            get
            {
                MenuItemTypeListCount = _menuItemTypeList.Count.ToString();
                if (_menuItemTypeList.Count > 0)
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
                return _menuItemTypeList;
            }
            set
            {
                _menuItemTypeList = value;
                NotifyPropertyChanged(m => m.MenuItemTypeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private MenuItemType _selectedMenuItemTypeMirror;
        public MenuItemType SelectedMenuItemTypeMirror
        {
            get { return _selectedMenuItemTypeMirror; }
            set { _selectedMenuItemTypeMirror = value; }
        }

        private System.Collections.IList _selectedMenuItemTypeList;
        public System.Collections.IList SelectedMenuItemTypeList
        {
            get { return _selectedMenuItemTypeList; }
            set
            {
                if (_selectedMenuItemType != value)
                {
                    _selectedMenuItemTypeList = value;
                    NotifyPropertyChanged(m => m.SelectedMenuItemTypeList);
                }
            }
        }

        private MenuItemType _selectedMenuItemType;
        public MenuItemType SelectedMenuItemType
        {
            get
            {
                return _selectedMenuItemType;
            }
            set
            {
                if (_selectedMenuItemType != value)
                {
                    _selectedMenuItemType = value;
                    //set the mirrored SelectedMenuItemType to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedMenuItemTypeMirror = new MenuItemType();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedMenuItemType.GetType().GetProperties())
                        {
                            SelectedMenuItemTypeMirror.SetPropertyValue(prop.Name, SelectedMenuItemType.GetPropertyValue(prop.Name));
                        }
                        SelectedMenuItemTypeMirror.MenuItemTypeID = _selectedMenuItemType.MenuItemTypeID;
                        NotifyPropertyChanged(m => m.SelectedMenuItemType);

                        SelectedMenuItemType.PropertyChanged += new PropertyChangedEventHandler(SelectedMenuItemType_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _menuItemTypeColumnMetaDataList;
        public List<ColumnMetaData> MenuItemTypeColumnMetaDataList
        {
            get { return _menuItemTypeColumnMetaDataList; }
            set
            {
                _menuItemTypeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.MenuItemTypeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _menuItemTypeMaxFieldValueDictionary;
        public Dictionary<string, int> MenuItemTypeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_menuItemTypeMaxFieldValueDictionary != null)
                {
                    return _menuItemTypeMaxFieldValueDictionary;
                }
                _menuItemTypeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("MenuItemTypes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _menuItemTypeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _menuItemTypeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedMenuItemType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "MenuItemTypeID")
            {//make sure it is has changed...
                if (SelectedMenuItemTypeMirror.MenuItemTypeID != SelectedMenuItemType.MenuItemTypeID)
                {
                    //if their are no records it is a key change
                    if (MenuItemTypeList != null && MenuItemTypeList.Count == 0
                        && SelectedMenuItemType != null && !string.IsNullOrEmpty(SelectedMenuItemType.MenuItemTypeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetMenuItemTypeState(SelectedMenuItemType);

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

            object propertyChangedValue = SelectedMenuItemType.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedMenuItemTypeMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedMenuItemType.GetPropertyType(e.PropertyName);
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
                    Update(SelectedMenuItemType);
                    //set the mirrored objects field...
                    SelectedMenuItemTypeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedMenuItemType.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedMenuItemType.MenuItemTypeID, out errorMessage))
            {
                //check to see if key is part of the current menuItemTypelist...
                MenuItemType query = MenuItemTypeList.Where(menuItemType => menuItemType.MenuItemTypeID == SelectedMenuItemType.MenuItemTypeID &&
                                                        menuItemType.AutoID != SelectedMenuItemType.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected menuItemType...
                    SelectedMenuItemType = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                MenuItemTypeList = GetMenuItemTypeByID(SelectedMenuItemType.MenuItemTypeID, ClientSessionSingleton.Instance.CompanyID);
                if (MenuItemTypeList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedMenuItemType.MenuItemTypeID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedMenuItemType = MenuItemTypeList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedMenuItemType.MenuItemTypeID != SelectedMenuItemTypeMirror.MenuItemTypeID)
                {
                    SelectedMenuItemType.MenuItemTypeID = SelectedMenuItemTypeMirror.MenuItemTypeID;
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
            foreach (MenuItemType menuItemType in MenuItemTypeList)
            {
                EntityStates entityState = GetMenuItemTypeState(menuItemType);
                if (entityState == EntityStates.Modified ||
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(menuItemType, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(menuItemType.Type, out errorMessage) == false)
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
                case "MenuItemTypeID":
                    rBool = NewKeyIsValid(SelectedMenuItemType, out errorMessage);
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

        private bool NewKeyIsValid(MenuItemType menuItemType, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(menuItemType.MenuItemTypeID, out errorMessage) == false)
            {
                return false;
            }

            if (MenuItemTypeExists(menuItemType.MenuItemTypeID.ToString(), ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "MenuItemTypeID " + menuItemType.MenuItemTypeID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object menuItemTypeID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)menuItemTypeID))
            {
                errorMessage = "MenuItemTypeID Is Required...";
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

        private EntityStates GetMenuItemTypeState(MenuItemType menuItemType)
        {
            return _serviceAgent.GetMenuItemTypeEntityState(menuItemType);
        }

        #region MenuItemType CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedMenuItemType.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (MenuItemType menuItemType in MenuItemTypeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (menuItemType.AutoID > 0)
                {
                    autoIDs = autoIDs + menuItemType.AutoID.ToString() + ",";
                }
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                MenuItemTypeList = new BindingList<MenuItemType>(_serviceAgent.RefreshMenuItemType(autoIDs).ToList());
                SelectedMenuItemType = (from q in MenuItemTypeList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<MenuItemType> GetMenuItemTypes(string companyID)
        {
            BindingList<MenuItemType> menuItemTypeList = new BindingList<MenuItemType>(_serviceAgent.GetMenuItemTypes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return menuItemTypeList;
        }

        private BindingList<MenuItemType> GetMenuItemTypes(MenuItemType menuItemType, string companyID)
        {
            BindingList<MenuItemType> menuItemTypeList = new BindingList<MenuItemType>(_serviceAgent.GetMenuItemTypes(menuItemType, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return menuItemTypeList;
        }

        private BindingList<MenuItemType> GetMenuItemTypeByID(string menuItemTypeID, string companyID)
        {
            BindingList<MenuItemType> menuItemTypeList = new BindingList<MenuItemType>(_serviceAgent.GetMenuItemTypeByID(menuItemTypeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return menuItemTypeList;
        }

        private bool MenuItemTypeExists(string menuItemTypeID, string companyID)
        {
            return _serviceAgent.MenuItemTypeExists(menuItemTypeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(MenuItemType menuItemType)
        {
            _serviceAgent.UpdateMenuItemTypeRepository(menuItemType);
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
            _serviceAgent.CommitMenuItemTypeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(MenuItemType menuItemType)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromMenuItemTypeRepository(menuItemType);
            return true;
        }

        private bool NewMenuItemType(string menuItemTypeID)
        {
            MenuItemType menuItemType = new MenuItemType();
            menuItemType.MenuItemTypeID = menuItemTypeID;
            menuItemType.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            MenuItemTypeList.Add(menuItemType);
            _serviceAgent.AddToMenuItemTypeRepository(menuItemType);
            SelectedMenuItemType = MenuItemTypeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion MenuItemType CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedMenuItemType = new MenuItemType();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            MenuItemTypeList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                MenuItemTypeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewMenuItemTypeCommand(""); //this will generate a new menuItemType and set it as the selected menuItemType...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedMenuItemType.SetPropertyValue(MenuItemTypeColumnMetaDataList[i].Name, columnValue);
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
            if (GetMenuItemTypeState(SelectedMenuItemType) != EntityStates.Detached)
            {
                if (Update(SelectedMenuItemType))
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
            for (int j = SelectedMenuItemTypeList.Count - 1; j >= 0; j--)
            {
                MenuItemType menuItemType = (MenuItemType)SelectedMenuItemTypeList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = MenuItemTypeList.IndexOf(menuItemType) - SelectedMenuItemTypeList.Count;
                }

                Delete(menuItemType);
                MenuItemTypeList.Remove(menuItemType);
            }

            if (MenuItemTypeList != null && MenuItemTypeList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedMenuItemType = MenuItemTypeList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }
        }

        public void NewMenuItemTypeCommand()
        {
            NewMenuItemType("");
            AllowCommit = false;
        }

        public void NewMenuItemTypeCommand(string menuItemTypeID)
        {
            NewMenuItemType(menuItemTypeID);
            if (string.IsNullOrEmpty(menuItemTypeID))
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
            RegisterToReceiveMessages<BindingList<MenuItemType>>(MessageTokens.MenuItemTypeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<MenuItemType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                MenuItemTypeList = e.Data;
                SelectedMenuItemType = MenuItemTypeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<MenuItemType>>(MessageTokens.MenuItemTypeSearchToken.ToString(), OnSearchResult);
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
                    SelectedMenuItemType.MenuItemTypeID = SelectedMenuItemTypeMirror.MenuItemTypeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with MenuItemTypeID provided...
                    NewMenuItemTypeCommand(SelectedMenuItemType.MenuItemTypeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedMenuItemType.MenuItemTypeID = SelectedMenuItemTypeMirror.MenuItemTypeID;
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
                    _serviceAgent.CommitMenuItemTypeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedMenuItemType.MenuItemTypeID = SelectedMenuItemTypeMirror.MenuItemTypeID;
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
        public static object GetPropertyValue(this MenuItemType myObj, string propertyName)
        {
            var propInfo = typeof(MenuItemType).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyType(this MenuItemType myObj, string propertyName)
        {
            var propInfo = typeof(MenuItemType).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this MenuItemType myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(MenuItemType).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}