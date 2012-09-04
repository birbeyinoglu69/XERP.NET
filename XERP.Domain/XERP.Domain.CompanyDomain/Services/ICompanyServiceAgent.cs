using System;
namespace XERP.Domain.CompanyDomain.Services
{
    public interface ICompanyServiceAgent
    {
        void AddToCompanyCodeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode companyCode);
        void AddToCompanyRepository(XERP.Domain.CompanyDomain.CompanyDataService.Company company);
        void AddToCompanyTypeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType companyType);
        void CommitCompanyCodeRepository();
        void CommitCompanyRepository();
        void CommitCompanyTypeRepository();
        bool CompanyCodeExists(string companyCodeID);
        bool CompanyExists(string companyID);
        bool CompanyTypeExists(string companyTypeID);
        void DeleteFromCompanyCodeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode companyCode);
        void DeleteFromCompanyRepository(XERP.Domain.CompanyDomain.CompanyDataService.Company company);
        void DeleteFromCompanyTypeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType companyType);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> GetCompanies();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> GetCompanies(XERP.Domain.CompanyDomain.CompanyDataService.Company companyQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> GetCompanyByID(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> GetCompanyCodeByID(string companyCodeID);
        System.Data.Services.Client.EntityStates GetCompanyCodeEntityState(XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode companyCode);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> GetCompanyCodes();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> GetCompanyCodes(XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode companyCodeQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> GetCompanyCodesReadOnly();
        System.Data.Services.Client.EntityStates GetCompanyEntityState(XERP.Domain.CompanyDomain.CompanyDataService.Company company);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypeByID(string companyTypeID);
        System.Data.Services.Client.EntityStates GetCompanyTypeEntityState(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType companyType);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypes();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypes(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType companyTypeQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypesReadOnly();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Temp> GetMetaData(string tableName);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> RefreshCompany(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> RefreshCompanyCode(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> RefreshCompanyType(string autoIDs);
        void UpdateCompanyCodeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode companyCode);
        void UpdateCompanyRepository(XERP.Domain.CompanyDomain.CompanyDataService.Company company);
        void UpdateCompanyTypeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType companyType);
    }
}
