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

namespace XERP.Client.WPF.WarehouseLocationMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newWarehouseLocationAutoId;

        private IWarehouseServiceAgent _serviceAgent;
        private enum _saveRequiredResultActions
        {
            ChangeKeyLogic,
            SearchLogic,
            ClearLogic
        }
        //required else it generates debug view designer issues 
        public MainMaintenanceViewModel() { }

        public void BuildDropDowns()
        {
            WarehouseLocationTypeList = BuildWarehouseLocationTypeDropDown();
            WarehouseLocationCodeList = BuildWarehouseLocationCodeDropDown();
            WarehouseList = BuildWarehouseDropDown();
        }

        public MainMaintenanceViewModel(IWarehouseServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            WarehouseLocationList = new BindingList<WarehouseLocation>();
            //disable new row feature...
            WarehouseLocationList.AllowNew = false;
            //disable Key Entry by default... it is only enabled on getnew...
            AllowKeyEntry = false;

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
        public event EventHandler<NotificationEventArgs> TypeSearchNotice;
        public event EventHandler<NotificationEventArgs> CodeSearchNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> SaveRequiredNotice;
        public event EventHandler<NotificationEventArgs<bool, MessageBoxResult>> NewRecordNeededNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        #endregion Notifications

        #region Properties
        #region General Form Function/State Properties
        //used to enable/disable Key Field/s
        private bool _allowKeyEntry;
        public bool AllowKeyEntry
        {
            get { return _allowKeyEntry; }
            set
            {
                _allowKeyEntry = value;
                NotifyPropertyChanged(m => m.AllowKeyEntry);
            }
        }

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

        private string _warehouseLocationListCount;
        public string WarehouseLocationListCount
        {
            get { return _warehouseLocationListCount; }
            set
            {
                _warehouseLocationListCount = value;
                NotifyPropertyChanged(m => m.WarehouseLocationListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region DropDown Collections
        private ObservableCollection<WarehouseLocationType> _warehouseLocationTypeList;
        public ObservableCollection<WarehouseLocationType> WarehouseLocationTypeList
        {
            get { return _warehouseLocationTypeList; }
            set
            {
                _warehouseLocationTypeList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationTypeList);
            }
        }

        private ObservableCollection<WarehouseLocationCode> _warehouseLocationCodeList;
        public ObservableCollection<WarehouseLocationCode> WarehouseLocationCodeList
        {
            get { return _warehouseLocationCodeList; }
            set
            {
                _warehouseLocationCodeList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationCodeList);
            }
        }

        private ObservableCollection<Warehouse> _warehouseList;
        public ObservableCollection<Warehouse> WarehouseList
        {
            get { return _warehouseList; }
            set
            {
                _warehouseList = value;
                NotifyPropertyChanged(m => m.WarehouseList);
            }
        }
        #endregion DropDown Collections

        #region Validation Properties
        //meta data of the object is used to set max length...
        private List<ColumnMetaData> _warehouseLocationColumnMetaDataList;
        public List<ColumnMetaData> WarehouseLocationColumnMetaDataList
        {
            get { return _warehouseLocationColumnMetaDataList; }
            set
            {
                _warehouseLocationColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _warehouseLocationMaxFieldValueDictionary;
        public Dictionary<string, int> WarehouseLocationMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_warehouseLocationMaxFieldValueDictionary != null)
                    return _warehouseLocationMaxFieldValueDictionary;

                _warehouseLocationMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("WarehouseLocations");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _warehouseLocationMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _warehouseLocationMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties

        #region CRUD Object Properties
        private BindingList<WarehouseLocation> _warehouseLocationList;
        public BindingList<WarehouseLocation> WarehouseLocationList
        {
            get
            {
                WarehouseLocationListCount = _warehouseLocationList.Count.ToString();
                if (_warehouseLocationList.Count > 0)
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
                return _warehouseLocationList;
            }
            set
            {
                _warehouseLocationList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationList);
            }
        }
        //this is used to collect previous values as to compare the changed values...
        private WarehouseLocation _selectedWarehouseLocationMirror;
        public WarehouseLocation SelectedWarehouseLocationMirror
        {
            get { return _selectedWarehouseLocationMirror; }
            set { _selectedWarehouseLocationMirror = value; }
        }

        private System.Collections.IList _selectedWarehouseLocationList;
        public System.Collections.IList SelectedWarehouseLocationList
        {
            get { return _selectedWarehouseLocationList; }
            set
            {
                if (_selectedWarehouseLocation != value)
                {
                    _selectedWarehouseLocationList = value;
                    NotifyPropertyChanged(m => m.SelectedWarehouseLocationList);
                }
            }
        }

        private WarehouseLocation _selectedWarehouseLocation;
        public WarehouseLocation SelectedWarehouseLocation
        {
            get
            {
                return _selectedWarehouseLocation;
            }
            set
            {
                if (_selectedWarehouseLocation != value)
                {
                    _selectedWarehouseLocation = value;
                    //set the mirrored SelectedWarehouseLocation to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedWarehouseLocationMirror = new WarehouseLocation();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedWarehouseLocation.GetType().GetProperties())
                        {
                            SelectedWarehouseLocationMirror.SetPropertyValue(prop.Name, SelectedWarehouseLocation.GetPropertyValue(prop.Name));
                        }
                        SelectedWarehouseLocationMirror.WarehouseLocationID = _selectedWarehouseLocation.WarehouseLocationID;
                        NotifyPropertyChanged(m => m.SelectedWarehouseLocation);

                        SelectedWarehouseLocation.PropertyChanged += new PropertyChangedEventHandler(SelectedWarehouseLocation_PropertyChanged);
                    }
                }
            }
        }
        #endregion CRUD Object Properties
        #endregion Properties

        #region ViewModel Property Events
        private void SelectedWarehouseLocation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {//these properties are not to be persisted we will igore them...
            if (e.PropertyName == "IsSelected" ||
                e.PropertyName == "IsExpanded" ||
                e.PropertyName == "IsValid" ||
                e.PropertyName == "NotValidMessage" ||
                e.PropertyName == "LastModifiedBy" ||
                e.PropertyName == "LastModifiedByDate" ||
                e.PropertyName == "PlantID")
            {//WarehouseID and PlantID or set from the WarehouseLocation selection...
                return;
            }
            //Key ID Logic...
            if (e.PropertyName == "WarehouseLocationID")
            {//make sure it is has changed...
                if (SelectedWarehouseLocationMirror.WarehouseLocationID != SelectedWarehouseLocation.WarehouseLocationID)
                {
                    //if their are no records it is a key change
                    if (WarehouseLocationList != null && WarehouseLocationList.Count == 0
                        && SelectedWarehouseLocation != null && !string.IsNullOrEmpty(SelectedWarehouseLocation.WarehouseLocationID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetWarehouseLocationState(SelectedWarehouseLocation);

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
            //2ndary key logic... when this key is selected we will populate it upstream keys...
            if (e.PropertyName == "WarehouseID" &&
                SelectedWarehouseLocationMirror.WarehouseID != SelectedWarehouseLocation.WarehouseID)
            {
                //look up WarehouseLocation to fetch its upstream properties...
                Warehouse item = WarehouseList.Where(q => q.WarehouseID == SelectedWarehouseLocation.WarehouseID).FirstOrDefault();
                SelectedWarehouseLocation.PlantID = item.PlantID;
            }

            object propertyChangedValue = SelectedWarehouseLocation.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedWarehouseLocationMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedWarehouseLocation.GetPropertyType(e.PropertyName);
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
                if (WarehouseLocationPropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedWarehouseLocation);
                    //set the mirrored objects field...
                    SelectedWarehouseLocationMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedWarehouseLocationMirror.IsValid = SelectedWarehouseLocation.IsValid;
                    SelectedWarehouseLocationMirror.IsExpanded = SelectedWarehouseLocation.IsExpanded;
                    SelectedWarehouseLocationMirror.NotValidMessage = SelectedWarehouseLocation.NotValidMessage;
                }
                else
                {
                    SelectedWarehouseLocation.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedWarehouseLocation.IsValid = SelectedWarehouseLocationMirror.IsValid;
                    SelectedWarehouseLocation.IsExpanded = SelectedWarehouseLocationMirror.IsExpanded;
                    SelectedWarehouseLocation.NotValidMessage = SelectedWarehouseLocationMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Property Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedWarehouseLocation.WarehouseLocationID))
            {//check to see if key is part of the current companylist...
                WarehouseLocation query = WarehouseLocationList.Where(company => company.WarehouseLocationID == SelectedWarehouseLocation.WarehouseLocationID &&
                                                        company.AutoID != SelectedWarehouseLocation.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back
                    SelectedWarehouseLocation.WarehouseLocationID = SelectedWarehouseLocationMirror.WarehouseLocationID;
                    //change to the newly selected company...
                    SelectedWarehouseLocation = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                WarehouseLocationList = GetWarehouseLocationByID(SelectedWarehouseLocation.WarehouseLocationID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (WarehouseLocationList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedWarehouseLocation.WarehouseLocationID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedWarehouseLocation = WarehouseLocationList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedWarehouseLocation.WarehouseLocationID != SelectedWarehouseLocationMirror.WarehouseLocationID)
                    SelectedWarehouseLocation.WarehouseLocationID = SelectedWarehouseLocationMirror.WarehouseLocationID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in WarehouseLocationList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedWarehouseLocation = new WarehouseLocation();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            WarehouseLocationList.Clear();
            SetAsEmptySelection();
        }

        private bool WarehouseLocationPropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "WarehouseLocationID":
                    rBool = WarehouseLocationIsValid(SelectedWarehouseLocation, _warehouseLocationValidationProperties.WarehouseLocationID, out errorMessage);
                    break;
                case "Name":
                    rBool = WarehouseLocationIsValid(SelectedWarehouseLocation, _warehouseLocationValidationProperties.Name, out errorMessage);
                    break;
                case "WarehouseID":
                    rBool = WarehouseLocationIsValid(SelectedWarehouseLocation, _warehouseLocationValidationProperties.WarehouseID, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedWarehouseLocation.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedWarehouseLocation.IsValid = WarehouseLocationIsValid(SelectedWarehouseLocation, out errorMessage);
                if (SelectedWarehouseLocation.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedWarehouseLocation.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _warehouseLocationValidationProperties
        {//we list all fields that require validation...
            WarehouseLocationID,
            Name,
            WarehouseID
        }

        //Object.Property Scope Validation...
        private bool WarehouseLocationIsValid(WarehouseLocation item, _warehouseLocationValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _warehouseLocationValidationProperties.WarehouseLocationID:
                    //validate key
                    if (string.IsNullOrEmpty(item.WarehouseLocationID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetWarehouseLocationState(item);
                    if (entityState == EntityStates.Added && WarehouseLocationExists(item.WarehouseLocationID, item.WarehouseLocationID, item.WarehouseID, item.PlantID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = WarehouseLocationList.Count(q => q.WarehouseLocationID == item.WarehouseLocationID);
                    if (count > 1)
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    break;
                case _warehouseLocationValidationProperties.Name:
                    //validate Description
                    if (string.IsNullOrEmpty(item.Name))
                    {
                        errorMessage = "Description Is Required.";
                        return false;
                    }
                    break;
                case _warehouseLocationValidationProperties.WarehouseID:
                    //validate 2nd key
                    if (string.IsNullOrEmpty(item.WarehouseID))
                    {
                        errorMessage = "Warehouse Is Required.";
                        return false;
                    }
                    break;
            }
            return true;
        }
        //WarehouseLocation Object Scope Validation check the entire object for validity...
        private byte WarehouseLocationIsValid(WarehouseLocation item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.WarehouseLocationID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetWarehouseLocationState(item);
            if (entityState == EntityStates.Added && WarehouseLocationExists(item.WarehouseLocationID, item.WarehouseLocationID, item.WarehouseID, item.PlantID))
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = WarehouseLocationList.Count(q => q.WarehouseLocationID == item.WarehouseLocationID);
            if (count > 1)
            {
                errorMessage = "Item All Ready Exists...";
                return 1;
            }
            //validate Name
            if (string.IsNullOrEmpty(item.Name))
            {
                errorMessage = "Name Is Required.";
                return 1;
            }
            //validate WarehouseLocationID
            if (string.IsNullOrEmpty(item.WarehouseLocationID))
            {
                errorMessage = "Warehouse Location Is Required.";
                return 1;
            }
            //validate WarehouseID
            if (string.IsNullOrEmpty(item.WarehouseLocationID))
            {
                errorMessage = "Warehouse Is Required.";
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
        private ObservableCollection<WarehouseLocationType> BuildWarehouseLocationTypeDropDown()
        {
            List<WarehouseLocationType> list = new List<WarehouseLocationType>();
            list = _serviceAgent.GetWarehouseLocationTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new WarehouseLocationType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<WarehouseLocationType>(list);
        }

        private ObservableCollection<WarehouseLocationCode> BuildWarehouseLocationCodeDropDown()
        {
            List<WarehouseLocationCode> list = new List<WarehouseLocationCode>();
            list = _serviceAgent.GetWarehouseLocationCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new WarehouseLocationCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<WarehouseLocationCode>(list);
        }

        private ObservableCollection<Warehouse> BuildWarehouseDropDown()
        {
            List<Warehouse> list = new List<Warehouse>();
            list = _serviceAgent.GetWarehousesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new Warehouse());
            list.Sort((x, y) => string.Compare(x.Name, y.Name));

            return new ObservableCollection<Warehouse>(list);
        }
        #endregion DropDown Methods

        private EntityStates GetWarehouseLocationState(WarehouseLocation item)
        {
            return _serviceAgent.GetWarehouseLocationEntityState(item);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.WarehouseLocationRepositoryIsDirty();
        }

        #region WarehouseLocation CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedWarehouseLocation.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (WarehouseLocation item in WarehouseLocationList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if (autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                WarehouseLocationList = new BindingList<WarehouseLocation>(_serviceAgent.RefreshWarehouseLocation(autoIDs).ToList());
                SelectedWarehouseLocation = (from q in WarehouseLocationList
                                                where q.AutoID == selectedAutoID
                                                select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<WarehouseLocation> GetWarehouseLocations(string companyID)
        {
            BindingList<WarehouseLocation> itemList = new BindingList<WarehouseLocation>(_serviceAgent.GetWarehouseLocations(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<WarehouseLocation> GetWarehouseLocations(WarehouseLocation item, string companyID)
        {
            BindingList<WarehouseLocation> itemList = new BindingList<WarehouseLocation>(_serviceAgent.GetWarehouseLocations(item, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<WarehouseLocation> GetWarehouseLocationByID(string itemID, string companyID)
        {
            BindingList<WarehouseLocation> itemList = new BindingList<WarehouseLocation>(_serviceAgent.GetWarehouseLocationByID(itemID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private bool WarehouseLocationExists(string itemID, string warehouseLocationID, string warehouseID, string plantID)
        {
            return _serviceAgent.WarehouseLocationExists(itemID, ClientSessionSingleton.Instance.CompanyID, warehouseID, plantID);
        }
        //udpate merely updates the repository a commit is required to commit it to the db...
        private bool Update(WarehouseLocation item)
        {
            _serviceAgent.UpdateWarehouseLocationRepository(item);
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
            bool forceRefresh = false;
            var items = (from q in WarehouseLocationList where q.IsValid == 2 select q).ToList();
            foreach (WarehouseLocation item in items)
            {
                //if item is added then we will trigger a refresh to fetch the navigated properties...
                if (GetWarehouseLocationState(item) == EntityStates.Added)
                    forceRefresh = true;

                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitWarehouseLocationRepository();
            Dirty = false;
            AllowCommit = false;
            AllowKeyEntry = false;
            if (forceRefresh)
                Refresh();

            return true;
        }

        private bool Delete(WarehouseLocation item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromWarehouseLocationRepository(item);
            return true;
        }

        private bool NewWarehouseLocation(string itemID)
        {
            WarehouseLocation newItem = new WarehouseLocation();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newWarehouseLocationAutoId = _newWarehouseLocationAutoId - 1;
            newItem.AutoID = _newWarehouseLocationAutoId;
            newItem.WarehouseLocationID = itemID;
            newItem.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            newItem.IsValid = 1;
            newItem.NotValidMessage = "New Record Key Field/s Are Required.";
            WarehouseLocationList.Add(newItem);
            _serviceAgent.AddToWarehouseLocationRepository(newItem);
            SelectedWarehouseLocation = WarehouseLocationList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            AllowKeyEntry = true;
            return true;
        }
        #endregion WarehouseLocation CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                WarehouseLocationColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewWarehouseLocationCommand(""); //this will generate a new item and set it as the selected item...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedWarehouseLocation.SetPropertyValue(WarehouseLocationColumnMetaDataList[i].Name, columnValue);
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
            if (GetWarehouseLocationState(SelectedWarehouseLocation) != EntityStates.Detached)
            {
                if (Update(SelectedWarehouseLocation))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteWarehouseLocationCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedWarehouseLocationList.Count - 1; j >= 0; j--)
                {
                    WarehouseLocation item = (WarehouseLocation)SelectedWarehouseLocationList[j];
                    //get Max Index...
                    i = WarehouseLocationList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    WarehouseLocationList.Remove(item);
                }

                if (WarehouseLocationList != null && WarehouseLocationList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= WarehouseLocationList.Count())
                        ii = WarehouseLocationList.Count - 1;

                    SelectedWarehouseLocation = WarehouseLocationList[ii];
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
                NotifyMessage("WarehouseLocation/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewWarehouseLocationCommand()
        {
            NewWarehouseLocation("");
            AllowCommit = false;
        }

        public void NewWarehouseLocationCommand(string itemID)
        {
            NewWarehouseLocation(itemID);
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
            RegisterToReceiveMessages<BindingList<WarehouseLocation>>(MessageTokens.WarehouseLocationSearchToken.ToString(), OnSearchResult);
            NotifySearch("");
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseLocation>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                WarehouseLocationList = e.Data;
                SelectedWarehouseLocation = WarehouseLocationList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<WarehouseLocation>>(MessageTokens.WarehouseLocationSearchToken.ToString(), OnSearchResult);
        }

        #region Right Click FK searches
        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<WarehouseLocationType>>(MessageTokens.WarehouseLocationTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseLocationType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedWarehouseLocation.WarehouseLocationTypeID = e.Data.FirstOrDefault().WarehouseLocationTypeID;

            UnregisterToReceiveMessages<BindingList<WarehouseLocationType>>(MessageTokens.WarehouseLocationTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<WarehouseLocationCode>>(MessageTokens.WarehouseLocationCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseLocationCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedWarehouseLocation.WarehouseLocationCodeID = e.Data.FirstOrDefault().WarehouseLocationCodeID;

            UnregisterToReceiveMessages<BindingList<WarehouseLocationType>>(MessageTokens.WarehouseLocationTypeSearchToken.ToString(), OnTypeSearchResult);
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
                    SelectedWarehouseLocation.WarehouseLocationID = SelectedWarehouseLocationMirror.WarehouseLocationID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with WarehouseLocationID provided...
                    NewWarehouseLocationCommand(SelectedWarehouseLocation.WarehouseLocationID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseLocation.WarehouseLocationID = SelectedWarehouseLocationMirror.WarehouseLocationID;
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
                    _serviceAgent.CommitWarehouseLocationRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseLocation.WarehouseLocationID = SelectedWarehouseLocationMirror.WarehouseLocationID;
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
        public static object GetPropertyValue(this WarehouseLocation myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseLocation).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this WarehouseLocation myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseLocation).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this WarehouseLocation myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(WarehouseLocation).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}