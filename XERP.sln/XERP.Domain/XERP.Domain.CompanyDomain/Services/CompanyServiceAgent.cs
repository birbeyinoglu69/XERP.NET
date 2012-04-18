using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using System.Reflection;
//using System.Linq.Dynamic;
using System.Collections.ObjectModel;
using XERP.Domain.CompanyDomain.CompanyDataService;
using XERP.Domain.CompanyDomain;

namespace XERP.Domain.CompanyDomain.Services
{
    public class CompanyServiceAgent : XERP.Domain.CompanyDomain.Services.ICompanyServiceAgent
    {
        #region Initialize Service
        public CompanyServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new CompanyEntities(_rootUri); 
        }
        #endregion Initialize Service

        #region Properties
        private Uri _rootUri;
        private CompanyEntities _context;
        #endregion Properties

        #region Read Only Methods  No Repository Required
        public IEnumerable<CompanyType> GetCompanyTypesReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from types in _context.CompanyTypes
                                select types);
            return queryResult;
        }

        public IEnumerable<CompanyCode> GetCompanyCodesReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from codes in _context.CompanyCodes
                                select codes);
            return queryResult;
        }

        public bool CompanyExists(string CompanyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Companies
                           where q.CompanyID == CompanyID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool CompanyTypeExists(string CompanyTypeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.CompanyTypes
                               where q.CompanyTypeID == CompanyTypeID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion Read Only Methods  No Repository Required

        #region Company Repository CRUD
        public IEnumerable<Company> GetCompanies()
        {
            return CompanySingletonRepository.Instance.GetCompanies();
        }

        public IEnumerable<Company> GetCompanies(Company companyQuerryObject)
        {
            return CompanySingletonRepository.Instance.GetCompanies(companyQuerryObject);
        }

        public IEnumerable<Company> GetCompanyByID(string companyID)
        {
            return CompanySingletonRepository.Instance.GetCompanyByID(companyID);
        }

        public void CommitCompanyRepository()
        {
            CompanySingletonRepository.Instance.CommitRepository();
        }

        public void UpdateCompanyRepository(Company company)
        {
            CompanySingletonRepository.Instance.UpdateRepository(company);
        }

        public void AddToCompanyRepository(Company company)
        {
            CompanySingletonRepository.Instance.AddToRepository(company);
        }

        public void DeleteFromCompanyRepository(Company company)
        {
            CompanySingletonRepository.Instance.DeleteFromRepository(company);
        }

        public EntityStates GetCompanyEntityState(Company company)
        {
            return CompanySingletonRepository.Instance.GetCompanyEntityState(company);
        }
        #endregion Company Repository CRUD

        #region CompanyType Repository CRUD
        public IEnumerable<CompanyType> GetCompanyTypes()
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypes();
        }

        public IEnumerable<CompanyType> GetCompanyTypes(CompanyType companyTypeQuerryObject)
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypes(companyTypeQuerryObject);
        }

        public IEnumerable<CompanyType> GetCompanyTypeByID(string companyTypeID)
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypeByID(companyTypeID);
        }
        public void CommitCompanyTypeRepository()
        {
            CompanyTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateCompanyTypeRepository(CompanyType companyType, out string UpdateError)
        {
            CompanyTypeSingletonRepository.Instance.UpdateRepository(companyType, out UpdateError);
        }

        public void AddToCompanyTypeRepository(CompanyType companyType)
        {
            CompanyTypeSingletonRepository.Instance.AddToRepository(companyType);
        }

        public void DeleteFromCompanyTypeRepository(CompanyType companyType)
        {
            CompanyTypeSingletonRepository.Instance.DeleteFromRepository(companyType);
        }

        public EntityStates GetCompanyTypeEntityState(CompanyType companyType)
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypeEntityState(companyType);
        }
        #endregion CompanyType Repository CRUD
    }
}
