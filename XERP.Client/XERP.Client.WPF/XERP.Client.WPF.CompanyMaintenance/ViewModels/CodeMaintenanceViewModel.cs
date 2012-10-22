using System;
using System.Windows;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;
using System.Collections.Generic;
// Toolkit namespace
using SimpleMvvmToolkit;
//XERP Namespaces
using XERP.Domain.CompanyDomain.Services;
using XERP.Domain.CompanyDomain.CompanyDataService;
//required for extension methods...
using ExtensionMethods;
using XERP.Client.Models;

namespace XERP.Client.WPF.CompanyMaintenance.ViewModels
{
    public class CodeMaintenanceViewModel : ViewModelBase<CodeMaintenanceViewModel>
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
        public CodeMaintenanceViewModel()
        { }

        public CodeMaintenanceViewModel(ICompanyServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SetAsEmptySelection();

            CompanyCodeList = new BindingList<CompanyCode>();
            //disable new row feature...
            CompanyCodeList.AllowNew = false;

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
            //CompanyCodeColumnMetaDataList = new List<ColumnMetaData>();
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

        private string _companyCodeListCount;
        public string CompanyCodeListCount
        {
            get { return _companyCodeListCount; }
            set
            {
                _companyCodeListCount = value;
                NotifyPropertyChanged(m => m.CompanyCodeListCount);
            }
        }

        private BindingList<CompanyCode> _companyCodeList;
        public BindingList<CompanyCode> CompanyCodeList
        {
            get
            {
                CompanyCodeListCount = _companyCodeList.Count.ToString();
                if (_companyCodeList.Count > 0)
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
                return _companyCodeList;
            }
            set
            {
                _companyCodeList = value;
                NotifyPropertyChanged(m => m.CompanyCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private CompanyCode _selectedCompanyCodeMirror;
        public CompanyCode SelectedCompanyCodeMirror
        {
            get { return _selectedCompanyCodeMirror; }
            set { _selectedCompanyCodeMirror = value; }
        }

        private System.Collections.IList _selectedCompanyCodeList;
        public System.Collections.IList SelectedCompanyCodeList
        {
            get { return _selectedCompanyCodeList; }
            set
            {
                if (_selectedCompanyCode != value)
                {
                    _selectedCompanyCodeList = value;
                    NotifyPropertyChanged(m => m.SelectedCompanyCodeList);
                }
            }
        }

        private CompanyCode _selectedCompanyCode;
        public CompanyCode SelectedCompanyCode
        {
            get
            {
                return _selectedCompanyCode;
            }
            set
            {
                if (_selectedCompanyCode != value)
                {
                    _selectedCompanyCode = value;
                    //set the mirrored SelectedCompanyCode to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedCompanyCodeMirror = new CompanyCode();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedCompanyCode.GetType().GetProperties())
                        {
                            SelectedCompanyCodeMirror.SetPropertyValue(prop.Name, SelectedCompanyCode.GetPropertyValue(prop.Name));
                        }
                        SelectedCompanyCodeMirror.CompanyCodeID = _selectedCompanyCode.CompanyCodeID;
                        NotifyPropertyChanged(m => m.SelectedCompanyCode);

                        SelectedCompanyCode.PropertyChanged += new PropertyChangedEventHandler(SelectedCompanyCode_PropertyChanged);
                    }
                }
            }
        }

        private List<ColumnMetaData> _companyCodeColumnMetaDataList;
        public List<ColumnMetaData> CompanyCodeColumnMetaDataList
        {
            get { return _companyCodeColumnMetaDataList; }
            set
            {
                _companyCodeColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.CompanyCodeColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _companyCodeMaxFieldValueDictionary;
        public Dictionary<string, int> CompanyCodeMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {//we only need to get this once...
                if (_companyCodeMaxFieldValueDictionary != null)
                {
                    return _companyCodeMaxFieldValueDictionary;
                }
                _companyCodeMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("CompanyCodes");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _companyCodeMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _companyCodeMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedCompanyCode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "CompanyCodeID")
            {//make sure it is has changed...
                if (SelectedCompanyCodeMirror.CompanyCodeID != SelectedCompanyCode.CompanyCodeID)
                {
                    //if their are no records it is a key change
                    if (CompanyCodeList != null && CompanyCodeList.Count == 0
                        && SelectedCompanyCode != null && !string.IsNullOrEmpty(SelectedCompanyCode.CompanyCodeID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetCompanyCodeState(SelectedCompanyCode);

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

            object propertyChangedValue = SelectedCompanyCode.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedCompanyCodeMirror.GetPropertyValue(e.PropertyName);
            string propertyCode = SelectedCompanyCode.GetPropertyCode(e.PropertyName);
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
                    Update(SelectedCompanyCode);
                    //set the mirrored objects field...
                    SelectedCompanyCodeMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedCompanyCode.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedCompanyCode.CompanyCodeID, out errorMessage))
            {
                //check to see if key is part of the current companyCodelist...
                CompanyCode query = CompanyCodeList.Where(companyCode => companyCode.CompanyCodeID == SelectedCompanyCode.CompanyCodeID &&
                                                        companyCode.AutoID != SelectedCompanyCode.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected companyCode...
                    SelectedCompanyCode = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                CompanyCodeList = GetCompanyCodeByID(SelectedCompanyCode.CompanyCodeID);
                if (CompanyCodeList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedCompanyCode.CompanyCodeID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedCompanyCode = CompanyCodeList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedCompanyCode.CompanyCodeID != SelectedCompanyCodeMirror.CompanyCodeID)
                {
                    SelectedCompanyCode.CompanyCodeID = SelectedCompanyCodeMirror.CompanyCodeID;
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
            foreach (CompanyCode companyCode in CompanyCodeList)
            {
                EntityStates entityState = GetCompanyCodeState(companyCode);
                if (entityState == EntityStates.Modified ||
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(companyCode, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(companyCode.Code, out errorMessage) == false)
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
                case "CompanyCodeID":
                    rBool = NewKeyIsValid(SelectedCompanyCode, out errorMessage);
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

        private bool NewKeyIsValid(CompanyCode companyCode, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(companyCode.CompanyCodeID, out errorMessage) == false)
            {
                return false;
            }
            if (CompanyCodeExists(companyCode.CompanyCodeID.ToString()))
            {
                errorMessage = "CompanyCodeID " + companyCode.CompanyCodeID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object companyCodeID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)companyCodeID))
            {
                errorMessage = "CompanyCodeID Is Required...";
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

        private EntityStates GetCompanyCodeState(CompanyCode companyCode)
        {
            return _serviceAgent.GetCompanyCodeEntityState(companyCode);
        }

        #region CompanyCode CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedCompanyCode.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (CompanyCode companyCode in CompanyCodeList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (companyCode.AutoID > 0)
                {
                    autoIDs = autoIDs + companyCode.AutoID.ToString() + ",";
                }
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                CompanyCodeList = new BindingList<CompanyCode>(_serviceAgent.RefreshCompanyCode(autoIDs).ToList());
                SelectedCompanyCode = (from q in CompanyCodeList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<CompanyCode> GetCompanyCodes()
        {
            BindingList<CompanyCode> companyCodeList = new BindingList<CompanyCode>(_serviceAgent.GetCompanyCodes().ToList());
            Dirty = false;
            AllowCommit = false;
            return companyCodeList;
        }

        private BindingList<CompanyCode> GetCompanyCodes(CompanyCode companyCode)
        {
            BindingList<CompanyCode> companyCodeList = new BindingList<CompanyCode>(_serviceAgent.GetCompanyCodes(companyCode).ToList());
            Dirty = false;
            AllowCommit = false;
            return companyCodeList;
        }

        private BindingList<CompanyCode> GetCompanyCodeByID(string id)
        {
            BindingList<CompanyCode> companyCodeList = new BindingList<CompanyCode>(_serviceAgent.GetCompanyCodeByID(id).ToList());
            Dirty = false;
            AllowCommit = false;
            return companyCodeList;
        }

        private bool CompanyCodeExists(string companyCodeID)
        {
            return _serviceAgent.CompanyCodeExists(companyCodeID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(CompanyCode companyCode)
        {
            _serviceAgent.UpdateCompanyCodeRepository(companyCode);
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
            _serviceAgent.CommitCompanyCodeRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(CompanyCode companyCode)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromCompanyCodeRepository(companyCode);
            return true;
        }

        private bool NewCompanyCode(string companyCodeID)
        {
            CompanyCode companyCode = new CompanyCode();
            companyCode.CompanyCodeID = companyCodeID;
            CompanyCodeList.Add(companyCode);
            _serviceAgent.AddToCompanyCodeRepository(companyCode);
            SelectedCompanyCode = CompanyCodeList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion CompanyCode CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedCompanyCode = new CompanyCode();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            CompanyCodeList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                CompanyCodeColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewCompanyCodeCommand(""); //this will generate a new companyCode and set it as the selected companyCode...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedCompanyCode.SetPropertyValue(CompanyCodeColumnMetaDataList[i].Name, columnValue);
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
            if (GetCompanyCodeState(SelectedCompanyCode) != EntityStates.Detached)
            {
                if (Update(SelectedCompanyCode))
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
            for (int j = SelectedCompanyCodeList.Count - 1; j >= 0; j--)
            {
                CompanyCode companyCode = (CompanyCode)SelectedCompanyCodeList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = CompanyCodeList.IndexOf(companyCode) - SelectedCompanyCodeList.Count;
                }

                Delete(companyCode);
                CompanyCodeList.Remove(companyCode);
            }

            if (CompanyCodeList != null && CompanyCodeList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedCompanyCode = CompanyCodeList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }
        }

        public void NewCompanyCodeCommand()
        {
            NewCompanyCode("");
            AllowCommit = false;
        }

        public void NewCompanyCodeCommand(string companyCodeID)
        {

            NewCompanyCode(companyCodeID);
            if (string.IsNullOrEmpty(companyCodeID))
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
            RegisterToReceiveMessages<BindingList<CompanyCode>>(MessageTokens.CompanyCodeSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<CompanyCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                CompanyCodeList = e.Data;
                SelectedCompanyCode = CompanyCodeList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<CompanyCode>>(MessageTokens.CompanyCodeSearchToken.ToString(), OnSearchResult);
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
                    SelectedCompanyCode.CompanyCodeID = SelectedCompanyCodeMirror.CompanyCodeID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with CompanyCodeID provided...
                    NewCompanyCodeCommand(SelectedCompanyCode.CompanyCodeID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedCompanyCode.CompanyCodeID = SelectedCompanyCodeMirror.CompanyCodeID;
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
                    _serviceAgent.CommitCompanyCodeRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedCompanyCode.CompanyCodeID = SelectedCompanyCodeMirror.CompanyCodeID;
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
        public static object GetPropertyValue(this CompanyCode myObj, string propertyName)
        {
            var propInfo = typeof(CompanyCode).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyCode(this CompanyCode myObj, string propertyName)
        {
            var propInfo = typeof(CompanyCode).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this CompanyCode myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(CompanyCode).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}