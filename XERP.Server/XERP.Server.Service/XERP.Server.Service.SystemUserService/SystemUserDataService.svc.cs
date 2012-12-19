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
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class SystemUserDataService : DataService<SystemUserEntities>
    {
        //private SystemUserEntities _context;
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
                case "SystemUsers":
                    SystemUser item = new SystemUser();
                    return item.GetMetaData().AsQueryable();
                case "SystemUserTypes":
                    SystemUserType itemType = new SystemUserType();
                    return itemType.GetMetaData().AsQueryable();
                case "SystemUserCodes":
                    SystemUserCode itemCode = new SystemUserCode();
                    return itemCode.GetMetaData().AsQueryable();
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
        public IQueryable<SystemUserCode> RefreshSystemUserCode(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.SystemUserDAL.DALUtility dalUtility = new DALUtility();
            var context = new SystemUserEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.SystemUserCodes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [ChangeInterceptor("SystemUserTypes")]
        public void OnChangeSystemUserTypes(SystemUserType itemType, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Type was used by its parent record...
                XERP.Server.DAL.SystemUserDAL.DALUtility dalUtility = new DALUtility();
                var context = new SystemUserEntities(dalUtility.EntityConectionString);
                context.SystemUsers.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string typeID = itemType.SystemUserTypeID;
                string sqlstring = "UPDATE SystemUsers SET SystemUserTypeID = null WHERE SystemUserTypeID = '" + typeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("SystemUserCodes")]
        public void OnChangeSystemUserCodes(SystemUserCode itemCode, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Code was used by its parent record...
                XERP.Server.DAL.SystemUserDAL.DALUtility dalUtility = new DALUtility();
                var context = new SystemUserEntities(dalUtility.EntityConectionString);
                context.SystemUsers.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string codeID = itemCode.SystemUserCodeID;
                string sqlstring = "UPDATE SystemUsers SET SystemUserCodeID = null Where SystemUserCodeID = '" + codeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        protected override SystemUserEntities CreateDataSource()
        {
            try
            {
                XERP.Server.DAL.SystemUserDAL.DALUtility dalUtility = new DALUtility();
                var context = new SystemUserEntities(dalUtility.EntityConectionString);

                //test it...
                //GetMetaData("SystemUsers");
                //IQueryable<SystemUser> itemQuerry = (from c in context.SystemUsers
                //                                    select c);

                //foreach (SystemUser cc in SystemUserQuery)
                //{
                //    string s = cc.Name.ToString();
                //}
                //SystemUser item = new SystemUser();
                //IQueryable<SystemUser> itemQuery = Refresh("5,6");
                return context;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
