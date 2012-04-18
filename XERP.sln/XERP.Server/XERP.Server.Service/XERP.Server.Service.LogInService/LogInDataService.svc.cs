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

namespace XERP.Server.Service.LogInService
{
    public class LogInDataService : DataService< LogInEntities >
    {
        private LogInEntities _context;
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
        public IQueryable<ExecutableProgram> GetExecutableProgramsAllowedByUser(string systemUserID)
        {//complex query required compound search criteria so this had to be done server side...  
            var query = (from sus in _context.SystemUserSecurities
                          from ms in _context.MenuSecurities
                          from mi in _context.MenuItems
                          from ep in _context.ExecutablePrograms
                          where sus.SystemUserID == systemUserID &&
                          sus.SecurityGroupID == ms.SecurityGroupID &&
                          ms.MenuItemID == mi.MenuItemID &&
                          mi.Executable == true && mi.AllowAll == false &&
                          string.IsNullOrEmpty(ep.ExecutableProgramID) == false &&
                          mi.ExecutableProgramID == ep.ExecutableProgramID
                          select ep);

            var query2 = (from mi in _context.MenuItems
                          from ep in _context.ExecutablePrograms
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
            EntityConnectionStringBuilder entityConectionString = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["LogInEntities"].ToString());

            XERPServerConfig config = new XERPServerConfig();
            entityConectionString.ProviderConnectionString = config.BaseSQLConnectionString;
            _context = new LogInEntities(entityConectionString.ConnectionString);
            //test it...
            //GetExecutableProgramsAllowedByUser("Base");

            //ToDo: ADD DAL Securities Logic... Not sure we need and or want this...
            //DAL Security Should require USERID and DALName
            //From those two bits of information we can see if the User has rights to the DAL...
            //By default the DAL will be wide open...  The DalItems and DalSecurities will have
            //to be appended if security at the DAL level is required...

            return _context;
        }
    }
}
