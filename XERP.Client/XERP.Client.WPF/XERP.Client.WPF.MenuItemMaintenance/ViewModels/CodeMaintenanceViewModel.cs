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
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
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
        public CodeMaintenanceViewModel()
        { }

        public CodeMaintenanceViewModel(IMenuItemServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            MenuItemCodeList = new BindingList<MenuItemCode>();
            //disable new row feature...
            MenuItemCodeList.AllowNew = false;

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
            //MenuItemCodeColumnMetaDataList = new List<ColumnMetaData>();
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
                    {
                        _menuItemCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _menuItemCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedMenuItemCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
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

            object propertyChangedValue = SelectedMenuItemCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedMenuItemCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyCode = SelectedMenuItemCode.GetPropertyCode(e.PropertyName);
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
                    Update(SelectedMenuItemCode);
                    //set the mirrored objects field...
                    SelectedMenuItemCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedMenuItemCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedMenuItemCode.MenuItemCodeID, out errorMessage))
            {
                //check to see if key is part of the current menuItemCodelist...
                MenuItemCode query = MenuItemCodeList.Where(menuItemCode => menuItemCode.MenuItemCodeID == SelectedMenuItemCode.MenuItemCodeID &&
                                                        menuItemCode.AutoID != SelectedMenuItemCode.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected menuItemCode...
                    SelectedMenuItemCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                MenuItemCodeList = GetMenuItemCodeByID(SelectedMenuItemCode.MenuItemCodeID, ClientSessionSingleton.Instance.CompanyID);
                if (MenuItemCodeList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedMenuItemCode.MenuItemCodeID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedMenuItemCode = MenuItemCodeList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedMenuItemCode.MenuItemCodeID != SelectedMenuItemCodeMirror.MenuItemCodeID)
                {
                    SelectedMenuItemCode.MenuItemCodeID = SelectedMenuItemCodeMirror.MenuItemCodeID;
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
            foreach (MenuItemCode menuItemCode in MenuItemCodeList)
            {
                EntityStates entityState = GetMenuItemCodeState(menuItemCode);
                if (entityState == EntityStates.Modified ||
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(menuItemCode, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(menuItemCode.Code, out errorMessage) == false)
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
                case "MenuItemCodeID":
                    rBool = NewKeyIsValid(SelectedMenuItemCode, out errorMessage);
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

        private bool NewKeyIsValid(MenuItemCode menuItemCode, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(menuItemCode.MenuItemCodeID, out errorMessage) == false)
            {
                return false;
            }
            if (MenuItemCodeExists(menuItemCode.MenuItemCodeID.ToString(), ClientSessionSingleton.Instance.CompanyID))
            {
                errorMessage = "MenuItemCodeID " + menuItemCode.MenuItemCodeID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object menuItemCodeID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)menuItemCodeID))
            {
                errorMessage = "MenuItemCodeID Is Required...";
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

        private EntityStates GetMenuItemCodeState(MenuItemCode menuItemCode)
        {
            return _serviceAgent.GetMenuItemCodeEntityState(menuItemCode);
        }

        #region MenuItemCode CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedMenuItemCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (MenuItemCode menuItemCode in MenuItemCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (menuItemCode.AutoID > 0)
                {
                    autoIDs = autoIDs + menuItemCode.AutoID.ToString() + ",";
                }
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

        private BindingList<MenuItemCode> GetMenuItemCodes(MenuItemCode menuItemCode, string companyID)
        {
            BindingList<MenuItemCode> menuItemCodeList = new BindingList<MenuItemCode>(_serviceAgent.GetMenuItemCodes(menuItemCode, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return menuItemCodeList;
        }

        private BindingList<MenuItemCode> GetMenuItemCodeByID(string menuItemCodeID, string companyID)
        {
            BindingList<MenuItemCode> menuItemCodeList = new BindingList<MenuItemCode>(_serviceAgent.GetMenuItemCodeByID(menuItemCodeID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return menuItemCodeList;
        }

        private bool MenuItemCodeExists(string menuItemCodeID, string companyID)
        {
            return _serviceAgent.MenuItemCodeExists(menuItemCodeID, companyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(MenuItemCode menuItemCode)
        {
            _serviceAgent.UpdateMenuItemCodeRepository(menuItemCode);
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
            _serviceAgent.CommitMenuItemCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(MenuItemCode menuItemCode)
        {
            _serviceAgent.DeleteFromMenuItemCodeRepository(menuItemCode);
            return true;
        }

        private bool NewMenuItemCode(string menuItemCodeID)
        {
            MenuItemCode menuItemCode = new MenuItemCode();
            menuItemCode.MenuItemCodeID = menuItemCodeID;
            menuItemCode.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            MenuItemCodeList.Add(menuItemCode);
            _serviceAgent.AddToMenuItemCodeRepository(menuItemCode);
            SelectedMenuItemCode = MenuItemCodeList.LastOrDefault();
            
            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion MenuItemCode CRUD
        #endregion ServiceAgent Call Methods

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
                    NewMenuItemCodeCommand(""); //this will generate a new menuItemCode and set it as the selected menuItemCode...
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
            for (int j = SelectedMenuItemCodeList.Count - 1; j >= 0; j--)
            {
                MenuItemCode menuItemCode = (MenuItemCode)SelectedMenuItemCodeList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = MenuItemCodeList.IndexOf(menuItemCode) - SelectedMenuItemCodeList.Count;
                }

                Delete(menuItemCode);
                MenuItemCodeList.Remove(menuItemCode);
            }

            if (MenuItemCodeList != null && MenuItemCodeList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedMenuItemCode = MenuItemCodeList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }
        }

        public void NewMenuItemCodeCommand()
        {
            NewMenuItemCode("");
            AllowCommit = false;
        }

        public void NewMenuItemCodeCommand(string menuItemCodeID)
        {
            NewMenuItemCode(menuItemCodeID);
            if (string.IsNullOrEmpty(menuItemCodeID)) 
            {//don't allow a save until a menuItemCodeID is provided...
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
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyCode(this MenuItemCode myObj, string propertyName)
        {
            var propInfo = typeof(MenuItemCode).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this MenuItemCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(MenuItemCode).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}