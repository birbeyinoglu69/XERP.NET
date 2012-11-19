using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Domain.MenuSecurityDomain
{
    public class ExecutableProgramTypeSingletonRepository
    {
        private ExecutableProgramTypeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new MenuSecurityEntities(_rootUri);
        }

        private static ExecutableProgramTypeSingletonRepository _instance;
        public static ExecutableProgramTypeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ExecutableProgramTypeSingletonRepository();
                return _instance;
            }
        }

        private Uri _rootUri;
        private MenuSecurityEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<ExecutableProgramType> GetExecutableProgramTypes(string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.ExecutableProgramTypes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<ExecutableProgramType> GetExecutableProgramTypes(ExecutableProgramType executableProgramTypeQuerryObject, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.ExecutableProgramTypes
                              where q.CompanyID == companyID
                              select q;
            if (!string.IsNullOrEmpty(executableProgramTypeQuerryObject.Type))
                queryResult = queryResult.Where(q => q.Type.StartsWith(executableProgramTypeQuerryObject.Type.ToString()));

            if (!string.IsNullOrEmpty(executableProgramTypeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(executableProgramTypeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(executableProgramTypeQuerryObject.ExecutableProgramTypeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(executableProgramTypeQuerryObject.ExecutableProgramTypeID.ToString()));

            return queryResult;
        }


        public IEnumerable<ExecutableProgramType> GetExecutableProgramTypeByID(string executableProgramTypeID, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.ExecutableProgramTypes
                               where q.ExecutableProgramTypeID == executableProgramTypeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<ExecutableProgramType> Refresh(string autoIDs)
        {

            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<ExecutableProgramType>("RefreshExecutableProgramType").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(ExecutableProgramType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                itemType.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemType.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemType);
            }
        }

        public void AddToRepository(ExecutableProgramType executableProgramType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToExecutableProgramTypes(executableProgramType);
        }

        public void DeleteFromRepository(ExecutableProgramType executableProgramType)
        {
            if (_repositoryContext.GetEntityDescriptor(executableProgramType) != null)
            {//if it exists in the db delete it from the db
                MenuSecurityEntities context = new MenuSecurityEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                ExecutableProgramType deletedExecutableProgramType = (from q in context.ExecutableProgramTypes
                                          where q.ExecutableProgramTypeID == executableProgramType.ExecutableProgramTypeID
                                          select q).SingleOrDefault();
                if (deletedExecutableProgramType != null)
                {
                    context.DeleteObject(deletedExecutableProgramType);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetExecutableProgramTypeEntityState(executableProgramType) != EntityStates.Detached)
                    _repositoryContext.Detach(executableProgramType);
            }
        }

        public EntityStates GetExecutableProgramTypeEntityState(ExecutableProgramType executableProgramType)
        {
            if (_repositoryContext.GetEntityDescriptor(executableProgramType) != null)
                return _repositoryContext.GetEntityDescriptor(executableProgramType).State;
            else
                return EntityStates.Detached;
        }
    }
}
