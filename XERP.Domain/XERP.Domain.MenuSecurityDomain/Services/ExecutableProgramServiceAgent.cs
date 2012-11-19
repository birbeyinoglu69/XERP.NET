using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;

using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
namespace XERP.Domain.MenuSecurityDomain.Services
{
    public class ExecutableProgramServiceAgent : XERP.Domain.MenuSecurityDomain.Services.IExecutableProgramServiceAgent
    {
        #region Initialize Service
        public ExecutableProgramServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new MenuSecurityEntities(_rootUri);
            _context.MergeOption = MergeOption.NoTracking;   
        }
        #endregion Initialize Service

        #region Properties
        private Uri _rootUri;
        private MenuSecurityEntities _context;        
        #endregion Properties

        #region Read Only Methods  No Repository Required
        public bool ExecutableProgramRepositoryIsDirty()
        {
            return ExecutableProgramSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool ExecutableProgramTypeRepositoryIsDirty()
        {
            return ExecutableProgramTypeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool MenuItemCodeRepositoryIsDirty()
        {
            return ExecutableProgramCodeSingletonRepository.Instance.RepositoryIsDirty();
        } 
        public IEnumerable<ExecutableProgramType> GetExecutableProgramTypesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.ExecutableProgramTypes
                               where q.CompanyID == companyID
                                select q);
            return queryResult;
        }

        public IEnumerable<ExecutableProgramCode> GetExecutableProgramCodesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.ExecutableProgramCodes
                               where q.CompanyID == companyID
                                select q);
            return queryResult;
        }

        public bool ExecutableProgramExists(string executableProgramID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.ExecutablePrograms
                           where q.ExecutableProgramID == executableProgramID &&
                           q.CompanyID == companyID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;
            return false;
        }

        public bool ExecutableProgramTypeExists(string executableProgramTypeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.ExecutableProgramTypes
                               where q.ExecutableProgramTypeID == executableProgramTypeID
                               where q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool ExecutableProgramCodeExists(string executableProgramCodeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.ExecutableProgramCodes
                               where q.ExecutableProgramCodeID == executableProgramCodeID &&
                               q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public IEnumerable<Temp> GetMetaData(string tableName)
        {
            //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<Temp>("GetMetaData").AddQueryOption("TableName", "'" + tableName + "'");
            return query;
        }
        #endregion Read Only Methods  No Repository Required

        #region ExecutableProgram Repository CRUD
        public IEnumerable<ExecutableProgram> RefreshExecutableProgram(string autoIDs)
        {
            return ExecutableProgramSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<ExecutableProgram> GetExecutablePrograms(string companyID)
        {
            return ExecutableProgramSingletonRepository.Instance.GetExecutablePrograms(companyID);
        }

        public IEnumerable<ExecutableProgram> GetExecutablePrograms(ExecutableProgram executableProgramQuerryObject, string companyID)
        {
            return ExecutableProgramSingletonRepository.Instance.GetExecutablePrograms(executableProgramQuerryObject, companyID);
        }

        public IEnumerable<ExecutableProgram> GetExecutableProgramByID(string executableProgramID, string companyID)
        {
            return ExecutableProgramSingletonRepository.Instance.GetExecutableProgramByID(executableProgramID, companyID);
        }

        public void CommitExecutableProgramRepository()
        {
            ExecutableProgramSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateExecutableProgramRepository(ExecutableProgram executableProgram)
        {
            ExecutableProgramSingletonRepository.Instance.UpdateRepository(executableProgram);
        }

        public void AddToExecutableProgramRepository(ExecutableProgram executableProgram)
        {
            ExecutableProgramSingletonRepository.Instance.AddToRepository(executableProgram);
        }

        public void DeleteFromExecutableProgramRepository(ExecutableProgram executableProgram)
        {
            ExecutableProgramSingletonRepository.Instance.DeleteFromRepository(executableProgram);
        }

        public EntityStates GetExecutableProgramEntityState(ExecutableProgram executableProgram)
        {
            return ExecutableProgramSingletonRepository.Instance.GetExecutableProgramEntityState(executableProgram);
        }
        #endregion ExecutableProgram Repository CRUD

        #region ExecutableProgramType Repository CRUD
        public IEnumerable<ExecutableProgramType> RefreshExecutableProgramType(string autoIDs)
        {
            return ExecutableProgramTypeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<ExecutableProgramType> GetExecutableProgramTypes(string companyID)
        {
            return ExecutableProgramTypeSingletonRepository.Instance.GetExecutableProgramTypes(companyID);
        }

        public IEnumerable<ExecutableProgramType> GetExecutableProgramTypes(ExecutableProgramType executableProgramTypeQuerryObject, string companyID)
        {
            return ExecutableProgramTypeSingletonRepository.Instance.GetExecutableProgramTypes(executableProgramTypeQuerryObject, companyID);
        }

        public IEnumerable<ExecutableProgramType> GetExecutableProgramTypeByID(string executableProgramTypeID, string companyID)
        {
            return ExecutableProgramTypeSingletonRepository.Instance.GetExecutableProgramTypeByID(executableProgramTypeID, companyID);
        }
        public void CommitExecutableProgramTypeRepository()
        {
            ExecutableProgramTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateExecutableProgramTypeRepository(ExecutableProgramType executableProgramType)
        {
            ExecutableProgramTypeSingletonRepository.Instance.UpdateRepository(executableProgramType);
        }

        public void AddToExecutableProgramTypeRepository(ExecutableProgramType executableProgramType)
        {
            ExecutableProgramTypeSingletonRepository.Instance.AddToRepository(executableProgramType);
        }

        public void DeleteFromExecutableProgramTypeRepository(ExecutableProgramType executableProgramType)
        {
            ExecutableProgramTypeSingletonRepository.Instance.DeleteFromRepository(executableProgramType);
        }

        public EntityStates GetExecutableProgramTypeEntityState(ExecutableProgramType executableProgramType)
        {
            return ExecutableProgramTypeSingletonRepository.Instance.GetExecutableProgramTypeEntityState(executableProgramType);
        }

        #endregion ExecutableProgramType Repository CRUD

        #region ExecutableProgramCode Repository CRUD
        public IEnumerable<ExecutableProgramCode> RefreshExecutableProgramCode(string autoIDs)
        {
            return ExecutableProgramCodeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<ExecutableProgramCode> GetExecutableProgramCodes(string companyID)
        {
            return ExecutableProgramCodeSingletonRepository.Instance.GetExecutableProgramCodes(companyID);
        }

        public IEnumerable<ExecutableProgramCode> GetExecutableProgramCodes(ExecutableProgramCode executableProgramCodeQuerryObject, string companyID)
        {
            return ExecutableProgramCodeSingletonRepository.Instance.GetExecutableProgramCodes(executableProgramCodeQuerryObject, companyID);
        }

        public IEnumerable<ExecutableProgramCode> GetExecutableProgramCodeByID(string executableProgramCodeID, string companyID)
        {
            return ExecutableProgramCodeSingletonRepository.Instance.GetExecutableProgramCodeByID(executableProgramCodeID, companyID);
        }
        public void CommitExecutableProgramCodeRepository()
        {
            ExecutableProgramCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateExecutableProgramCodeRepository(ExecutableProgramCode executableProgramCode)
        {
            ExecutableProgramCodeSingletonRepository.Instance.UpdateRepository(executableProgramCode);
        }

        public void AddToExecutableProgramCodeRepository(ExecutableProgramCode executableProgramCode)
        {
            ExecutableProgramCodeSingletonRepository.Instance.AddToRepository(executableProgramCode);
        }

        public void DeleteFromExecutableProgramCodeRepository(ExecutableProgramCode executableProgramCode)
        {
            ExecutableProgramCodeSingletonRepository.Instance.DeleteFromRepository(executableProgramCode);
        }

        public EntityStates GetExecutableProgramCodeEntityState(ExecutableProgramCode executableProgramCode)
        {
            return ExecutableProgramCodeSingletonRepository.Instance.GetExecutableProgramCodeEntityState(executableProgramCode);
        }

        #endregion ExecutableProgramCode Repository CRUD
    }
}
