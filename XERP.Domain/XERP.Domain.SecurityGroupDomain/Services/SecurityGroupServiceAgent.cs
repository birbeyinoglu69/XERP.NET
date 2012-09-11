using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;

using XERP.Domain.SecurityGroupDomain.SecurityGroupDataService;
namespace XERP.Domain.SecurityGroupDomain.Services
{
    public class SecurityGroupServiceAgent : XERP.Domain.SecurityGroupDomain.Services.ISecurityGroupServiceAgent
    {
        #region Initialize Service
        public SecurityGroupServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new SecurityGroupEntities(_rootUri);
            _context.MergeOption = MergeOption.NoTracking;   
        }
        #endregion Initialize Service

        #region Properties
        private Uri _rootUri;
        private SecurityGroupEntities _context;        
        #endregion Properties

        #region Read Only Methods  No Repository Required
        public IEnumerable<SecurityGroupType> GetSecurityGroupTypesReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from types in _context.SecurityGroupTypes
                                select types);
            return queryResult;
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodesReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from codes in _context.SecurityGroupCodes
                                select codes);
            return queryResult;
        }

        public bool SecurityGroupExists(string securityGroupID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroups
                           where q.SecurityGroupID == securityGroupID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool SecurityGroupTypeExists(string securityGroupTypeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroupTypes
                               where q.SecurityGroupTypeID == securityGroupTypeID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool SecurityGroupCodeExists(string securityGroupCodeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroupCodes
                               where q.SecurityGroupCodeID == securityGroupCodeID
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
        #endregion Read Only Methods  No Repository Required

        #region SecurityGroup Repository CRUD
        public IEnumerable<SecurityGroup> RefreshSecurityGroup(string autoIDs)
        {
            return SecurityGroupSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<SecurityGroup> GetSecurityGroups()
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroups();
        }

        public IEnumerable<SecurityGroup> GetSecurityGroups(SecurityGroup securityGroupQuerryObject)
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroups(securityGroupQuerryObject);
        }

        public IEnumerable<SecurityGroup> GetSecurityGroupByID(string securityGroupID)
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroupByID(securityGroupID);
        }

        public void CommitSecurityGroupRepository()
        {
            SecurityGroupSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSecurityGroupRepository(SecurityGroup securityGroup)
        {
            SecurityGroupSingletonRepository.Instance.UpdateRepository(securityGroup);
        }

        public void AddToSecurityGroupRepository(SecurityGroup securityGroup)
        {
            SecurityGroupSingletonRepository.Instance.AddToRepository(securityGroup);
        }

        public void DeleteFromSecurityGroupRepository(SecurityGroup securityGroup)
        {
            SecurityGroupSingletonRepository.Instance.DeleteFromRepository(securityGroup);
        }

        public EntityStates GetSecurityGroupEntityState(SecurityGroup securityGroup)
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroupEntityState(securityGroup);
        }
        #endregion SecurityGroup Repository CRUD

        #region SecurityGroupType Repository CRUD
        public IEnumerable<SecurityGroupType> RefreshSecurityGroupType(string autoIDs)
        {
            return SecurityGroupTypeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypes()
        {
            return SecurityGroupTypeSingletonRepository.Instance.GetSecurityGroupTypes();
        }

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypes(SecurityGroupType securityGroupTypeQuerryObject)
        {
            return SecurityGroupTypeSingletonRepository.Instance.GetSecurityGroupTypes(securityGroupTypeQuerryObject);
        }

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypeByID(string securityGroupTypeID)
        {
            return SecurityGroupTypeSingletonRepository.Instance.GetSecurityGroupTypeByID(securityGroupTypeID);
        }
        public void CommitSecurityGroupTypeRepository()
        {
            SecurityGroupTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSecurityGroupTypeRepository(SecurityGroupType securityGroupType)
        {
            SecurityGroupTypeSingletonRepository.Instance.UpdateRepository(securityGroupType);
        }

        public void AddToSecurityGroupTypeRepository(SecurityGroupType securityGroupType)
        {
            SecurityGroupTypeSingletonRepository.Instance.AddToRepository(securityGroupType);
        }

        public void DeleteFromSecurityGroupTypeRepository(SecurityGroupType securityGroupType)
        {
            SecurityGroupTypeSingletonRepository.Instance.DeleteFromRepository(securityGroupType);
        }

        public EntityStates GetSecurityGroupTypeEntityState(SecurityGroupType securityGroupType)
        {
            return SecurityGroupTypeSingletonRepository.Instance.GetSecurityGroupTypeEntityState(securityGroupType);
        }

        #endregion SecurityGroupType Repository CRUD

        #region SecurityGroupCode Repository CRUD
        public IEnumerable<SecurityGroupCode> RefreshSecurityGroupCode(string autoIDs)
        {
            return SecurityGroupCodeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodes()
        {
            return SecurityGroupCodeSingletonRepository.Instance.GetSecurityGroupCodes();
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodes(SecurityGroupCode securityGroupCodeQuerryObject)
        {
            return SecurityGroupCodeSingletonRepository.Instance.GetSecurityGroupCodes(securityGroupCodeQuerryObject);
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodeByID(string securityGroupCodeID)
        {
            return SecurityGroupCodeSingletonRepository.Instance.GetSecurityGroupCodeByID(securityGroupCodeID);
        }
        public void CommitSecurityGroupCodeRepository()
        {
            SecurityGroupCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSecurityGroupCodeRepository(SecurityGroupCode securityGroupCode)
        {
            SecurityGroupCodeSingletonRepository.Instance.UpdateRepository(securityGroupCode);
        }

        public void AddToSecurityGroupCodeRepository(SecurityGroupCode securityGroupCode)
        {
            SecurityGroupCodeSingletonRepository.Instance.AddToRepository(securityGroupCode);
        }

        public void DeleteFromSecurityGroupCodeRepository(SecurityGroupCode securityGroupCode)
        {
            SecurityGroupCodeSingletonRepository.Instance.DeleteFromRepository(securityGroupCode);
        }

        public EntityStates GetSecurityGroupCodeEntityState(SecurityGroupCode securityGroupCode)
        {
            return SecurityGroupCodeSingletonRepository.Instance.GetSecurityGroupCodeEntityState(securityGroupCode);
        }

        #endregion SecurityGroupCode Repository CRUD
    }
}
