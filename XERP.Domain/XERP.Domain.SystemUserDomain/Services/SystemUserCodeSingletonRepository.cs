using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.SystemUserDomain.SystemUserDataService;
using XERP.Domain.SystemUserDomain.Services;

namespace XERP.Domain.SystemUserDomain.Services
{
    public class SystemUserCodeSingletonRepository
    {
        private SystemUserCodeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new SystemUserEntities(_rootUri);
        }

        private static SystemUserCodeSingletonRepository _instance;
        public static SystemUserCodeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SystemUserCodeSingletonRepository();
                return _instance;
            }
        }

        private Uri _rootUri;
        private SystemUserEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<SystemUserCode> GetSystemUserCodes()
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SystemUserCodes
                               select q);
            return queryResult;
        }

        public IEnumerable<SystemUserCode> GetSystemUserCodes(SystemUserCode itemCodeQuerryObject)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SystemUserCodes
                              select q;
            if (!string.IsNullOrEmpty(itemCodeQuerryObject.Code))
                queryResult = queryResult.Where(q => q.Code.StartsWith(itemCodeQuerryObject.Code.ToString()));

            if (!string.IsNullOrEmpty(itemCodeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemCodeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemCodeQuerryObject.SystemUserCodeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemCodeQuerryObject.SystemUserCodeID.ToString()));

            return queryResult;
        }


        public IEnumerable<SystemUserCode> GetSystemUserCodeByID(string itemCodeID)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SystemUserCodes
                               where q.SystemUserCodeID == itemCodeID
                               select q);
            return queryResult;
        }

        public IEnumerable<SystemUserCode> Refresh(string autoIDs)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<SystemUserCode>("RefreshSystemUserCode").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(SystemUserCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {
                itemCode.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemCode.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemCode);
            }
        }

        public void AddToRepository(SystemUserCode itemCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSystemUserCodes(itemCode);
        }

        public void DeleteFromRepository(SystemUserCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {//if it exists in the db delete it from the db
                SystemUserEntities context = new SystemUserEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SystemUserCode deletedSystemUserCode = (from q in context.SystemUserCodes
                                          where q.SystemUserCodeID == itemCode.SystemUserCodeID
                                          select q).FirstOrDefault();
                if (deletedSystemUserCode != null)
                {
                    context.DeleteObject(deletedSystemUserCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetSystemUserCodeEntityState(itemCode) != EntityStates.Detached)
                    _repositoryContext.Detach(itemCode);
            }
        }

        public EntityStates GetSystemUserCodeEntityState(SystemUserCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
                return _repositoryContext.GetEntityDescriptor(itemCode).State;
            else
                return EntityStates.Detached;
        }
    }
}