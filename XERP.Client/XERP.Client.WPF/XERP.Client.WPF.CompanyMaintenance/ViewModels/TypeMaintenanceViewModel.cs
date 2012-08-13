using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
// Toolkit namespace
using SimpleMvvmToolkit;
//XERP Namespaces
using XERP.Domain.CompanyDomain.Services;
using XERP.Domain.CompanyDomain.CompanyDataService;
using XERP.Domain.ClientModels;
using XERP.Client;
//required for extension methods...
using ExtensionMethods;
using System.Text;
using XERP.Client.Models;

namespace XERP.Client.WPF.CompanyMaintenance.ViewModels
{
    public class TypeMaintenanceViewModel : ViewModelBase<TypeMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();

        private ICompanyServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public TypeMaintenanceViewModel()
        { }

        public TypeMaintenanceViewModel(ICompanyServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            CompanyTypeList = new BindingList<CompanyType>();
            //disable new row feature...
            CompanyTypeList.AllowNew = false;

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
            //CompanyTypeColumnMetaDataList = new List<ColumnMetaData>();
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

        private string _companyTypeListCount;
        public string CompanyTypeListCount
        {
            get { return _companyTypeListCount; }
            set
            {
                _companyTypeListCount = value;
                NotifyPropertyChanged(m => m.CompanyTypeListCount);
            }
        }

        private BindingList<CompanyType> _companyTypeList;
        public BindingList<CompanyType> CompanyTypeList
        {
            get
            {
                CompanyTypeListCount = _companyTypeList.Count.ToString();
                if (_companyTypeList.Count > 0)
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
                return _companyTypeList;
            }
            set
            {
                _companyTypeList = value;
                NotifyPropertyChanged(m => m.CompanyTypeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private CompanyType _selectedCompanyTypeMirror;
        public CompanyType SelectedCompanyTypeMirror
        {
            get { return _selectedCompanyTypeMirror; }
            set { _selectedCompanyTypeMirror = value; }
        }

        private System.Collections.IList _selectedCompanyTypeList;
        public System.Collections.IList SelectedCompanyTypeList
        {
            get { return _selectedCompanyTypeList; }
            set
            {
                if (_selectedCompanyType != value)
                {
                    _selectedCompanyTypeList = value;
                    NotifyPropertyChanged(m => m.SelectedCompanyTypeList);
                }
            }
        }

        private CompanyType _selectedCompanyType;
        public CompanyType SelectedCompanyType
        {
            get
            {
                return _selectedCompanyType;
            }
            set
            {
                if (_selectedCompanyType != value)
                {
                    _selectedCompanyType = value;
                    //set the mirrored SelectedCompanyType to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedCompanyTypeMirror = new CompanyType();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedCompanyType.GetType().GetProperties())
                        {
                            SelectedCompanyTypeMirror.SetPropertyValue(prop.Name, SelectedCompanyType.GetPropertyValue(prop.Name));
                        }
                        SelectedCompanyTypeMirror.CompanyTypeID = _selectedCompanyType.CompanyTypeID;
                        NotifyPropertyChanged(m => m.SelectedCompanyType);

                        SelectedCompanyType.PropertyChanged += new PropertyChangedEventHandler(SelectedCompanyType_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _companyTypeColumnMetaDataList;
        public List<ColumnMetaData> CompanyTypeColumnMetaDataList
        {
            get { return _companyTypeColumnMetaDataList; }
            set
            {
                _companyTypeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.CompanyTypeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _companyTypeMaxFieldValueDictionary;
        public Dictionary<string, int> CompanyTypeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                _companyTypeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("CompanyTypes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _companyTypeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _companyTypeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedCompanyType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "CompanyTypeID")
            {//make sure it is has changed...
                if (SelectedCompanyTypeMirror.CompanyTypeID != SelectedCompanyType.CompanyTypeID)
                {
                    //if their are no records it is a key change
                    if (CompanyTypeList != null && CompanyTypeList.Count == 0
                        && SelectedCompanyType != null && !string.IsNullOrEmpty(SelectedCompanyType.CompanyTypeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetCompanyTypeState(SelectedCompanyType);

                    if (entityState == EntityStates.Unchanged ||
                        entityState == EntityStates.Modified)
                    {//once a key is added it can not be modified...
                        if (Dirty)
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

            object propertyChangedValue = SelectedCompanyType.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedCompanyTypeMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedCompanyType.GetPropertyType(e.PropertyName);
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
                    Update(SelectedCompanyType);
                    //set the mirrored objects field...
                    SelectedCompanyTypeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedCompanyType.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedCompanyType.CompanyTypeID, out errorMessage))
            {
                //check to see if key is part of the current companyTypelist...
                CompanyType query = CompanyTypeList.Where(companyType => companyType.CompanyTypeID == SelectedCompanyType.CompanyTypeID &&
                                                        companyType.AutoID != SelectedCompanyType.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected companyType...
                    SelectedCompanyType = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                CompanyTypeList = GetCompanyTypeByID(SelectedCompanyType.CompanyTypeID);
                if (CompanyTypeList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedCompanyType.CompanyTypeID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedCompanyType = CompanyTypeList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedCompanyType.CompanyTypeID != SelectedCompanyTypeMirror.CompanyTypeID)
                {
                    SelectedCompanyType.CompanyTypeID = SelectedCompanyTypeMirror.CompanyTypeID;
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
            foreach (CompanyType companyType in CompanyTypeList)
            {
                EntityStates entityState = GetCompanyTypeState(companyType);
                if (entityState == EntityStates.Modified ||
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(companyType, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(companyType.Type, out errorMessage) == false)
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
                case "CompanyTypeID":
                    rBool = NewKeyIsValid(SelectedCompanyType, out errorMessage);
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

        private bool NewKeyIsValid(CompanyType companyType, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(companyType.CompanyTypeID, out errorMessage) == false)
            {
                return false;
            }
            if (CompanyTypeExists(companyType.CompanyTypeID.ToString()))
            {
                errorMessage = "CompanyTypeID " + companyType.CompanyTypeID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object companyTypeID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)companyTypeID))
            {
                errorMessage = "CompanyTypeID Is Required...";
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

        private EntityStates GetCompanyTypeState(CompanyType companyType)
        {
            return _serviceAgent.GetCompanyTypeEntityState(companyType);
        }

        #region CompanyType CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedCompanyType.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (CompanyType companyType in CompanyTypeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (companyType.AutoID > 0)
                {
                    autoIDs = autoIDs + companyType.AutoID.ToString() + ",";
                }
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                CompanyTypeList = new BindingList<CompanyType>(_serviceAgent.RefreshCompanyType(autoIDs).ToList());
                SelectedCompanyType = (from q in CompanyTypeList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<CompanyType> GetCompanyTypes()
        {
            BindingList<CompanyType> companyTypeList = new BindingList<CompanyType>(_serviceAgent.GetCompanyTypes().ToList());
            Dirty = false;
            AllowCommit = false;
            return companyTypeList;
        }

        private BindingList<CompanyType> GetCompanyTypes(CompanyType companyType)
        {
            BindingList<CompanyType> companyTypeList = new BindingList<CompanyType>(_serviceAgent.GetCompanyTypes(companyType).ToList());
            Dirty = false;
            AllowCommit = false;
            return companyTypeList;
        }

        private BindingList<CompanyType> GetCompanyTypeByID(string id)
        {
            BindingList<CompanyType> companyTypeList = new BindingList<CompanyType>(_serviceAgent.GetCompanyTypeByID(id).ToList());
            Dirty = false;
            AllowCommit = false;
            return companyTypeList;
        }

        private bool CompanyTypeExists(string companyTypeID)
        {
            return _serviceAgent.CompanyTypeExists(companyTypeID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(CompanyType companyType)
        {
            _serviceAgent.UpdateCompanyTypeRepository(companyType);
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
            _serviceAgent.CommitCompanyTypeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(CompanyType companyType)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromCompanyTypeRepository(companyType);
            return true;
        }

        private bool NewCompanyType(CompanyType companyType)
        {
            _serviceAgent.AddToCompanyTypeRepository(companyType);
            SelectedCompanyType = CompanyTypeList.LastOrDefault();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        #endregion CompanyType CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedCompanyType = new CompanyType();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            CompanyTypeList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                CompanyTypeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewCompanyTypeCommand(); //this will generate a new companyType and set it as the selected companyType...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedCompanyType.SetPropertyValue(CompanyTypeColumnMetaDataList[i].Name, columnValue);
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
            if (GetCompanyTypeState(SelectedCompanyType) != EntityStates.Detached)
            {
                if (Update(SelectedCompanyType))
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
            for (int j = SelectedCompanyTypeList.Count - 1; j >= 0; j--)
            {
                CompanyType companyType = (CompanyType)SelectedCompanyTypeList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = CompanyTypeList.IndexOf(companyType) - SelectedCompanyTypeList.Count;
                }

                Delete(companyType);
                CompanyTypeList.Remove(companyType);
            }

            if (CompanyTypeList != null && CompanyTypeList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedCompanyType = CompanyTypeList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }
        }

        public void NewCompanyTypeCommand()
        {
            CompanyType companyType = new CompanyType();
            CompanyTypeList.Add(companyType);
            NewCompanyType(companyType);
            AllowEdit = true;
            //don't allow a save until a companyTypeID is provided...
            AllowCommit = false;
            NotifyNewRecordCreated();
        }
        //overloaded to allow a companyTypeID to be provided...
        public void NewCompanyTypeCommand(string companyTypeID)
        {
            CompanyType companyType = new CompanyType();
            companyType.CompanyTypeID = companyTypeID;
            CompanyTypeList.Add(companyType);
            NewCompanyType(companyType);
            AllowEdit = true;
            AllowCommit = CommitIsAllowed();
        }

        public void ClearCommand()
        {
            if (Dirty)
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
            if (Dirty)
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
            RegisterToReceiveMessages<BindingList<CompanyType>>(MessageTokens.CompanyTypeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<CompanyType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                CompanyTypeList = e.Data;
                SelectedCompanyType = CompanyTypeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<CompanyType>>(MessageTokens.CompanyTypeSearchToken.ToString(), OnSearchResult);
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
                    SelectedCompanyType.CompanyTypeID = SelectedCompanyTypeMirror.CompanyTypeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with CompanyTypeID provided...
                    NewCompanyTypeCommand(SelectedCompanyType.CompanyTypeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedCompanyType.CompanyTypeID = SelectedCompanyTypeMirror.CompanyTypeID;
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
                    _serviceAgent.CommitCompanyTypeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedCompanyType.CompanyTypeID = SelectedCompanyTypeMirror.CompanyTypeID;
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
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;

    public static partial class XERPExtensions
    {
        public static object GetPropertyValue(this CompanyType myObj, string propertyName)
        {
            var propInfo = typeof(CompanyType).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyType(this CompanyType myObj, string propertyName)
        {
            var propInfo = typeof(CompanyType).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this CompanyType myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(CompanyType).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}