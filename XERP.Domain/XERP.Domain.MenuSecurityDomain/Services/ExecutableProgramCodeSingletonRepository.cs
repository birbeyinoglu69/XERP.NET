using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Domain.MenuSecurityDomain.Services
{
    public class ExecutableProgramCodeSingletonRepository
    {
        private ExecutableProgramCodeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new MenuSecurityEntities(_rootUri);
        }

        private static ExecutableProgramCodeSingletonRepository _instance;
        public static ExecutableProgramCodeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ExecutableProgramCodeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private MenuSecurityEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<ExecutableProgramCode> GetExecutableProgramCodes(string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.ExecutableProgramCodes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<ExecutableProgramCode> GetExecutableProgramCodes(ExecutableProgramCode executableProgramCodeQuerryObject, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.ExecutableProgramCodes
                              where q.CompanyID == companyID
                              select q;
            if (!string.IsNullOrEmpty(executableProgramCodeQuerryObject.Code))
                queryResult = queryResult.Where(q => q.Code.StartsWith(executableProgramCodeQuerryObject.Code.ToString()));

            if (!string.IsNullOrEmpty(executableProgramCodeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(executableProgramCodeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(executableProgramCodeQuerryObject.ExecutableProgramCodeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(executableProgramCodeQuerryObject.ExecutableProgramCodeID.ToString()));

            return queryResult;
        }


        public IEnumerable<ExecutableProgramCode> GetExecutableProgramCodeByID(string executableProgramCodeID, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.ExecutableProgramCodes
                               where q.ExecutableProgramCodeID == executableProgramCodeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<ExecutableProgramCode> Refresh(string autoIDs)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<ExecutableProgramCode>("RefreshExecutableProgramCode").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(ExecutableProgramCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {
                itemCode.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemCode.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemCode);
            }
        }

        public void AddToRepository(ExecutableProgramCode executableProgramCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToExecutableProgramCodes(executableProgramCode);
        }

        public void DeleteFromRepository(ExecutableProgramCode executableProgramCode)
        {
            if (_repositoryContext.GetEntityDescriptor(executableProgramCode) != null)
            {//if it exists in the db delete it from the db
                MenuSecurityEntities context = new MenuSecurityEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                ExecutableProgramCode deletedExecutableProgramCode = (from q in context.ExecutableProgramCodes
                                          where q.ExecutableProgramCodeID == executableProgramCode.ExecutableProgramCodeID
                                          select q).FirstOrDefault();
                if (deletedExecutableProgramCode != null)
                {
                    context.DeleteObject(deletedExecutableProgramCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetExecutableProgramCodeEntityState(executableProgramCode) != EntityStates.Detached)
                    _repositoryContext.Detach(executableProgramCode);
            }
        }

        public EntityStates GetExecutableProgramCodeEntityState(ExecutableProgramCode executableProgramCode)
        {
            if (_repositoryContext.GetEntityDescriptor(executableProgramCode) != null)
                return _repositoryContext.GetEntityDescriptor(executableProgramCode).State;
            else
                return EntityStates.Detached;
        }
    }
}