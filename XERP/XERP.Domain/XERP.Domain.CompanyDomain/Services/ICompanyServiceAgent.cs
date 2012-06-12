using System;
namespace XERP.Domain.CompanyDomain.Services
{
    public interface ICompanyServiceAgent
    {
        void AddToCompanyRepository(XERP.Domain.CompanyDomain.CompanyDataService.Company company);
        void AddToCompanyTypeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType companyType);
        void CommitCompanyRepository();
        void CommitCompanyTypeRepository();
        bool CompanyExists(string companyID);
        bool CompanyTypeExists(string companyTypeID);
        void DeleteFromCompanyRepository(XERP.Domain.CompanyDomain.CompanyDataService.Company company);
        void DeleteFromCompanyTypeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType companyType);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> GetCompanies();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> GetCompanies(XERP.Domain.CompanyDomain.CompanyDataService.Company companyQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Company> GetCompanyByID(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyCode> GetCompanyCodesReadOnly();
        System.Data.Services.Client.EntityStates GetCompanyEntityState(XERP.Domain.CompanyDomain.CompanyDataService.Company company);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypeByID(string companyTypeID);
        System.Data.Services.Client.EntityStates GetCompanyTypeEntityState(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType companyType);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypes();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypes(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType companyTypeQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypesReadOnly();
        System.Collections.Generic.IEnumerable<XERP.Domain.CompanyDomain.CompanyDataService.Temp> GetMetaData(string tableName);
        void UpdateCompanyRepository(XERP.Domain.CompanyDomain.CompanyDataService.Company company);
        void UpdateCompanyTypeRepository(XERP.Domain.CompanyDomain.CompanyDataService.CompanyType companyType);
    }
}
