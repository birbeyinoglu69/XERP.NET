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

namespace XERP.Client.WPF.WarehouseLocationBinMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newWarehouseLocationBinAutoId;

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
            WarehouseLocationBinTypeList = BuildWarehouseLocationBinTypeDropDown();
            WarehouseLocationBinCodeList = BuildWarehouseLocationBinCodeDropDown();
            WarehouseLocationList = BuildWarehouseLocationDropDown();
        }

        public MainMaintenanceViewModel(IWarehouseServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            WarehouseLocationBinList = new BindingList<WarehouseLocationBin>();
            //disable new row feature...
            WarehouseLocationBinList.AllowNew = false;
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

        private string _warehouseLocationBinListCount;
        public string WarehouseLocationBinListCount
        {
            get { return _warehouseLocationBinListCount; }
            set
            {
                _warehouseLocationBinListCount = value;
                NotifyPropertyChanged(m => m.WarehouseLocationBinListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region DropDown Collections
        private ObservableCollection<WarehouseLocationBinType> _warehouseLocationBinTypeList;
        public ObservableCollection<WarehouseLocationBinType> WarehouseLocationBinTypeList
        {
            get { return _warehouseLocationBinTypeList; }
            set
            {
                _warehouseLocationBinTypeList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationBinTypeList);
            }
        }

        private ObservableCollection<WarehouseLocationBinCode> _warehouseLocationBinCodeList;
        public ObservableCollection<WarehouseLocationBinCode> WarehouseLocationBinCodeList
        {
            get { return _warehouseLocationBinCodeList; }
            set
            {
                _warehouseLocationBinCodeList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationBinCodeList);
            }
        }

        private ObservableCollection<WarehouseLocation> _warehouseLocationList;
        public ObservableCollection<WarehouseLocation> WarehouseLocationList
        {
            get { return _warehouseLocationList; }
            set
            {
                _warehouseLocationList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationList);
            }
        }
        #endregion DropDown Collections

        #region Validation Properties
        //meta data of the object is used to set max length...
        private List<ColumnMetaData> _warehouseLocationBinColumnMetaDataList;
        public List<ColumnMetaData> WarehouseLocationBinColumnMetaDataList
        {
            get { return _warehouseLocationBinColumnMetaDataList; }
            set
            {
                _warehouseLocationBinColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationBinColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _warehouseLocationBinMaxFieldValueDictionary;
        public Dictionary<string, int> WarehouseLocationBinMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_warehouseLocationBinMaxFieldValueDictionary != null)
                    return _warehouseLocationBinMaxFieldValueDictionary;

                _warehouseLocationBinMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("WarehouseLocationBins");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _warehouseLocationBinMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _warehouseLocationBinMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties

        #region CRUD Object Properties
        private BindingList<WarehouseLocationBin> _warehouseLocationBinList;
        public BindingList<WarehouseLocationBin> WarehouseLocationBinList
        {
            get
            {
                WarehouseLocationBinListCount = _warehouseLocationBinList.Count.ToString();
                if (_warehouseLocationBinList.Count > 0)
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
                return _warehouseLocationBinList;
            }
            set
            {
                _warehouseLocationBinList = value;
                NotifyPropertyChanged(m => m.WarehouseLocationBinList);
            }
        }
        //this is used to collect previous values as to compare the changed values...
        private WarehouseLocationBin _selectedWarehouseLocationBinMirror;
        public WarehouseLocationBin SelectedWarehouseLocationBinMirror
        {
            get { return _selectedWarehouseLocationBinMirror; }
            set { _selectedWarehouseLocationBinMirror = value; }
        }

        private System.Collections.IList _selectedWarehouseLocationBinList;
        public System.Collections.IList SelectedWarehouseLocationBinList
        {
            get { return _selectedWarehouseLocationBinList; }
            set
            {
                if (_selectedWarehouseLocationBin != value)
                {
                    _selectedWarehouseLocationBinList = value;
                    NotifyPropertyChanged(m => m.SelectedWarehouseLocationBinList);
                }  
            }
        }

        private WarehouseLocationBin _selectedWarehouseLocationBin;
        public WarehouseLocationBin SelectedWarehouseLocationBin
        {
            get 
            {
                return _selectedWarehouseLocationBin; 
            }
            set
            {
                if (_selectedWarehouseLocationBin != value)
                {
                    _selectedWarehouseLocationBin = value;
                    //set the mirrored SelectedWarehouseLocationBin to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedWarehouseLocationBinMirror = new WarehouseLocationBin();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedWarehouseLocationBin.GetType().GetProperties())
                        {
                            SelectedWarehouseLocationBinMirror.SetPropertyValue(prop.Name, SelectedWarehouseLocationBin.GetPropertyValue(prop.Name));
                        }
                        SelectedWarehouseLocationBinMirror.WarehouseLocationBinID = _selectedWarehouseLocationBin.WarehouseLocationBinID;
                        NotifyPropertyChanged(m => m.SelectedWarehouseLocationBin);
                        
                        SelectedWarehouseLocationBin.PropertyChanged += new PropertyChangedEventHandler(SelectedWarehouseLocationBin_PropertyChanged); 
                    }
                }
            }
        }
        #endregion CRUD Object Properties
        #endregion Properties

        #region ViewModel Property Events
        private void SelectedWarehouseLocationBin_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {//these properties are not to be persisted we will igore them...
            if (e.PropertyName == "IsSelected" ||
                e.PropertyName == "IsExpanded" ||
                e.PropertyName == "IsValid" ||
                e.PropertyName == "NotValidMessage" ||
                e.PropertyName == "LastModifiedBy" ||
                e.PropertyName == "LastModifiedByDate" ||
                e.PropertyName == "PlantID" ||
                e.PropertyName == "WarehouseID")
            {//WarehouseID and PlantID or set from the WarehouseLocation selection...
                return;
            }
            //Key ID Logic...
            if (e.PropertyName == "WarehouseLocationBinID")
            {//make sure it is has changed...
                if (SelectedWarehouseLocationBinMirror.WarehouseLocationBinID != SelectedWarehouseLocationBin.WarehouseLocationBinID)
                {
                    //if their are no records it is a key change
                    if (WarehouseLocationBinList != null && WarehouseLocationBinList.Count == 0
                        && SelectedWarehouseLocationBin != null && !string.IsNullOrEmpty(SelectedWarehouseLocationBin.WarehouseLocationBinID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetWarehouseLocationBinState(SelectedWarehouseLocationBin);

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
            //2ndary key logic... when this key is selected we will populate it upstream keys...
            if (e.PropertyName == "WarehouseLocationID"  &&
                SelectedWarehouseLocationBinMirror.WarehouseLocationID != SelectedWarehouseLocationBin.WarehouseLocationID)
            {
                //look up WarehouseLocation to fetch its upstream properties...
                WarehouseLocation item = WarehouseLocationList.Where(q => q.WarehouseLocationID == SelectedWarehouseLocationBin.WarehouseLocationID).FirstOrDefault();
                SelectedWarehouseLocationBin.WarehouseID = item.WarehouseID;
                SelectedWarehouseLocationBin.PlantID = item.PlantID;
            }
            
            object propertyChangedValue = SelectedWarehouseLocationBin.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedWarehouseLocationBinMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedWarehouseLocationBin.GetPropertyType(e.PropertyName);
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
                if (WarehouseLocationBinPropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedWarehouseLocationBin);
                    //set the mirrored objects field...
                    SelectedWarehouseLocationBinMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedWarehouseLocationBinMirror.IsValid = SelectedWarehouseLocationBin.IsValid;
                    SelectedWarehouseLocationBinMirror.IsExpanded = SelectedWarehouseLocationBin.IsExpanded;
                    SelectedWarehouseLocationBinMirror.NotValidMessage = SelectedWarehouseLocationBin.NotValidMessage;
                }
                else
                {
                    SelectedWarehouseLocationBin.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedWarehouseLocationBin.IsValid = SelectedWarehouseLocationBinMirror.IsValid;
                    SelectedWarehouseLocationBin.IsExpanded = SelectedWarehouseLocationBinMirror.IsExpanded;
                    SelectedWarehouseLocationBin.NotValidMessage = SelectedWarehouseLocationBinMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Property Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedWarehouseLocationBin.WarehouseLocationBinID))
            {//check to see if key is part of the current companylist...
                WarehouseLocationBin query = WarehouseLocationBinList.Where(company => company.WarehouseLocationBinID == SelectedWarehouseLocationBin.WarehouseLocationBinID &&
                                                        company.AutoID != SelectedWarehouseLocationBin.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back
                    SelectedWarehouseLocationBin.WarehouseLocationBinID = SelectedWarehouseLocationBinMirror.WarehouseLocationBinID;
                    //change to the newly selected company...
                    SelectedWarehouseLocationBin = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                WarehouseLocationBinList = GetWarehouseLocationBinByID(SelectedWarehouseLocationBin.WarehouseLocationBinID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (WarehouseLocationBinList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedWarehouseLocationBin.WarehouseLocationBinID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedWarehouseLocationBin = WarehouseLocationBinList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedWarehouseLocationBin.WarehouseLocationBinID != SelectedWarehouseLocationBinMirror.WarehouseLocationBinID)
                    SelectedWarehouseLocationBin.WarehouseLocationBinID = SelectedWarehouseLocationBinMirror.WarehouseLocationBinID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in WarehouseLocationBinList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedWarehouseLocationBin = new WarehouseLocationBin();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            WarehouseLocationBinList.Clear();
            SetAsEmptySelection();
        }

        private bool WarehouseLocationBinPropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "WarehouseLocationBinID":
                    rBool = WarehouseLocationBinIsValid(SelectedWarehouseLocationBin, _warehouseLocationBinValidationProperties.WarehouseLocationBinID, out errorMessage);
                    break;
                case "Name":
                    rBool = WarehouseLocationBinIsValid(SelectedWarehouseLocationBin, _warehouseLocationBinValidationProperties.Name, out errorMessage);
                    break;
                case "WarehouseLocationID":
                    rBool = WarehouseLocationBinIsValid(SelectedWarehouseLocationBin, _warehouseLocationBinValidationProperties.WarehouseLocationID, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedWarehouseLocationBin.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedWarehouseLocationBin.IsValid = WarehouseLocationBinIsValid(SelectedWarehouseLocationBin, out errorMessage);
                if (SelectedWarehouseLocationBin.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedWarehouseLocationBin.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _warehouseLocationBinValidationProperties
        {//we list all fields that require validation...
            WarehouseLocationBinID,
            Name,
            WarehouseLocationID
        }

        //Object.Property Scope Validation...
        private bool WarehouseLocationBinIsValid(WarehouseLocationBin item, _warehouseLocationBinValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _warehouseLocationBinValidationProperties.WarehouseLocationBinID:
                    //validate key
                    if (string.IsNullOrEmpty(item.WarehouseLocationBinID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetWarehouseLocationBinState(item);
                    if (entityState == EntityStates.Added && WarehouseLocationBinExists(item.WarehouseLocationBinID, item.WarehouseLocationID, item.WarehouseID, item.PlantID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = WarehouseLocationBinList.Count(q => q.WarehouseLocationBinID == item.WarehouseLocationBinID);
                    if(count > 1)
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    break;
                case _warehouseLocationBinValidationProperties.Name:
                    //validate Description
                    if (string.IsNullOrEmpty(item.Name))
                    {
                        errorMessage = "Description Is Required.";
                        return false;
                    }
                    break;
                case _warehouseLocationBinValidationProperties.WarehouseLocationID:
                    //validate 2ndary key...
                    if (string.IsNullOrEmpty(item.WarehouseLocationID))
                    {
                        errorMessage = "Warehouse Is Required.";
                        return false;
                    }
                    break;
            }
            return true;
        }
        //WarehouseLocationBin Object Scope Validation check the entire object for validity...
        private byte WarehouseLocationBinIsValid(WarehouseLocationBin item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.WarehouseLocationBinID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetWarehouseLocationBinState(item);
            if (entityState == EntityStates.Added && WarehouseLocationBinExists(item.WarehouseLocationBinID, item.WarehouseLocationID, item.WarehouseID, item.PlantID))
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = WarehouseLocationBinList.Count(q => q.WarehouseLocationBinID == item.WarehouseLocationBinID);
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
            //a value of 2 is pending changes...
            //On Commit we will give it a value of 0...
            return 2;
        }
        #endregion Validation Methods

        #endregion ViewModel Logic Methods

        #region ServiceAgent Call Methods
        #region DropDown Methods
        private ObservableCollection<WarehouseLocationBinType> BuildWarehouseLocationBinTypeDropDown()
        {
            List<WarehouseLocationBinType> list = new List<WarehouseLocationBinType>();
            list = _serviceAgent.GetWarehouseLocationBinTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new WarehouseLocationBinType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<WarehouseLocationBinType>(list);
        }

        private ObservableCollection<WarehouseLocationBinCode> BuildWarehouseLocationBinCodeDropDown()
        {
            List<WarehouseLocationBinCode> list = new List<WarehouseLocationBinCode>();
            list = _serviceAgent.GetWarehouseLocationBinCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new WarehouseLocationBinCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<WarehouseLocationBinCode>(list);
        }

        private ObservableCollection<WarehouseLocation> BuildWarehouseLocationDropDown()
        {
            List<WarehouseLocation> list = new List<WarehouseLocation>();
            list = _serviceAgent.GetWarehouseLocationsReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new WarehouseLocation());
            list.Sort((x, y) => string.Compare(x.Name, y.Name));

            return new ObservableCollection<WarehouseLocation>(list);
        }
        #endregion DropDown Methods

        private EntityStates GetWarehouseLocationBinState(WarehouseLocationBin item)
        {
            return _serviceAgent.GetWarehouseLocationBinEntityState(item);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.WarehouseLocationBinRepositoryIsDirty();
        }

        #region WarehouseLocationBin CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedWarehouseLocationBin.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (WarehouseLocationBin item in WarehouseLocationBinList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                WarehouseLocationBinList = new BindingList<WarehouseLocationBin>(_serviceAgent.RefreshWarehouseLocationBin(autoIDs).ToList());
                SelectedWarehouseLocationBin = (from q in WarehouseLocationBinList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<WarehouseLocationBin> GetWarehouseLocationBins(string companyID)
        {
            BindingList<WarehouseLocationBin> itemList = new BindingList<WarehouseLocationBin>(_serviceAgent.GetWarehouseLocationBins(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private BindingList<WarehouseLocationBin> GetWarehouseLocationBins(WarehouseLocationBin item, string companyID)
        {
            BindingList<WarehouseLocationBin> itemList = new BindingList<WarehouseLocationBin>(_serviceAgent.GetWarehouseLocationBins(item, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<WarehouseLocationBin> GetWarehouseLocationBinByID(string itemID, string companyID)
        {
            BindingList<WarehouseLocationBin> itemList = new BindingList<WarehouseLocationBin>(_serviceAgent.GetWarehouseLocationBinByID(itemID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private bool WarehouseLocationBinExists(string itemID, string warehouseLocationID, string warehouseID, string plantID)
        {
            return _serviceAgent.WarehouseLocationBinExists(itemID, ClientSessionSingleton.Instance.CompanyID, warehouseLocationID, warehouseID, plantID);
        }
        //udpate merely updates the repository a commit is required to commit it to the db...
        private bool Update(WarehouseLocationBin item)
        {
            _serviceAgent.UpdateWarehouseLocationBinRepository(item);
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
            var items = (from q in WarehouseLocationBinList where q.IsValid == 2 select q).ToList();
            foreach (WarehouseLocationBin item in items)
            {
                //if item is added then we will trigger a refresh to fetch the navigated properties...
                if (GetWarehouseLocationBinState(item) == EntityStates.Added)
                    forceRefresh = true;

                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitWarehouseLocationBinRepository();
            Dirty = false;
            AllowCommit = false;
            AllowKeyEntry = false;
            if (forceRefresh)
                Refresh();

            return true;
        }

        private bool Delete(WarehouseLocationBin item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromWarehouseLocationBinRepository(item);
            return true;
        }

        private bool NewWarehouseLocationBin(string itemID)
        {
            WarehouseLocationBin newItem = new WarehouseLocationBin();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newWarehouseLocationBinAutoId = _newWarehouseLocationBinAutoId - 1;
            newItem.AutoID = _newWarehouseLocationBinAutoId;
            newItem.WarehouseLocationBinID = itemID;
            newItem.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            newItem.IsValid = 1;
            newItem.NotValidMessage = "New Record Key Field/s Are Required.";
            WarehouseLocationBinList.Add(newItem);
            _serviceAgent.AddToWarehouseLocationBinRepository(newItem);
            SelectedWarehouseLocationBin = WarehouseLocationBinList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            AllowKeyEntry = true;
            return true;
        }
        #endregion WarehouseLocationBin CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                WarehouseLocationBinColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewWarehouseLocationBinCommand(""); //this will generate a new item and set it as the selected item...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedWarehouseLocationBin.SetPropertyValue(WarehouseLocationBinColumnMetaDataList[i].Name, columnValue);
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
            if (GetWarehouseLocationBinState(SelectedWarehouseLocationBin) != EntityStates.Detached)
            {
                if (Update(SelectedWarehouseLocationBin))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteWarehouseLocationBinCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedWarehouseLocationBinList.Count - 1; j >= 0; j--)
                {
                    WarehouseLocationBin item = (WarehouseLocationBin)SelectedWarehouseLocationBinList[j];
                    //get Max Index...
                    i = WarehouseLocationBinList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    WarehouseLocationBinList.Remove(item);
                }

                if (WarehouseLocationBinList != null && WarehouseLocationBinList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= WarehouseLocationBinList.Count())
                        ii = WarehouseLocationBinList.Count - 1;

                    SelectedWarehouseLocationBin = WarehouseLocationBinList[ii];
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
                NotifyMessage("WarehouseLocationBin/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewWarehouseLocationBinCommand()
        {
            NewWarehouseLocationBin("");
            AllowCommit = false;
        }

        public void NewWarehouseLocationBinCommand(string itemID)
        {
            NewWarehouseLocationBin(itemID);
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
            RegisterToReceiveMessages<BindingList<WarehouseLocationBin>>(MessageTokens.WarehouseLocationBinSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseLocationBin>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                WarehouseLocationBinList = e.Data;
                SelectedWarehouseLocationBin = WarehouseLocationBinList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<WarehouseLocationBin>>(MessageTokens.WarehouseLocationBinSearchToken.ToString(), OnSearchResult);
        }

        #region Right Click FK searches
        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<WarehouseLocationBinType>>(MessageTokens.WarehouseLocationBinTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseLocationBinType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedWarehouseLocationBin.WarehouseLocationBinTypeID = e.Data.FirstOrDefault().WarehouseLocationBinTypeID;

            UnregisterToReceiveMessages<BindingList<WarehouseLocationBinType>>(MessageTokens.WarehouseLocationBinTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<WarehouseLocationBinCode>>(MessageTokens.WarehouseLocationBinCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<WarehouseLocationBinCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedWarehouseLocationBin.WarehouseLocationBinCodeID = e.Data.FirstOrDefault().WarehouseLocationBinCodeID;

            UnregisterToReceiveMessages<BindingList<WarehouseLocationBinType>>(MessageTokens.WarehouseLocationBinTypeSearchToken.ToString(), OnTypeSearchResult);
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
                    SelectedWarehouseLocationBin.WarehouseLocationBinID = SelectedWarehouseLocationBinMirror.WarehouseLocationBinID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with WarehouseLocationBinID provided...
                    NewWarehouseLocationBinCommand(SelectedWarehouseLocationBin.WarehouseLocationBinID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseLocationBin.WarehouseLocationBinID = SelectedWarehouseLocationBinMirror.WarehouseLocationBinID;
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
                    _serviceAgent.CommitWarehouseLocationBinRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedWarehouseLocationBin.WarehouseLocationBinID = SelectedWarehouseLocationBinMirror.WarehouseLocationBinID;
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
        public static object GetPropertyValue(this WarehouseLocationBin myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseLocationBin).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this WarehouseLocationBin myObj, string propertyName)
        {
            var propInfo = typeof(WarehouseLocationBin).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this WarehouseLocationBin myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(WarehouseLocationBin).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}