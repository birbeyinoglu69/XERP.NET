using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XERP.CompanyDomain.CompanyDataService;
using System.Data.Services.Client;
using XERP.CompanyDomain.CompanyDataService;

namespace XERP.CompanyDomain.Services
{
    public class SingletonRepository
    {
        
        private SingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new CompanyEntities(_rootUri);
        }
        
        private static SingletonRepository _instance;
        public static SingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SingletonRepository();
                }
                return _instance;
            }
        }

        private Uri _rootUri;
        private CompanyEntities _repositoryContext;

        private bool _contextHasChanges;
        public bool ContextHasChanges
        {
            get { return _contextHasChanges; }
        }

        public IEnumerable<Company> GetCompanies()
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.Companies
                             select q);
            return queryResult;
        }

        public IEnumerable<Company> GetCompanies(Company companyQuerryObject)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.Companies
                             select q;
            
            if  (!string.IsNullOrEmpty(companyQuerryObject.Name))
            {
                queryResult = queryResult.Where(x => x.Name.StartsWith(companyQuerryObject.Name.ToString())); 
            }

            if (!string.IsNullOrEmpty(companyQuerryObject.Description))
            {
                queryResult = queryResult.Where(x => x.Description.StartsWith(companyQuerryObject.Description.ToString()));
            }

            if (!string.IsNullOrEmpty(companyQuerryObject.CompanyTypeID))
            {
                queryResult = queryResult.Where(x => x.Description.StartsWith(companyQuerryObject.CompanyTypeID.ToString()));
            }

            if (!string.IsNullOrEmpty(companyQuerryObject.CompanyCodeID))
            {
                queryResult = queryResult.Where(x => x.Description.StartsWith(companyQuerryObject.CompanyCodeID.ToString()));
            }
            return queryResult;
        }

        public IEnumerable<Company> GetCompanyByID(string companyID)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from c in _repositoryContext.Companies
                          where c.CompanyID == companyID
                          select c);
            
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
            _contextHasChanges = false;
        }

        public void UpdateRepository(Company company)
        {
            if (_repositoryContext.GetEntityDescriptor(company) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(company);
                _contextHasChanges = true;
            }
        }

        public void AddToRepository(Company company)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToCompanies(company);
            _contextHasChanges = true;
        }

        public void DeleteFromRepository(Company company)
        {
            if (_repositoryContext.GetEntityDescriptor(company) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.DeleteObject(company);
                _contextHasChanges = true;
            }

        }

        public EntityStates GetCompanyEntityState(Company company)
        {
            if (_repositoryContext.GetEntityDescriptor(company) != null)
            {
                return _repositoryContext.GetEntityDescriptor(company).State;
            }
            else
            {
                return EntityStates.Detached;
            }
        }   
    }

}
