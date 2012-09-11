using System;
namespace XERP.Domain.SecurityGroupDomain.Services
{
    public interface ISecurityGroupServiceAgent
    {
        void AddToSecurityGroupCodeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode securityGroupCode);
        void AddToSecurityGroupRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup securityGroup);
        void AddToSecurityGroupTypeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType securityGroupType);
        void CommitSecurityGroupCodeRepository();
        void CommitSecurityGroupRepository();
        void CommitSecurityGroupTypeRepository();
        void DeleteFromSecurityGroupCodeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode securityGroupCode);
        void DeleteFromSecurityGroupRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup securityGroup);
        void DeleteFromSecurityGroupTypeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType securityGroupType);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.Temp> GetMetaData(string tableName);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> GetSecurityGroupByID(string securityGroupID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodeByID(string securityGroupCodeID);
        System.Data.Services.Client.EntityStates GetSecurityGroupCodeEntityState(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode securityGroupCode);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodes();
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodes(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode securityGroupCodeQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodesReadOnly();
        System.Data.Services.Client.EntityStates GetSecurityGroupEntityState(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup securityGroup);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> GetSecurityGroups();
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> GetSecurityGroups(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup securityGroupQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypeByID(string securityGroupTypeID);
        System.Data.Services.Client.EntityStates GetSecurityGroupTypeEntityState(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType securityGroupType);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypes();
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypes(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType securityGroupTypeQuerryObject);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypesReadOnly();
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> RefreshSecurityGroup(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> RefreshSecurityGroupCode(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> RefreshSecurityGroupType(string autoIDs);
        bool SecurityGroupCodeExists(string securityGroupCodeID);
        bool SecurityGroupExists(string securityGroupID);
        bool SecurityGroupTypeExists(string securityGroupTypeID);
        void UpdateSecurityGroupCodeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode securityGroupCode);
        void UpdateSecurityGroupRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup securityGroup);
        void UpdateSecurityGroupTypeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType securityGroupType);
    }
}
