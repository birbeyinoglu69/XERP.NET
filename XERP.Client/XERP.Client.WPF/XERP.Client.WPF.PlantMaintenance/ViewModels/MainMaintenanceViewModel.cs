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
using XERP.Domain.PlantDomain.PlantDataService;
using XERP.Domain.PlantDomain.Services;

namespace XERP.Client.WPF.PlantMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newPlantAutoId;

        private IPlantServiceAgent _serviceAgent;
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
            PlantTypeList = BuildPlantTypeDropDown();
            PlantCodeList = BuildPlantCodeDropDown();
            AddressList = BuildAddressDropDown();
        }

        public MainMaintenanceViewModel(IPlantServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            PlantList = new BindingList<Plant>();
            //disable new row feature...
            PlantList.AllowNew = false;
            
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

        private string _plantListCount;
        public string PlantListCount
        {
            get { return _plantListCount; }
            set
            {
                _plantListCount = value;
                NotifyPropertyChanged(m => m.PlantListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region DropDown Collections
        private ObservableCollection<PlantType> _plantTypeList;
        public ObservableCollection<PlantType> PlantTypeList
        {
            get { return _plantTypeList; }
            set
            {
                _plantTypeList = value;
                NotifyPropertyChanged(m => m.PlantTypeList);
            }
        }

        private ObservableCollection<PlantCode> _plantCodeList;
        public ObservableCollection<PlantCode> PlantCodeList
        {
            get { return _plantCodeList; }
            set
            {
                _plantCodeList = value;
                NotifyPropertyChanged(m => m.PlantCodeList);
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
        private List<ColumnMetaData> _plantColumnMetaDataList;
        public List<ColumnMetaData> PlantColumnMetaDataList
        {
            get { return _plantColumnMetaDataList; }
            set
            {
                _plantColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.PlantColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _plantMaxFieldValueDictionary;
        public Dictionary<string, int> PlantMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_plantMaxFieldValueDictionary != null)
                    return _plantMaxFieldValueDictionary;

                _plantMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("Plants");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _plantMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _plantMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties

        #region CRUD Object Properties
        private BindingList<Plant> _plantList;
        public BindingList<Plant> PlantList
        {
            get
            {
                PlantListCount = _plantList.Count.ToString();
                if (_plantList.Count > 0)
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
                return _plantList;
            }
            set
            {
                _plantList = value;
                NotifyPropertyChanged(m => m.PlantList);
            }
        }
        //this is used to collect previous values as to compare the changed values...
        private Plant _selectedPlantMirror;
        public Plant SelectedPlantMirror
        {
            get { return _selectedPlantMirror; }
            set { _selectedPlantMirror = value; }
        }

        private System.Collections.IList _selectedPlantList;
        public System.Collections.IList SelectedPlantList
        {
            get { return _selectedPlantList; }
            set
            {
                if (_selectedPlant != value)
                {
                    _selectedPlantList = value;
                    NotifyPropertyChanged(m => m.SelectedPlantList);
                }  
            }
        }

        private Plant _selectedPlant;
        public Plant SelectedPlant
        {
            get 
            {
                return _selectedPlant; 
            }
            set
            {
                if (_selectedPlant != value)
                {
                    _selectedPlant = value;
                    //set the mirrored SelectedPlant to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedPlantMirror = new Plant();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedPlant.GetType().GetProperties())
                        {
                            SelectedPlantMirror.SetPropertyValue(prop.Name, SelectedPlant.GetPropertyValue(prop.Name));
                        }
                        SelectedPlantMirror.PlantID = _selectedPlant.PlantID;
                        NotifyPropertyChanged(m => m.SelectedPlant);
                        
                        SelectedPlant.PropertyChanged += new PropertyChangedEventHandler(SelectedPlant_PropertyChanged); 
                    }
                }
            }
        }
        #endregion CRUD Object Properties
        #endregion Properties

        #region ViewModel Property Events
        private void SelectedPlant_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "PlantID")
            {//make sure it is has changed...
                if (SelectedPlantMirror.PlantID != SelectedPlant.PlantID)
                {
                    //if their are no records it is a key change
                    if (PlantList != null && PlantList.Count == 0
                        && SelectedPlant != null && !string.IsNullOrEmpty(SelectedPlant.PlantID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetPlantState(SelectedPlant);

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
            
            object propertyChangedValue = SelectedPlant.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedPlantMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedPlant.GetPropertyType(e.PropertyName);
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
                if (PlantPropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedPlant);
                    //set the mirrored objects field...
                    SelectedPlantMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedPlantMirror.IsValid = SelectedPlant.IsValid;
                    SelectedPlantMirror.IsExpanded = SelectedPlant.IsExpanded;
                    SelectedPlantMirror.NotValidMessage = SelectedPlant.NotValidMessage;
                }
                else
                {
                    SelectedPlant.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedPlant.IsValid = SelectedPlantMirror.IsValid;
                    SelectedPlant.IsExpanded = SelectedPlantMirror.IsExpanded;
                    SelectedPlant.NotValidMessage = SelectedPlantMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Property Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedPlant.PlantID))
            {//check to see if key is part of the current companylist...
                Plant query = PlantList.Where(company => company.PlantID == SelectedPlant.PlantID &&
                                                        company.AutoID != SelectedPlant.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back
                    SelectedPlant.PlantID = SelectedPlantMirror.PlantID;
                    //change to the newly selected company...
                    SelectedPlant = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                PlantList = GetPlantByID(SelectedPlant.PlantID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (PlantList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedPlant.PlantID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedPlant = PlantList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedPlant.PlantID != SelectedPlantMirror.PlantID)
                    SelectedPlant.PlantID = SelectedPlantMirror.PlantID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in PlantList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedPlant = new Plant();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            PlantList.Clear();
            SetAsEmptySelection();
        }

        private bool PlantPropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "PlantID":
                    rBool = PlantIsValid(SelectedPlant, _plantValidationProperties.PlantID, out errorMessage);
                    break;
                case "Name":
                    rBool = PlantIsValid(SelectedPlant, _plantValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedPlant.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedPlant.IsValid = PlantIsValid(SelectedPlant, out errorMessage);
                if (SelectedPlant.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedPlant.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _plantValidationProperties
        {//we list all fields that require validation...
            PlantID,
            Name
        }

        //Object.Property Scope Validation...
        private bool PlantIsValid(Plant item, _plantValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _plantValidationProperties.PlantID:
                    //validate key
                    if (string.IsNullOrEmpty(item.PlantID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetPlantState(item);
                    if (entityState == EntityStates.Added && PlantExists(item.PlantID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = PlantList.Count(q => q.PlantID == item.PlantID);
                    if(count > 1)
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    break;
                case _plantValidationProperties.Name:
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
        //Plant Object Scope Validation check the entire object for validity...
        private byte PlantIsValid(Plant item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.PlantID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetPlantState(item);
            if (entityState == EntityStates.Added && PlantExists(item.PlantID))
            {
                errorMessage = "Item All Ready Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = PlantList.Count(q => q.PlantID == item.PlantID);
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
        private ObservableCollection<PlantType> BuildPlantTypeDropDown()
        {
            List<PlantType> list = new List<PlantType>();
            list = _serviceAgent.GetPlantTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new PlantType());
            list.Sort((x, y) => string.Compare(x.Type, y.Type));

            return new ObservableCollection<PlantType>(list);
        }

        private ObservableCollection<PlantCode> BuildPlantCodeDropDown()
        {
            List<PlantCode> list = new List<PlantCode>();
            list = _serviceAgent.GetPlantCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList();
            list.Add(new PlantCode());
            list.Sort((x, y) => string.Compare(x.Code, y.Code));

            return new ObservableCollection<PlantCode>(list);
        }

        private ObservableCollection<Address> BuildAddressDropDown()
        {
            List<Address> list = new List<Address>();
            list = _serviceAgent.GetAddressesReadOnly().ToList();
            list.Add(new Address());
            list.Sort((x, y) => string.Compare(x.Name, y.Name));

            return new ObservableCollection<Address>(list);
        }

        #endregion DropDown Methods

        private EntityStates GetPlantState(Plant item)
        {
            return _serviceAgent.GetPlantEntityState(item);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.PlantRepositoryIsDirty();
        }

        #region Plant CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedPlant.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (Plant item in PlantList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                PlantList = new BindingList<Plant>(_serviceAgent.RefreshPlant(autoIDs).ToList());
                SelectedPlant = (from q in PlantList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<Plant> GetPlants(string companyID)
        {
            BindingList<Plant> itemList = new BindingList<Plant>(_serviceAgent.GetPlants(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private BindingList<Plant> GetPlants(Plant item, string companyID)
        {
            BindingList<Plant> itemList = new BindingList<Plant>(_serviceAgent.GetPlants(item, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<Plant> GetPlantByID(string itemID, string companyID)
        {
            BindingList<Plant> itemList = new BindingList<Plant>(_serviceAgent.GetPlantByID(itemID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private bool PlantExists(string itemID)
        {
            return _serviceAgent.PlantExists(itemID, ClientSessionSingleton.Instance.CompanyID);
        }
        //udpate merely updates the repository a commit is required to commit it to the db...
        private bool Update(Plant item)
        {
            _serviceAgent.UpdatePlantRepository(item);
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
            var items = (from q in PlantList where q.IsValid == 2 select q).ToList();
            foreach (Plant item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitPlantRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }

        private bool Delete(Plant item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromPlantRepository(item);
            return true;
        }

        private bool NewPlant(string itemID)
        {
            Plant newItem = new Plant();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newPlantAutoId = _newPlantAutoId - 1;
            newItem.AutoID = _newPlantAutoId;
            newItem.PlantID = itemID;
            newItem.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            newItem.IsValid = 1;
            newItem.NotValidMessage = "New Record Key Field/s Are Required.";
            PlantList.Add(newItem);
            _serviceAgent.AddToPlantRepository(newItem);
            SelectedPlant = PlantList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }
        #endregion Plant CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        public void PasteRowCommand()
        {
            try
            {
                PlantColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
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
                    NewPlantCommand(""); //this will generate a new item and set it as the selected item...
                    //split row into cell values
                    string[] valuesInRow = row.Split(columnSplitter);
                    int i = 0;
                    foreach (string columnValue in valuesInRow)
                    {
                        SelectedPlant.SetPropertyValue(PlantColumnMetaDataList[i].Name, columnValue);
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
            if (GetPlantState(SelectedPlant) != EntityStates.Detached)
            {
                if (Update(SelectedPlant))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeletePlantCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedPlantList.Count - 1; j >= 0; j--)
                {
                    Plant item = (Plant)SelectedPlantList[j];
                    //get Max Index...
                    i = PlantList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    PlantList.Remove(item);
                }

                if (PlantList != null && PlantList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= PlantList.Count())
                        ii = PlantList.Count - 1;

                    SelectedPlant = PlantList[ii];
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
                NotifyMessage("Plant/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewPlantCommand()
        {
            NewPlant("");
            AllowCommit = false;
        }

        public void NewPlantCommand(string itemID)
        {
            NewPlant(itemID);
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
            RegisterToReceiveMessages<BindingList<Plant>>(MessageTokens.PlantSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<Plant>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                PlantList = e.Data;
                SelectedPlant = PlantList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<Plant>>(MessageTokens.PlantSearchToken.ToString(), OnSearchResult);
        }

        #region Right Click FK searches
        public void TypeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<PlantType>>(MessageTokens.PlantTypeSearchToken.ToString(), OnTypeSearchResult);
            NotifyTypeSearch("");
        }

        private void OnTypeSearchResult(object sender, NotificationEventArgs<BindingList<PlantType>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedPlant.PlantTypeID = e.Data.FirstOrDefault().PlantTypeID;

            UnregisterToReceiveMessages<BindingList<PlantType>>(MessageTokens.PlantTypeSearchToken.ToString(), OnTypeSearchResult);
        }

        public void CodeSearchCommand()
        {
            RegisterToReceiveMessages<BindingList<PlantCode>>(MessageTokens.PlantCodeSearchToken.ToString(), OnCodeSearchResult);
            NotifyCodeSearch("");
        }

        private void OnCodeSearchResult(object sender, NotificationEventArgs<BindingList<PlantCode>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
                SelectedPlant.PlantCodeID = e.Data.FirstOrDefault().PlantCodeID;

            UnregisterToReceiveMessages<BindingList<PlantType>>(MessageTokens.PlantTypeSearchToken.ToString(), OnTypeSearchResult);
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
                    SelectedPlant.PlantID = SelectedPlantMirror.PlantID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with PlantID provided...
                    NewPlantCommand(SelectedPlant.PlantID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedPlant.PlantID = SelectedPlantMirror.PlantID;
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
                    _serviceAgent.CommitPlantRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedPlant.PlantID = SelectedPlantMirror.PlantID;
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
        public static object GetPropertyValue(this Plant myObj, string propertyName)
        {
            var propInfo = typeof(Plant).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this Plant myObj, string propertyName)
        {
            var propInfo = typeof(Plant).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this Plant myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(Plant).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}