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
        public IEnumerable<SecurityGroupType> GetSecurityGroupTypesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroupTypes
                               where q.CompanyID == companyID
                                select q);
            return queryResult;
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroupCodes
                               where q.CompanyID == companyID
                                select q);
            return queryResult;
        }

        public bool SecurityGroupExists(string securityGroupID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroups
                           where q.SecurityGroupID == securityGroupID &&
                           q.CompanyID == companyID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool SecurityGroupTypeExists(string securityGroupTypeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroupTypes
                               where q.SecurityGroupTypeID == securityGroupTypeID
                               where q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool SecurityGroupCodeExists(string securityGroupCodeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroupCodes
                               where q.SecurityGroupCodeID == securityGroupCodeID &&
                               q.CompanyID == companyID
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
        public IEnumerable<SecurityGroup> GetSecurityGroups(string companyID)
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroups(companyID);
        }

        public IEnumerable<SecurityGroup> GetSecurityGroups(SecurityGroup securityGroupQuerryObject, string companyID)
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroups(securityGroupQuerryObject, companyID);
        }

        public IEnumerable<SecurityGroup> GetSecurityGroupByID(string securityGroupID, string companyID)
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroupByID(securityGroupID, companyID);
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

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypes(string companyID)
        {
            return SecurityGroupTypeSingletonRepository.Instance.GetSecurityGroupTypes(companyID);
        }

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypes(SecurityGroupType securityGroupTypeQuerryObject, string companyID)
        {
            return SecurityGroupTypeSingletonRepository.Instance.GetSecurityGroupTypes(securityGroupTypeQuerryObject, companyID);
        }

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypeByID(string securityGroupTypeID, string companyID)
        {
            return SecurityGroupTypeSingletonRepository.Instance.GetSecurityGroupTypeByID(securityGroupTypeID, companyID);
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

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodes(string companyID)
        {
            return SecurityGroupCodeSingletonRepository.Instance.GetSecurityGroupCodes(companyID);
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodes(SecurityGroupCode securityGroupCodeQuerryObject, string companyID)
        {
            return SecurityGroupCodeSingletonRepository.Instance.GetSecurityGroupCodes(securityGroupCodeQuerryObject, companyID);
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodeByID(string securityGroupCodeID, string companyID)
        {
            return SecurityGroupCodeSingletonRepository.Instance.GetSecurityGroupCodeByID(securityGroupCodeID, companyID);
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
