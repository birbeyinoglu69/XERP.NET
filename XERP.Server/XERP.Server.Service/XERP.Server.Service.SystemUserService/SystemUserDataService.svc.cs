using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using XERP.Server.DAL.SystemUserDAL;
using System.ServiceModel.Web;
using System.Collections.Generic;
using ExtensionMethods;

namespace XERP.Server.Service.SystemUserService
{
    public class SystemUserDataService : DataService< SystemUserEntities >
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
                case "Addresses":
                    Address Address = new Address();
                    return Address.GetMetaData().AsQueryable();
                case "SystemUsers":
                    SystemUser SystemUser = new SystemUser();
                    return SystemUser.GetMetaData().AsQueryable();
                case "SystemUserTypes":
                    SystemUserType SystemUserType = new SystemUserType();
                    return SystemUserType.GetMetaData().AsQueryable();
                case "SystemUserCodes":
                    SystemUserCode SystemUserCode = new SystemUserCode();
                    return SystemUserCode.GetMetaData().AsQueryable();
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
        public IQueryable<SystemUser> RefreshSystemUser(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.SystemUserDAL.DALUtility dalUtility = new DALUtility();
            var context = new SystemUserEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.SystemUsers
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<SystemUserSecurity> RefreshSystemUserSecurity(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.SystemUserDAL.DALUtility dalUtility = new DALUtility();
            var context = new SystemUserEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.SystemUserSecurities
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<Address> RefreshAddress(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.SystemUserDAL.DALUtility dalUtility = new DALUtility();
            var context = new SystemUserEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.Addresses
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<SystemUserType> RefreshSystemUserType(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.SystemUserDAL.DALUtility dalUtility = new DALUtility();
            var context = new SystemUserEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.SystemUserTypes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<SecurityGroup> GetAvailableSecurityGroups(string securityGroupID)
        {
            XERP.Server.DAL.SystemUserDAL.DALUtility dalUtility = new DALUtility();
            var context = new SystemUserEntities(dalUtility.EntityConectionString);
            var queryResult =
                from sg in context.SecurityGroups
                where !context.SystemUserSecurities.Any(sus => sus.SecurityGroupID == sg.SecurityGroupID &&
                    sus.SystemUserID == securityGroupID)
                select sg;
            return queryResult;
        }

        protected override SystemUserEntities CreateDataSource()
        {
            try
            {
                XERP.Server.DAL.SystemUserDAL.DALUtility dalUtility = new DALUtility();
                var context = new SystemUserEntities(dalUtility.EntityConectionString);

                //test it...
                //IQueryable<SystemUserSecurity> query = (from q in context.SystemUserSecurities where q.SystemUserID == "Base"
                //                                   select q);
                //IQueryable<SecurityGroup> query2 = from sg in context.SecurityGroups
                //                                  where !context.SystemUserSecurities.Any(sus => query.SecurityGroupID == sg.SecurityGroupCodeID)
                //                                  select sg;
                //IQueryable<SecurityGroup> query = from sg in context.SecurityGroups
                //                                  select sg;
                //GetMetaData("SystemUsers");
                //IQueryable<SystemUser> SystemUserQuery = (from c in context.SystemUsers
                //                                    select c);

                //foreach (SystemUser cc in SystemUserQuery)
                //{
                //    string s = cc.Name.ToString();
                //}
                //SystemUser SystemUser = new SystemUser();
                //IQueryable<SystemUser> SystemUserQuery = Refresh("5,6");
                //var query =
                //    from sg in context.SecurityGroups
                //    where !context.SystemUserSecurities.Any(sus => sus.SecurityGroupID == sg.SecurityGroupID &&
                //        sus.SystemUserID == "Base")
                //    select sg;
                RefreshSystemUser("1,6");
                return context;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
