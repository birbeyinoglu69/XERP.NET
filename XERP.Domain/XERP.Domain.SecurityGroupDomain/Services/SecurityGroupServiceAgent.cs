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
        public bool SecurityGroupRepositoryIsDirty()
        {
            return SecurityGroupSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool SecurityGroupTypeRepositoryIsDirty()
        {
            return SecurityGroupTypeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool SecurityGroupCodeRepositoryIsDirty()
        {
            return SecurityGroupCodeSingletonRepository.Instance.RepositoryIsDirty();
        } 
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

        public bool SecurityGroupExists(string  itemID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroups
                           where q.SecurityGroupID ==  itemID &&
                           q.CompanyID == companyID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool SecurityGroupTypeExists(string  itemTypeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroupTypes
                               where q.SecurityGroupTypeID ==  itemTypeID
                               where q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool SecurityGroupCodeExists(string  itemCodeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.SecurityGroupCodes
                               where q.SecurityGroupCodeID ==  itemCodeID &&
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

        #region SecurityGroup Repository CRUD
        public IEnumerable<SecurityGroup> RefreshSecurityGroup(string autoIDs)
        {
            return SecurityGroupSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<SecurityGroup> GetSecurityGroups(string companyID)
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroups(companyID);
        }

        public IEnumerable<SecurityGroup> GetSecurityGroups(SecurityGroup  itemQuerryObject, string companyID)
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroups( itemQuerryObject, companyID);
        }

        public IEnumerable<SecurityGroup> GetSecurityGroupByID(string  itemID, string companyID)
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroupByID( itemID, companyID);
        }

        public void CommitSecurityGroupRepository()
        {
            SecurityGroupSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSecurityGroupRepository(SecurityGroup  item)
        {
            SecurityGroupSingletonRepository.Instance.UpdateRepository( item);
        }

        public void AddToSecurityGroupRepository(SecurityGroup  item)
        {
            SecurityGroupSingletonRepository.Instance.AddToRepository( item);
        }

        public void DeleteFromSecurityGroupRepository(SecurityGroup  item)
        {
            SecurityGroupSingletonRepository.Instance.DeleteFromRepository( item);
        }

        public EntityStates GetSecurityGroupEntityState(SecurityGroup  item)
        {
            return SecurityGroupSingletonRepository.Instance.GetSecurityGroupEntityState( item);
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

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypes(SecurityGroupType  itemTypeQuerryObject, string companyID)
        {
            return SecurityGroupTypeSingletonRepository.Instance.GetSecurityGroupTypes( itemTypeQuerryObject, companyID);
        }

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypeByID(string  itemTypeID, string companyID)
        {
            return SecurityGroupTypeSingletonRepository.Instance.GetSecurityGroupTypeByID( itemTypeID, companyID);
        }
        public void CommitSecurityGroupTypeRepository()
        {
            SecurityGroupTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSecurityGroupTypeRepository(SecurityGroupType  itemType)
        {
            SecurityGroupTypeSingletonRepository.Instance.UpdateRepository( itemType);
        }

        public void AddToSecurityGroupTypeRepository(SecurityGroupType  itemType)
        {
            SecurityGroupTypeSingletonRepository.Instance.AddToRepository( itemType);
        }

        public void DeleteFromSecurityGroupTypeRepository(SecurityGroupType  itemType)
        {
            SecurityGroupTypeSingletonRepository.Instance.DeleteFromRepository( itemType);
        }

        public EntityStates GetSecurityGroupTypeEntityState(SecurityGroupType  itemType)
        {
            return SecurityGroupTypeSingletonRepository.Instance.GetSecurityGroupTypeEntityState( itemType);
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

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodes(SecurityGroupCode  itemCodeQuerryObject, string companyID)
        {
            return SecurityGroupCodeSingletonRepository.Instance.GetSecurityGroupCodes( itemCodeQuerryObject, companyID);
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodeByID(string  itemCodeID, string companyID)
        {
            return SecurityGroupCodeSingletonRepository.Instance.GetSecurityGroupCodeByID( itemCodeID, companyID);
        }
        public void CommitSecurityGroupCodeRepository()
        {
            SecurityGroupCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateSecurityGroupCodeRepository(SecurityGroupCode  itemCode)
        {
            SecurityGroupCodeSingletonRepository.Instance.UpdateRepository( itemCode);
        }

        public void AddToSecurityGroupCodeRepository(SecurityGroupCode  itemCode)
        {
            SecurityGroupCodeSingletonRepository.Instance.AddToRepository( itemCode);
        }

        public void DeleteFromSecurityGroupCodeRepository(SecurityGroupCode  itemCode)
        {
            SecurityGroupCodeSingletonRepository.Instance.DeleteFromRepository( itemCode);
        }

        public EntityStates GetSecurityGroupCodeEntityState(SecurityGroupCode  itemCode)
        {
            return SecurityGroupCodeSingletonRepository.Instance.GetSecurityGroupCodeEntityState( itemCode);
        }

        #endregion SecurityGroupCode Repository CRUD
    }
}
