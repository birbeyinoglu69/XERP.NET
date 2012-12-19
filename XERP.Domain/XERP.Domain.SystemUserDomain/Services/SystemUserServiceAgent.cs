using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using XERP.Domain.SystemUserDomain.SystemUserDataService;

namespace XERP.Domain.SystemUserDomain.Services
{
    public class SystemUserServiceAgent : XERP.Domain.SystemUserDomain.Services.ISystemUserServiceAgent 
    {
        #region Initialize Service
        public SystemUserServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new SystemUserEntities(_rootUri);
            _context.MergeOption = MergeOption.NoTracking;   
        }
        #endregion Initialize Service

        #region Properties
        private Uri _rootUri;
        private SystemUserEntities _context;        
        #endregion Properties

        #region Read Only Methods  No Repository Required
        public bool SystemUserRepositoryIsDirty()
        {
            return SystemUserSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool SystemUserTypeRepositoryIsDirty()
        {
            return SystemUserTypeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool SystemUserCodeRepositoryIsDirty()
        {
            return SystemUserCodeSingletonRepository.Instance.RepositoryIsDirty();
        } 

        public IEnumerable<SystemUserType> GetSystemUserTypesReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from types in _context.SystemUserTypes
                                select types);
            return queryResult;
        }

        public IEnumerable<SystemUserCode> GetSystemUserCodesReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from codes in _context.SystemUserCodes
                                select codes);
            return queryResult;
        }

        public bool SystemUserExists(string itemID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SystemUsers
                               where q.SystemUserID == itemID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool SystemUserTypeExists(string itemTypeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SystemUserTypes
                               where q.SystemUserTypeID == itemTypeID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool SystemUserCodeExists(string itemCodeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SystemUserCodes
                               where q.SystemUserCodeID == itemCodeID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public IEnumerable<Temp> GetMetaData(string tableName)
        {   //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<Temp>("GetMetaData").AddQueryOption("TableName", "'" + tableName + "'");
            return query;
        }
        #endregion Read Only Methods  No Repository Required

        #region SystemUser Repository CRUD
        public IEnumerable<SystemUser> RefreshSystemUser(string autoIDs)
        {
            return SystemUserSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<SystemUser> GetSystemUsers()
        {
            return SystemUserSingletonRepository.Instance.GetSystemUsers();
        }

        public IEnumerable<SystemUser> GetSystemUsers(SystemUser itemQuerryObject)
        {
            return SystemUserSingletonRepository.Instance.GetSystemUsers(itemQuerryObject);
        }

        public IEnumerable<SystemUser> GetSystemUserByID(string itemID)
        {
            return SystemUserSingletonRepository.Instance.GetSystemUserByID(itemID);
        }

        public void CommitSystemUserRepository()
        {
            SystemUserSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSystemUserRepository(SystemUser item)
        {
            SystemUserSingletonRepository.Instance.UpdateRepository(item);
        }

        public void AddToSystemUserRepository(SystemUser item)
        {
            SystemUserSingletonRepository.Instance.AddToRepository(item);
        }

        public void DeleteFromSystemUserRepository(SystemUser item)
        {
            SystemUserSingletonRepository.Instance.DeleteFromRepository(item);
        }

        public EntityStates GetSystemUserEntityState(SystemUser item)
        {
            return SystemUserSingletonRepository.Instance.GetSystemUserEntityState(item);
        }
        #endregion SystemUser Repository CRUD

        #region SystemUserType Repository CRUD
        public IEnumerable<SystemUserType> RefreshSystemUserType(string autoIDs)
        {
            return SystemUserTypeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<SystemUserType> GetSystemUserTypes()
        {
            return SystemUserTypeSingletonRepository.Instance.GetSystemUserTypes();
        }

        public IEnumerable<SystemUserType> GetSystemUserTypes(SystemUserType itemTypeQuerryObject)
        {
            return SystemUserTypeSingletonRepository.Instance.GetSystemUserTypes(itemTypeQuerryObject);
        }

        public IEnumerable<SystemUserType> GetSystemUserTypeByID(string itemTypeID)
        {
            return SystemUserTypeSingletonRepository.Instance.GetSystemUserTypeByID(itemTypeID);
        }
        public void CommitSystemUserTypeRepository()
        {
            SystemUserTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSystemUserTypeRepository(SystemUserType itemType)
        {
            SystemUserTypeSingletonRepository.Instance.UpdateRepository(itemType);
        }

        public void AddToSystemUserTypeRepository(SystemUserType itemType)
        {
            SystemUserTypeSingletonRepository.Instance.AddToRepository(itemType);
        }

        public void DeleteFromSystemUserTypeRepository(SystemUserType itemType)
        {
            SystemUserTypeSingletonRepository.Instance.DeleteFromRepository(itemType);
        }

        public EntityStates GetSystemUserTypeEntityState(SystemUserType itemType)
        {
            return SystemUserTypeSingletonRepository.Instance.GetSystemUserTypeEntityState(itemType);
        }
        #endregion SystemUserType Repository CRUD

        #region SystemUserCode Repository CRUD
        public IEnumerable<SystemUserCode> RefreshSystemUserCode(string autoIDs)
        {
            return SystemUserCodeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<SystemUserCode> GetSystemUserCodes()
        {
            return SystemUserCodeSingletonRepository.Instance.GetSystemUserCodes();
        }

        public IEnumerable<SystemUserCode> GetSystemUserCodes(SystemUserCode itemCodeQuerryObject)
        {
            return SystemUserCodeSingletonRepository.Instance.GetSystemUserCodes(itemCodeQuerryObject);
        }

        public IEnumerable<SystemUserCode> GetSystemUserCodeByID(string itemCodeID)
        {
            return SystemUserCodeSingletonRepository.Instance.GetSystemUserCodeByID(itemCodeID);
        }
        public void CommitSystemUserCodeRepository()
        {
            SystemUserCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSystemUserCodeRepository(SystemUserCode itemCode)
        {
            SystemUserCodeSingletonRepository.Instance.UpdateRepository(itemCode);
        }

        public void AddToSystemUserCodeRepository(SystemUserCode itemCode)
        {
            SystemUserCodeSingletonRepository.Instance.AddToRepository(itemCode);
        }

        public void DeleteFromSystemUserCodeRepository(SystemUserCode itemCode)
        {
            SystemUserCodeSingletonRepository.Instance.DeleteFromRepository(itemCode);
        }

        public EntityStates GetSystemUserCodeEntityState(SystemUserCode itemCode)
        {
            return SystemUserCodeSingletonRepository.Instance.GetSystemUserCodeEntityState(itemCode);
        }
        #endregion SystemUserCode Repository CRUD
    }
}
