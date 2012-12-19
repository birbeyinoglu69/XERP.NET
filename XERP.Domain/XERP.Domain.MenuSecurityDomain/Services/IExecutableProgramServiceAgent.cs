using System;
namespace XERP.Domain.MenuSecurityDomain.Services
{
    public interface IExecutableProgramServiceAgent
    {
        void AddToExecutableProgramCodeRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramCode executableProgramCode);
        void AddToExecutableProgramRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgram executableProgram);
        void AddToExecutableProgramTypeRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramType executableProgramType);
        void CommitExecutableProgramCodeRepository();
        void CommitExecutableProgramRepository();
        void CommitExecutableProgramTypeRepository();
        void DeleteFromExecutableProgramCodeRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramCode executableProgramCode);
        void DeleteFromExecutableProgramRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgram executableProgram);
        void DeleteFromExecutableProgramTypeRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramType executableProgramType);
        bool ExecutableProgramCodeExists(string executableProgramCodeID, string companyID);
        bool ExecutableProgramCodeRepositoryIsDirty();
        bool ExecutableProgramExists(string executableProgramID, string companyID);
        bool ExecutableProgramRepositoryIsDirty();
        bool ExecutableProgramTypeExists(string executableProgramTypeID, string companyID);
        bool ExecutableProgramTypeRepositoryIsDirty();
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgram> GetExecutableProgramByID(string executableProgramID, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramCode> GetExecutableProgramCodeByID(string executableProgramCodeID, string companyID);
        System.Data.Services.Client.EntityStates GetExecutableProgramCodeEntityState(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramCode executableProgramCode);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramCode> GetExecutableProgramCodes(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramCode> GetExecutableProgramCodes(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramCode executableProgramCodeQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramCode> GetExecutableProgramCodesReadOnly(string companyID);
        System.Data.Services.Client.EntityStates GetExecutableProgramEntityState(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgram executableProgram);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgram> GetExecutablePrograms(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgram> GetExecutablePrograms(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgram executableProgramQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramType> GetExecutableProgramTypeByID(string executableProgramTypeID, string companyID);
        System.Data.Services.Client.EntityStates GetExecutableProgramTypeEntityState(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramType executableProgramType);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramType> GetExecutableProgramTypes(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramType> GetExecutableProgramTypes(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramType executableProgramTypeQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramType> GetExecutableProgramTypesReadOnly(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.Temp> GetMetaData(string tableName);
        bool MenuItemCodeRepositoryIsDirty();
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgram> RefreshExecutableProgram(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramCode> RefreshExecutableProgramCode(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramType> RefreshExecutableProgramType(string autoIDs);
        void UpdateExecutableProgramCodeRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramCode executableProgramCode);
        void UpdateExecutableProgramRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgram executableProgram);
        void UpdateExecutableProgramTypeRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.ExecutableProgramType executableProgramType);
    }
}
