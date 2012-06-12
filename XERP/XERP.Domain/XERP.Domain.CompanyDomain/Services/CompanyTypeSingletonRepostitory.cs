using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using XERP.Domain.CompanyDomain.CompanyDataService;
using XERP.Domain.CompanyDomain.Services;

namespace XERP.Domain.CompanyDomain
{
    public class CompanyTypeSingletonRepository
    {
        
        private CompanyTypeSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new CompanyEntities(_rootUri);
        }
        
        private static CompanyTypeSingletonRepository _instance;
        public static CompanyTypeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CompanyTypeSingletonRepository();
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

        public IEnumerable<CompanyType> GetCompanyTypes()
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.CompanyTypes
                               select q);
            return queryResult;
        }

        public IEnumerable<CompanyType> GetCompanyTypes(CompanyType companyTypeQuerryObject)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.CompanyTypes
                              select q;

            if (!string.IsNullOrEmpty(companyTypeQuerryObject.Type))
            {
                queryResult = queryResult.Where(q => q.Type.StartsWith(companyTypeQuerryObject.Type.ToString()));
            }

            if (!string.IsNullOrEmpty(companyTypeQuerryObject.Description))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(companyTypeQuerryObject.Description.ToString()));
            }

            if (!string.IsNullOrEmpty(companyTypeQuerryObject.CompanyTypeID))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(companyTypeQuerryObject.CompanyTypeID.ToString()));
            }

            return queryResult;
        }

        public IEnumerable<CompanyType> GetCompanyTypeByID(string companyTypeID)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.CompanyTypes
                               where q.CompanyTypeID == companyTypeID
                               select q);

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
            _contextHasChanges = false;
        }

        public void UpdateRepository(CompanyType companytype)
        {
            if (_repositoryContext.GetEntityDescriptor(companytype) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(companytype);
                _contextHasChanges = true;
            }
        }

        public void AddToRepository(CompanyType companyType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToCompanyTypes(companyType);
            _contextHasChanges = true;
        }

        public void DeleteFromRepository(CompanyType companyType)
        {
            if (_repositoryContext.GetEntityDescriptor(companyType) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.DeleteObject(companyType);
                _contextHasChanges = true;
            }

        }

        public EntityStates GetCompanyTypeEntityState(CompanyType companyType)
        {
            if (_repositoryContext.GetEntityDescriptor(companyType) != null)
            {
                return _repositoryContext.GetEntityDescriptor(companyType).State;
            }
            else
            {
                return EntityStates.Detached;
            }
        }   
    }

}
