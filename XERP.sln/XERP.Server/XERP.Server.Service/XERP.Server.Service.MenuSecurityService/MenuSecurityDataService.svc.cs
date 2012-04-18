using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using XERP.Server.DAL.MenuSecurityDAL;
using System.Data.EntityClient;
using System.Configuration;

namespace XERP.Server.Service.MenuSecurityService
{
    public class MenuSecurityDataService : DataService<MenuSecurityEntities>
    {
        private MenuSecurityEntities _context;

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
        public IQueryable<MenuItem> GetMenuItemsAllowedByUser(string systemUserID)
        {                           
            var query = (from sus in _context.SystemUserSecurities
                          from ms in _context.MenuSecurities
                          from mi in _context.MenuItems
                          where sus.SystemUserID == systemUserID &&
                          sus.SecurityGroupID == ms.SecurityGroupID &&
                          ms.MenuItemID == mi.MenuItemID &&
                          mi.AllowAll == false
                          select mi);


            var query2 = (from mi in _context.MenuItems
                          where mi.AllowAll == true
                          select mi);
            var mergedList = query.Union(query2);
            return mergedList;
        }

        protected override MenuSecurityEntities CreateDataSource()
        {
            EntityConnectionStringBuilder entityConectionString = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["MenuSecurityEntities"].ToString());

            XERPServerConfig config = new XERPServerConfig();
            entityConectionString.ProviderConnectionString = config.BaseSQLConnectionString;
            _context = new MenuSecurityEntities(entityConectionString.ConnectionString);

            //test it
            //GetMenuItemsAllowedByUser("Base");
            //test it...
            //IQueryable<MenuSecurity> query = (from q in _context.MenuSecurities
            //                                  select q);
            //var query = (from mi in _context.MenuItems
            //             from ms in _context.MenuSecurities
            //             where mi.MenuItemID == ms.MenuItemID &&
            //                   mi.AllowAll == true
            //             select mi).ToList();
            //foreach (MenuItem mi in query)
            //{
            //    string s = mi.MenuItemID.ToString();
            //}

            //ToDo: ADD DAL Securities Logic...
            //DAL Security Should require USERID and DALName
            //From those two bits of information we can see if the User has rights to the DAL...
            //By default the DAL will be wide open...  The DalItems and DalSecurities will have
            //to be appended if security at the DAL level is required...
            return _context;
        }

    }
}
