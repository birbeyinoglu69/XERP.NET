using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using ExtensionMethods;
using SimpleMvvmToolkit;
using XERP.Client.Models;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Client.WPF.MenuItemMaintenance.ViewModels
{
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newMenuItemCodeAutoId;

        private IMenuItemServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public CodeMaintenanceViewModel(){}

        public CodeMaintenanceViewModel(IMenuItemServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            MenuItemCodeList = new BindingList<MenuItemCode>();
            //disable new row feature...
            MenuItemCodeList.AllowNew = false;

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
            if (ClientSessionSingleton.Instance.ExecutableProgramIDList.Contains(_globalProperties.ExecutableProgramName))
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
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> SaveRequiredNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> NewRecordNeededNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        public event EventHandler<NotificationEventArgs> NewRecordCreatedNotice;
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

        private string _menuItemCodeListCount;
        public string MenuItemCodeListCount
        {
            get { return _menuItemCodeListCount; }
            set
            {
                _menuItemCodeListCount = value;
                NotifyPropertyChanged(m => m.MenuItemCodeListCount);
            }
        }
        #endregion General Form Function/State Properties
        private BindingList<MenuItemCode> _menuItemCodeList;
        public BindingList<MenuItemCode> MenuItemCodeList
        {
            get
            {
                MenuItemCodeListCount = _menuItemCodeList.Count.ToString();
                if (_menuItemCodeList.Count > 0)
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
                return _menuItemCodeList;
            }
            set
            {
                _menuItemCodeList = value;
                NotifyPropertyChanged(m => m.MenuItemCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private MenuItemCode _selectedMenuItemCodeMirror;
        public MenuItemCode SelectedMenuItemCodeMirror
        {
            get { return _selectedMenuItemCodeMirror; }
            set { _selectedMenuItemCodeMirror = value; }
        }

        private System.Collections.IList _selectedMenuItemCodeList;
        public System.Collections.IList SelectedMenuItemCodeList
        {
            get { return _selectedMenuItemCodeList; }
            set
            {
                if (_selectedMenuItemCode != value)
                {
                    _selectedMenuItemCodeList = value;
                    NotifyPropertyChanged(m => m.SelectedMenuItemCodeList);
                }
            }
        }

        private MenuItemCode _selectedMenuItemCode;
        public MenuItemCode SelectedMenuItemCode
        {
            get
            {
                return _selectedMenuItemCode;
            }
            set
            {
                if (_selectedMenuItemCode != value)
                {
                    _selectedMenuItemCode = value;
                    //set the mirrored SelectedMenuItemCode to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedMenuItemCodeMirror = new MenuItemCode();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedMenuItemCode.GetType().GetProperties())
                        {
                            SelectedMenuItemCodeMirror.SetPropertyValue(prop.Name, SelectedMenuItemCode.GetPropertyValue(prop.Name));
                        }
                        SelectedMenuItemCodeMirror.MenuItemCodeID = _selectedMenuItemCode.MenuItemCodeID;
                        NotifyPropertyChanged(m => m.SelectedMenuItemCode);

                        SelectedMenuItemCode.PropertyChanged += new PropertyChangedEventHandler(SelectedMenuItemCode_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _menuItemCodeColumnMetaDataList;
        public List<ColumnMetaData> MenuItemCodeColumnMetaDataList
        {
            get { return _menuItemCodeColumnMetaDataList; }
            set
            {
                _menuItemCodeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.MenuItemCodeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _menuItemCodeMaxFieldValueDictionary;
        public Dictionary<string, int> MenuItemCodeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_menuItemCodeMaxFieldValueDictionary != null)
                {
                    return _menuItemCodeMaxFieldValueDictionary;
                }
                _menuItemCodeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("MenuItemCodes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _menuItemCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _menuItemCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedMenuItemCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "MenuItemCodeID")
            {//make sure it is has changed...
                if (SelectedMenuItemCodeMirror.MenuItemCodeID != SelectedMenuItemCode.MenuItemCodeID)
                {
                    //if their are no records it is a key change
                    if (MenuItemCodeList != null && MenuItemCodeList.Count == 0
                        && SelectedMenuItemCode != null && !string.IsNullOrEmpty(SelectedMenuItemCode.MenuItemCodeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetMenuItemCodeState(SelectedMenuItemCode);

                    if (entityState == EntityStates.Unchanged ||
                        entityState == EntityStates.Modified)
                    {//once a key is added it can not be modified...
                        if (Dirty && AllowCommit)//dirty record exists ask if save is required...
                            NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ChangeKeyLogic);
                        else
                            ChangeKeyLogic();

                        return;
                    }
                }
            }//end KeyID logic...

            object propertyChangedValue = SelectedMenuItemCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedMenuItemCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyCode = SelectedMenuItemCode.GetPropertyCode(e.PropertyName);
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
                if (MenuItemCodePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyCode))
                {
                    Update(SelectedMenuItemCode);
                    SelectedMenuItemCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedMenuItemCodeMirror.IsValid = SelectedMenuItemCode.IsValid;
                    SelectedMenuItemCodeMirror.IsExpanded = SelectedMenuItemCode.IsExpanded;
                    SelectedMenuItemCodeMirror.NotValidMessage = SelectedMenuItemCode.NotValidMessage;

                }
                else//revert back to its previous value... 
                {
                    SelectedMenuItemCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedMenuItemCode.IsValid = SelectedMenuItemCodeMirror.IsValid;
                    SelectedMenuItemCode.IsExpanded = SelectedMenuItemCodeMirror.IsExpanded;
                    SelectedMenuItemCode.NotValidMessage = SelectedMenuItemCodeMirror.NotValidMessage;
                }

            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedMenuItemCode.MenuItemCodeID))
            {//check to see if key is part of the current companylist...
                MenuItemCode query = MenuItemCodeList.Where(company => company.MenuItemCodeID == SelectedMenuItemCode.MenuItemCodeID &&
                                                        company.AutoID != SelectedMenuItemCode.AutoID).SingleOrDefault();
                if (query != null)
                {//revert it back...
                    SelectedMenuItemCode.MenuItemCodeID = SelectedMenuItemCodeMirror.MenuItemCodeID;
                    //change to the newly selected item...
                    SelectedMenuItemCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                MenuItemCodeList = GetMenuItemCodeByID(SelectedMenuItemCode.MenuItemCodeID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (MenuItemCodeList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedMenuItemCode.MenuItemCodeID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedMenuItemCode = MenuItemCodeList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedMenuItemCode.MenuItemCodeID != SelectedMenuItemCodeMirror.MenuItemCodeID)
                    SelectedMenuItemCode.MenuItemCodeID = SelectedMenuItemCodeMirror.MenuItemCodeID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in MenuItemCodeList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedMenuItemCode = new MenuItemCode();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            MenuItemCodeList.Clear();
            SetAsEmptySelection();
        }

        private bool MenuItemCodePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "MenuItemCodeID":
                    rBool = MenuItemCodeIsValid(SelectedMenuItemCode, _companyValidationProperties.MenuItemCodeID, out errorMessage);
                    break;
                case "Name":
                    rBool = MenuItemCodeIsValid(SelectedMenuItemCode, _companyValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedMenuItemCode.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedMenuItemCode.IsValid = MenuItemCodeIsValid(SelectedMenuItemCode, out errorMessage);
                if (SelectedMenuItemCode.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedMenuItemCode.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _companyValidationProperties
        {//we list all fields that require validation...
            MenuItemCodeID,
            Name
        }

        //Object.Property Scope Validation...
        private bool MenuItemCodeIsValid(MenuItemCode item, _companyValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _companyValidationProperties.MenuItemCodeID:
                    //validate key
                    if (string.IsNullOrEmpty(item.MenuItemCodeID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetMenuItemCodeState(item);
                    if (entityState == EntityStates.Added && MenuItemCodeExists(item.MenuItemCodeID, ClientSessionSingleton.Instance.CompanyID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = MenuItemCodeList.Count(q => q.MenuItemCodeID == item.MenuItemCodeID);
                    if (count > 1)
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    break;
                case _companyValidationProperties.Name:
                    //validate Description
                    if (string.IsNullOrEmpty(item.Description))
                    {
                        errorMessage = "Description Is Required.";
                        return false;
                    }
                    break;
            }
            return true;
        }
        //MenuItemCode Object Scope Validation check the entire object for validity...
        private byte MenuItemCodeIsValid(MenuItemCode item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.MenuItemCodeID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetMenuItemCodeState(item);
            if (entityState == EntityStates.Added && MenuItemCodeExists(item.MenuItemCodeID, ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = MenuItemCodeList.Count(q => q.MenuItemCodeID == item.MenuItemCodeID);
            if (count > 1)
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //validate Description
            if (string.IsNullOrEmpty(item.Description))
            {
                errorMessage = "Description Is Required.";
                return 1;
            }
            //a value of 2 is pending changes...
            //On Commit we will give it a value of 0...
            return 2;
        }
        #endregion Validation Methods
        #endregion ViewModel Logic Methods

        #region ServiceAgent Call Methods
        private EntityStates GetMenuItemCodeState(MenuItemCode itemCode)
        {
            return _serviceAgent.GetMenuItemCodeEntityState(itemCode);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.MenuItemCodeRepositoryIsDirty();
        }

        #region MenuItemCode CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedMenuItemCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (MenuItemCode itemCode in MenuItemCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (itemCode.AutoID > 0)
                    autoIDs = autoIDs + itemCode.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                MenuItemCodeList = new BindingList<MenuItemCode>(_serviceAgent.RefreshMenuItemCode(autoIDs).ToList());
                SelectedMenuItemCode = (from q in MenuItemCodeList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<MenuItemCode> GetMenuItemCodes(string companyID)
        {
            BindingList<MenuItemCode> menuItemCodeList = new BindingList<MenuItemCode>(_serviceAgent.GetMenuItemCodes(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return menuItemCodeList;
        }

        private BindingList<MenuItemCode> GetMenuItemCodes(MenuItemCode itemCode, string companyID)
        {
            BindingList<MenuItemCode> itemCodeList = new BindingList<MenuItemCode>(_serviceAgent.GetMenuItemCodes(itemCode, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemCodeList;
        }

        private BindingList<MenuItemCode> GetMenuItemCodeByID(string itemCodeID, string companyID)
        {
            BindingList<MenuItemCode> itemCodeList = new BindingList<MenuItemCode>(_serviceAgent.GetMenuItemCodeByID(itemCodeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemCodeList;
        }

        private bool MenuItemCodeExists(string itemCodeID, string companyID)
        {
            return _serviceAgent.MenuItemCodeExists(itemCodeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(MenuItemCode item)
        {
            _serviceAgent.UpdateMenuItemCodeRepository(item);
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
            var items = (from q in MenuItemCodeList where q.IsValid == 2 select q).ToList();
            foreach (MenuItemCode item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitMenuItemCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }


        private bool Delete(MenuItemCode itemCode)
        {
            _serviceAgent.DeleteFromMenuItemCodeRepository(itemCode);
            return true;
        }

        private bool NewMenuItemCode(string id)
        {
            MenuItemCode item = new MenuItemCode();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newMenuItemCodeAutoId = _newMenuItemCodeAutoId - 1;
            item.AutoID = _newMenuItemCodeAutoId;
            item.MenuItemCodeID = id;
            item.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            item.IsValid = 1;
            item.NotValidMessage = "New Record Key Field/s Are Required.";
            MenuItemCodeList.Add(item);
            _serviceAgent.AddToMenuItemCodeRepository(item);
            SelectedMenuItemCode = MenuItemCodeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion MenuItemCode CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                MenuItemCodeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewMenuItemCodeCommand(""); //this will generate a new itemCode and set it as the selected itemCode...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedMenuItemCode.SetPropertyValue(MenuItemCodeColumnMetaDataList[i].Name, columnValue);
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
            if (GetMenuItemCodeState(SelectedMenuItemCode) != EntityStates.Detached)
            {
                if (Update(SelectedMenuItemCode))
                    Commit();
                else
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }
        public void RefreshCommand()
        {
            Refresh();
        }
        public void DeleteMenuItemCodeCommand()
        {
            try
            {//company is fk to 100's of tables deleting it can be tricky...
                int i = 0;
                int ii = 0;
                for (int j = SelectedMenuItemCodeList.Count - 1; j >= 0; j--)
                {
                    MenuItemCode item = (MenuItemCode)SelectedMenuItemCodeList[j];
                    //get Max Index...
                    i = MenuItemCodeList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    MenuItemCodeList.Remove(item);
                }

                if (MenuItemCodeList != null && MenuItemCodeList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= MenuItemCodeList.Count())
                        ii = MenuItemCodeList.Count - 1;

                    SelectedMenuItemCode = MenuItemCodeList[ii];
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
                NotifyMessage("MenuItemCode/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }


        public void NewMenuItemCodeCommand()
        {
            NewMenuItemCode("");
            AllowCommit = false;
        }

        public void NewMenuItemCodeCommand(string itemCodeID)
        {
            NewMenuItemCode(itemCodeID);
            if (string.IsNullOrEmpty(itemCodeID)) //don't allow a save until a itemCodeID is provided...
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
            RegisterToReceiveMessages<BindingList<MenuItemCode>>(MessageTokens.MenuItemCodeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<MenuItemCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                MenuItemCodeList = e.Data;
                SelectedMenuItemCode = MenuItemCodeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<MenuItemCode>>(MessageTokens.MenuItemCodeSearchToken.ToString(), OnSearchResult);
        }

        #endregion Commands

        #region Completion Callbacks

        // TODO: Optionally add callback methods for async calls to the service agent

        #endregion Completion Callbacks

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
        
        private void NotifySearch(string message)
        {//Notify view to launch search...
            Notify(SearchNotice, new NotificationEventArgs(message));
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
                    SelectedMenuItemCode.MenuItemCodeID = SelectedMenuItemCodeMirror.MenuItemCodeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with MenuItemCodeID provided...
                    NewMenuItemCodeCommand(SelectedMenuItemCode.MenuItemCodeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedMenuItemCode.MenuItemCodeID = SelectedMenuItemCodeMirror.MenuItemCodeID;
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
                    _serviceAgent.CommitMenuItemCodeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedMenuItemCode.MenuItemCodeID = SelectedMenuItemCodeMirror.MenuItemCodeID;
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
        public static object GetPropertyValue(this MenuItemCode myObj, string propertyName)
        {
            var propInfo = typeof(MenuItemCode).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyCode(this MenuItemCode myObj, string propertyName)
        {
            var propInfo = typeof(MenuItemCode).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this MenuItemCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(MenuItemCode).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}