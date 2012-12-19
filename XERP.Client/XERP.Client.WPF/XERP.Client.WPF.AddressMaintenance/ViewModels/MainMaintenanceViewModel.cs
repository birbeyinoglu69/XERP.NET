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
using XERP.Domain.AddressDomain.AddressDataService;
using XERP.Domain.AddressDomain.Services;

namespace XERP.Client.WPF.AddressMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newAddressAutoId;

        private IAddressServiceAgent _serviceAgent;
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

        }

        public MainMaintenanceViewModel(IAddressServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            AddressList = new BindingList<Address>();
            //disable new row feature...
            AddressList.AllowNew = false;
            
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

        private string _addressListCount;
        public string AddressListCount
        {
            get { return _addressListCount; }
            set
            {
                _addressListCount = value;
                NotifyPropertyChanged(m => m.AddressListCount);
            }
        }
        
        private BindingList<Address> _addressList;
        public BindingList<Address> AddressList
        {
            get
            {
                AddressListCount = _addressList.Count.ToString();
                if (_addressList.Count > 0)
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
                return _addressList;
            }
            set
            {
                _addressList = value;
                NotifyPropertyChanged(m => m.AddressList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private Address _selectedAddressMirror;
        public Address SelectedAddressMirror
        {
            get { return _selectedAddressMirror; }
            set { _selectedAddressMirror = value; }
        }

        private System.Collections.IList _selectedAddressList;
        public System.Collections.IList SelectedAddressList
        {
            get { return _selectedAddressList; }
            set
            {
                if (_selectedAddress != value)
                {
                    _selectedAddressList = value;
                    NotifyPropertyChanged(m => m.SelectedAddressList);
                }  
            }
        }

        private Address _selectedAddress;
        public Address SelectedAddress
        {
            get 
            {
                return _selectedAddress; 
            }
            set
            {
                if (_selectedAddress != value)
                {
                    _selectedAddress = value;
                    //set the mirrored SelectedAddress to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedAddressMirror = new Address();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedAddress.GetType().GetProperties())
                        {
                            SelectedAddressMirror.SetPropertyValue(prop.Name, SelectedAddress.GetPropertyValue(prop.Name));
                        }
                        SelectedAddressMirror.AddressID = _selectedAddress.AddressID;
                        NotifyPropertyChanged(m => m.SelectedAddress);
                        
                        SelectedAddress.PropertyChanged += new PropertyChangedEventHandler(SelectedAddress_PropertyChanged); 
                    }
                }
            }
        }

        private List<ColumnMetaData> _addressColumnMetaDataList;
        public List<ColumnMetaData> AddressColumnMetaDataList
        {
            get { return _addressColumnMetaDataList; }
            set 
            { 
                _addressColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.AddressColumnMetaDataList);
            }
        }

        #region Validation Properties
        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _addressMaxFieldValueDictionary;
        public Dictionary<string, int> AddressMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_addressMaxFieldValueDictionary != null)
                    return _addressMaxFieldValueDictionary;

                _addressMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("Addresses");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _addressMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _addressMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedAddress_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "AddressID")
            {//make sure it is has changed...
                if (SelectedAddressMirror.AddressID != SelectedAddress.AddressID)
                {
                    //if their are no records it is a key change
                    if (AddressList != null && AddressList.Count == 0
                        && SelectedAddress != null && !string.IsNullOrEmpty(SelectedAddress.AddressID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetAddressState(SelectedAddress);

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
            
            object propertyChangedValue = SelectedAddress.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedAddressMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedAddress.GetPropertyType(e.PropertyName);
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
                if (AddressPropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedAddress);
                    //set the mirrored objects field...
                    SelectedAddressMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedAddressMirror.IsValid = SelectedAddress.IsValid;
                    SelectedAddressMirror.IsExpanded = SelectedAddress.IsExpanded;
                    SelectedAddressMirror.NotValidMessage = SelectedAddress.NotValidMessage;
                }
                else
                {
                    SelectedAddress.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedAddress.IsValid = SelectedAddressMirror.IsValid;
                    SelectedAddress.IsExpanded = SelectedAddressMirror.IsExpanded;
                    SelectedAddress.NotValidMessage = SelectedAddressMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedAddress.AddressID))
            {//check to see if key is part of the current companylist...
                Address query = AddressList.Where(company => company.AddressID == SelectedAddress.AddressID &&
                                                        company.AutoID != SelectedAddress.AutoID).SingleOrDefault();
                if (query != null)
                {//revert it back
                    SelectedAddress.AddressID = SelectedAddressMirror.AddressID;
                    //change to the newly selected company...
                    SelectedAddress = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                AddressList = GetAddressByID(SelectedAddress.AddressID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (AddressList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedAddress.AddressID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedAddress = AddressList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedAddress.AddressID != SelectedAddressMirror.AddressID)
                    SelectedAddress.AddressID = SelectedAddressMirror.AddressID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in AddressList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedAddress = new Address();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            AddressList.Clear();
            SetAsEmptySelection();
        }

        private bool AddressPropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "AddressID":
                    rBool = AddressIsValid(SelectedAddress, _addressValidationProperties.AddressID, out errorMessage);
                    break;
                case "Name":
                    rBool = AddressIsValid(SelectedAddress, _addressValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedAddress.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedAddress.IsValid = AddressIsValid(SelectedAddress, out errorMessage);
                if (SelectedAddress.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedAddress.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _addressValidationProperties
        {//we list all fields that require validation...
            AddressID,
            Name
        }

        //Object.Property Scope Validation...
        private bool AddressIsValid(Address item, _addressValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _addressValidationProperties.AddressID:
                    //validate key
                    if (string.IsNullOrEmpty(item.AddressID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetAddressState(item);
                    if (entityState == EntityStates.Added && AddressExists(item.AddressID))
                    {
                        errorMessage = "Item AllReady Exists...";
                        return false;
                    }
                    break;
                case _addressValidationProperties.Name:
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
        //Address Object Scope Validation check the entire object for validity...
        private byte AddressIsValid(Address item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.AddressID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetAddressState(item);
            if (entityState == EntityStates.Added && AddressExists(item.AddressID))
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

        private EntityStates GetAddressState(Address item)
        {
            return _serviceAgent.GetAddressEntityState(item);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.AddressRepositoryIsDirty();
        }

        #region Address CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedAddress.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (Address item in AddressList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                AddressList = new BindingList<Address>(_serviceAgent.RefreshAddress(autoIDs).ToList());
                SelectedAddress = (from q in AddressList
                                   where q.AutoID == selectedAutoID
                                   select q).SingleOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<Address> GetAddresses(string companyID)
        {
            BindingList<Address> itemList = new BindingList<Address>(_serviceAgent.GetAddresses(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private BindingList<Address> GetAddresses(Address item, string companyID)
        {
            BindingList<Address> itemList = new BindingList<Address>(_serviceAgent.GetAddresses(item, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<Address> GetAddressByID(string itemID, string companyID)
        {
            BindingList<Address> itemList = new BindingList<Address>(_serviceAgent.GetAddressByID(itemID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private bool AddressExists(string itemID)
        {
            return _serviceAgent.AddressExists(itemID, ClientSessionSingleton.Instance.CompanyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(Address item)
        {
            _serviceAgent.UpdateAddressRepository(item);
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
            var items = (from q in AddressList where q.IsValid == 2 select q).ToList();
            foreach (Address item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitAddressRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }


        private bool Delete(Address item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromAddressRepository(item);
            return true;
        }

        private bool NewAddress(string itemID)
        {
            Address newItem = new Address();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newAddressAutoId = _newAddressAutoId - 1;
            newItem.AutoID = _newAddressAutoId;
            newItem.AddressID = itemID;
            newItem.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            newItem.IsValid = 1;
            newItem.NotValidMessage = "New Record Key Field/s Are Required.";
            AddressList.Add(newItem);
            _serviceAgent.AddToAddressRepository(newItem);
            SelectedAddress = AddressList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }


        #endregion Address CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                AddressColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewAddressCommand(""); //this will generate a new item and set it as the selected item...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedAddress.SetPropertyValue(AddressColumnMetaDataList[i].Name, columnValue);
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
            if (GetAddressState(SelectedAddress) != EntityStates.Detached)
            {
                if (Update(SelectedAddress))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }
        public void RefreshCommand()
        {
            Refresh();
        }
        public void DeleteAddressCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedAddressList.Count - 1; j >= 0; j--)
                {
                    Address item = (Address)SelectedAddressList[j];
                    //get Max Index...
                    i = AddressList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    AddressList.Remove(item);
                }

                if (AddressList != null && AddressList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= AddressList.Count())
                        ii = AddressList.Count - 1;

                    SelectedAddress = AddressList[ii];
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
                NotifyMessage("Address/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewAddressCommand()
        {
            NewAddress("");
            AllowCommit = false;
        }

        public void NewAddressCommand(string itemID)
        {
            NewAddress(itemID);
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
            RegisterToReceiveMessages<BindingList<Address>>(MessageTokens.AddressSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<Address>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                AddressList = e.Data;
                SelectedAddress = AddressList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<Address>>(MessageTokens.AddressSearchToken.ToString(), OnSearchResult);
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
                    SelectedAddress.AddressID = SelectedAddressMirror.AddressID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with AddressID provided...
                    NewAddressCommand(SelectedAddress.AddressID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedAddress.AddressID = SelectedAddressMirror.AddressID;
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
                    _serviceAgent.CommitAddressRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedAddress.AddressID = SelectedAddressMirror.AddressID;
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
        public static object GetPropertyValue(this Address myObj, string propertyName)
        {
            var propInfo = typeof(Address).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this Address myObj, string propertyName)
        {
            var propInfo = typeof(Address).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this Address myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(Address).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}