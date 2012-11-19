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

        public IEnumerable<SystemUserCode> GetSystemUserCodes(SystemUserCode companyCodeQuerryObject)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SystemUserCodes
                              select q;

            if (!string.IsNullOrEmpty(companyCodeQuerryObject.Code))
                queryResult = queryResult.Where(q => q.Code.StartsWith(companyCodeQuerryObject.Code.ToString()));

            if (!string.IsNullOrEmpty(companyCodeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(companyCodeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(companyCodeQuerryObject.SystemUserCodeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(companyCodeQuerryObject.SystemUserCodeID.ToString()));

            return queryResult;
        }


        public IEnumerable<SystemUserCode> GetSystemUserCodeByID(string companyCodeID)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SystemUserCodes
                               where q.SystemUserCodeID == companyCodeID
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

        public void UpdateRepository(SystemUserCode companyCode)
        {
            if (_repositoryContext.GetEntityDescriptor(companyCode) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(companyCode);
            }
        }

        public void AddToRepository(SystemUserCode companyCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSystemUserCodes(companyCode);
        }

        public void DeleteFromRepository(SystemUserCode companyCode)
        {
            if (_repositoryContext.GetEntityDescriptor(companyCode) != null)
            {
                //if it exists in the db delete it from the db
                SystemUserEntities context = new SystemUserEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SystemUserCode deletedSystemUserCode = (from q in context.SystemUserCodes
                                          where q.SystemUserCodeID == companyCode.SystemUserCodeID
                                          select q).SingleOrDefault();
                if (deletedSystemUserCode != null)
                {
                    context.DeleteObject(deletedSystemUserCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetSystemUserCodeEntityState(companyCode) != EntityStates.Detached)
                    _repositoryContext.Detach(companyCode);
            }
        }

        public EntityStates GetSystemUserCodeEntityState(SystemUserCode companyCode)
        {
            if (_repositoryContext.GetEntityDescriptor(companyCode) != null)
                return _repositoryContext.GetEntityDescriptor(companyCode).State;
            else
                return EntityStates.Detached;
        }
    }
}