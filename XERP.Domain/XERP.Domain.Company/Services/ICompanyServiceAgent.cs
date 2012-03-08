using System;
namespace XERP.CompanyDomain.Services
{
    public interface ICompanyServiceAgent
    {
        void AddToRepository(XERP.CompanyDomain.CompanyDataService.Company company);
        void CommitRepository();
        bool CompanyExists(string CompanyID);
        void DeleteFromRepository(XERP.CompanyDomain.CompanyDataService.Company company);
        System.Collections.Generic.IEnumerable<XERP.CompanyDomain.CompanyDataService.Company> GetCompanies();
        System.Collections.Generic.IEnumerable<XERP.CompanyDomain.CompanyDataService.Company> GetCompanies(XERP.CompanyDomain.CompanyDataService.Company companyQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.CompanyDomain.CompanyDataService.Company> GetCompanyByID(string companyID);
        System.Collections.Generic.IEnumerable<XERP.CompanyDomain.CompanyDataService.CompanyCode> GetCompanyCodes();
        System.Data.Services.Client.EntityStates GetCompanyEntityState(XERP.CompanyDomain.CompanyDataService.Company company);
        System.Collections.Generic.IEnumerable<XERP.CompanyDomain.CompanyDataService.CompanyType> GetCompanyTypes();
        void UpdateRepository(XERP.CompanyDomain.CompanyDataService.Company company);
    }
}
