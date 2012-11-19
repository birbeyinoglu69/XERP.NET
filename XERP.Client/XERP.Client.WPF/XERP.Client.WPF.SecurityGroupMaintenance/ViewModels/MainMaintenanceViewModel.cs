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
using XERP.Domain.SecurityGroupDomain.SecurityGroupDataService;
using XERP.Domain.SecurityGroupDomain.Services;

namespace XERP.Client.WPF.SecurityGroupMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newSecurityGroupAutoId;

        private ISecurityGroupServiceAgent _serviceAgent;
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
            SecurityGroupTypeList = BuildSecurityGroupTypeDropDown();
            SecurityGroupCodeList = BuildSecurityGroupCodeDropDown();
        }

        public MainMaintenanceViewModel(ISecurityGroupServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            SecurityGroupList = new BindingList<SecurityGroup>();
            //disable new row feature...
            SecurityGroupList.AllowNew = false;
            
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

        private string _securityGroupListCount;
        public string SecurityGroupListCount
        {
            get { return _securityGroupListCount; }
            set
            {
                _securityGroupListCount = value;
                NotifyPropertyChanged(m => m.SecurityGroupListCount);
            }
        }
        
        private BindingList<SecurityGroup> _securityGroupList;
        public BindingList<SecurityGroup> SecurityGroupList
        {
            get
            {
                SecurityGroupListCount = _securityGroupList.Count.ToString();
                if (_securityGroupList.Count > 0)
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
                return _securityGroupList;
            }
            set
            {
                _securityGroupList = value;
                NotifyPropertyChanged(m => m.SecurityGroupList);
            }
        }

        private ObservableCollection<SecurityGroupType> _securityGroupTypeList;
        public ObservableCollection<SecurityGroupType> SecurityGroupTypeList
        {
            get { return _securityGroupTypeList; }
            set
            {
                _securityGroupTypeList = value;
                NotifyPropertyChanged(m => m.SecurityGroupTypeList);
            }
        }

        private ObservableCollection<SecurityGroupCode> _securityGroupCodeList;
        public ObservableCollection<SecurityGroupCode> SecurityGroupCodeList
        {
            get { return _securityGroupCodeList; }
            set
            {
                _securityGroupCodeList = value;
                NotifyPropertyChanged(m => m.SecurityGroupCodeList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private SecurityGroup _selectedSecurityGroupMirror;
        public SecurityGroup SelectedSecurityGroupMirror
        {
            get { return _selectedSecurityGroupMirror; }
            set { _selectedSecurityGroupMirror = value; }
        }

        private System.Collections.IList _selectedSecurityGroupList;
        public System.Collections.IList SelectedSecurityGroupList
        {
            get { return _selectedSecurityGroupList; }
            set
            {
                if (_selectedSecurityGroup != value)
                {
                    _selectedSecurityGroupList = value;
                    NotifyPropertyChanged(m => m.SelectedSecurityGroupList);
                }  
            }
        }

        private SecurityGroup _selectedSecurityGroup;
        public SecurityGroup SelectedSecurityGroup
        {
            get 
            {
                return _selectedSecurityGroup; 
            }
            set
            {
                if (_selectedSecurityGroup != value)
                {
                    _selectedSecurityGroup = value;
                    //set the mirrored SelectedSecurityGroup to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedSecurityGroupMirror = new SecurityGroup();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedSecurityGroup.GetType().GetProperties())
                        {
                            SelectedSecurityGroupMirror.SetPropertyValue(prop.Name, SelectedSecurityGroup.GetPropertyValue(prop.Name));
                        }
                        SelectedSecurityGroupMirror.SecurityGroupID = _selectedSecurityGroup.SecurityGroupID;
                        NotifyPropertyChanged(m => m.SelectedSecurityGroup);
                        
                        SelectedSecurityGroup.PropertyChanged += new PropertyChangedEventHandler(SelectedSecurityGroup_PropertyChanged); 
                    }
                }
            }
        }

        private List<ColumnMetaData> _securityGroupColumnMetaDataList;
        public List<ColumnMetaData> SecurityGroupColumnMetaDataList
        {
            get { return _securityGroupColumnMetaDataList; }
            set 
            { 
                _securityGroupColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.SecurityGroupColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _securityGroupMaxFieldValueDictionary;
        public Dictionary<string, int> SecurityGroupMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_securityGroupMaxFieldValueDictionary != null)
                    return _securityGroupMaxFieldValueDictionary;

                _securityGroupMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("SecurityGroups");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _securityGroupMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _securityGroupMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedSecurityGroup_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "SecurityGroupID")
            {//make sure it is has changed...
                if (SelectedSecurityGroupMirror.SecurityGroupID != SelectedSecurityGroup.SecurityGroupID)
                {
                    //if their are no records it is a key change
                    if (SecurityGroupList != null && SecurityGroupList.Count == 0
                        && SelectedSecurityGroup != null && !string.IsNullOrEmpty(SelectedSecurityGroup.SecurityGroupID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetSecurityGroupState(SelectedSecurityGroup);

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
            
            object propertyChangedValue = SelectedSecurityGroup.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedSecurityGroupMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedSecurityGroup.GetPropertyType(e.PropertyName);
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
                if (SecurityGroupPropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedSecurityGroup);
                    //set the mirrored objects field...
                    SelectedSecurityGroupMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedSecurityGroupMirror.IsValid = SelectedSecurityGroup.IsValid;
                    SelectedSecurityGroupMirror.IsExpanded = SelectedSecurityGroup.IsExpanded;
                    SelectedSecurityGroupMirror.NotValidMessage = SelectedSecurityGroup.NotValidMessage;
                }
                else
                {
                    SelectedSecurityGroup.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedSecurityGroup.IsValid = SelectedSecurityGroupMirror.IsValid;
                    SelectedSecurityGroup.IsExpanded = SelectedSecurityGroupMirror.IsExpanded;
                    SelectedSecurityGroup.NotValidMessage = SelectedSecurityGroupMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedSecurityGroup.SecurityGroupID))
            {//check to see if key is part of the current companylist...
                SecurityGroup query = SecurityGroupList.Where(company => company.SecurityGroupID == SelectedSecurityGroup.SecurityGroupID &&
                                                        company.AutoID != SelectedSecurityGroup.AutoID).SingleOrDefault();
                if (query != null)
                {//revert it back
                    SelectedSecurityGroup.SecurityGroupID = SelectedSecurityGroupMirror.SecurityGroupID;
                    //change to the newly selected company...
                    SelectedSecurityGroup = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                SecurityGroupList = GetSecurityGroupByID(SelectedSecurityGroup.SecurityGroupID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (SecurityGroupList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedSecurityGroup.SecurityGroupID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedSecurityGroup = SecurityGroupList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedSecurityGroup.SecurityGroupID != SelectedSecurityGroupMirror.SecurityGroupID)
                    SelectedSecurityGroup.SecurityGroupID = SelectedSecurityGroupMirror.SecurityGroupID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in SecurityGroupList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedSecurityGroup = new SecurityGroup();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            SecurityGroupList.Clear();
            SetAsEmptySelection();
        }

        private bool SecurityGroupPropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "SecurityGroupID":
                    rBool = SecurityGroupIsValid(SelectedSecurityGroup, _securityGroupValidationProperties.SecurityGroupID, out errorMessage);
                    break;
                case "Name":
                    rBool = SecurityGroupIsValid(SelectedSecurityGroup, _securityGroupValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedSecurityGroup.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedSecurityGroup.IsValid = SecurityGroupIsValid(SelectedSecurityGroup, out errorMessage);
                if (SelectedSecurityGroup.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedSecurityGroup.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _securityGroupValidationProperties
        {//we list all fields that require validation...
            SecurityGroupID,
            Name
        }

        //Object.Property Scope Validation...
        private bool SecurityGroupIsValid(SecurityGroup item, _securityGroupValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _securityGroupValidationProperties.SecurityGroupID:
                    //validate key
                    if (string.IsNullOrEmpty(item.SecurityGroupID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetSecurityGroupState(item);
                    if (entityState == EntityStates.Added && SecurityGroupExists(item.SecurityGroupID))
                    {
                        errorMessage = "Item AllReady Exists...";
                        return false;
                    }
                    break;
                case _securityGroupValidationProperties.Name:
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
        //SecurityGroup Object Scope Validation check the entire object for validity...
        private byte SecurityGroupIsValid(SecurityGroup item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.SecurityGroupID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetSecurityGroupState(item);
            if (entityState == EntityStates.Added && SecurityGroupExists(item.SecurityGroupID))
            {
                errorMessage = "Item AllReady Exists.";
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
        private ObservableCollection<SecurityGroupType> BuildSecurityGroupTypeDropDown()
        {
            List<SecurityGroupType> list = new List<SecurityGroupType>();
            list = _serviceAgent.GetSecurityGroupTypes(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new SecurityGroupType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<SecurityGroupType>(list);
        }

        private ObservableCollection<SecurityGroupCode> BuildSecurityGroupCodeDropDown()
        {
            List<SecurityGroupCode> list = new List<SecurityGroupCode>();
            list = _serviceAgent.GetSecurityGroupCodes(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new SecurityGroupCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<SecurityGroupCode>(list);
        }

        private EntityStates GetSecurityGroupState(SecurityGroup item)
        {
            return _serviceAgent.GetSecurityGroupEntityState(item);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.SecurityGroupRepositoryIsDirty();
        }

        #region SecurityGroup CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedSecurityGroup.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (SecurityGroup item in SecurityGroupList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                SecurityGroupList = new BindingList<SecurityGroup>(_serviceAgent.RefreshSecurityGroup(autoIDs).ToList());
                SelectedSecurityGroup = (from q in SecurityGroupList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<SecurityGroup> GetSecurityGroups(string companyID)
        {
            BindingList<SecurityGroup> itemList = new BindingList<SecurityGroup>(_serviceAgent.GetSecurityGroups(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private BindingList<SecurityGroup> GetSecurityGroups(SecurityGroup item, string companyID)
        {
            BindingList<SecurityGroup> itemList = new BindingList<SecurityGroup>(_serviceAgent.GetSecurityGroups(item, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<SecurityGroup> GetSecurityGroupByID(string itemID, string companyID)
        {
            BindingList<SecurityGroup> itemList = new BindingList<SecurityGroup>(_serviceAgent.GetSecurityGroupByID(itemID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private bool SecurityGroupExists(string itemID)
        {
            return _serviceAgent.SecurityGroupExists(itemID, ClientSessionSingleton.Instance.CompanyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(SecurityGroup item)
        {
            _serviceAgent.UpdateSecurityGroupRepository(item);
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
            var items = (from q in SecurityGroupList where q.IsValid == 2 select q).ToList();
            foreach (SecurityGroup item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitSecurityGroupRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }


        private bool Delete(SecurityGroup item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromSecurityGroupRepository(item);
            return true;
        }

        private bool NewSecurityGroup(string itemID)
        {
            SecurityGroup newItem = new SecurityGroup();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newSecurityGroupAutoId = _newSecurityGroupAutoId - 1;
            newItem.AutoID = _newSecurityGroupAutoId;
            newItem.SecurityGroupID = itemID;
            newItem.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            newItem.IsValid = 1;
            newItem.NotValidMessage = "New Record Key Field/s Are Required.";
            SecurityGroupList.Add(newItem);
            _serviceAgent.AddToSecurityGroupRepository(newItem);
            SelectedSecurityGroup = SecurityGroupList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }


        #endregion SecurityGroup CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                SecurityGroupColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewSecurityGroupCommand(""); //this will generate a new item and set it as the selected item...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedSecurityGroup.SetPropertyValue(SecurityGroupColumnMetaDataList[i].Name, columnValue);
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
            if (GetSecurityGroupState(SelectedSecurityGroup) != EntityStates.Detached)
            {
                if (Update(SelectedSecurityGroup))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }
        public void RefreshCommand()
        {
            Refresh();
        }
        public void DeleteSecurityGroupCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedSecurityGroupList.Count - 1; j >= 0; j--)
                {
                    SecurityGroup item = (SecurityGroup)SelectedSecurityGroupList[j];
                    //get Max Index...
                    i = SecurityGroupList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    SecurityGroupList.Remove(item);
                }

                if (SecurityGroupList != null && SecurityGroupList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= SecurityGroupList.Count())
                        ii = SecurityGroupList.Count - 1;

                    SelectedSecurityGroup = SecurityGroupList[ii];
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
                NotifyMessage("SecurityGroup/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewSecurityGroupCommand()
        {
            NewSecurityGroup("");
            AllowCommit = false;
        }

        public void NewSecurityGroupCommand(string itemID)
        {
            NewSecurityGroup(itemID);
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
            RegisterToReceiveMessages<BindingList<SecurityGroup>>(MessageTokens.SecurityGroupSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<SecurityGroup>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                SecurityGroupList = e.Data;
                SelectedSecurityGroup = SecurityGroupList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<SecurityGroup>>(MessageTokens.SecurityGroupSearchToken.ToString(), OnSearchResult);
        }

        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<SecurityGroupType>>(MessageTokens.SecurityGroupTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<SecurityGroupType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedSecurityGroup.SecurityGroupTypeID = e.Data.FirstOrDefault().SecurityGroupTypeID;

            UnregisterToReceiveMessages<BindingList<SecurityGroupType>>(MessageTokens.SecurityGroupTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<SecurityGroupCode>>(MessageTokens.SecurityGroupCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<SecurityGroupCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedSecurityGroup.SecurityGroupCodeID = e.Data.FirstOrDefault().SecurityGroupCodeID;

            UnregisterToReceiveMessages<BindingList<SecurityGroupType>>(MessageTokens.SecurityGroupTypeSearchToken.ToString(), OnTypeSearchResult);
        }
        
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
        
        private void NotifySearch(string message)
        {//Notify view to launch search...
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
                    SelectedSecurityGroup.SecurityGroupID = SelectedSecurityGroupMirror.SecurityGroupID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with SecurityGroupID provided...
                    NewSecurityGroupCommand(SelectedSecurityGroup.SecurityGroupID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSecurityGroup.SecurityGroupID = SelectedSecurityGroupMirror.SecurityGroupID;
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
                    _serviceAgent.CommitSecurityGroupRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedSecurityGroup.SecurityGroupID = SelectedSecurityGroupMirror.SecurityGroupID;
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
        public static object GetPropertyValue(this SecurityGroup myObj, string propertyName)
        {
            var propInfo = typeof(SecurityGroup).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this SecurityGroup myObj, string propertyName)
        {
            var propInfo = typeof(SecurityGroup).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this SecurityGroup myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(SecurityGroup).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}