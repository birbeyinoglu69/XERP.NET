using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
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
                    _instance = new CompanyTypeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private CompanyEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
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

        public IEnumerable<CompanyType> GetCompanyTypes(CompanyType itemTypeQuerryObject)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.CompanyTypes
                              select q;
            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Type))
                queryResult = queryResult.Where(q => q.Type.StartsWith(itemTypeQuerryObject.Type.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.CompanyTypeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.CompanyTypeID.ToString()));

            return queryResult;
        }


        public IEnumerable<CompanyType> GetCompanyTypeByID(string itemTypeID)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.CompanyTypes
                               where q.CompanyTypeID == itemTypeID
                               select q);
            return queryResult;
        }

        public IEnumerable<CompanyType> Refresh(string autoIDs)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<CompanyType>("RefreshCompanyType").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(CompanyType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                itemType.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemType.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemType);
            }
        }

        public void AddToRepository(CompanyType itemType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToCompanyTypes(itemType);
        }

        public void DeleteFromRepository(CompanyType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                //if it exists in the db delete it from the db
                CompanyEntities context = new CompanyEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                CompanyType deletedCompanyType = (from q in context.CompanyTypes
                                          where q.CompanyTypeID == itemType.CompanyTypeID
                                          select q).SingleOrDefault();
                if (deletedCompanyType != null)
                {
                    context.DeleteObject(deletedCompanyType);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetCompanyTypeEntityState(itemType) != EntityStates.Detached)
                    _repositoryContext.Detach(itemType);
            }
        }

        public EntityStates GetCompanyTypeEntityState(CompanyType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
                return _repositoryContext.GetEntityDescriptor(itemType).State;
            else
                return EntityStates.Detached;
        }
    }
}
