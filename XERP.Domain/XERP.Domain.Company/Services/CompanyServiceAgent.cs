using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XERP.CompanyDomain.CompanyDataService;
using System.Data.Services.Client;
using System.Reflection;
//using System.Linq.Dynamic;
using System.Collections.ObjectModel;

namespace XERP.CompanyDomain.Services
{
    public class CompanyServiceAgent : XERP.CompanyDomain.Services.ICompanyServiceAgent 
    {
        public CompanyServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new CompanyEntities(_rootUri); 
        }

        private Uri _rootUri;
        private CompanyEntities _context;
        
        //private bool _repositoryHasChanges;
        //public bool RepositoryHasChanges
        //{
        //    get { return _repository.rep; }
        //}

        public IEnumerable<CompanyType> GetCompanyTypes()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from types in _context.CompanyTypes
                                select types);
            return queryResult;
        }

        public IEnumerable<CompanyCode> GetCompanyCodes()
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

        public IEnumerable<Company> GetCompanies()
        {
            return SingletonRepository.Instance.GetCompanies();
        }

        public IEnumerable<Company> GetCompanies(Company companyQuerryObject)
        {
            return SingletonRepository.Instance.GetCompanies(companyQuerryObject);
        }

        public IEnumerable<Company> GetCompanyByID(string companyID)
        {
            return SingletonRepository.Instance.GetCompanyByID(companyID);
        }

        public void CommitRepository()
        {
            SingletonRepository.Instance.CommitRepository();
        }

        public void UpdateRepository(Company company)
        {
            SingletonRepository.Instance.UpdateRepository(company);
        }

        public void AddToRepository(Company company)
        {
            SingletonRepository.Instance.AddToRepository(company);
        }

        public void DeleteFromRepository(Company company)
        {
            SingletonRepository.Instance.DeleteFromRepository(company);
        }

        public EntityStates GetCompanyEntityState(Company company)
        {
            return SingletonRepository.Instance.GetCompanyEntityState(company);
        }
    }
}
