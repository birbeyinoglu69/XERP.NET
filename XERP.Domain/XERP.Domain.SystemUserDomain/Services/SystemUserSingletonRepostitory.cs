using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using XERP.Domain.SystemUserDomain.SystemUserDataService;
using XERP.Domain.SystemUserDomain.Services;

namespace XERP.Domain.SystemUserDomain
{
    public class SystemUserSingletonRepository
    {
        private SystemUserSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new SystemUserEntities(_rootUri);
        }
        
        private static SystemUserSingletonRepository _instance;
        public static SystemUserSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SystemUserSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private SystemUserEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<SystemUser> GetSystemUsers()
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SystemUsers
                             select q);
            return queryResult;
        }

        public IEnumerable<SystemUser> GetSystemUsers(SystemUser itemQuerryObject)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SystemUsers
                             select q;
            
            if  (!string.IsNullOrEmpty(itemQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(itemQuerryObject.Name.ToString())); 


            if (!string.IsNullOrEmpty(itemQuerryObject.SystemUserTypeID))
                queryResult = queryResult.Where(q => q.SystemUserTypeID.StartsWith(itemQuerryObject.SystemUserTypeID.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.SystemUserCodeID))
                queryResult = queryResult.Where(q => q.SystemUserCodeID.StartsWith(itemQuerryObject.SystemUserCodeID.ToString()));

            return queryResult;
        }

        public IEnumerable<SystemUser> GetSystemUserByID(string itemID)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SystemUsers
                          where q.SystemUserID == itemID
                          select q);
            return queryResult;
        }

        public IEnumerable<SystemUser> Refresh(string autoIDs)
        {

            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<SystemUser>("RefreshSystemUser").AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(SystemUser item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(SystemUser item)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSystemUsers(item);
        }

        public void DeleteFromRepository(SystemUser item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {//if it exists in the db delete it from the db
                SystemUserEntities context = new SystemUserEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SystemUser deletedSystemUser = (from q in context.SystemUsers
                                          where q.SystemUserID == item.SystemUserID
                                          select q).FirstOrDefault();
                if (deletedSystemUser != null)
                {
                    context.DeleteObject(deletedSystemUser);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetSystemUserEntityState(item) != EntityStates.Detached)
                    _repositoryContext.Detach(item);
            }
        }

        public EntityStates GetSystemUserEntityState(SystemUser item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
                return _repositoryContext.GetEntityDescriptor(item).State;
            else
                return EntityStates.Detached;
        }   
    }
}
