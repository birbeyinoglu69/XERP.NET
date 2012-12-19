using System;
namespace XERP.Domain.SystemUserDomain.Services
{
    public interface ISystemUserServiceAgent
    {
        void AddToSystemUserCodeRepository(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserCode itemCode);
        void AddToSystemUserRepository(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUser item);
        void AddToSystemUserTypeRepository(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserType itemType);
        void CommitSystemUserCodeRepository();
        void CommitSystemUserRepository();
        void CommitSystemUserTypeRepository();
        bool SystemUserCodeExists(string itemCodeID);
        bool SystemUserExists(string itemID);
        bool SystemUserTypeExists(string itemTypeID);
        void DeleteFromSystemUserCodeRepository(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserCode itemCode);
        void DeleteFromSystemUserRepository(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUser item);
        //void DeleteFromSystemUserRepository(System.Collections.IList deletedRecords);
        void DeleteFromSystemUserTypeRepository(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserType itemType);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUser> GetSystemUsers();
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUser> GetSystemUsers(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUser itemQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUser> GetSystemUserByID(string itemID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserCode> GetSystemUserCodeByID(string itemCodeID);
        System.Data.Services.Client.EntityStates GetSystemUserCodeEntityState(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserCode itemCode);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserCode> GetSystemUserCodes();
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserCode> GetSystemUserCodes(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserCode itemCodeQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserCode> GetSystemUserCodesReadOnly();
        System.Data.Services.Client.EntityStates GetSystemUserEntityState(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUser item);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserType> GetSystemUserTypeByID(string itemTypeID);
        System.Data.Services.Client.EntityStates GetSystemUserTypeEntityState(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserType itemType);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserType> GetSystemUserTypes();
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserType> GetSystemUserTypes(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserType itemTypeQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserType> GetSystemUserTypesReadOnly();
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.Temp> GetMetaData(string tableName);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUser> RefreshSystemUser(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserCode> RefreshSystemUserCode(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserType> RefreshSystemUserType(string autoIDs);
        void UpdateSystemUserCodeRepository(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserCode itemCode);
        void UpdateSystemUserRepository(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUser item);
        void UpdateSystemUserTypeRepository(XERP.Domain.SystemUserDomain.SystemUserDataService.SystemUserType itemType);
        bool SystemUserRepositoryIsDirty();
        bool SystemUserCodeRepositoryIsDirty();
        bool SystemUserTypeRepositoryIsDirty();
    }
}
