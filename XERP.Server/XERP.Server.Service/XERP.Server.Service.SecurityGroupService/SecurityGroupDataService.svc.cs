using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using XERP.Server.DAL.SecurityGroupDAL;
using System.ServiceModel.Web;
using System.Collections.Generic;
using ExtensionMethods;

namespace XERP.Server.Service.SecurityGroupService
{
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]   
    public class SecurityGroupDataService : DataService< SecurityGroupEntities >
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            //I noticed when setting page size it would always limit my expand payload to 1
            //I have elected to disable paging...
            //config.SetEntitySetPageSize("*", 50);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        }
        //The procedure below uses the Temp table as following...
        //ID item count key id...
        //Name MetaData FieldName
        //Int_1 String Field Max Length
        //Bool_1 Error Flag
        [WebGet]
        public IQueryable<Temp> GetMetaData(string tableName)
        {
            switch (tableName)
            {
                case "SecurityGroups":
                    SecurityGroup SecurityGroup = new SecurityGroup();
                    return SecurityGroup.GetMetaData().AsQueryable();
                case "SecurityGroupTypes":
                    SecurityGroupType SecurityGroupType = new SecurityGroupType();
                    return SecurityGroupType.GetMetaData().AsQueryable();
                case "SecurityGroupCodes":
                    SecurityGroupCode SecurityGroupCode = new SecurityGroupCode();
                    return SecurityGroupCode.GetMetaData().AsQueryable();
                default: //no table exists for the given tablename given...
                    List<Temp> tempList = new List<Temp>();
                    Temp temp = new Temp();
                    temp.ID = 0;
                    temp.Int_1 = 0;
                    temp.Bool_1 = true; //bool_1 will flag it as an error...
                    temp.Name = "Error";
                    temp.ShortChar_1 = "Table " + tableName + " Is Not A Valid Table Within The Given Entity Collection, Or Meta Data Was Not Defined For The Given Table Name";
                    tempList.Add(temp);
                    return tempList.AsQueryable();
            }
        }

        [WebGet]
        public IQueryable<SecurityGroup> RefreshSecurityGroup(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.SecurityGroupDAL.DALUtility dalUtility = new DALUtility();
            var context = new SecurityGroupEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.SecurityGroups
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<SecurityGroupType> RefreshSecurityGroupType(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.SecurityGroupDAL.DALUtility dalUtility = new DALUtility();
            var context = new SecurityGroupEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.SecurityGroupTypes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<SecurityGroupCode> RefreshSecurityGroupCode(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.SecurityGroupDAL.DALUtility dalUtility = new DALUtility();
            var context = new SecurityGroupEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.SecurityGroupCodes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [ChangeInterceptor("SecurityGroupTypes")]
        public void OnChangeSecurityGroupTypes(SecurityGroupType securityGroupType, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Type was used by its parent record...
                XERP.Server.DAL.SecurityGroupDAL.DALUtility dalUtility = new DALUtility();
                var context = new SecurityGroupEntities(dalUtility.EntityConectionString);
                context.SecurityGroups.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = securityGroupType.CompanyID;
                string typeID = securityGroupType.SecurityGroupTypeID;
                string sqlstring = "UPDATE SecurityGroups SET SecurityGroupTypeID = null WHERE CompanyID = '" + companyID + "' and SecurityGroupTypeID = '" + typeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("SecurityGroupCodes")]
        public void OnChangeSecurityGroupTypes(SecurityGroupCode securityGroupCode, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Code was used by its parent record...
                XERP.Server.DAL.SecurityGroupDAL.DALUtility dalUtility = new DALUtility();
                var context = new SecurityGroupEntities(dalUtility.EntityConectionString);
                context.SecurityGroups.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = securityGroupCode.CompanyID;
                string codeID = securityGroupCode.SecurityGroupCodeID;
                string sqlstring = "UPDATE SecurityGroups SET SecurityGroupCodeID = null WHERE CompanyID = '" + companyID + "' and SecurityGroupCodeID = '" + codeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        protected override SecurityGroupEntities CreateDataSource()
        {
            try
            {
                XERP.Server.DAL.SecurityGroupDAL.DALUtility dalUtility = new DALUtility();
                var context = new SecurityGroupEntities(dalUtility.EntityConectionString);

                //test it...
                //GetMetaData("SecurityGroups");
                //IQueryable<SecurityGroup> SecurityGroupQuery = (from c in context.SecurityGroups
                //                                    select c);

                //foreach (SecurityGroup cc in SecurityGroupQuery)
                //{
                //    string s = cc.Name.ToString();
                //}
                //SecurityGroup SecurityGroup = new SecurityGroup();
                //IQueryable<SecurityGroup> SecurityGroupQuery = Refresh("5,6");
                return context;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}

