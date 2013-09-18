using System;
namespace XERP.Domain.PlantDomain.Services
{
    public interface IPlantServiceAgent
    {
        void AddToPlantCodeRepository(XERP.Domain.PlantDomain.PlantDataService.PlantCode itemCode);
        void AddToPlantRepository(XERP.Domain.PlantDomain.PlantDataService.Plant item);
        void AddToPlantTypeRepository(XERP.Domain.PlantDomain.PlantDataService.PlantType itemType);
        void CommitPlantCodeRepository();
        void CommitPlantRepository();
        void CommitPlantTypeRepository();
        void DeleteFromPlantCodeRepository(XERP.Domain.PlantDomain.PlantDataService.PlantCode itemCode);
        void DeleteFromPlantRepository(XERP.Domain.PlantDomain.PlantDataService.Plant item);
        void DeleteFromPlantTypeRepository(XERP.Domain.PlantDomain.PlantDataService.PlantType itemType);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.Temp> GetMetaData(string tableName);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.Plant> GetPlantByID(string itemID, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.PlantCode> GetPlantCodeByID(string itemCodeID, string companyID);
        System.Data.Services.Client.EntityStates GetPlantCodeEntityState(XERP.Domain.PlantDomain.PlantDataService.PlantCode itemCode);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.PlantCode> GetPlantCodes(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.PlantCode> GetPlantCodes(XERP.Domain.PlantDomain.PlantDataService.PlantCode itemCodeQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.PlantCode> GetPlantCodesReadOnly(string companyID);
        System.Data.Services.Client.EntityStates GetPlantEntityState(XERP.Domain.PlantDomain.PlantDataService.Plant item);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.Plant> GetPlants(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.Plant> GetPlants(XERP.Domain.PlantDomain.PlantDataService.Plant itemQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.PlantType> GetPlantTypeByID(string itemTypeID, string companyID);
        System.Data.Services.Client.EntityStates GetPlantTypeEntityState(XERP.Domain.PlantDomain.PlantDataService.PlantType itemType);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.PlantType> GetPlantTypes(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.PlantType> GetPlantTypes(XERP.Domain.PlantDomain.PlantDataService.PlantType itemTypeQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.PlantType> GetPlantTypesReadOnly(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.Plant> RefreshPlant(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.PlantCode> RefreshPlantCode(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.PlantType> RefreshPlantType(string autoIDs);
        bool PlantCodeExists(string itemCodeID, string companyID);
        bool PlantCodeRepositoryIsDirty();
        bool PlantExists(string itemID, string companyID);
        bool PlantRepositoryIsDirty();
        bool PlantTypeExists(string itemTypeID, string companyID);
        bool PlantTypeRepositoryIsDirty();
        void UpdatePlantCodeRepository(XERP.Domain.PlantDomain.PlantDataService.PlantCode itemCode);
        void UpdatePlantRepository(XERP.Domain.PlantDomain.PlantDataService.Plant item);
        void UpdatePlantTypeRepository(XERP.Domain.PlantDomain.PlantDataService.PlantType itemType);
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.Address> GetAddressesReadOnly();
        System.Collections.Generic.IEnumerable<XERP.Domain.PlantDomain.PlantDataService.Plant> GetPlantsReadOnly();
    }
}
