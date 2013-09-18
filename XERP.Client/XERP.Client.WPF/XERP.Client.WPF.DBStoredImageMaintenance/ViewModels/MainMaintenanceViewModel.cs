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
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;
using System.IO;
using System.Windows.Media.Imaging;

namespace XERP.Client.WPF.DBStoredImageMaintenance.ViewModels
{
    public class MainMaintenanceViewModel : ViewModelBase<MainMaintenanceViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private int _newDBStoredImageAutoId;

        private IDBStoredImageServiceAgent _serviceAgent;
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

        public MainMaintenanceViewModel(IDBStoredImageServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            BuildDropDowns();

            SetAsEmptySelection();

            DBStoredImageList = new BindingList<DBStoredImage>();
            //disable new row feature...
            DBStoredImageList.AllowNew = false;
            
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

        private string _dBStoredImageListCount;
        public string DBStoredImageListCount
        {
            get { return _dBStoredImageListCount; }
            set
            {
                _dBStoredImageListCount = value;
                NotifyPropertyChanged(m => m.DBStoredImageListCount);
            }
        }
        #endregion General Form Function/State Properties

        #region CRUD Properties
        private System.Windows.Media.Imaging.BitmapImage _iconImage = new System.Windows.Media.Imaging.BitmapImage();
        public System.Windows.Media.Imaging.BitmapImage IconImage
        {
            get { return _iconImage; }
            set
            {
                _iconImage = value;
                NotifyPropertyChanged(m => m.IconImage);
            }
        }

        private BindingList<DBStoredImage> _dBStoredImageList;
        public BindingList<DBStoredImage> DBStoredImageList
        {
            get
            {
                DBStoredImageListCount = _dBStoredImageList.Count.ToString();
                if (_dBStoredImageList.Count > 0)
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
                return _dBStoredImageList;
            }
            set
            {
                _dBStoredImageList = value;
                NotifyPropertyChanged(m => m.DBStoredImageList);
            }
        }

        //this is used to collect previous values as to compare the changed values...
        private DBStoredImage _selectedDBStoredImageMirror;
        public DBStoredImage SelectedDBStoredImageMirror
        {
            get { return _selectedDBStoredImageMirror; }
            set { _selectedDBStoredImageMirror = value; }
        }

        private System.Collections.IList _selectedDBStoredImageList;
        public System.Collections.IList SelectedDBStoredImageList
        {
            get { return _selectedDBStoredImageList; }
            set
            {
                if (_selectedDBStoredImage != value)
                {
                    _selectedDBStoredImageList = value;
                    NotifyPropertyChanged(m => m.SelectedDBStoredImageList);
                }  
            }
        }

        private DBStoredImage _selectedDBStoredImage;
        public DBStoredImage SelectedDBStoredImage
        {
            get 
            {
                return _selectedDBStoredImage; 
            }
            set
            {
                if (_selectedDBStoredImage != value)
                {
                    _selectedDBStoredImage = value;
                    //set the mirrored SelectedDBStoredImage to allow to track property changes w/o
                    //explicitly providing a property for each field...
                    SelectedDBStoredImageMirror = new DBStoredImage();
                    if (value != null)
                    {//default the PreviousKeyID... 
                        foreach (var prop in SelectedDBStoredImage.GetType().GetProperties())
                        {
                            SelectedDBStoredImageMirror.SetPropertyValue(prop.Name, SelectedDBStoredImage.GetPropertyValue(prop.Name));
                        }
                        SelectedDBStoredImageMirror.ImageID = _selectedDBStoredImage.ImageID;
                        NotifyPropertyChanged(m => m.SelectedDBStoredImage);
                        
                        SelectedDBStoredImage.PropertyChanged += new PropertyChangedEventHandler(SelectedDBStoredImage_PropertyChanged); 
                    }
                    //reset Image Object...
                    IconImage = new System.Windows.Media.Imaging.BitmapImage();
                    //only try to display image if image exists in db...
                    if (value.StoredImage == null)
                        return;
                    
                    try
                    {
                        IconImage = GetMenuItemImage(value.ImageID, value.CompanyID);
                    }
                    catch(Exception e)
                    {//upon any errors return a new bitmap image as to not return a corrupted one and cause display errors...
                        IconImage = new System.Windows.Media.Imaging.BitmapImage(); ;
                    }
                }
            }
        }
        #endregion CRUD Properties

        #region Validation Properties
        private List<ColumnMetaData> _dBStoredImageColumnMetaDataList;
        public List<ColumnMetaData> DBStoredImageColumnMetaDataList
        {
            get { return _dBStoredImageColumnMetaDataList; }
            set 
            { 
                _dBStoredImageColumnMetaDataList = value;
                NotifyPropertyChanged(m => m.DBStoredImageColumnMetaDataList);
            }
        }

        //we use this dictionary to bind all textbox maxLenght properties in the View...
        private Dictionary<string, int> _dBStoredImageMaxFieldValueDictionary;
        public Dictionary<string, int> DBStoredImageMaxFieldValueDictionary //= new Dictionary<string, int>();
        {
            get
            {
                if (_dBStoredImageMaxFieldValueDictionary != null)
                    return _dBStoredImageMaxFieldValueDictionary;

                _dBStoredImageMaxFieldValueDictionary = new Dictionary<string, int>();
                var metaData = _serviceAgent.GetMetaData("DBStoredImages");

                foreach (var data in metaData)
                {
                    if (data.ShortChar_1 == "String")
                        _dBStoredImageMaxFieldValueDictionary.Add(data.Name.ToString(), (int)data.Int_1);
                }
                return _dBStoredImageMaxFieldValueDictionary;
            }
        }
        #endregion Validation Properties
        #endregion Properties

        #region ViewModel Propertie's Events
        private void SelectedDBStoredImage_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            if (e.PropertyName == "ImageID")
            {//make sure it is has changed...
                if (SelectedDBStoredImageMirror.ImageID != SelectedDBStoredImage.ImageID)
                {
                    //if their are no records it is a key change
                    if (DBStoredImageList != null && DBStoredImageList.Count == 0
                        && SelectedDBStoredImage != null && !string.IsNullOrEmpty(SelectedDBStoredImage.ImageID))
                    {
                        ChangeKeyLogic();
                        return;
                    }

                    EntityStates entityState = GetDBStoredImageState(SelectedDBStoredImage);

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
            
            object propertyChangedValue = SelectedDBStoredImage.GetPropertyValue(e.PropertyName);
            object prevPropertyValue = SelectedDBStoredImageMirror.GetPropertyValue(e.PropertyName);
            string propertyType = SelectedDBStoredImage.GetPropertyType(e.PropertyName);
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
                if (DBStoredImagePropertyChangeIsValid(e.PropertyName, propertyChangedValue, prevPropertyValue, propertyType))
                {
                    Update(SelectedDBStoredImage);
                    //set the mirrored objects field...
                    SelectedDBStoredImageMirror.SetPropertyValue(e.PropertyName, propertyChangedValue);
                    SelectedDBStoredImageMirror.IsValid = SelectedDBStoredImage.IsValid;
                    SelectedDBStoredImageMirror.IsExpanded = SelectedDBStoredImage.IsExpanded;
                    SelectedDBStoredImageMirror.NotValidMessage = SelectedDBStoredImage.NotValidMessage;
                }
                else
                {
                    SelectedDBStoredImage.SetPropertyValue(e.PropertyName, prevPropertyValue);
                    SelectedDBStoredImage.IsValid = SelectedDBStoredImageMirror.IsValid;
                    SelectedDBStoredImage.IsExpanded = SelectedDBStoredImageMirror.IsExpanded;
                    SelectedDBStoredImage.NotValidMessage = SelectedDBStoredImageMirror.NotValidMessage;
                }
            }
        }
        #endregion ViewModel Propertie's Events

        #region Methods
        #region ViewModel Logic Methods
        private void ChangeKeyLogic()
        {
            if (!string.IsNullOrEmpty(SelectedDBStoredImage.ImageID))
            {//check to see if key is part of the current companylist...
                DBStoredImage query = DBStoredImageList.Where(company => company.ImageID == SelectedDBStoredImage.ImageID &&
                                                        company.AutoID != SelectedDBStoredImage.AutoID).FirstOrDefault();
                if (query != null)
                {//revert it back
                    SelectedDBStoredImage.ImageID = SelectedDBStoredImageMirror.ImageID;
                    //change to the newly selected company...
                    SelectedDBStoredImage = query;
                    return;
                }
                //it is not part of the existing list try to fetch it from the db...
                DBStoredImageList = GetDBStoredImageByID(SelectedDBStoredImage.ImageID, XERP.Client.ClientSessionSingleton.Instance.CompanyID);
                if (DBStoredImageList.Count == 0)//it was not found do new record required logic...
                    NotifyNewRecordNeeded("Record " + SelectedDBStoredImage.ImageID + " Does Not Exist.  Create A New Record?");
                else
                    SelectedDBStoredImage = DBStoredImageList.FirstOrDefault();
            }
            else
            {
                string errorMessage = "ID Is Required.";
                NotifyMessage(errorMessage);
                //revert back to the value it was before it was changed...
                if (SelectedDBStoredImage.ImageID != SelectedDBStoredImageMirror.ImageID)
                    SelectedDBStoredImage.ImageID = SelectedDBStoredImageMirror.ImageID;
            }
        }
        //XERP allows for bulk updates we only allow save
        //if all bulk update requirements are met...
        private bool CommitIsAllowed()
        {//Check for any repository changes that are not yet committed to the db...
            Dirty = RepositoryIsDirty();
            //check for any invalid rows...
            int count = (from q in DBStoredImageList where q.IsValid == 1 select q).Count();
            if (count > 0)
                return false;
            return true;
        }

        private void SetAsEmptySelection()
        {
            SelectedDBStoredImage = new DBStoredImage();
            AllowEdit = false;
            AllowDelete = false;
            Dirty = false;
            AllowCommit = false;
            AllowRowCopy = false;
        }

        public void ClearLogic()
        {
            DBStoredImageList.Clear();
            SetAsEmptySelection();
        }

        private bool DBStoredImagePropertyChangeIsValid(string propertyName, object changedValue, object previousValue, string type)
        {
            string errorMessage = "";
            bool rBool = true;
            switch (propertyName)
            {
                case "ImageID":
                    rBool = DBStoredImageIsValid(SelectedDBStoredImage, _dBStoredImageValidationProperties.ImageID, out errorMessage);
                    break;
                case "Name":
                    rBool = DBStoredImageIsValid(SelectedDBStoredImage, _dBStoredImageValidationProperties.Name, out errorMessage);
                    break;
            }
            if (rBool == false)
            {//here we give a specific error to the specific change
                NotifyMessage(errorMessage);
                SelectedDBStoredImage.IsValid = 1;
            }
            else //check the enire rows validity...
            {//here we check the entire row for validity the property change may be valid
                //but we still do not know if the entire row is valid...
                //if the row is valid we will set it to 2 (pending changes...)
                //on the commit we will set it to 0 and it will be valid and saved to the db...
                SelectedDBStoredImage.IsValid = DBStoredImageIsValid(SelectedDBStoredImage, out errorMessage);
                if (SelectedDBStoredImage.IsValid == 2)
                    errorMessage = "Pending Changes...";
            }
            SelectedDBStoredImage.NotValidMessage = errorMessage;
            return rBool;
        }

        #region Validation Methods
        //XERP Validation is done by the entire object or by Object property...
        //So we must be sure to add the validation in both places...
        private enum _dBStoredImageValidationProperties
        {//we list all fields that require validation...
            ImageID,
            Name
        }

        //Object.Property Scope Validation...
        private bool DBStoredImageIsValid(DBStoredImage item, _dBStoredImageValidationProperties validationProperties, out string errorMessage)
        {
            errorMessage = "";
            switch (validationProperties)
            {
                case _dBStoredImageValidationProperties.ImageID:
                    //validate key
                    if (string.IsNullOrEmpty(item.ImageID))
                    {
                        errorMessage = "ID Is Required.";
                        return false;
                    }
                    EntityStates entityState = GetDBStoredImageState(item);
                    if (entityState == EntityStates.Added && DBStoredImageExists(item.ImageID))
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    //check cached list for duplicates...
                    int count = DBStoredImageList.Count(q => q.ImageID == item.ImageID);
                    if (count > 1)
                    {
                        errorMessage = "Item All Ready Exists...";
                        return false;
                    }
                    break;

                //case _dBStoredImageValidationProperties.Name:
                //    //validate Description
                //    if (string.IsNullOrEmpty(item.Name))
                //    {
                //        errorMessage = "Description Is Required.";
                //        return false;
                //    }
                //    break;
            }
            return true;
        }
        //DBStoredImage Object Scope Validation check the entire object for validity...
        private byte DBStoredImageIsValid(DBStoredImage item, out string errorMessage)
        {   //validate key
            errorMessage = "";
            if (string.IsNullOrEmpty(item.ImageID))
            {
                errorMessage = "ID Is Required.";
                return 1;
            }
            EntityStates entityState = GetDBStoredImageState(item);
            if (entityState == EntityStates.Added && DBStoredImageExists(item.ImageID))
            {
                errorMessage = "Item AllReady Exists.";
                return 1;
            }
            //check cached list for duplicates...
            int count = DBStoredImageList.Count(q => q.ImageID == item.ImageID);
            if (count > 1)
            {
                errorMessage = "Item AllReady Exists.";
                return 1;
            }
            ////validate Description
            //if (string.IsNullOrEmpty(item.Name))
            //{
            //    errorMessage = "Name Is Required.";
            //    return 1;
            //}
            ////a value of 2 is pending changes...
            ////On Commit we will give it a value of 0...
            return 2;
        }
        #endregion Validation Methods

        #endregion ViewModel Logic Methods

        #region ServiceAgent Call Methods
        private BitmapImage GetMenuItemImage(string imageID, string companyID)
        {
            return _serviceAgent.GetMenuItemImage(imageID, companyID);
        }

        private EntityStates GetDBStoredImageState(DBStoredImage item)
        {
            return _serviceAgent.GetDBStoredImageEntityState(item);
        }

        private bool RepositoryIsDirty()
        {
            return _serviceAgent.DBStoredImageRepositoryIsDirty();
        }

        #region DBStoredImage CRUD
        private void Refresh()
        {//refetch current records...
            long selectedAutoID = SelectedDBStoredImage.AutoID;
            string autoIDs = "";
            //bool isFirstItem = true;
            foreach (DBStoredImage item in DBStoredImageList)
            {//auto seeded starts at 1 any records at 0 or less or not valid records...
                if (item.AutoID > 0)
                    autoIDs = autoIDs + item.AutoID.ToString() + ",";
            }
            if(autoIDs.Length > 0)
            {
                //ditch the extra comma...
                autoIDs = autoIDs.Remove(autoIDs.Length - 1, 1);
                DBStoredImageList = new BindingList<DBStoredImage>(_serviceAgent.RefreshDBStoredImage(autoIDs).ToList());
                SelectedDBStoredImage = (from q in DBStoredImageList
                                   where q.AutoID == selectedAutoID
                                   select q).FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
        }

        private BindingList<DBStoredImage> GetDBStoredImages(string companyID)
        {
            BindingList<DBStoredImage> itemList = new BindingList<DBStoredImage>(_serviceAgent.GetDBStoredImages(companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private BindingList<DBStoredImage> GetDBStoredImages(DBStoredImage item, string companyID)
        {
            BindingList<DBStoredImage> itemList = new BindingList<DBStoredImage>(_serviceAgent.GetDBStoredImages(item, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList;
        }

        private BindingList<DBStoredImage> GetDBStoredImageByID(string itemID, string companyID)
        {
            BindingList<DBStoredImage> itemList = new BindingList<DBStoredImage>(_serviceAgent.GetDBStoredImageByID(itemID, companyID).ToList());
            Dirty = false;
            AllowCommit = false;
            return itemList; 
        }

        private bool DBStoredImageExists(string itemID)
        {
            return _serviceAgent.DBStoredImageExists(itemID, ClientSessionSingleton.Instance.CompanyID);
        }
        //udpate merely updates the repository a commit is required 
        //to commit it to the db...
        private bool Update(DBStoredImage item)
        {
            _serviceAgent.UpdateDBStoredImageRepository(item);
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
            var items = (from q in DBStoredImageList where q.IsValid == 2 select q).ToList();
            foreach (DBStoredImage item in items)
            {
                item.IsValid = 0;
                item.NotValidMessage = null;
            }
            _serviceAgent.CommitDBStoredImageRepository();
            Dirty = false;
            AllowCommit = false;
            return true;
        }


        private bool Delete(DBStoredImage item)
        {//deletes are done indenpendently of the repository as a delete will not commit 
            //dirty records it will simply just delete the record...
            _serviceAgent.DeleteFromDBStoredImageRepository(item);
            return true;
        }

        private bool NewDBStoredImage(string itemID)
        {
            DBStoredImage newItem = new DBStoredImage();
            //all new records will be give a negative int autoid...
            //when they are updated then sql will generate one for them overiding this set value...
            //it will allow us to give uniqueness to the tempory new records...
            //Before they are updated to the entity and given an autoid...
            //we use a negative number and keep subtracting by 1 for each new item added...
            //This will allow it to alwasy be unique and never interfere with SQL's positive autoid...
            _newDBStoredImageAutoId = _newDBStoredImageAutoId - 1;
            newItem.AutoID = _newDBStoredImageAutoId;
            newItem.ImageID = itemID;
            newItem.CompanyID = ClientSessionSingleton.Instance.CompanyID;
            newItem.IsValid = 1;
            newItem.NotValidMessage = "New Record Key Field/s Are Required.";
            DBStoredImageList.Add(newItem);
            _serviceAgent.AddToDBStoredImageRepository(newItem);
            SelectedDBStoredImage = DBStoredImageList.LastOrDefault();

            AllowEdit = true;
            Dirty = false;
            return true;
        }

        #endregion DBStoredImage CRUD
        #endregion ServiceAgent Call Methods
        #endregion Methods

        #region Commands
        //public void PasteRowCommand()
        //{
        //    try
        //    {
        //        DBStoredImageColumnMetaDataList.Sort(delegate(ColumnMetaData c1, ColumnMetaData c2)
        //        { return c1.Order.CompareTo(c2.Order); });

        //        char[] rowSplitter = { '\r', '\n' };
        //        char[] columnSplitter = { '\t' };
        //        //get the text from clipboard
        //        IDataObject dataInClipboard = Clipboard.GetDataObject();
        //        string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);
        //        //split it into rows...
        //        string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);

        //        foreach (string row in rowsInClipboard)
        //        {
        //            NewDBStoredImageCommand(""); //this will generate a new item and set it as the selected item...
        //            //split row into cell values
        //            string[] valuesInRow = row.Split(columnSplitter);
        //            int i = 0;
        //            foreach (string columnValue in valuesInRow)
        //            {
        //                SelectedDBStoredImage.SetPropertyValue(DBStoredImageColumnMetaDataList[i].Name, columnValue);
        //                i++;
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        NotifyMessage(ex.InnerException.ToString());
        //    }
        //}

        public void SaveCommand()
        {
            if (GetDBStoredImageState(SelectedDBStoredImage) != EntityStates.Detached)
            {
                if (Update(SelectedDBStoredImage))
                    Commit();
                else//if and where we have a hole in our allowcommit logic...
                    NotifyMessage("Save Failed Check Your Work And Try Again...");
            }
        }

        public void RefreshCommand()
        {
            Refresh();
        }

        public void DeleteDBStoredImageCommand()
        {
            try
            {
                int i = 0;
                int ii = 0;
                for (int j = SelectedDBStoredImageList.Count - 1; j >= 0; j--)
                {
                    DBStoredImage item = (DBStoredImage)SelectedDBStoredImageList[j];
                    //get Max Index...
                    i = DBStoredImageList.IndexOf(item);
                    if (i > ii)
                        ii = i;
                    Delete(item);
                    DBStoredImageList.Remove(item);
                }

                if (DBStoredImageList != null && DBStoredImageList.Count > 0)
                {
                    //back off one index from the max index...
                    ii = ii - 1;

                    //if they delete the first row...
                    if (ii < 0)
                        ii = 0;

                    //make sure it does not exceed the list count...
                    if (ii >= DBStoredImageList.Count())
                        ii = DBStoredImageList.Count - 1;

                    SelectedDBStoredImage = DBStoredImageList[ii];
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
                NotifyMessage("DBStoredImage/s Can Not Be Deleted.  Contact XERP Admin For More Details.");
                Refresh();
            }
        }

        public void NewDBStoredImageCommand()
        {
            NewDBStoredImage("");
            AllowCommit = false;
        }

        public void NewDBStoredImageCommand(string itemID)
        {
            NewDBStoredImage(itemID);
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
            RegisterToReceiveMessages<BindingList<DBStoredImage>>(MessageTokens.DBStoredImageSearchToken.ToString(), OnSearchResult);
            NotifySearch("");       
        }

        private void OnSearchResult(object sender, NotificationEventArgs<BindingList<DBStoredImage>> e)
        {
            if (e.Data != null && e.Data.Count > 0)
            {
                DBStoredImageList = e.Data;
                SelectedDBStoredImage = DBStoredImageList.FirstOrDefault();
                Dirty = false;
                AllowCommit = false;
            }
            UnregisterToReceiveMessages<BindingList<DBStoredImage>>(MessageTokens.DBStoredImageSearchToken.ToString(), OnSearchResult);
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
                    SelectedDBStoredImage.ImageID = SelectedDBStoredImageMirror.ImageID;
                    break;
                case MessageBoxResult.Yes:
                    //create new record with ImageID provided...
                    NewDBStoredImageCommand(SelectedDBStoredImage.ImageID);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedDBStoredImage.ImageID = SelectedDBStoredImageMirror.ImageID;
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
                    _serviceAgent.CommitDBStoredImageRepository();
                    CaseSaveResultActions(resultAction);
                    break;
                case MessageBoxResult.Cancel:
                    //revert back...
                    SelectedDBStoredImage.ImageID = SelectedDBStoredImageMirror.ImageID;
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
        public static object GetPropertyValue(this DBStoredImage myObj, string propertyName)
        {
            var propInfo = typeof(DBStoredImage).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.GetValue(myObj, null);
            else
                return string.Empty;
        }

        public static string GetPropertyType(this DBStoredImage myObj, string propertyName)
        {
            var propInfo = typeof(DBStoredImage).GetProperty(propertyName);
            if (propInfo != null)
                return propInfo.PropertyType.Name.ToString();
            else
                return null;
        }

        public static void SetPropertyValue(this DBStoredImage myObj, object propertyName, object propertyValue)
        {
            var propInfo = typeof(DBStoredImage).GetProperty((string)propertyName);
            if (propInfo != null)
                propInfo.SetValue(myObj, propertyValue, null);
        }
    }
}