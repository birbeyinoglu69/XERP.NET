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
using XERP.Domain.AddressDomain.Services;
using XERP.Domain.AddressDomain.AddressDataService;
//required for extension methods...
using ExtensionMethods;
using XERP.Client.Models;

namespace XERP.Client.WPF.AddressMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        
        private IAddressServiceAgent _serviceAgent;
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
                {
                    return _addressMaxFieldValueDictionary;
                }
                _addressMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("Addresses");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                    {
                        _addressMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                    }
                }
                return _addressMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedAddress_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {   //Key ID Logic...
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
            
            object propertyChangedValue = SelectedAddress.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedAddressMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedAddress.GetPropertyType(e.PropertyName);
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
                    Update(SelectedAddress);
                    //set the mirrored objects field...
                    SelectedAddressMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                }
                else
                {//revert back to its previous value... 
                    SelectedAddress.SetPropertyValue(e.PropertyName, prevPropertyValue);
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
            if (KeyChangeIsValid(SelectedAddress.AddressID, out errorMessage))
            {
                //check to see if key is part of the current addresslist...
                Address query = AddressList.Where(address => address.AddressID == SelectedAddress.AddressID &&
                                                        address.AutoID != SelectedAddress.AutoID).FirstOrDefault();
                if (query != null)
                {//change to the newly selected address...
                    SelectedAddress = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                AddressList = GetAddressByID(SelectedAddress.AddressID, ClientSessionSingleton.Instance.CompanyID);
                if (AddressList.Count == 0)
                {//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedAddress.AddressID + " Does Not Exist.  Create A New Record?");
                }
                else
                {
                    SelectedAddress = AddressList.FirstOrDefault();
                }
            }
            else
            {
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedAddress.AddressID != SelectedAddressMirror.AddressID)
                {
                    SelectedAddress.AddressID = SelectedAddressMirror.AddressID;
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
            foreach (Address address in AddressList)
            {
                EntityStates entityState = GetAddressState(address);
                if (entityState == EntityStates.Modified || 
                    entityState == EntityStates.Detached)
                {
                    Dirty = true;
                }
                if (entityState == EntityStates.Added)
                {
                    Dirty = true;
                    //only one record can be added at a time...
                    if (NewKeyIsValid(address, out errorMessage) == false)
                    {
                        rBool = false;
                    }
                }
                if (NameIsValid(address.Name, out errorMessage) == false)
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
                case "AddressID":
                    rBool = NewKeyIsValid(SelectedAddress, out errorMessage);
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

        private bool NewKeyIsValid(Address address, out string errorMessage)
        {
            errorMessage = "";
            if (KeyChangeIsValid(address.AddressID, out errorMessage) == false)
            {
                return false;
            }
            if (AddressExists(address.AddressID.ToString()))
            {
                errorMessage = "AddressID " + address.AddressID + " Allready Exists...";
                return false;
            }
            return true;
        }

        private bool KeyChangeIsValid(object addressID, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty((string)addressID))
            {
                errorMessage = "AddressID Is Required...";
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
       
        private EntityStates GetAddressState(Address address)
        {
            return _serviceAgent.GetAddressEntityState(address);
        }

        #region Address CRUD
        private void Refresh()
        {

            //refetch current records...
            long selectedAutoID = SelectedAddress.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (Address address in AddressList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (address.AutoID > 0)
                {
                    autoIDs = autoIDs + address.AutoID.ToString() + ",";
                }
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
            BindingList<Address> addressList = new BindingList<Address>(_serviceAgent.GetAddresses(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return addressList; 
        }

        private BindingList<Address> GetAddresses(Address address, string companyID)
        {
            BindingList<Address> addressList = new BindingList<Address>(_serviceAgent.GetAddresses(address, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return addressList;
        }

        private BindingList<Address> GetAddressByID(string addressID, string companyID)
        {
            BindingList<Address> addressList = new BindingList<Address>(_serviceAgent.GetAddressByID(addressID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return addressList; 
        }

        private bool AddressExists(string addressID)
        {
            return _serviceAgent.AddressExists(addressID, ClientSessionSingleton.Instance.CompanyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(Address address)
        {
            _serviceAgent.UpdateAddressRepository(address);
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
            _serviceAgent.CommitAddressRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(Address address)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromAddressRepository(address);
            return true;
        }

        private bool NewAddress(string addressID)
        {
            Address address = new Address();
            address.AddressID = addressID;
            address.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            AddressList.Add(address);
            _serviceAgent.AddToAddressRepository(address);
            SelectedAddress = AddressList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion Address CRUD
        #endregion ServiceAgent Call Methods

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
                    NewAddressCommand(""); //this will generate a new address and set it as the selected address...
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
            for (int j = SelectedAddressList.Count - 1; j >= 0; j--)
            {
                Address address = (Address)SelectedAddressList[j];
                if (isFirstDelete)
                {//the result of this will be the record directly before the selected records...
                    i = AddressList.IndexOf(address) - SelectedAddressList.Count;
                }
                
                Delete(address);
                AddressList.Remove(address);
            }

            if (AddressList != null && AddressList.Count > 0)
            {
                //if they delete the first row...
                if (i < 0)
                {
                    i = 0;
                }
                SelectedAddress = AddressList[i];
                AllowCommit = CommitIsAllowed();
            }
            else
            {//only one record, deleting will result in no records...
                SetAsEmptySelection();
            }  
        }

        public void NewAddressCommand()
        {
            NewAddress("");
            AllowCommit = false;
        }

        public void NewAddressCommand(string addressID)
        {
            NewAddress(addressID);
            if (string.IsNullOrEmpty(addressID))
            {//don't allow a save until a addressID is provided...
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
            {
                return propInfo.GetValue(myObj, null);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPropertyType(this Address myObj, string propertyName)
        {
            var propInfo = typeof(Address).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.PropertyType.Name.ToString();
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue(this Address myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(Address).GetProperty((string)propertyName);

            if (propInfo != null)
            {
                propInfo.SetValue(myObj, propertyValue, null);
            }
        }
    }
}