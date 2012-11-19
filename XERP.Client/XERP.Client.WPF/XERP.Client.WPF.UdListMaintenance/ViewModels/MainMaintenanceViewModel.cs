using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using ExtensionMethods;
using SimpleMvvmToolkit;
using XERP.Client.Models;
using XERP.Domain.UdListDomain.Services;
using XERP.Domain.UdListDomain.UdListDataService;

namespace XERP.Client.WPF.UdListMaintenance.ViewModels
{
    public partial class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newUdListAutoId;

        private IUdListServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }

        //required else it generates debug view designer issues 
        public MainMaintenanceViewModel(){ }

        public MainMaintenanceViewModel(IUdListServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            SetAsEmptySelection();

            UdListList = new BindingList<UdList>();
            //disable new row feature...
            UdListList.AllowNew = false;
            
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
        {   //we need to make the system user is allowed access to this UI..
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
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> SaveRequiredNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> NewRecordNeededNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        public event EventHandler<NotificationEventArgs> WiggleToGhostFieldNotice;
        #endregion Notifications    

        #region Properties
        #region GeneralProperties

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
        #endregion GeneralProcedures

        #region UDList Properties
        private string _udListListCount;
        public string UdListListCount
        {
            get { return _udListListCount; }
            set
            {
                _udListListCount = value;
                NotifyPropertyChanged(m => m.UdListListCount);
            }
        }

        private BindingList<UdList> _udListList;
        public BindingList<UdList> UdListList
        {
            get
            {
                UdListListCount = _udListList.Count.ToString();
                if (_udListList.Count > 0)
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
                return _udListList;
            }
            set
            {
                _udListList = value;
                NotifyPropertyChanged(m => m.UdListList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private UdList _selectedUdListMirror;
        public UdList SelectedUdListMirror
        {
            get { return _selectedUdListMirror; }
            set { _selectedUdListMirror = value; }
        }

        private System.Collections.IList _selectedUdListList;
        public System.Collections.IList SelectedUdListList
        {
            get { return _selectedUdListList; }
            set
            {
                if (_selectedUdList != value)
                {
                    _selectedUdListList = value;
                    NotifyPropertyChanged(m => m.SelectedUdListList);
                }  
            }
        }

        private UdList _selectedUdList;
        public UdList SelectedUdList
        {
            get
            {
                return _selectedUdList;
            }
            set
            {   
                if (_selectedUdList != value)
                {
                    _selectedUdList = value;
                    //default UdListItem properties...
                    //if the selecteditem has an autoID > 0 we will allow children...
                    if (value.AutoID > 0)
                        AllowNewUdListItem = true;
                    else
                        AllowNewUdListItem = false;

                    AllowEditUdListItem = false;
                    AllowDeleteUdListItem = false;
                    Dirty = false;
                    AllowRowCopyUdListItem = false;
                    //we do a try catch supress as in some instance the UdListList is not instantiated...
                    try
                    {
                        if (UdListList != null && UdListList.Count > 0)
                        {
                            var udList = UdListList.SingleOrDefault(q => q.AutoID == value.AutoID);
                            //if the selection came from the child datagrid or the child was selected from the tree
                            //then we will not set the parent to be active nor will we auto select the first child...
                            if (UdListItemIsSelected == false)
                            {
                                udList.IsSelected = true;
                                //set the Child Select and List...
                                //don't set the child if the child is active...
                                //as the child will get set through the binding...
                                if (value.UdListItems.Count > 0)
                                    if(SelectedUdList.AutoID != udList.UdListItems.FirstOrDefault().AutoID)
                                        SelectedUdListItem = udList.UdListItems.FirstOrDefault();
                                else//if their are no items we have an emtpy items situation...
                                    SetAsEmptyItemSelection();
                            }
                            if (udList.UdListItems.Count > 0)
                            {
                                AllowEditUdListItem = true;
                                AllowDeleteUdListItem = true;
                                AllowRowCopyUdListItem = true;
                            }
                        }
                    }
                    catch { }

                    //set the mirrored SelectedUdList to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedUdListMirror = new UdList();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedUdList.GetType().GetProperties())
                        {
                            SelectedUdListMirror.SetPropertyValue(prop.Name, SelectedUdList.GetPropertyValue(prop.Name));
                        }
                        SelectedUdListMirror.UdListID = _selectedUdList.UdListID;
                        NotifyPropertyChanged(m => m.SelectedUdList);

                        SelectedUdList.PropertyChanged += new PropertyChangedEventHandler(SelectedUdList_PropertyChanged);
                    }
                }
            } //if (_selectedUdList != value)
        }

        private List<ColumnMetaData> _udListColumnMetaDataList;
        public List<ColumnMetaData> UdListColumnMetaDataList
        {
            get { return _udListColumnMetaDataList; }
            set
            {
                _udListColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.UdListColumnMetaDataList);
            }
        }
        #endregion UDList Properties

        #region Nested List Properties
        private object _selectedTreeItem;
        public object SelectedTreeItem
        {
            get { return _selectedTreeItem; }
            set
            {//set the udlist from the selectedtreeitem...
                _selectedTreeItem = value;
                UdList udList = new UdList();
                UdListItem udListItem = new UdListItem();
                ConvertNestedObject(value, out udList, out udListItem);
                //if the selecteditem is null then set it...
                if (SelectedUdList == null)
                    SelectedUdList = udList;
                else
                    if (SelectedUdList.AutoID != udList.AutoID)
                        SelectedUdList = udList;
                //if list items exist...
                if (udList.UdListItems.Count > 0)
                {//if it is null we will set it...
                    if (SelectedUdListItem == null)
                        SelectedUdListItem = udListItem;
                    else
                        if (SelectedUdListItem.AutoID != udListItem.AutoID)
                            SelectedUdListItem = udListItem;
                }
                else//no Items exists for selected UdList set it items selection and list to empty...
                    SetAsEmptyItemSelection();  
                NotifyPropertyChanged(m => m.SelectedTreeItem);
            }
        }

        public void ConvertNestedObject(object obj, out UdList udListout, out UdListItem udListItemout)
        {
            string type = obj.GetType().Name;
            udListout = new UdList();
            udListItemout = new UdListItem();
            UdListItemIsSelected = false;
            switch (type)
            {
                case "UdList":
                    udListout = (UdList)obj;
                    udListItemout = udListout.UdListItems.FirstOrDefault();
                    break;
                case "UdListItem":
                    UdListItemIsSelected = true;
                    udListItemout = (UdListItem)obj;
                    UdListItem item = (UdListItem)obj;
                    udListout = (from q in UdListList
                                 where q.UdListID == item.UdListID &&
                                 q.CompanyID == item.CompanyID
                                 select q).SingleOrDefault();
                    break;
                default:
                    break;
            }
        }
        #endregion Nested List Properties
        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _udListMaxFieldValueDictionary;
        public Dictionary<string, int> UdListMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_udListMaxFieldValueDictionary != null)
                    return _udListMaxFieldValueDictionary;

                _udListMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("UdLists");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _udListMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _udListMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedUdList_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   
            //these properties are not to be persisted we will igore them...
            if (e.PropertyName == "IsSelected" || 
                e.PropertyName == "IsExpanded" ||
                e.PropertyName == "IsValid" ||
                e.PropertyName == "NotValidMessage" ||
                e.PropertyName == "LastModifiedBy" ||
                e.PropertyName == "LastModifiedByDate")
                return;

            //Key ID Logic...
            if (e.PropertyName == "UdListID")
            {//make sure it is has changed...
                if (SelectedUdListMirror.UdListID != SelectedUdList.UdListID)
                {
                    //if their are no records it is a key change
                    if (UdListList != null && UdListList.Count == 0
                        && SelectedUdList != null && !string.IsNullOrEmpty(SelectedUdList.UdListID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetUdListState(SelectedUdList);

                    if (entityState == EntityStates.Unchanged ||
                        entityState == EntityStates.Modified)
                    {
                        if (Dirty  && AllowCommit)
                            NotifySaveRequired("Do you want to save changes?", _saveRequiredResultActions.ChangeKeyLogic);
                        else
                            ChangeKeyLogic();

                        return;
                    }
                }
            }//end KeyID logic...
            
            object propertyChangedValue = SelectedUdList.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedUdListMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedUdList.GetPropertyType(e.PropertyName);
            //in some instances the value is not really changing but yet it still is tripping property change..
            //This will ensure that the field has physically been modified...
            //As well when we revert back it constitutes a property change but they will be = and it will bypass the logic...
            bool objectsAreEqual;
            if (propertyChangedValue == null)
            {
                if (prevPropertyValue == null)
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
                if (UdListPropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedUdList);
                    //set the mirrored objects field...
                    SelectedUdListMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedUdListMirror.IsValid = SelectedUdList.IsValid;
                    SelectedUdListMirror.IsExpanded = SelectedUdList.IsExpanded;
                    SelectedUdListMirror.NotValidMessage = SelectedUdList.NotValidMessage;
                }
                else
                {//revert back to its previous value... 
                    SelectedUdList.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedUdList.IsValid = SelectedUdListMirror.IsValid;
                    SelectedUdList.IsExpanded = SelectedUdListMirror.IsExpanded;
                    SelectedUdList.NotValidMessage = SelectedUdListMirror.NotValidMessage;
                    return;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods

        private void ChangeKeyLogic()
        {
            if (! string.IsNullOrEmpty(SelectedUdList.UdListID))
            {
                //check to see if key is part of the current udListlist...
                UdList query = UdListList.Where(udList => udList.UdListID == SelectedUdList.UdListID &&
                                                        udList.AutoID != SelectedUdList.AutoID).SingleOrDefault();
                if (query != null)
                {//change to the newly selected udList...
                    //before navigating to the newly selected client cached record revert the old one back to what it was...
                    SelectedUdList.UdListID = SelectedUdListMirror.UdListID;
                    SelectedUdList = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                UdListList = GetUdListByID(SelectedUdList.UdListID, ClientSessionSingleton.Instance.CompanyID);
                if (UdListList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedUdList.UdListID + " Does Not Exist.  Create A New Record?");
                }
                else
                    SelectedUdList = UdListList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedUdList.UdListID != SelectedUdListMirror.UdListID)
                    SelectedUdList.UdListID = SelectedUdListMirror.UdListID;
            }
        }

        private bool CommitIsAllowed()
        {
            ////Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            foreach (UdList udList in UdListList)
            {   //check if row isvalid...
                if (udList.IsValid == 1)
                    return false;
                //make sure all children are valid...
                int count = (from q in udList.UdListItems where q.IsValid == 1 select q).Count();
                if (count > 0)
                    return false;
            }
           
            return true;
        }
        private void SetAsEmptySelection()
        {
            SelectedUdList = new UdList();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
            AllowNewUdListItem = false;
        }

        public void ClearLogic()
        {
            UdListList.Clear();
            SetAsEmptySelection();
            SetAsEmptyItemSelection();
        }
        //property change event...
        private bool UdListPropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {   
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "UdListID":
                    rBool = UdListIsValid(SelectedUdList, _udListValidationProperties.UdListID, out errorMessage);
                    break;
                case "Name":
                    rBool = UdListIsValid(SelectedUdList, _udListValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedUdList.IsValid = 1;
            }
            else //check the enire rows validity...
                SelectedUdList.IsValid = UdListIsValid(SelectedUdList, out errorMessage);

            SelectedUdList.NotValidMessage = errorMessage;
            return rBool;
        }
        
        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _udListValidationProperties
        {
            UdListID,
            Name
        }

        //Object.Property Scope Validation...
        private bool UdListIsValid(UdList udList, _udListValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _udListValidationProperties.UdListID:
                    //validate key
                    if (string.IsNullOrEmpty(udList.UdListID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetUdListState(udList);
                    if (entityState == EntityStates.Added && UdListExists(udList.UdListID))
                    {
                        errorMessage = "Item AllReady Exists...";
                        return false;
                    }
                    break;
                case _udListValidationProperties.Name:
                    //validate Description
                    if (string.IsNullOrEmpty(udList.Name))
                    {
                        errorMessage = "Description Is Required.";
                        return false;
                    }
                    break;
            }
            return true;
        }

        //UdList Object Scope Validation check the entire object for validity...
        private byte UdListIsValid(UdList udList, out string errorMessage)
        {
            //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(udList.UdListID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetUdListState(udList);
            if (entityState == EntityStates.Added && UdListExists(udList.UdListID))
            {
                errorMessage = "Item AllReady Exists.";
                return 1;
            }
            
            //validate Description
            if (string.IsNullOrEmpty(udList.Name))
            {
                errorMessage = "Name Is Required.";
                return 1;
            }
            return 2;
        }
        #endregion Validation Methods

        #region ServiceAgent Call Methods
        private EntityStates GetUdListState(UdList udList)
        {
            return _serviceAgent.GetUdListEntityState(udList);
        }
        //check to see if the repository has pending changes...
        private bool RepositoryIsDirty()
        {
            return _serviceAgent.UdListRepositoryIsDirty();
        }

        #region UdList CRUD
        private void Refresh()
        {
            //refetch current records...
            long selectedAutoID = SelectedUdList.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (UdList udList in UdListList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (udList.AutoID > 0)
                {
                    autoIDs = autoIDs + udList.AutoID.ToString() + ",";
                }
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                UdListList = new BindingList<UdList>(_serviceAgent.RefreshUdList(autoIDs).ToList());
                SelectedUdList = (from q in UdListList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();

                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<UdList> GetUdLists(string companyID)
        {
            BindingList<UdList> udListList = new BindingList<UdList>(_serviceAgent.GetUdLists(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return udListList; 
        }

        private BindingList<UdList> GetUdLists(UdList udList, string companyID)
        {
            BindingList<UdList> udListList = new BindingList<UdList>(_serviceAgent.GetUdLists(udList, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return udListList;
        }

        private BindingList<UdList> GetUdListByID(string udListID, string companyID)
        {
            BindingList<UdList> udListList = new BindingList<UdList>(_serviceAgent.GetUdListByID(udListID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return udListList; 
        }

        private bool UdListExists(string udListID)
        {
            return _serviceAgent.UdListExists(udListID, ClientSessionSingleton.Instance.CompanyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(UdList udList)
        {
            _serviceAgent.UpdateUdListRepository(udList);
            Dirty = true;
            if (CommitIsAllowed())
                AllowCommit = true;
            else
                AllowCommit = false;

            return AllowCommit;
        }
        //commits repository to the db...
        private bool Commit()
        {   //loop parent item to allow for searching of child items...
            foreach (UdList parentItem in UdListList)
            {
                if (parentItem.IsValid != 0)
                {
                    parentItem.IsValid = 0;
                    parentItem.NotValidMessage = null;
                }
                //search child  for pending saved marked records and mark them as valid...
                var childItems = (from q in parentItem.UdListItems where q.IsValid == 2 select q).ToList();
                foreach (UdListItem childItem in childItems)
                {
                    childItem.IsValid = 0;
                    childItem.NotValidMessage = null;
                }  
            }
            _serviceAgent.CommitUdListRepository();
            Dirty = false;
            AllowCommit = false;
            //their are no new records
            //if the currently selected record was new we will make sure children
            //are now allowed to be added to it...
            AllowNewUdListItem = true;
            return true;
        }

        private bool Delete(UdList udList)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromUdListRepository(udList);
            return true;
        }

        private bool NewUdList(string udListID)
        {
            UdList udList = new UdList();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newUdListAutoId = _newUdListAutoId - 1;
            udList.AutoID = _newUdListAutoId;
            udList.UdListID = udListID;
            udList.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            udList.IsValid = 1;
            udList.NotValidMessage = "New Record Key Field/s Are Required.";
            UdListList.Add(udList);
            _serviceAgent.AddToUdListRepository(udList);
            SelectedUdList = UdListList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }
        #endregion UdList CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteUdListRowCommand()
        {
            try
            {
                UdListColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewUdListCommand(""); //this will generate a new udList and set it as the selected udList...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedUdList.SetPropertyValue(UdListColumnMetaDataList[i].Name, columnValue);
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
            if (GetUdListState(SelectedUdList) != EntityStates.Detached)
            {
                if (Update(SelectedUdList))
                    Commit();
                else//this should not be hit but just in case we will catch it and then see 
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteUdListCommand()
        {
            int i = 0;
            int ii = 0;
            for (int j = SelectedUdListList.Count - 1; j >= 0; j--)
            {
                UdList udList = (UdList)SelectedUdListList[j];
                //get Max Index...
                i = UdListList.IndexOf(udList);
                if (i > ii)
                    ii = i;
                
                Delete(udList);
                UdListList.Remove(udList);
            }

            if (UdListList != null && UdListList.Count > 0)
            {
                //back off one index from the max index...
                ii = ii - 1;    

                //if they delete the first row...
                if (ii < 0)
                    ii = 0;

                //make sure it does not exceed the list count...
                if (ii >= UdListList.Count())
                    ii = UdListList.Count - 1;

                SelectedUdList = UdListList[ii];
                //we will only enable committ for dirty validated records...
                if (Dirty == true)
                    AllowCommit = CommitIsAllowed();
                else
                    AllowCommit = false;
            }
            else//only one record, deleting will result in no records...
                SetAsEmptySelection(); 
        }

        public void NewUdListCommand()
        {
            NewUdList("");
            AllowCommit = false;
        }

        public void NewUdListCommand(string udListID)
        {
            NewUdList(udListID);
            if (string.IsNullOrEmpty(udListID))
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
            RegisterToReceiveMessages<BindingList<UdList>>(MessageTokens.UdListSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<UdList>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                UdListList = e.Data;
                SelectedUdList = UdListList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<UdList>>(MessageTokens.UdListSearchToken.ToString(), OnSearchResult);
        }
        #endregion Commands

        #region Helpers

        //this will allow us to notify between mulitple user controls...
        //as when a save is made we will want to notify all the children user controls...
        //to wiggle between elements to post any current changes that were not committed...
        //because they were not tabbed from...
        public void NotifyWiggleToGhostField()
        {
            Notify(WiggleToGhostFieldNotice, new NotificationEventArgs());
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
                    SelectedUdList.UdListID = SelectedUdListMirror.UdListID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with UdListID provided...
                    NewUdListCommand(SelectedUdList.UdListID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedUdList.UdListID = SelectedUdListMirror.UdListID;
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
                    //_serviceAgent.CommitUdListRepository();
                    Commit();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedUdList.UdListID = SelectedUdListMirror.UdListID;
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
        #region UdList Extensions
        public static object GetPropertyValue(this UdList myObj, string propertyName)
        {
            var propInfo = typeof(UdList).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this UdList myObj, string propertyName)
        {
            var propInfo = typeof(UdList).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this UdList myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(UdList).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
        #endregion UdList Extensions
    }
}