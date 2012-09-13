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
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> GetSecurityGroupByID(string securityGroupID, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodeByID(string securityGroupCodeID, string companyID);
        System.Data.Services.Client.EntityStates GetSecurityGroupCodeEntityState(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode securityGroupCode);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodes(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodes(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode securityGroupCodeQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> GetSecurityGroupCodesReadOnly(string companyID);
        System.Data.Services.Client.EntityStates GetSecurityGroupEntityState(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup securityGroup);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> GetSecurityGroups(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> GetSecurityGroups(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup securityGroupQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypeByID(string securityGroupTypeID, string companyID);
        System.Data.Services.Client.EntityStates GetSecurityGroupTypeEntityState(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType securityGroupType);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypes(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypes(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType securityGroupTypeQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> GetSecurityGroupTypesReadOnly(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup> RefreshSecurityGroup(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode> RefreshSecurityGroupCode(string autoIDs);
        System.Collections.Generic.IEnumerable<XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType> RefreshSecurityGroupType(string autoIDs);
        bool SecurityGroupCodeExists(string securityGroupCodeID, string companyID);
        bool SecurityGroupExists(string securityGroupID, string companyID);
        bool SecurityGroupTypeExists(string securityGroupTypeID, string companyID);
        void UpdateSecurityGroupCodeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupCode securityGroupCode);
        void UpdateSecurityGroupRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroup securityGroup);
        void UpdateSecurityGroupTypeRepository(XERP.Domain.SecurityGroupDomain.SecurityGroupDataService.SecurityGroupType securityGroupType);
    }
}
