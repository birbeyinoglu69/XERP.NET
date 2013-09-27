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
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Client.WPF.WarehouseMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newWarehouseAutoId;

        private IWarehouseServiceAgent _serviceAgent;
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
            WarehouseTypeList = BuildWarehouseTypeDropDown();
            WarehouseCodeList = BuildWarehouseCodeDropDown();
            PlantList = BuildPlantDropDown();
            AddressList = BuildAddressDropDown();
        }

        public MainMaintenanceViewModel(IWarehouseServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            WarehouseList = new BindingList<Warehouse>();
            //disable new row feature...
            WarehouseList.AllowNew = false;
            
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

        private string _warehouseListCount;
        public string WarehouseListCount
        {
            get { return _warehouseListCount; }
            set
            {
                _warehouseListCount = value;
                NotifyPropertyChanged(m => m.WarehouseListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region DropDown Collections
        private ObservableCollection<WarehouseType> _warehouseTypeList;
        public ObservableCollection<WarehouseType> WarehouseTypeList
        {
            get { return _warehouseTypeList; }
            set
            {
                _warehouseTypeList = value;
                NotifyPropertyChanged(m => m.WarehouseTypeList);
            }
        }

        private ObservableCollection<WarehouseCode> _warehouseCodeList;
        public ObservableCollection<WarehouseCode> WarehouseCodeList
        {
            get { return _warehouseCodeList; }
            set
            {
                _warehouseCodeList = value;
                NotifyPropertyChanged(m => m.WarehouseCodeList);
            }
        }

        private ObservableCollection<Plant> _plantList;
        public ObservableCollection<Plant> PlantList
        {
            get { return _plantList; }
            set
            {
                _plantList = value;
                NotifyPropertyChanged(m => m.PlantList);
            }
        }

        private ObservableCollection<Address> _addressList;
        public ObservableCollection<Address> AddressList
        {
            get { return _addressList; }
            set
            {
                _addressList = value;
                NotifyPropertyChanged(m => m.AddressList);
            }
        }
        #endregion DropDown Collections

        #region Validation Properties
        //meta data of the object is used to set max length...
        private List<ColumnMetaData> _warehouseColumnMetaDataList;
        public List<ColumnMetaData> WarehouseColumnMetaDataList
        {
            get { return _warehouseColumnMetaDataList; }
            set
            {
                _warehouseColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.WarehouseColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _warehouseMaxFieldValueDictionary;
        public Dictionary<string, int> WarehouseMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_warehouseMaxFieldValueDictionary != null)
                    return _warehouseMaxFieldValueDictionary;

                _warehouseMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("Warehouses");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _warehouseMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _warehouseMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties

        #region CRUD Object Properties
        private BindingList<Warehouse> _warehouseList;
        public BindingList<Warehouse> WarehouseList
        {
            get
            {
                WarehouseListCount = _warehouseList.Count.ToString();
                if (_warehouseList.Count > 0)
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
                return _warehouseList;
            }
            set
            {
                _warehouseList = value;
                NotifyPropertyChanged(m => m.WarehouseList);
            }
        }
        //this is used to collect previous values as to compare the changed values...
        private Warehouse _selectedWarehouseMirror;
        public Warehouse SelectedWarehouseMirror
        {
            get { return _selectedWarehouseMirror; }
            set { _selectedWarehouseMirror = value; }
        }

        private System.Collections.IList _selectedWarehouseList;
        public System.Collections.IList SelectedWarehouseList
        {
            get { return _selectedWarehouseList; }
            set
            {
                if (_selectedWarehouse != value)
                {
                    _selectedWarehouseList = value;
                    NotifyPropertyChanged(m => m.SelectedWarehouseList);
                }  
            }
        }

        private Warehouse _selectedWarehouse;
        public Warehouse SelectedWarehouse
        {
            get 
            {
                return _selectedWarehouse; 
            }
            set
            {
                if (_selectedWarehouse != value)
                {
                    _selectedWarehouse = value;
                    //set the mirrored SelectedWarehouse to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedWarehouseMirror = new Warehouse();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedWarehouse.GetType().GetProperties())
                        {
                            SelectedWarehouseMirror.SetPropertyValue(prop.Name, SelectedWarehouse.GetPropertyValue(prop.Name));
                        }
                        SelectedWarehouseMirror.WarehouseID = _selectedWarehouse.WarehouseID;
                        NotifyPropertyChanged(m => m.SelectedWarehouse);
                        
                        SelectedWarehouse.PropertyChanged += new PropertyChangedEventHandler(SelectedWarehouse_PropertyChanged); 
                    }
                }
            }
        }
        #endregion CRUD Object Properties
        #endregion Properties

        #region ViewModel Property Events
        private void SelectedWarehouse_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "WarehouseID")
            {//make sure it is has changed...
                if (SelectedWarehouseMirror.WarehouseID != SelectedWarehouse.WarehouseID)
                {
                    //if their are no records it is a key change
                    if (WarehouseList != null && WarehouseList.Count == 0
                        && SelectedWarehouse != null && !string.IsNullOrEmpty(SelectedWarehouse.WarehouseID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetWarehouseState(SelectedWarehouse);

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

            object propertyChangedValue = SelectedWarehouse.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedWarehouseMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedWarehouse.GetPropertyType(e.PropertyName);
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
                if (WarehousePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedWarehouse);
                    //set the mirrored objects field...
                    SelectedWarehouseMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedWarehouseMirror.IsValid = SelectedWarehouse.IsValid;
                    SelectedWarehouseMirror.IsExpanded = SelectedWarehouse.IsExpanded;
                    SelectedWarehouseMirror.NotValidMessage = SelectedWarehouse.NotValidMessage;
                }
                else
                {
                    SelectedWarehouse.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedWarehouse.IsValid = SelectedWarehouseMirror.IsValid;
                    SelectedWarehouse.IsExpanded = SelectedWarehouseMirror.IsExpanded;
                    SelectedWarehouse.NotValidMessage = SelectedWarehouseMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Property Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedWarehouse.WarehouseID))
            {//check to see if key is part of the current companylist...
                Warehouse query = WarehouseList.Where(company => company.WarehouseID == SelectedWarehouse.WarehouseID &&
                                                        company.AutoID != SelectedWarehouse.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back
                    SelectedWarehouse.WarehouseID = SelectedWarehouseMirror.WarehouseID;
                    //change to the newly selected company...
                    SelectedWarehouse = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                WarehouseList = GetWarehouseByID(SelectedWarehouse.WarehouseID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (WarehouseList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedWarehouse.WarehouseID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedWarehouse = WarehouseList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedWarehouse.WarehouseID != SelectedWarehouseMirror.WarehouseID)
                    SelectedWarehouse.WarehouseID = SelectedWarehouseMirror.WarehouseID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in WarehouseList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedWarehouse = new Warehouse();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            WarehouseList.Clear();
            SetAsEmptySelection();
        }

        private bool WarehousePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "WarehouseID":
                    rBool = WarehouseIsValid(SelectedWarehouse, _warehouseValidationProperties.WarehouseID, out errorMessage);
                    break;
                case "Name":
                    rBool = WarehouseIsValid(SelectedWarehouse, _warehouseValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedWarehouse.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedWarehouse.IsValid = WarehouseIsValid(SelectedWarehouse, out errorMessage);
                if (SelectedWarehouse.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedWarehouse.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _warehouseValidationProperties
        {//we list all fields that require validation...
            WarehouseID,
            Name
        }

        //Object.Property Scope Validation...
        private bool WarehouseIsValid(Warehouse item, _warehouseValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _warehouseValidationProperties.WarehouseID:
                    //validate key
                    if (string.IsNullOrEmpty(item.WarehouseID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetWarehouseState(item);
                    if (entityState == EntityStates.Added && WarehouseExists(item.WarehouseID, item.PlantID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = WarehouseList.Count(q => q.WarehouseID == item.WarehouseID);
                    if(count > 1)
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    break;
                case _warehouseValidationProperties.Name:
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
        //Warehouse Object Scope Validation check the entire object for validity...
        private byte WarehouseIsValid(Warehouse item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.WarehouseID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetWarehouseState(item);
            if (entityState == EntityStates.Added && WarehouseExists(item.WarehouseID, item.PlantID))
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = WarehouseList.Count(q => q.WarehouseID == item.WarehouseID);
            if (count > 1)
            {
                errorMessage = "Item All Ready Exists...";
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
        #region DropDown Methods
        private ObservableCollection<WarehouseType> BuildWarehouseTypeDropDown()
        {
            List<WarehouseType> list = new List<WarehouseType>();
            list = _serviceAgent.GetWarehouseTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new WarehouseType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<WarehouseType>(list);
        }

        private ObservableCollection<WarehouseCode> BuildWarehouseCodeDropDown()
        {
            List<WarehouseCode> list = new List<WarehouseCode>();
            list = _serviceAgent.GetWarehouseCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new WarehouseCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<WarehouseCode>(list);
        }

        private ObservableCollection<Plant> BuildPlantDropDown()
        {
            List<Plant> list = new List<Plant>();
            list = _serviceAgent.GetPlantsReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new Plant());
            list.Sort((x, y) => string.Compare(x.Name, y.Name));

            return new ObservableCollection<Plant>(list);
        }

        private ObservableCollection<Address> BuildAddressDropDown()
        {
            List<Address> list = new List<Address>();
            list = _serviceAgent.GetAddressesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new Address());
            list.Sort((x, y) => string.Compare(x.Name, y.Name));

            return new ObservableCollection<Address>(list);
        }
        #endregion DropDown Methods

        private EntityStates GetWarehouseState(Warehouse item)
        {
            return _serviceAgent.GetWarehouseEntityState(item);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.WarehouseRepositoryIsDirty();
        }

        #region Warehouse CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedWarehouse.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (Warehouse item in WarehouseList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                WarehouseList = new BindingList<Warehouse>(_serviceAgent.RefreshWarehouse(autoIDs).ToList());
                SelectedWarehouse = (from q in WarehouseList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<Warehouse> GetWarehouses(string companyID)
        {
            BindingList<Warehouse> itemList = new BindingList<Warehouse>(_serviceAgent.GetWarehouses(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private BindingList<Warehouse> GetWarehouses(Warehouse item, string companyID)
        {
            BindingList<Warehouse> itemList = new BindingList<Warehouse>(_serviceAgent.GetWarehouses(item, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<Warehouse> GetWarehouseByID(string itemID, string companyID)
        {
            BindingList<Warehouse> itemList = new BindingList<Warehouse>(_serviceAgent.GetWarehouseByID(itemID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private bool WarehouseExists(string itemID, string plantID)
        {
            return _serviceAgent.WarehouseExists(itemID, ClientSessionSingleton.Instance.CompanyID, plantID);
        }
        //udpate merely updates the repository a commit is required to commit it to the db...
        private bool Update(Warehouse item)
        {
            _serviceAgent.UpdateWarehouseRepository(item);
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
            var items = (from q in WarehouseList where q.IsValid == 2 select q).ToList();
            foreach (Warehouse item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitWarehouseRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(Warehouse item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromWarehouseRepository(item);
            return true;
        }

        private bool NewWarehouse(string itemID)
        {
            Warehouse newItem = new Warehouse();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newWarehouseAutoId = _newWarehouseAutoId - 1;
            newItem.AutoID = _newWarehouseAutoId;
            newItem.WarehouseID = itemID;
            newItem.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            newItem.IsValid = 1;
            newItem.NotValidMessage = "New Record Key Field/s Are Required.";
            WarehouseList.Add(newItem);
            _serviceAgent.AddToWarehouseRepository(newItem);
            SelectedWarehouse = WarehouseList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }
        #endregion Warehouse CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                WarehouseColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewWarehouseCommand(""); //this will generate a new item and set it as the selected item...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedWarehouse.SetPropertyValue(WarehouseColumnMetaDataList[i].Name, columnValue);
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
            if (GetWarehouseState(SelectedWarehouse) != EntityStates.Detached)
            {
                if (Update(SelectedWarehouse))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteWarehouseCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedWarehouseList.Count - 1; j >= 0; j--)
                {
                    Warehouse item = (Warehouse)SelectedWarehouseList[j];
                    //get Max Index...
                    i = WarehouseList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    WarehouseList.Remove(item);
                }

                if (WarehouseList != null && WarehouseList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= WarehouseList.Count())
                        ii = WarehouseList.Count - 1;

                    SelectedWarehouse = WarehouseList[ii];
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
                NotifyMessage("Warehouse/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewWarehouseCommand()
        {
            NewWarehouse("");
            AllowCommit = false;
        }

        public void NewWarehouseCommand(string itemID)
        {
            NewWarehouse(itemID);
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
            RegisterToReceiveMessages<BindingList<Warehouse>>(MessageTokens.WarehouseSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<Warehouse>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                WarehouseList = e.Data;
                SelectedWarehouse = WarehouseList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<Warehouse>>(MessageTokens.WarehouseSearchToken.ToString(), OnSearchResult);
        }

        #region Right Click FK searches
        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<WarehouseType>>(MessageTokens.WarehouseTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedWarehouse.WarehouseTypeID = e.Data.FirstOrDefault().WarehouseTypeID;

            UnregisterToReceiveMessages<BindingList<WarehouseType>>(MessageTokens.WarehouseTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<WarehouseCode>>(MessageTokens.WarehouseCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedWarehouse.WarehouseCodeID = e.Data.FirstOrDefault().WarehouseCodeID;

            UnregisterToReceiveMessages<BindingList<WarehouseType>>(MessageTokens.WarehouseTypeSearchToken.ToString(), OnTypeSearchResult);
        }
        #endregion Right Click FK searches
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
                    SelectedWarehouse.WarehouseID = SelectedWarehouseMirror.WarehouseID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with WarehouseID provided...
                    NewWarehouseCommand(SelectedWarehouse.WarehouseID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouse.WarehouseID = SelectedWarehouseMirror.WarehouseID;
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
                    _serviceAgent.CommitWarehouseRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouse.WarehouseID = SelectedWarehouseMirror.WarehouseID;
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

        private void NotifySearch(string message)
        {//Notify view to launch search...
            Notify(SearchNotice, new NotificationEventArgs(message));
        }

        #region Right Click FK Helpers
        private void NotifyTypeSearch(string message)
        {
            Notify(TypeSearchNotice, new NotificationEventArgs(message));
        }

        private void NotifyCodeSearch(string message)
        {
            Notify(CodeSearchNotice, new NotificationEventArgs(message));
        }
        #endregion Right Click FK Notifies
        #endregion Helpers
    }
}

namespace ExtensionMethods
{
    public static partial class XERPExtensions
    {
        public static object GetPropertyValue(this Warehouse myObj, string propertyName)
        {
            var propInfo = typeof(Warehouse).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this Warehouse myObj, string propertyName)
        {
            var propInfo = typeof(Warehouse).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this Warehouse myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(Warehouse).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}