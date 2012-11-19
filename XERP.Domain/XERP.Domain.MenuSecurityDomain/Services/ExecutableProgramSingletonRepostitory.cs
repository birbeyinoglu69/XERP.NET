using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Domain.MenuSecurityDomain
{
    public class ExecutableProgramSingletonRepository
    {
        private ExecutableProgramSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new MenuSecurityEntities(_rootUri);
        }
        
        private static ExecutableProgramSingletonRepository _instance;
        public static ExecutableProgramSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ExecutableProgramSingletonRepository();
                return _instance;
            }
        }

        private Uri _rootUri;
        private MenuSecurityEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<ExecutableProgram> GetExecutablePrograms(string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.ExecutablePrograms
                               where q.CompanyID == companyID
                             select q);
            return queryResult;
        }

        public IEnumerable<ExecutableProgram> GetExecutablePrograms(ExecutableProgram executableProgramQuerryObject, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.ExecutablePrograms
                              where q.CompanyID == companyID
                             select q;
            if  (!string.IsNullOrEmpty(executableProgramQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(executableProgramQuerryObject.Name.ToString())); 

            if (!string.IsNullOrEmpty(executableProgramQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(executableProgramQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(executableProgramQuerryObject.ExecutableProgramTypeID))
                queryResult = queryResult.Where(q => q.ExecutableProgramTypeID.StartsWith(executableProgramQuerryObject.ExecutableProgramTypeID.ToString()));

            if (!string.IsNullOrEmpty(executableProgramQuerryObject.ExecutableProgramCodeID))
                queryResult = queryResult.Where(q => q.ExecutableProgramCodeID.StartsWith(executableProgramQuerryObject.ExecutableProgramCodeID.ToString()));
            return queryResult;
        }

        public IEnumerable<ExecutableProgram> GetExecutableProgramByID(string executableProgramID, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.ExecutablePrograms
                          where q.ExecutableProgramID == executableProgramID &&
                          q.CompanyID == companyID
                          select q);
            return queryResult;
        }

        public IEnumerable<ExecutableProgram> Refresh(string autoIDs)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<ExecutableProgram>("RefreshExecutableProgram").AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(ExecutableProgram item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(ExecutableProgram executableProgram)
        {
            executableProgram.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToExecutablePrograms(executableProgram);
        }

        public void DeleteFromRepository(ExecutableProgram executableProgram)
        {
            if (_repositoryContext.GetEntityDescriptor(executableProgram) != null)
            {//if it exists in the db delete it from the db
                MenuSecurityEntities context = new MenuSecurityEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                ExecutableProgram deletedExecutableProgram = (from q in context.ExecutablePrograms
                                          where q.ExecutableProgramID == executableProgram.ExecutableProgramID
                                          select q).SingleOrDefault();
                if (deletedExecutableProgram != null)
                {
                    context.DeleteObject(deletedExecutableProgram);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetExecutableProgramEntityState(executableProgram) != EntityStates.Detached)
                    _repositoryContext.Detach(executableProgram);
            }
        }

        public EntityStates GetExecutableProgramEntityState(ExecutableProgram executableProgram)
        {
            if (_repositoryContext.GetEntityDescriptor(executableProgram) != null)
                return _repositoryContext.GetEntityDescriptor(executableProgram).State;
            else
                return EntityStates.Detached;
        }   
    }
}
