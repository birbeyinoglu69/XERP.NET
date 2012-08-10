using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using XERP.Server.DAL.LogInDAL;
using System.Data.EntityClient;
using XERP.Server;
using System.Configuration;
using ExtensionMethods;

namespace XERP.Server.Service.LogInService
{
    public class LogInDataService : DataService< LogInEntities >
    {
        //private LogInEntities _context = null;
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            //I noticed when setting page size it would always limit my expand payload to 1
            //I have elected to disable paging...
            //config.SetEntitySetPageSize("*", 50);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        }

        [WebGet]
        public IQueryable<Temp> GetMetaData(string tableName)
        {
            switch (tableName)
            {
                case "SystemUsers":
                    SystemUser systemUser = new SystemUser();
                    return systemUser.GetMetaData().AsQueryable();
                case "SystemUsersTypes":
                    SystemUserType systemUserTypes = new SystemUserType();
                    return systemUserTypes.GetMetaData().AsQueryable();
                case "SystemUserCodes":
                    SystemUserCode systemUserCode = new SystemUserCode();
                    return systemUserCode.GetMetaData().AsQueryable();
                case "SystemUserSecurities":
                    SystemUserSecurity systemUserSecurity = new SystemUserSecurity();
                    return systemUserSecurity.GetMetaData().AsQueryable();
                case "SecurityGroups":
                    SecurityGroup securityGroup = new SecurityGroup();
                    return securityGroup.GetMetaData().AsQueryable();
                default: //no table exists for the given tablename given...
                    List<Temp> tempList = new List<Temp>();
                    Temp temp = new Temp();
                    temp.ID = 0;
                    temp.Int_1 = 0;
                    temp.Bool_1 = true; //bool_1 will flag it as an error...
                    temp.Name = "Error";
                    temp.ShortChar_1 = "Table " + tableName + " Is Not A Valid Table Within The Given Entity Collection, Or Meta Data Is Not Publc For The Given Table Name";
                    tempList.Add(temp);
                    return tempList.AsQueryable();
            }
        }

        [WebGet]
        public IQueryable<ExecutableProgram> GetExecutableProgramsAllowedByUser(string systemUserID)
        {//complex query required compound search criteria so this had to be done server side...  
            XERP.Server.DAL.LogInDAL.DALUtility dalUtility = new DALUtility();
            var context = new LogInEntities(dalUtility.EntityConectionString);
            
            var query = (from sus in context.SystemUserSecurities
                          from ms in context.MenuSecurities
                          from mi in context.MenuItems
                          from ep in context.ExecutablePrograms
                          where sus.SystemUserID == systemUserID &&
                          sus.SecurityGroupID == ms.SecurityGroupID &&
                          ms.MenuItemID == mi.MenuItemID &&
                          mi.Executable == true && mi.AllowAll == false &&
                          string.IsNullOrEmpty(ep.ExecutableProgramID) == false &&
                          mi.ExecutableProgramID == ep.ExecutableProgramID
                          select ep);

            var query2 = (from mi in context.MenuItems
                          from ep in context.ExecutablePrograms
                          where mi.ExecutableProgramID == ep.ExecutableProgramID &&
                          mi.AllowAll == true &&
                          string.IsNullOrEmpty(ep.ExecutableProgramID) == false &&
                          string.IsNullOrEmpty(mi.ExecutableProgramID) == false
                          select ep);
            var mergedList = query.Union(query2);
            return mergedList;
        }

        protected override LogInEntities CreateDataSource()
        {
            XERP.Server.DAL.LogInDAL.DALUtility dalUtility = new DALUtility();
            var context = new LogInEntities(dalUtility.EntityConectionString);

            //test it...
            //GetExecutableProgramsAllowedByUser("Base");

            //ToDo: ADD DAL Securities Logic... Not sure we need and or want this...
            //DAL Security Should require USERID and DALName
            //From those two bits of information we can see if the User has rights to the DAL...
            //By default the DAL will be wide open...  The DalItems and DalSecurities will have
            //to be appended if security at the DAL level is required...

            return context;
        }
    }
}
