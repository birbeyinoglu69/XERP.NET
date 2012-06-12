using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using XERP.Domain.CompanyDomain.CompanyDataService;
using XERP.Domain.CompanyDomain.Services;

namespace XERP.Domain.CompanyDomain
{
    public class CompanySingletonRepository
    {
        private CompanySingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new CompanyEntities(_rootUri);
        }
        
        private static CompanySingletonRepository _instance;
        public static CompanySingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CompanySingletonRepository();
                }
                return _instance;
            }
        }

        private Uri _rootUri;
        private CompanyEntities _repositoryContext;

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
                queryResult = queryResult.Where(q => q.Name.StartsWith(companyQuerryObject.Name.ToString())); 
            }

            if (!string.IsNullOrEmpty(companyQuerryObject.Description))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(companyQuerryObject.Description.ToString()));
            }

            if (!string.IsNullOrEmpty(companyQuerryObject.CompanyTypeID))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(companyQuerryObject.CompanyTypeID.ToString()));
            }

            if (!string.IsNullOrEmpty(companyQuerryObject.CompanyCodeID))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(companyQuerryObject.CompanyCodeID.ToString()));
            }
            return queryResult;
        }

        public IEnumerable<Company> GetCompanyByID(string companyID)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.Companies
                          where q.CompanyID == companyID
                          select q);
            
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(Company company)
        {
            if (_repositoryContext.GetEntityDescriptor(company) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(company);
            }
        }

        public void AddToRepository(Company company)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToCompanies(company);
        }

        public void DeleteFromRepository(Company company)
        {
            if (_repositoryContext.GetEntityDescriptor(company) != null)
            {
                //if it exists in the db delete it from the db
                CompanyEntities context = new CompanyEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                Company deletedCompany = (from q in context.Companies
                                          where q.CompanyID == company.CompanyID
                                          select q).SingleOrDefault();
                if (deletedCompany != null)
                {
                    context.DeleteObject(deletedCompany);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetCompanyEntityState(company) != EntityStates.Detached)
                {
                    _repositoryContext.Detach(company);
                }
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
