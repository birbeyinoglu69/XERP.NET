using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
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
                    _instance = new CompanySingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private CompanyEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
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

        public IEnumerable<Company> GetCompanies(Company itemQuerryObject)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.Companies
                             select q;
            
            if  (!string.IsNullOrEmpty(itemQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(itemQuerryObject.Name.ToString())); 

            if (!string.IsNullOrEmpty(itemQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.CompanyTypeID))
                queryResult = queryResult.Where(q => q.CompanyTypeID.StartsWith(itemQuerryObject.CompanyTypeID.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.CompanyCodeID))
                queryResult = queryResult.Where(q => q.CompanyCodeID.StartsWith(itemQuerryObject.CompanyCodeID.ToString()));

            return queryResult;
        }

        public IEnumerable<Company> GetCompanyByID(string itemID)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.Companies
                          where q.CompanyID == itemID
                          select q);
            return queryResult;
        }

        public IEnumerable<Company> Refresh(string autoIDs)
        {

            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<Company>("RefreshCompany").AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(Company item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(Company item)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToCompanies(item);
        }

        public void DeleteFromRepository(Company item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {//if it exists in the db delete it from the db
                CompanyEntities context = new CompanyEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                Company deletedCompany = (from q in context.Companies
                                          where q.CompanyID == item.CompanyID
                                          select q).FirstOrDefault();
                if (deletedCompany != null)
                {
                    context.DeleteObject(deletedCompany);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetCompanyEntityState(item) != EntityStates.Detached)
                    _repositoryContext.Detach(item);
            }
        }

        public EntityStates GetCompanyEntityState(Company item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
                return _repositoryContext.GetEntityDescriptor(item).State;
            else
                return EntityStates.Detached;
        }   
    }
}
