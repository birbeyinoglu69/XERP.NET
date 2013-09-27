using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;

using XERP.Domain.WarehouseDomain.WarehouseDataService;
namespace XERP.Domain.WarehouseDomain.Services
{
    public class WarehouseServiceAgent : XERP.Domain.WarehouseDomain.Services.IWarehouseServiceAgent  
    {
        #region Initialize Service
        public WarehouseServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new WarehouseEntities(_rootUri);
            _context.MergeOption = MergeOption.NoTracking;   
        }
        #endregion Initialize Service

        #region Properties
        private Uri _rootUri;
        private WarehouseEntities _context;        
        #endregion Properties

        #region Read Only Methods  No Repository Required

        #region Warehouse Specific Read Only Methods
        public bool WarehouseRepositoryIsDirty()
        {
            return WarehouseSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool WarehouseTypeRepositoryIsDirty()
        {
            return WarehouseTypeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool WarehouseCodeRepositoryIsDirty()
        {
            return WarehouseCodeSingletonRepository.Instance.RepositoryIsDirty();
        } 

        public IEnumerable<WarehouseType> GetWarehouseTypesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseTypes
                               where q.CompanyID == companyID
                                select q);
            return queryResult;
        }

        public IEnumerable<WarehouseCode> GetWarehouseCodesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseCodes
                               where q.CompanyID == companyID
                                select q);
            return queryResult;
        }

        public bool WarehouseExists(string  itemID, string companyID, string plantID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Warehouses
                           where q.WarehouseID ==  itemID &&
                           q.CompanyID == companyID &&
                           q.PlantID == plantID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool WarehouseTypeExists(string  itemTypeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseTypes
                               where q.WarehouseTypeID ==  itemTypeID
                               where q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool WarehouseCodeExists(string  itemCodeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseCodes
                               where q.WarehouseCodeID ==  itemCodeID &&
                               q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }
        #endregion Warehouse Specific Read Only Methods

        #region WarehouseLocation Specific Read Only Methods
        public bool WarehouseLocationRepositoryIsDirty()
        {
            return WarehouseLocationSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool WarehouseLocationTypeRepositoryIsDirty()
        {
            return WarehouseLocationTypeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool WarehouseLocationCodeRepositoryIsDirty()
        {
            return WarehouseLocationCodeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public IEnumerable<WarehouseLocationType> GetWarehouseLocationTypesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationTypes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationCode> GetWarehouseLocationCodesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationCodes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public bool WarehouseLocationExists(string itemID, string companyID, string warehouseID, string plantID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocations
                               where q.WarehouseLocationID == itemID &&
                               q.CompanyID == companyID &&
                               q.WarehouseID == warehouseID &&
                               q.PlantID == plantID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool WarehouseLocationTypeExists(string itemTypeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationTypes
                               where q.WarehouseLocationTypeID == itemTypeID
                               where q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool WarehouseLocationCodeExists(string itemCodeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationCodes
                               where q.WarehouseLocationCodeID == itemCodeID &&
                               q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }
        #endregion WarehouseLocation Specific Read Only Methods

        #region WarehouseLocationBin Specific Read Only Methods
        public bool WarehouseLocationBinRepositoryIsDirty()
        {
            return WarehouseLocationBinSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool WarehouseLocationBinTypeRepositoryIsDirty()
        {
            return WarehouseLocationBinTypeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool WarehouseLocationBinCodeRepositoryIsDirty()
        {
            return WarehouseLocationBinCodeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public IEnumerable<WarehouseLocationBinType> GetWarehouseLocationBinTypesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationBinTypes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationBinCode> GetWarehouseLocationBinCodesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationBinCodes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public bool WarehouseLocationBinExists(string itemID, string companyID, string warehouseLocationID, string warehouseID, string plantID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationBins
                               where q.WarehouseLocationBinID == itemID &&
                               q.CompanyID == companyID &&
                               q.WarehouseLocationID == warehouseLocationID &&
                               q.WarehouseID == warehouseID &&
                               q.PlantID == plantID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool WarehouseLocationBinTypeExists(string itemTypeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationBinTypes
                               where q.WarehouseLocationBinTypeID == itemTypeID
                               where q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool WarehouseLocationBinCodeExists(string itemCodeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationBinCodes
                               where q.WarehouseLocationBinCodeID == itemCodeID &&
                               q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }
        #endregion WarehouseLocationBin Specific Read Only Methods

        #region Read Only General/Shared Methods
        #region Read Only WarehouseLocationBin Gets
        public IEnumerable<WarehouseLocationBin> GetWarehouseLocationBinsReadOnly(string companyID, string plantID, string warehouseID, string warehouseLocationID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationBins
                               where q.CompanyID == companyID &&
                                     q.PlantID == plantID &&
                                     q.WarehouseID == warehouseID &&
                                     q.WarehouseLocationID == warehouseLocationID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationBin> GetWarehouseLocationBinsReadOnly(string companyID, string plantID, string warehouseID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationBins
                               where q.CompanyID == companyID &&
                                     q.PlantID == plantID &&
                                     q.WarehouseID == warehouseID 
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationBin> GetWarehouseLocationBinsReadOnly(string companyID, string plantID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationBins
                               where q.CompanyID == companyID &&
                                     q.PlantID == plantID 
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationBin> GetWarehouseLocationBinsReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocationBins
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }
        #endregion Read Only WarehouseLocationBin Gets

        #region Read Only WarehouseLocation Gets
        public IEnumerable<WarehouseLocation> GetWarehouseLocationsReadOnly(string companyID, string plantID, string warehouseID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocations
                               where q.CompanyID == companyID &&
                                     q.PlantID == plantID &&
                                     q.WarehouseID == warehouseID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocation> GetWarehouseLocationsReadOnly(string companyID, string plantID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocations
                               where q.CompanyID == companyID &&
                                     q.PlantID == plantID 
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocation> GetWarehouseLocationsReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.WarehouseLocations
                               where q.CompanyID == companyID 
                               select q);
            return queryResult;
        }
        #endregion Read Only WarehouseLocation Gets

        #region Read Only Warehouse Gets
        public IEnumerable<Warehouse> GetWarehousesReadOnly(string companyID, string plantID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Warehouses
                               where q.CompanyID == companyID &&
                                     q.PlantID == plantID
                               select q);
            return queryResult;
        }
        public IEnumerable<Warehouse> GetWarehousesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Warehouses
                               where q.CompanyID == companyID 
                               select q);
            return queryResult;
        }
        #endregion Read Only Warehouse Gets
        public IEnumerable<Plant> GetPlantsReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Plants
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<Address> GetAddressesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Addresses
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }
        #endregion Read Only General/Shared Methods

        public IEnumerable<Temp> GetMetaData(string tableName)
        {
            //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<Temp>("GetMetaData").AddQueryOption("TableName", "'" + tableName + "'");
            return query;
        }

        #endregion Read Only Methods  No Repository Required

        #region Warehouse Specific CRUD
        #region Warehouse Repository CRUD
        public IEnumerable<Warehouse> RefreshWarehouse(string autoIDs)
        {
            return WarehouseSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<Warehouse> GetWarehouses(string companyID)
        {
            return WarehouseSingletonRepository.Instance.GetWarehouses(companyID);
        }

        public IEnumerable<Warehouse> GetWarehouses(Warehouse  itemQuerryObject, string companyID)
        {
            return WarehouseSingletonRepository.Instance.GetWarehouses( itemQuerryObject, companyID);
        }

        public IEnumerable<Warehouse> GetWarehouseByID(string  itemID, string companyID)
        {
            return WarehouseSingletonRepository.Instance.GetWarehouseByID( itemID, companyID);
        }

        public void CommitWarehouseRepository()
        {
            WarehouseSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateWarehouseRepository(Warehouse  item)
        {
            WarehouseSingletonRepository.Instance.UpdateRepository( item);
        }

        public void AddToWarehouseRepository(Warehouse  item)
        {
            WarehouseSingletonRepository.Instance.AddToRepository( item);
        }

        public void DeleteFromWarehouseRepository(Warehouse  item)
        {
            WarehouseSingletonRepository.Instance.DeleteFromRepository( item);
        }

        public EntityStates GetWarehouseEntityState(Warehouse  item)
        {
            return WarehouseSingletonRepository.Instance.GetWarehouseEntityState( item);
        }
        #endregion Warehouse Repository CRUD

        #region WarehouseType Repository CRUD
        public IEnumerable<WarehouseType> RefreshWarehouseType(string autoIDs)
        {
            return WarehouseTypeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<WarehouseType> GetWarehouseTypes(string companyID)
        {
            return WarehouseTypeSingletonRepository.Instance.GetWarehouseTypes(companyID);
        }

        public IEnumerable<WarehouseType> GetWarehouseTypes(WarehouseType  itemTypeQuerryObject, string companyID)
        {
            return WarehouseTypeSingletonRepository.Instance.GetWarehouseTypes( itemTypeQuerryObject, companyID);
        }

        public IEnumerable<WarehouseType> GetWarehouseTypeByID(string  itemTypeID, string companyID)
        {
            return WarehouseTypeSingletonRepository.Instance.GetWarehouseTypeByID( itemTypeID, companyID);
        }
        public void CommitWarehouseTypeRepository()
        {
            WarehouseTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateWarehouseTypeRepository(WarehouseType  itemType)
        {
            WarehouseTypeSingletonRepository.Instance.UpdateRepository( itemType);
        }

        public void AddToWarehouseTypeRepository(WarehouseType  itemType)
        {
            WarehouseTypeSingletonRepository.Instance.AddToRepository( itemType);
        }

        public void DeleteFromWarehouseTypeRepository(WarehouseType  itemType)
        {
            WarehouseTypeSingletonRepository.Instance.DeleteFromRepository( itemType);
        }

        public EntityStates GetWarehouseTypeEntityState(WarehouseType  itemType)
        {
            return WarehouseTypeSingletonRepository.Instance.GetWarehouseTypeEntityState( itemType);
        }

        #endregion WarehouseType Repository CRUD

        #region WarehouseCode Repository CRUD
        public IEnumerable<WarehouseCode> RefreshWarehouseCode(string autoIDs)
        {
            return WarehouseCodeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<WarehouseCode> GetWarehouseCodes(string companyID)
        {
            return WarehouseCodeSingletonRepository.Instance.GetWarehouseCodes(companyID);
        }

        public IEnumerable<WarehouseCode> GetWarehouseCodes(WarehouseCode  itemCodeQuerryObject, string companyID)
        {
            return WarehouseCodeSingletonRepository.Instance.GetWarehouseCodes( itemCodeQuerryObject, companyID);
        }

        public IEnumerable<WarehouseCode> GetWarehouseCodeByID(string  itemCodeID, string companyID)
        {
            return WarehouseCodeSingletonRepository.Instance.GetWarehouseCodeByID( itemCodeID, companyID);
        }
        public void CommitWarehouseCodeRepository()
        {
            WarehouseCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateWarehouseCodeRepository(WarehouseCode  itemCode)
        {
            WarehouseCodeSingletonRepository.Instance.UpdateRepository( itemCode);
        }

        public void AddToWarehouseCodeRepository(WarehouseCode  itemCode)
        {
            WarehouseCodeSingletonRepository.Instance.AddToRepository( itemCode);
        }

        public void DeleteFromWarehouseCodeRepository(WarehouseCode  itemCode)
        {
            WarehouseCodeSingletonRepository.Instance.DeleteFromRepository( itemCode);
        }

        public EntityStates GetWarehouseCodeEntityState(WarehouseCode  itemCode)
        {
            return WarehouseCodeSingletonRepository.Instance.GetWarehouseCodeEntityState( itemCode);
        }

        #endregion WarehouseCode Repository CRUD
        #endregion Warehouse Specific CRUD

        #region WarehouseLocation Specific CRUD

        #region WarehouseLocation Repository CRUD
        public IEnumerable<WarehouseLocation> RefreshWarehouseLocation(string autoIDs)
        {
            return WarehouseLocationSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<WarehouseLocation> GetWarehouseLocations(string companyID)
        {
            return WarehouseLocationSingletonRepository.Instance.GetWarehouseLocations(companyID);
        }

        public IEnumerable<WarehouseLocation> GetWarehouseLocations(WarehouseLocation itemQuerryObject, string companyID)
        {
            return WarehouseLocationSingletonRepository.Instance.GetWarehouseLocations(itemQuerryObject, companyID);
        }

        public IEnumerable<WarehouseLocation> GetWarehouseLocationByID(string itemID, string companyID)
        {
            return WarehouseLocationSingletonRepository.Instance.GetWarehouseLocationByID(itemID, companyID);
        }

        public void CommitWarehouseLocationRepository()
        {
            WarehouseLocationSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateWarehouseLocationRepository(WarehouseLocation item)
        {
            WarehouseLocationSingletonRepository.Instance.UpdateRepository(item);
        }

        public void AddToWarehouseLocationRepository(WarehouseLocation item)
        {
            WarehouseLocationSingletonRepository.Instance.AddToRepository(item);
        }

        public void DeleteFromWarehouseLocationRepository(WarehouseLocation item)
        {
            WarehouseLocationSingletonRepository.Instance.DeleteFromRepository(item);
        }

        public EntityStates GetWarehouseLocationEntityState(WarehouseLocation item)
        {
            return WarehouseLocationSingletonRepository.Instance.GetWarehouseLocationEntityState(item);
        }
        #endregion WarehouseLocation Repository CRUD

        #region WarehouseLocationType Repository CRUD
        public IEnumerable<WarehouseLocationType> RefreshWarehouseLocationType(string autoIDs)
        {
            return WarehouseLocationTypeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<WarehouseLocationType> GetWarehouseLocationTypes(string companyID)
        {
            return WarehouseLocationTypeSingletonRepository.Instance.GetWarehouseLocationTypes(companyID);
        }

        public IEnumerable<WarehouseLocationType> GetWarehouseLocationTypes(WarehouseLocationType itemTypeQuerryObject, string companyID)
        {
            return WarehouseLocationTypeSingletonRepository.Instance.GetWarehouseLocationTypes(itemTypeQuerryObject, companyID);
        }

        public IEnumerable<WarehouseLocationType> GetWarehouseLocationTypeByID(string itemTypeID, string companyID)
        {
            return WarehouseLocationTypeSingletonRepository.Instance.GetWarehouseLocationTypeByID(itemTypeID, companyID);
        }
        public void CommitWarehouseLocationTypeRepository()
        {
            WarehouseLocationTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateWarehouseLocationTypeRepository(WarehouseLocationType itemType)
        {
            WarehouseLocationTypeSingletonRepository.Instance.UpdateRepository(itemType);
        }

        public void AddToWarehouseLocationTypeRepository(WarehouseLocationType itemType)
        {
            WarehouseLocationTypeSingletonRepository.Instance.AddToRepository(itemType);
        }

        public void DeleteFromWarehouseLocationTypeRepository(WarehouseLocationType itemType)
        {
            WarehouseLocationTypeSingletonRepository.Instance.DeleteFromRepository(itemType);
        }

        public EntityStates GetWarehouseLocationTypeEntityState(WarehouseLocationType itemType)
        {
            return WarehouseLocationTypeSingletonRepository.Instance.GetWarehouseLocationTypeEntityState(itemType);
        }

        #endregion WarehouseLocationType Repository CRUD

        #region WarehouseLocationCode Repository CRUD
        public IEnumerable<WarehouseLocationCode> RefreshWarehouseLocationCode(string autoIDs)
        {
            return WarehouseLocationCodeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<WarehouseLocationCode> GetWarehouseLocationCodes(string companyID)
        {
            return WarehouseLocationCodeSingletonRepository.Instance.GetWarehouseLocationCodes(companyID);
        }

        public IEnumerable<WarehouseLocationCode> GetWarehouseLocationCodes(WarehouseLocationCode itemCodeQuerryObject, string companyID)
        {
            return WarehouseLocationCodeSingletonRepository.Instance.GetWarehouseLocationCodes(itemCodeQuerryObject, companyID);
        }

        public IEnumerable<WarehouseLocationCode> GetWarehouseLocationCodeByID(string itemCodeID, string companyID)
        {
            return WarehouseLocationCodeSingletonRepository.Instance.GetWarehouseLocationCodeByID(itemCodeID, companyID);
        }
        public void CommitWarehouseLocationCodeRepository()
        {
            WarehouseLocationCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateWarehouseLocationCodeRepository(WarehouseLocationCode itemCode)
        {
            WarehouseLocationCodeSingletonRepository.Instance.UpdateRepository(itemCode);
        }

        public void AddToWarehouseLocationCodeRepository(WarehouseLocationCode itemCode)
        {
            WarehouseLocationCodeSingletonRepository.Instance.AddToRepository(itemCode);
        }

        public void DeleteFromWarehouseLocationCodeRepository(WarehouseLocationCode itemCode)
        {
            WarehouseLocationCodeSingletonRepository.Instance.DeleteFromRepository(itemCode);
        }

        public EntityStates GetWarehouseLocationCodeEntityState(WarehouseLocationCode itemCode)
        {
            return WarehouseLocationCodeSingletonRepository.Instance.GetWarehouseLocationCodeEntityState(itemCode);
        }

        #endregion WarehouseLocationCode Repository CRUD

        #endregion WarehouseLocation Specific CRUD

        #region WarehouseLocationBin Specific CRUD

        #region WarehouseLocationBin Repository CRUD
        public IEnumerable<WarehouseLocationBin> RefreshWarehouseLocationBin(string autoIDs)
        {
            return WarehouseLocationBinSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<WarehouseLocationBin> GetWarehouseLocationBins(string companyID)
        {
            return WarehouseLocationBinSingletonRepository.Instance.GetWarehouseLocationBins(companyID);
        }

        public IEnumerable<WarehouseLocationBin> GetWarehouseLocationBins(WarehouseLocationBin itemQuerryObject, string companyID)
        {
            return WarehouseLocationBinSingletonRepository.Instance.GetWarehouseLocationBins(itemQuerryObject, companyID);
        }

        public IEnumerable<WarehouseLocationBin> GetWarehouseLocationBinByID(string itemID, string companyID)
        {
            return WarehouseLocationBinSingletonRepository.Instance.GetWarehouseLocationBinByID(itemID, companyID);
        }

        public void CommitWarehouseLocationBinRepository()
        {
            WarehouseLocationBinSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateWarehouseLocationBinRepository(WarehouseLocationBin item)
        {
            WarehouseLocationBinSingletonRepository.Instance.UpdateRepository(item);
        }

        public void AddToWarehouseLocationBinRepository(WarehouseLocationBin item)
        {
            WarehouseLocationBinSingletonRepository.Instance.AddToRepository(item);
        }

        public void DeleteFromWarehouseLocationBinRepository(WarehouseLocationBin item)
        {
            WarehouseLocationBinSingletonRepository.Instance.DeleteFromRepository(item);
        }

        public EntityStates GetWarehouseLocationBinEntityState(WarehouseLocationBin item)
        {
            return WarehouseLocationBinSingletonRepository.Instance.GetWarehouseLocationBinEntityState(item);
        }
        #endregion WarehouseLocationBin Repository CRUD

        #region WarehouseLocationBinType Repository CRUD
        public IEnumerable<WarehouseLocationBinType> RefreshWarehouseLocationBinType(string autoIDs)
        {
            return WarehouseLocationBinTypeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<WarehouseLocationBinType> GetWarehouseLocationBinTypes(string companyID)
        {
            return WarehouseLocationBinTypeSingletonRepository.Instance.GetWarehouseLocationBinTypes(companyID);
        }

        public IEnumerable<WarehouseLocationBinType> GetWarehouseLocationBinTypes(WarehouseLocationBinType itemTypeQuerryObject, string companyID)
        {
            return WarehouseLocationBinTypeSingletonRepository.Instance.GetWarehouseLocationBinTypes(itemTypeQuerryObject, companyID);
        }

        public IEnumerable<WarehouseLocationBinType> GetWarehouseLocationBinTypeByID(string itemTypeID, string companyID)
        {
            return WarehouseLocationBinTypeSingletonRepository.Instance.GetWarehouseLocationBinTypeByID(itemTypeID, companyID);
        }
        public void CommitWarehouseLocationBinTypeRepository()
        {
            WarehouseLocationBinTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateWarehouseLocationBinTypeRepository(WarehouseLocationBinType itemType)
        {
            WarehouseLocationBinTypeSingletonRepository.Instance.UpdateRepository(itemType);
        }

        public void AddToWarehouseLocationBinTypeRepository(WarehouseLocationBinType itemType)
        {
            WarehouseLocationBinTypeSingletonRepository.Instance.AddToRepository(itemType);
        }

        public void DeleteFromWarehouseLocationBinTypeRepository(WarehouseLocationBinType itemType)
        {
            WarehouseLocationBinTypeSingletonRepository.Instance.DeleteFromRepository(itemType);
        }

        public EntityStates GetWarehouseLocationBinTypeEntityState(WarehouseLocationBinType itemType)
        {
            return WarehouseLocationBinTypeSingletonRepository.Instance.GetWarehouseLocationBinTypeEntityState(itemType);
        }

        #endregion WarehouseLocationBinType Repository CRUD

        #region WarehouseLocationBinCode Repository CRUD
        public IEnumerable<WarehouseLocationBinCode> RefreshWarehouseLocationBinCode(string autoIDs)
        {
            return WarehouseLocationBinCodeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<WarehouseLocationBinCode> GetWarehouseLocationBinCodes(string companyID)
        {
            return WarehouseLocationBinCodeSingletonRepository.Instance.GetWarehouseLocationBinCodes(companyID);
        }

        public IEnumerable<WarehouseLocationBinCode> GetWarehouseLocationBinCodes(WarehouseLocationBinCode itemCodeQuerryObject, string companyID)
        {
            return WarehouseLocationBinCodeSingletonRepository.Instance.GetWarehouseLocationBinCodes(itemCodeQuerryObject, companyID);
        }

        public IEnumerable<WarehouseLocationBinCode> GetWarehouseLocationBinCodeByID(string itemCodeID, string companyID)
        {
            return WarehouseLocationBinCodeSingletonRepository.Instance.GetWarehouseLocationBinCodeByID(itemCodeID, companyID);
        }
        public void CommitWarehouseLocationBinCodeRepository()
        {
            WarehouseLocationBinCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateWarehouseLocationBinCodeRepository(WarehouseLocationBinCode itemCode)
        {
            WarehouseLocationBinCodeSingletonRepository.Instance.UpdateRepository(itemCode);
        }

        public void AddToWarehouseLocationBinCodeRepository(WarehouseLocationBinCode itemCode)
        {
            WarehouseLocationBinCodeSingletonRepository.Instance.AddToRepository(itemCode);
        }

        public void DeleteFromWarehouseLocationBinCodeRepository(WarehouseLocationBinCode itemCode)
        {
            WarehouseLocationBinCodeSingletonRepository.Instance.DeleteFromRepository(itemCode);
        }

        public EntityStates GetWarehouseLocationBinCodeEntityState(WarehouseLocationBinCode itemCode)
        {
            return WarehouseLocationBinCodeSingletonRepository.Instance.GetWarehouseLocationBinCodeEntityState(itemCode);
        }

        #endregion WarehouseLocationBinCode Repository CRUD

        #endregion WarehouseLocationBin Specific CRUD
    }
}
