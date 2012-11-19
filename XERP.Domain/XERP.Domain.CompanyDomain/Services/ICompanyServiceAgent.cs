using System;
namespace XERP.Domain.CompanyDomain.Services
{
    public interface ICompanyServiceAgent
    {
        void AddToCompanyCodeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode itemCode);
        void AddToCompanyRepository(XERP.Domain.CompanyDomain.CompanyDataService.Company item);
        void AddToCompanyTypeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType itemType);
        void CommitCompanyCodeRepository();
        void CommitCompanyRepository();
        void CommitCompanyTypeRepository();
        bool CompanyCodeExists(string itemCodeID);
        bool CompanyExists(string itemID);
        bool CompanyTypeExists(string itemTypeID);
        void DeleteFromCompanyCodeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode itemCode);
        void DeleteFromCompanyRepository(XERP.Domain.CompanyDomain.CompanyDataService.Company item);
        //void DeleteFromCompanyRepository(System.Collections.IList deletedRecords);
        void DeleteFromCompanyTypeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType itemType);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> GetCompanies();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> GetCompanies(XERP.Domain.CompanyDomain.CompanyDataService.Company itemQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> GetCompanyByID(string itemID);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> GetCompanyCodeByID(string itemCodeID);
        System.Data.Services.Client.EntityStates GetCompanyCodeEntityState(XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode itemCode);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> GetCompanyCodes();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> GetCompanyCodes(XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode itemCodeQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> GetCompanyCodesReadOnly();
        System.Data.Services.Client.EntityStates GetCompanyEntityState(XERP.Domain.CompanyDomain.CompanyDataService.Company item);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypeByID(string itemTypeID);
        System.Data.Services.Client.EntityStates GetCompanyTypeEntityState(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType itemType);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypes();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypes(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType itemTypeQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypesReadOnly();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Temp> GetMetaData(string tableName);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> RefreshCompany(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> RefreshCompanyCode(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> RefreshCompanyType(string autoIDs);
        void UpdateCompanyCodeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode itemCode);
        void UpdateCompanyRepository(XERP.Domain.CompanyDomain.CompanyDataService.Company item);
        void UpdateCompanyTypeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType itemType);
        bool CompanyRepositoryIsDirty();
        bool CompanyCodeRepositoryIsDirty();
        bool CompanyTypeRepositoryIsDirty();
    }
}
