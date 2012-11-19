using System;
namespace XERP.Domain.SecurityGroupDomain.Services
{
    public interface ISecurityGroupServiceAgent
    {
        void AddToSecurityGroupCodeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode itemCode);
        void AddToSecurityGroupRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup item);
        void AddToSecurityGroupTypeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType itemType);
        void CommitSecurityGroupCodeRepository();
        void CommitSecurityGroupRepository();
        void CommitSecurityGroupTypeRepository();
        void DeleteFromSecurityGroupCodeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode itemCode);
        void DeleteFromSecurityGroupRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup item);
        void DeleteFromSecurityGroupTypeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType itemType);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.Temp> GetMetaData(string tableName);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> GetSecurityGroupByID(string itemID, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodeByID(string itemCodeID, string companyID);
        System.Data.Services.Client.EntityStates GetSecurityGroupCodeEntityState(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode itemCode);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodes(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodes(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode itemCodeQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodesReadOnly(string companyID);
        System.Data.Services.Client.EntityStates GetSecurityGroupEntityState(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup item);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> GetSecurityGroups(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> GetSecurityGroups(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup itemQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypeByID(string itemTypeID, string companyID);
        System.Data.Services.Client.EntityStates GetSecurityGroupTypeEntityState(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType itemType);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypes(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypes(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType itemTypeQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypesReadOnly(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> RefreshSecurityGroup(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> RefreshSecurityGroupCode(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> RefreshSecurityGroupType(string autoIDs);
        bool SecurityGroupCodeExists(string itemCodeID, string companyID);
        bool SecurityGroupCodeRepositoryIsDirty();
        bool SecurityGroupExists(string itemID, string companyID);
        bool SecurityGroupRepositoryIsDirty();
        bool SecurityGroupTypeExists(string itemTypeID, string companyID);
        bool SecurityGroupTypeRepositoryIsDirty();
        void UpdateSecurityGroupCodeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode itemCode);
        void UpdateSecurityGroupRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup item);
        void UpdateSecurityGroupTypeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType itemType);
    }
}
