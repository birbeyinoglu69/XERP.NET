using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using XERP.Domain.SystemUserDomain.SystemUserDataService;
using XERP.Domain.SystemUserDomain.Services;

namespace XERP.Domain.SystemUserDomain
{
    public class SystemUserTypeSingletonRepository
    {
        private SystemUserTypeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new SystemUserEntities(_rootUri);
        }

        private static SystemUserTypeSingletonRepository _instance;
        public static SystemUserTypeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SystemUserTypeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private SystemUserEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<SystemUserType> GetSystemUserTypes()
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SystemUserTypes
                               select q);
            return queryResult;
        }

        public IEnumerable<SystemUserType> GetSystemUserTypes(SystemUserType itemTypeQuerryObject)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SystemUserTypes
                              select q;
            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Type))
                queryResult = queryResult.Where(q => q.Type.StartsWith(itemTypeQuerryObject.Type.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.SystemUserTypeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.SystemUserTypeID.ToString()));

            return queryResult;
        }


        public IEnumerable<SystemUserType> GetSystemUserTypeByID(string itemTypeID)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SystemUserTypes
                               where q.SystemUserTypeID == itemTypeID
                               select q);
            return queryResult;
        }

        public IEnumerable<SystemUserType> Refresh(string autoIDs)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<SystemUserType>("RefreshSystemUserType").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(SystemUserType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                itemType.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemType.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemType);
            }
        }

        public void AddToRepository(SystemUserType itemType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSystemUserTypes(itemType);
        }

        public void DeleteFromRepository(SystemUserType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                //if it exists in the db delete it from the db
                SystemUserEntities context = new SystemUserEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SystemUserType deletedSystemUserType = (from q in context.SystemUserTypes
                                          where q.SystemUserTypeID == itemType.SystemUserTypeID
                                          select q).FirstOrDefault();
                if (deletedSystemUserType != null)
                {
                    context.DeleteObject(deletedSystemUserType);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetSystemUserTypeEntityState(itemType) != EntityStates.Detached)
                    _repositoryContext.Detach(itemType);
            }
        }

        public EntityStates GetSystemUserTypeEntityState(SystemUserType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
                return _repositoryContext.GetEntityDescriptor(itemType).State;
            else
                return EntityStates.Detached;
        }
    }
}
