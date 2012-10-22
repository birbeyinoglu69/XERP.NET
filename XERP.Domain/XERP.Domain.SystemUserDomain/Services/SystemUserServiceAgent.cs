using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;

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
        public IEnumerable<SystemUserSecurity> GetSystemUserSecuritiesReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SystemUserSecurities
                               select q);
            return queryResult;
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

        public bool SystemUserExists(string systemUserID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SystemUsers
                           where q.SystemUserID == systemUserID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool SystemUserTypeExists(string systemUserTypeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SystemUserTypes
                               where q.SystemUserTypeID == systemUserTypeID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool SystemUserCodeExists(string systemUserCodeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SystemUserCodes
                               where q.SystemUserCodeID == systemUserCodeID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
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

        public IEnumerable<SecurityGroup> GetAvailableSecurityGroups(string securityGroupID)
        {
            //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<SecurityGroup>("GetAvailableSecurityGroups").AddQueryOption("SecurityGroupID", "'" + securityGroupID + "'");
            return query;
        }

        public IEnumerable<SecurityGroup> GetAllSecurityGroups()
        {
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<SecurityGroup>("GetAvailableSecurityGroups");
            return query;
        }

        public IEnumerable<SecurityGroup> GetSecurityGroupByIDReadOnly(string companyID, string securtiyGroupID)
        {
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var queryResult = (from q in _context.SecurityGroups
                               where q.CompanyID == companyID &&
                               q.SecurityGroupID == securtiyGroupID 
                               select q);
            return queryResult;
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

        public IEnumerable<SystemUser> GetSystemUsers(SystemUser systemUserQuerryObject)
        {
            return SystemUserSingletonRepository.Instance.GetSystemUsers(systemUserQuerryObject);
        }

        public IEnumerable<SystemUser> GetSystemUserByID(string systemUserID)
        {
            return SystemUserSingletonRepository.Instance.GetSystemUserByID(systemUserID);
        }

        public void CommitSystemUserRepository()
        {
            SystemUserSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSystemUserRepository(SystemUser systemUser)
        {
            SystemUserSingletonRepository.Instance.UpdateRepository(systemUser);
        }

        public void AddToSystemUserRepository(SystemUser systemUser)
        {
            SystemUserSingletonRepository.Instance.AddToRepository(systemUser);
        }

        public void AddToSystemUserRepository(SystemUserSecurity systemUserSecurity)
        {
            SystemUserSingletonRepository.Instance.AddToRepository(systemUserSecurity);
        }

        public void DeleteFromSystemUserRepository(SystemUser systemUser)
        {
            SystemUserSingletonRepository.Instance.DeleteFromRepository(systemUser);
        }

        public void DeleteFromSystemUserRepository(SystemUserSecurity systemUserSecurity)
        {
            SystemUserSingletonRepository.Instance.DeleteFromRepository(systemUserSecurity);
        }

        public EntityStates GetSystemUserEntityState(SystemUser systemUser)
        {
            return SystemUserSingletonRepository.Instance.GetSystemUserEntityState(systemUser);
        }

        public EntityStates GetSystemUserSecurityEntityState(SystemUserSecurity systemUserSecurity)
        {
            return SystemUserSingletonRepository.Instance.GetSystemUserSecurityEntityState(systemUserSecurity);
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

        public IEnumerable<SystemUserType> GetSystemUserTypes(SystemUserType systemUserTypeQuerryObject)
        {
            return SystemUserTypeSingletonRepository.Instance.GetSystemUserTypes(systemUserTypeQuerryObject);
        }

        public IEnumerable<SystemUserType> GetSystemUserTypeByID(string systemUserTypeID)
        {
            return SystemUserTypeSingletonRepository.Instance.GetSystemUserTypeByID(systemUserTypeID);
        }
        public void CommitSystemUserTypeRepository()
        {
            SystemUserTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSystemUserTypeRepository(SystemUserType systemUserType)
        {
            SystemUserTypeSingletonRepository.Instance.UpdateRepository(systemUserType);
        }

        public void AddToSystemUserTypeRepository(SystemUserType systemUserType)
        {
            SystemUserTypeSingletonRepository.Instance.AddToRepository(systemUserType);
        }

        public void DeleteFromSystemUserTypeRepository(SystemUserType systemUserType)
        {
            SystemUserTypeSingletonRepository.Instance.DeleteFromRepository(systemUserType);
        }

        public EntityStates GetSystemUserTypeEntityState(SystemUserType systemUserType)
        {
            return SystemUserTypeSingletonRepository.Instance.GetSystemUserTypeEntityState(systemUserType);
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

        public IEnumerable<SystemUserCode> GetSystemUserCodes(SystemUserCode systemUserCodeQuerryObject)
        {
            return SystemUserCodeSingletonRepository.Instance.GetSystemUserCodes(systemUserCodeQuerryObject);
        }

        public IEnumerable<SystemUserCode> GetSystemUserCodeByID(string systemUserCodeID)
        {
            return SystemUserCodeSingletonRepository.Instance.GetSystemUserCodeByID(systemUserCodeID);
        }
        public void CommitSystemUserCodeRepository()
        {
            SystemUserCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSystemUserCodeRepository(SystemUserCode systemUserCode)
        {
            SystemUserCodeSingletonRepository.Instance.UpdateRepository(systemUserCode);
        }

        public void AddToSystemUserCodeRepository(SystemUserCode systemUserCode)
        {
            SystemUserCodeSingletonRepository.Instance.AddToRepository(systemUserCode);
        }

        public void DeleteFromSystemUserCodeRepository(SystemUserCode systemUserCode)
        {
            SystemUserCodeSingletonRepository.Instance.DeleteFromRepository(systemUserCode);
        }

        public EntityStates GetSystemUserCodeEntityState(SystemUserCode systemUserCode)
        {
            return SystemUserCodeSingletonRepository.Instance.GetSystemUserCodeEntityState(systemUserCode);
        }

        #endregion SystemUserCode Repository CRUD

        #region Address Repository CRUD
        public IEnumerable<Address> RefreshAddress(string autoIDs)
        {
            return AddressSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<Address> GetAddresss(string companyID)
        {
            return AddressSingletonRepository.Instance.GetAddresses(companyID);
        }

        public IEnumerable<Address> GetAddresss(Address systemUserQuerryObject, string companyID)
        {
            return AddressSingletonRepository.Instance.GetAddresses(systemUserQuerryObject, companyID);
        }

        public IEnumerable<Address> GetAddressByID(string systemUserID, string companyID)
        {
            return AddressSingletonRepository.Instance.GetAddressByID(systemUserID, companyID);
        }

        public void CommitAddressRepository()
        {
            AddressSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateAddressRepository(Address systemUser)
        {
            AddressSingletonRepository.Instance.UpdateRepository(systemUser);
        }

        public void AddToAddressRepository(Address systemUser)
        {
            AddressSingletonRepository.Instance.AddToRepository(systemUser);
        }

        public void DeleteFromAddressRepository(Address systemUser, string companyID)
        {
            AddressSingletonRepository.Instance.DeleteFromRepository(systemUser, companyID);
        }

        public EntityStates GetAddressEntityState(Address systemUser)
        {
            return AddressSingletonRepository.Instance.GetAddressEntityState(systemUser);
        }
        #endregion Address Repository CRUD
    }
}
