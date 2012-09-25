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

namespace XERP.Client.WPF.ExecutableProgramMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        
        private IExecutableProgramServiceAgent _serviceAgent;
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
            ExecutableProgramTypeList = BuildExecutableProgramTypeDropDown();
            ExecutableProgramCodeList = BuildExecutableProgramCodeDropDown();
        }

        public MainMaintenanceViewModel(IExecutableProgramServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            ExecutableProgramList = new BindingList<ExecutableProgram>();
            //disable new row feature...
            ExecutableProgramList.AllowNew = false;
            
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

        private string _executableProgramListCount;
        public string ExecutableProgramListCount
        {
            get { return _executableProgramListCount; }
            set
            {
                _executableProgramListCount = value;
                NotifyPropertyChanged(m => m.ExecutableProgramListCount);
            }
        }
        
        private BindingList<ExecutableProgram> _executableProgramList;
        public BindingList<ExecutableProgram> ExecutableProgramList
        {
            get
            {
                ExecutableProgramListCount = _executableProgramList.Count.ToString();
                if (_executableProgramList.Count > 0)
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
                return _executableProgramList;
            }
            set
            {
                _executableProgramList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramList);
            }
        }

        private ObservableCollection<ExecutableProgramType> _executableProgramTypeList;
        public ObservableCollection<ExecutableProgramType> ExecutableProgramTypeList
        {
            get { return _executableProgramTypeList; }
            set
            {
                _executableProgramTypeList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramTypeList);
            }
        }

        private ObservableCollection<ExecutableProgramCode> _executableProgramCodeList;
        public ObservableCollection<ExecutableProgramCode> ExecutableProgramCodeList
        {
            get { return _executableProgramCodeList; }
            set
            {
                _executableProgramCodeList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private ExecutableProgram _selectedExecutableProgramMirror;
        public ExecutableProgram SelectedExecutableProgramMirror
        {
            get { return _selectedExecutableProgramMirror; }
            set { _selectedExecutableProgramMirror = value; }
        }

        private System.Collections.IList _selectedExecutableProgramList;
        public System.Collections.IList SelectedExecutableProgramList
        {
            get { return _selectedExecutableProgramList; }
            set
            {
                if (_selectedExecutableProgram != value)
                {
                    _selectedExecutableProgramList = value;
                    NotifyPropertyChanged(m => m.SelectedExecutableProgramList);
                }  
            }
        }

        private ExecutableProgram _selectedExecutableProgram;
        public ExecutableProgram SelectedExecutableProgram
        {
            get 
            {
                return _selectedExecutableProgram; 
            }
            set
            {
                if (_selectedExecutableProgram != value)
                {
                    _selectedExecutableProgram = value;
                    //set the mirrored SelectedExecutableProgram to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedExecutableProgramMirror = new ExecutableProgram();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedExecutableProgram.GetType().GetProperties())
                        {
                            SelectedExecutableProgramMirror.SetPropertyValue(prop.Name, SelectedExecutableProgram.GetPropertyValue(prop.Name));
                        }
                        SelectedExecutableProgramMirror.ExecutableProgramID = _selectedExecutableProgram.ExecutableProgramID;
                        NotifyPropertyChanged(m => m.SelectedExecutableProgram);
                        
                        SelectedExecutableProgram.PropertyChanged += new PropertyChangedEventHandler(SelectedExecutableProgram_PropertyChanged); 
                    }
                }
            }
        }

        private List<ColumnMetaData> _executableProgramColumnMetaDataList;
        public List<ColumnMetaData> ExecutableProgramColumnMetaDataList
        {
            get { return _executableProgramColumnMetaDataList; }
            set 
            { 
                _executableProgramColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _executableProgramMaxFieldValueDictionary;
        public Dictionary<string, int> ExecutableProgramMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_executableProgramMaxFieldValueDictionary != null)
                {
                    return _executableProgramMaxFieldValueDictionary;
                }
                _executableProgramMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("ExecutablePrograms");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _executableProgramMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _executableProgramMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedExecutableProgram_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
            if (e.PropertyName == "ExecutableProgramID")
            {//make sure it is has changed...
                if (SelectedExecutableProgramMirror.ExecutableProgramID != SelectedExecutableProgram.ExecutableProgramID)
                {
                    //if their are no records it is a key change
                    if (ExecutableProgramList != null && ExecutableProgramList.Count == 0
                        && SelectedExecutableProgram != null && !string.IsNullOrEmpty(SelectedExecutableProgram.ExecutableProgramID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetExecutableProgramState(SelectedExecutableProgram);

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
            
            object propertyChangedValue = SelectedExecutableProgram.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedExecutableProgramMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedExecutableProgram.GetPropertyType(e.PropertyName);
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
                    Update(SelectedExecutableProgram);
                    //set the mirrored objects field...
                    SelectedExecutableProgramMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedExecutableProgram.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedExecutableProgram.ExecutableProgramID, out errorMessage))
            {
                //check to see if key is part of the current executableProgramlist...
                ExecutableProgram query = ExecutableProgramList.Where(executableProgram => executableProgram.ExecutableProgramID == SelectedExecutableProgram.ExecutableProgramID &&
                                                        executableProgram.AutoID != SelectedExecutableProgram.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected executableProgram...
                    SelectedExecutableProgram = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                ExecutableProgramList = GetExecutableProgramByID(SelectedExecutableProgram.ExecutableProgramID, ClientSessionSingleton.Instance.CompanyID);
                if (ExecutableProgramList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedExecutableProgram.ExecutableProgramID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedExecutableProgram = ExecutableProgramList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedExecutableProgram.ExecutableProgramID != SelectedExecutableProgramMirror.ExecutableProgramID)
                {
                    SelectedExecutableProgram.ExecutableProgramID = SelectedExecutableProgramMirror.ExecutableProgramID;
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
            foreach (ExecutableProgram executableProgram in ExecutableProgramList)
            {
                EntityStates entityState = GetExecutableProgramState(executableProgram);
                if (entityState == EntityStates.Modified || 
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(executableProgram, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(executableProgram.Name, out errorMessage) == false)
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
                case "ExecutableProgramID":
                    rBool = NewKeyIsValid(SelectedExecutableProgram, out errorMessage);
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

        private bool NewKeyIsValid(ExecutableProgram executableProgram, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(executableProgram.ExecutableProgramID, out errorMessage) == false)
            {
                return false;
            }
            if (ExecutableProgramExists(executableProgram.ExecutableProgramID.ToString()))
            {
                errorMessage = "ExecutableProgramID " + executableProgram.ExecutableProgramID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object executableProgramID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)executableProgramID))
            {
                errorMessage = "ExecutableProgramID Is Required...";
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
        private ObservableCollection<ExecutableProgramType> BuildExecutableProgramTypeDropDown()
        {
            List<ExecutableProgramType> list = new List<ExecutableProgramType>();
            list = _serviceAgent.GetExecutableProgramTypes(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new ExecutableProgramType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<ExecutableProgramType>(list);
        }

        private ObservableCollection<ExecutableProgramCode> BuildExecutableProgramCodeDropDown()
        {
            List<ExecutableProgramCode> list = new List<ExecutableProgramCode>();
            list = _serviceAgent.GetExecutableProgramCodes(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new ExecutableProgramCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<ExecutableProgramCode>(list);
        }

        private EntityStates GetExecutableProgramState(ExecutableProgram executableProgram)
        {
            return _serviceAgent.GetExecutableProgramEntityState(executableProgram);
        }

        #region ExecutableProgram CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedExecutableProgram.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (ExecutableProgram executableProgram in ExecutableProgramList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (executableProgram.AutoID > 0)
                {
                    autoIDs = autoIDs + executableProgram.AutoID.ToString() + ",";
                }
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                ExecutableProgramList = new BindingList<ExecutableProgram>(_serviceAgent.RefreshExecutableProgram(autoIDs).ToList());
                SelectedExecutableProgram = (from q in ExecutableProgramList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<ExecutableProgram> GetExecutablePrograms(string companyID)
        {
            BindingList<ExecutableProgram> executableProgramList = new BindingList<ExecutableProgram>(_serviceAgent.GetExecutablePrograms(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return executableProgramList; 
        }

        private BindingList<ExecutableProgram> GetExecutablePrograms(ExecutableProgram executableProgram, string companyID)
        {
            BindingList<ExecutableProgram> executableProgramList = new BindingList<ExecutableProgram>(_serviceAgent.GetExecutablePrograms(executableProgram, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return executableProgramList;
        }

        private BindingList<ExecutableProgram> GetExecutableProgramByID(string executableProgramID, string companyID)
        {
            BindingList<ExecutableProgram> executableProgramList = new BindingList<ExecutableProgram>(_serviceAgent.GetExecutableProgramByID(executableProgramID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return executableProgramList; 
        }

        private bool ExecutableProgramExists(string executableProgramID)
        {
            return _serviceAgent.ExecutableProgramExists(executableProgramID, ClientSessionSingleton.Instance.CompanyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(ExecutableProgram executableProgram)
        {
            _serviceAgent.UpdateExecutableProgramRepository(executableProgram);
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
            _serviceAgent.CommitExecutableProgramRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(ExecutableProgram executableProgram)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromExecutableProgramRepository(executableProgram);
            return true;
        }

        private bool NewExecutableProgram(string executableProgramID)
        {
            ExecutableProgram executableProgram = new ExecutableProgram();
            executableProgram.ExecutableProgramID = executableProgramID;
            executableProgram.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            ExecutableProgramList.Add(executableProgram);
            _serviceAgent.AddToExecutableProgramRepository(executableProgram);
            SelectedExecutableProgram = ExecutableProgramList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion ExecutableProgram CRUD
        #endregion ServiceAgent Call Methods

        private void SetAsEmptySelection()
        {
            SelectedExecutableProgram = new ExecutableProgram();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            ExecutableProgramList.Clear();
            SetAsEmptySelection();
        }

        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                ExecutableProgramColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewExecutableProgramCommand(""); //this will generate a new executableProgram and set it as the selected executableProgram...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedExecutableProgram.SetPropertyValue(ExecutableProgramColumnMetaDataList[i].Name, columnValue);
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
            if (GetExecutableProgramState(SelectedExecutableProgram) != EntityStates.Detached)
            {
                if (Update(SelectedExecutableProgram))
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
            for (int j = SelectedExecutableProgramList.Count - 1; j >= 0; j--)
            {
                ExecutableProgram executableProgram = (ExecutableProgram)SelectedExecutableProgramList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = ExecutableProgramList.IndexOf(executableProgram) - SelectedExecutableProgramList.Count;
                }
                
                Delete(executableProgram);
                ExecutableProgramList.Remove(executableProgram);
            }

            if (ExecutableProgramList != null && ExecutableProgramList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedExecutableProgram = ExecutableProgramList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }  
        }

        public void NewExecutableProgramCommand()
        {
            NewExecutableProgram("");
            AllowCommit = false;
        }

        public void NewExecutableProgramCommand(string executableProgramID)
        {
            NewExecutableProgram(executableProgramID);
            if (string.IsNullOrEmpty(executableProgramID))
            {//don't allow a save until a executableProgramID is provided...
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
            RegisterToReceiveMessages<BindingList<ExecutableProgram>>(MessageTokens.ExecutableProgramSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<ExecutableProgram>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                ExecutableProgramList = e.Data;
                SelectedExecutableProgram = ExecutableProgramList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<ExecutableProgram>>(MessageTokens.ExecutableProgramSearchToken.ToString(), OnSearchResult);
        }

        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<ExecutableProgramType>>(MessageTokens.ExecutableProgramTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<ExecutableProgramType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SelectedExecutableProgram.ExecutableProgramTypeID = e.Data.FirstOrDefault().ExecutableProgramTypeID;
            }
            UnregisterToReceiveMessages<BindingList<ExecutableProgramType>>(MessageTokens.ExecutableProgramTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<ExecutableProgramCode>>(MessageTokens.ExecutableProgramCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<ExecutableProgramCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SelectedExecutableProgram.ExecutableProgramCodeID = e.Data.FirstOrDefault().ExecutableProgramCodeID;
            }
            UnregisterToReceiveMessages<BindingList<ExecutableProgramType>>(MessageTokens.ExecutableProgramTypeSearchToken.ToString(), OnTypeSearchResult);
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
                    SelectedExecutableProgram.ExecutableProgramID = SelectedExecutableProgramMirror.ExecutableProgramID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with ExecutableProgramID provided...
                    NewExecutableProgramCommand(SelectedExecutableProgram.ExecutableProgramID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedExecutableProgram.ExecutableProgramID = SelectedExecutableProgramMirror.ExecutableProgramID;
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
                    _serviceAgent.CommitExecutableProgramRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedExecutableProgram.ExecutableProgramID = SelectedExecutableProgramMirror.ExecutableProgramID;
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
        public static object GetPropertyValue(this ExecutableProgram myObj, string propertyName)
        {
            var propInfo = typeof(ExecutableProgram).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyType(this ExecutableProgram myObj, string propertyName)
        {
            var propInfo = typeof(ExecutableProgram).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this ExecutableProgram myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(ExecutableProgram).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}