using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;

using XERP.Domain.PlantDomain.PlantDataService;
namespace XERP.Domain.PlantDomain.Services
{
    public class PlantServiceAgent : XERP.Domain.PlantDomain.Services.IPlantServiceAgent
    {
        #region Initialize Service
        public PlantServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new PlantEntities(_rootUri);
            _context.MergeOption = MergeOption.NoTracking;   
        }
        #endregion Initialize Service

        #region Properties
        private Uri _rootUri;
        private PlantEntities _context;        
        #endregion Properties

        #region Read Only Methods  No Repository Required
        public bool PlantRepositoryIsDirty()
        {
            return PlantSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool PlantTypeRepositoryIsDirty()
        {
            return PlantTypeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool PlantCodeRepositoryIsDirty()
        {
            return PlantCodeSingletonRepository.Instance.RepositoryIsDirty();
        } 
        public IEnumerable<PlantType> GetPlantTypesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.PlantTypes
                               where q.CompanyID == companyID
                                select q);
            return queryResult;
        }

        public IEnumerable<PlantCode> GetPlantCodesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.PlantCodes
                               where q.CompanyID == companyID
                                select q);
            return queryResult;
        }

        public IEnumerable<Plant> GetPlantsReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from plants in _context.Plants
                               select plants);
            return queryResult;
        }

        public IEnumerable<Address> GetAddressesReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from addresses in _context.Addresses
                               select addresses);
            return queryResult;
        }

        public bool PlantExists(string  itemID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Plants
                           where q.PlantID ==  itemID &&
                           q.CompanyID == companyID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool PlantTypeExists(string  itemTypeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.PlantTypes
                               where q.PlantTypeID ==  itemTypeID
                               where q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool PlantCodeExists(string  itemCodeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.PlantCodes
                               where q.PlantCodeID ==  itemCodeID &&
                               q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

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

        #region Plant Repository CRUD
        public IEnumerable<Plant> RefreshPlant(string autoIDs)
        {
            return PlantSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<Plant> GetPlants(string companyID)
        {
            return PlantSingletonRepository.Instance.GetPlants(companyID);
        }

        public IEnumerable<Plant> GetPlants(Plant  itemQuerryObject, string companyID)
        {
            return PlantSingletonRepository.Instance.GetPlants( itemQuerryObject, companyID);
        }

        public IEnumerable<Plant> GetPlantByID(string  itemID, string companyID)
        {
            return PlantSingletonRepository.Instance.GetPlantByID( itemID, companyID);
        }

        public void CommitPlantRepository()
        {
            PlantSingletonRepository.Instance.CommitRepository();
        }

        public void UpdatePlantRepository(Plant  item)
        {
            PlantSingletonRepository.Instance.UpdateRepository( item);
        }

        public void AddToPlantRepository(Plant  item)
        {
            PlantSingletonRepository.Instance.AddToRepository( item);
        }

        public void DeleteFromPlantRepository(Plant  item)
        {
            PlantSingletonRepository.Instance.DeleteFromRepository( item);
        }

        public EntityStates GetPlantEntityState(Plant  item)
        {
            return PlantSingletonRepository.Instance.GetPlantEntityState( item);
        }
        #endregion Plant Repository CRUD

        #region PlantType Repository CRUD
        public IEnumerable<PlantType> RefreshPlantType(string autoIDs)
        {
            return PlantTypeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<PlantType> GetPlantTypes(string companyID)
        {
            return PlantTypeSingletonRepository.Instance.GetPlantTypes(companyID);
        }

        public IEnumerable<PlantType> GetPlantTypes(PlantType  itemTypeQuerryObject, string companyID)
        {
            return PlantTypeSingletonRepository.Instance.GetPlantTypes( itemTypeQuerryObject, companyID);
        }

        public IEnumerable<PlantType> GetPlantTypeByID(string  itemTypeID, string companyID)
        {
            return PlantTypeSingletonRepository.Instance.GetPlantTypeByID( itemTypeID, companyID);
        }
        public void CommitPlantTypeRepository()
        {
            PlantTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdatePlantTypeRepository(PlantType  itemType)
        {
            PlantTypeSingletonRepository.Instance.UpdateRepository( itemType);
        }

        public void AddToPlantTypeRepository(PlantType  itemType)
        {
            PlantTypeSingletonRepository.Instance.AddToRepository( itemType);
        }

        public void DeleteFromPlantTypeRepository(PlantType  itemType)
        {
            PlantTypeSingletonRepository.Instance.DeleteFromRepository( itemType);
        }

        public EntityStates GetPlantTypeEntityState(PlantType  itemType)
        {
            return PlantTypeSingletonRepository.Instance.GetPlantTypeEntityState( itemType);
        }

        #endregion PlantType Repository CRUD

        #region PlantCode Repository CRUD
        public IEnumerable<PlantCode> RefreshPlantCode(string autoIDs)
        {
            return PlantCodeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<PlantCode> GetPlantCodes(string companyID)
        {
            return PlantCodeSingletonRepository.Instance.GetPlantCodes(companyID);
        }

        public IEnumerable<PlantCode> GetPlantCodes(PlantCode  itemCodeQuerryObject, string companyID)
        {
            return PlantCodeSingletonRepository.Instance.GetPlantCodes( itemCodeQuerryObject, companyID);
        }

        public IEnumerable<PlantCode> GetPlantCodeByID(string  itemCodeID, string companyID)
        {
            return PlantCodeSingletonRepository.Instance.GetPlantCodeByID( itemCodeID, companyID);
        }
        public void CommitPlantCodeRepository()
        {
            PlantCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdatePlantCodeRepository(PlantCode  itemCode)
        {
            PlantCodeSingletonRepository.Instance.UpdateRepository( itemCode);
        }

        public void AddToPlantCodeRepository(PlantCode  itemCode)
        {
            PlantCodeSingletonRepository.Instance.AddToRepository( itemCode);
        }

        public void DeleteFromPlantCodeRepository(PlantCode  itemCode)
        {
            PlantCodeSingletonRepository.Instance.DeleteFromRepository( itemCode);
        }

        public EntityStates GetPlantCodeEntityState(PlantCode  itemCode)
        {
            return PlantCodeSingletonRepository.Instance.GetPlantCodeEntityState( itemCode);
        }

        #endregion PlantCode Repository CRUD
    }
}
